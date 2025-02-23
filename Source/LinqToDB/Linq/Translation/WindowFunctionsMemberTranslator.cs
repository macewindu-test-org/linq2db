﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

using LinqToDB.Common;
using LinqToDB.Expressions;
using LinqToDB.Expressions.Internal;
using LinqToDB.SqlQuery;

namespace LinqToDB.Linq.Translation
{
	public class WindowFunctionsMemberTranslator : MemberTranslatorBase
	{
		public WindowFunctionsMemberTranslator()
		{
			Registration.RegisterMethod(() => Sql.Window.RowNumber(f => f.OrderBy(1)), TranslateRowNumber);
			Registration.RegisterMethod((IEnumerable<int> g) => g.PercentileCont(0.5, (e, f) => f.OrderBy(e)), TranslatePercentileCont);
			Registration.RegisterMethod((IQueryable<int>  g) => g.PercentileCont(0.5, (e, f) => f.OrderBy(e)), TransformPercentileCont);

			RegisterSum();
		}

		void RegisterSum()
		{
			Registration.RegisterMethod(() => Sql.Window.Sum(1,            f => f.OrderBy(1)), TranslateSum);
			Registration.RegisterMethod(() => Sql.Window.Sum(1L,           f => f.OrderBy(1)), TranslateSum);
			Registration.RegisterMethod(() => Sql.Window.Sum(1.0,          f => f.OrderBy(1)), TranslateSum);
			Registration.RegisterMethod(() => Sql.Window.Sum(1f,           f => f.OrderBy(1)), TranslateSum);
			Registration.RegisterMethod(() => Sql.Window.Sum(1M,           f => f.OrderBy(1)), TranslateSum);

			Registration.RegisterMethod(() => Sql.Window.Sum((int?)1,      f => f.OrderBy(1)), TranslateSum);
			Registration.RegisterMethod(() => Sql.Window.Sum((long?)1L,    f => f.OrderBy(1)), TranslateSum);
			Registration.RegisterMethod(() => Sql.Window.Sum((double?)1.0, f => f.OrderBy(1)), TranslateSum);
			Registration.RegisterMethod(() => Sql.Window.Sum((float?)1f,   f => f.OrderBy(1)), TranslateSum);
			Registration.RegisterMethod(() => Sql.Window.Sum((decimal?)1M, f => f.OrderBy(1)), TranslateSum);
		}

		public record ArgumentInformation(Expression expr, Sql.AggregateModifier modifier);
		public record OrderByInformation(Expression expr, bool isDescending, Sql.NullsPosition nulls);

		public class WindowFunctionInformation
		{
			public required ArgumentInformation[]?  Arguments   { get; set; }
			public required Expression[]?         PartitionBy { get; set; }
			public required OrderByInformation[]? OrderBy     { get; set; }
			public required Expression?           Filter      { get; set; }
		}

		protected static bool CollectWindowFunctionInformation(
			ITranslationContext                                 translationContext,
			Type                                                expressionType,
			Expression[]?                                       functionArguments,
			Expression                                          buildBody,
			[NotNullWhen(true)] out  WindowFunctionInformation? functionInfo,
			[NotNullWhen(false)] out SqlErrorExpression?        error)
		{
			functionInfo = null;
			error        = null;

			List<ArgumentInformation>? argumentsList   = null;
			List<Expression>?          partitionByList = null;
			List<OrderByInformation>?  orderByList     = null;
			Expression?                filter          = null;

			if (functionArguments != null)
			{
				argumentsList ??= new();
				foreach (var argument in functionArguments)
				{
					argumentsList.Add(new(argument, Sql.AggregateModifier.None));
				}
			}

			while (buildBody is MethodCallExpression mc)
			{
				switch (mc.Method.Name)
				{
					case nameof(WindowFunctionBuilder.IOrderByPart<object>.OrderBy):
					case nameof(WindowFunctionBuilder.IOrderByPart<object>.OrderByDesc):
					case nameof(WindowFunctionBuilder.IThenOrderPart<object>.ThenBy):
					case nameof(WindowFunctionBuilder.IThenOrderPart<object>.ThenByDesc):
					{
						var isDesc = mc.Method.Name == nameof(WindowFunctionBuilder.IOrderByPart<object>.OrderByDesc) ||
						             mc.Method.Name == nameof(WindowFunctionBuilder.IThenOrderPart<object>.ThenByDesc);

						orderByList ??= new();

						var        nulls = Sql.NullsPosition.None;
						Expression argument;
						if (mc.Arguments.Count == 2)
						{
							argument = mc.Arguments[0];
							nulls = (Sql.NullsPosition)mc.Arguments[1].EvaluateExpression()!;
						}
						else
						{
							argument = mc.Arguments[0];
						}

						orderByList.Insert(0, new (argument, isDesc, nulls));

						buildBody = mc.Object!;
						break;
					}

					case nameof(WindowFunctionBuilder.IPartitionPart<object>.PartitionBy):
					{
						partitionByList ??= new();

						if (mc.Arguments[0].NodeType == ExpressionType.NewArrayInit)
						{
							foreach (var argument in ((NewArrayExpression)mc.Arguments[0]).Expressions)
							{
								partitionByList.Add(argument);
							}
						}
						else
						{
							partitionByList.Add(mc.Arguments[0]);
						}

						buildBody = mc.Object!;
						break;
					}

					case nameof(WindowFunctionBuilder.IArgumentPart<object>.Argument):
					{
						argumentsList ??= new();
						var        modifier = Sql.AggregateModifier.None;
						Expression argument;
						if (mc.Arguments.Count == 2)
						{
							modifier = (Sql.AggregateModifier)mc.Arguments[0].EvaluateExpression()!;
							argument = mc.Arguments[1];
						}
						else
						{
							argument = mc.Arguments[0];
						}

						argumentsList.Add(new (argument, modifier));

						buildBody = mc.Object!;
						break;
					}

					case nameof(WindowFunctionBuilder.IFilterPart<object>.Filter):
					{
						filter = mc.Arguments[0];

						buildBody = mc.Object!;
						break;
					}

					case nameof(WindowFunctionBuilder.IUseWindow<object>.UseWindow):
					{
						buildBody = mc.Arguments[0];
						var expanded = translationContext.Translate(buildBody, TranslationFlags.Expand);
						if (expanded is MethodCallExpression { Method.Name: nameof(WindowFunctionBuilder.DefineWindow) } mce)
						{
							buildBody = mce.Arguments[1].UnwrapLambda().Body;
						}
						else
						{
							error = translationContext.CreateErrorExpression(buildBody, "Expected window definition", expressionType);
							return false;
						}

						break;
					}

					case nameof(WindowFunctionBuilder.DefineWindow):
					{
						buildBody = mc.Arguments[1];
						break;
					}

				}

				if (buildBody == mc)
					break;
			}

			functionInfo = new WindowFunctionInformation
			{
				Arguments = argumentsList?.ToArray(),
				PartitionBy = partitionByList?.ToArray(),
				OrderBy = orderByList?.ToArray(),
				Filter = filter
			};

			return true;
		}

		protected bool TranslateOrderItems(ITranslationContext translationContext, Type errorType, IEnumerable<OrderByInformation> orderBy, List<SqlWindowOrderItem> orderItems, [NotNullWhen(false)] out SqlErrorExpression? error)
		{
			error = null;
			foreach (var orderItem in orderBy)
			{
				var translated = translationContext.Translate(orderItem.expr);
				if (translated is not SqlPlaceholderExpression placeholder)
				{
					error = SqlErrorExpression.EnsureError(translated, errorType);
					return false;
				}

				orderItems.Add(new SqlWindowOrderItem(placeholder.Sql, orderItem.isDescending, orderItem.nulls));
			}
			return true;
		}

		protected bool TranslatePartitionBy(ITranslationContext translationContext, Type errorType, IEnumerable<Expression> partitionBy, List<ISqlExpression> partitionByItems, [NotNullWhen(false)] out SqlErrorExpression? error)
		{
			error = null;
			foreach (var partition in partitionBy)
			{
				var translated = translationContext.Translate(partition);
				if (translated is not SqlPlaceholderExpression placeholder)
				{
					error = SqlErrorExpression.EnsureError(translated, errorType);
					return false;
				}
				partitionByItems.Add(placeholder.Sql);
			}
			return true;
		}

		protected Expression TranslateWindowFunction(
			ITranslationContext  translationContext,
			MethodCallExpression methodCall,
			int?                 argumentIndex,
			int                  windowArgument,
			DbDataType           dbDataType,
			string               functionName)
		{
			if (!CollectWindowFunctionInformation(
				    translationContext, 
				    methodCall.Type, 
				    argumentIndex == null ? null : [methodCall.Arguments[argumentIndex.Value]],
				    methodCall.Arguments[windowArgument].UnwrapLambda().Body, 
				    out var information, 
				    out var error))
				return error;

			var arguments   = new List<SqlFunctionArgument>();
			List<ISqlExpression>?     partitionBy = null;
			List<SqlWindowOrderItem>? orderItems  = null;
			SqlSearchCondition?       filter      = null;

			if (information.Arguments != null)
			{
				foreach (var argument in information.Arguments)
				{
					var translated = translationContext.Translate(argument.expr);
					if (translated is not SqlPlaceholderExpression placeholder)
						return SqlErrorExpression.EnsureError(translated, methodCall.Type);
					arguments.Add(new SqlFunctionArgument(placeholder.Sql, argument.modifier));
				}
			}

			if (information.PartitionBy != null)
			{
				partitionBy ??= new();
				if (!TranslatePartitionBy(translationContext, methodCall.Type, information.PartitionBy, partitionBy, out var partitionError))
					return partitionError;
			}

			if (information.OrderBy != null)
			{
				orderItems ??= new();
				if (!TranslateOrderItems(translationContext, methodCall.Type, information.OrderBy, orderItems, out var orderError))
					return orderError;
			}

			if (information.Filter != null)
			{
				var translated = translationContext.Translate(information.Filter);
				if (translated is not SqlPlaceholderExpression placeholder || placeholder.Sql is not SqlSearchCondition sc)
					return SqlErrorExpression.EnsureError(translated, methodCall.Type);
				filter = sc;
			}

			var function = translationContext.ExpressionFactory.WindowFunction(dbDataType, functionName, 
				arguments.ToArray(), 
				arguments.Select(a => true).ToArray(), 
				partitionBy: partitionBy, 
				orderBy: orderItems,
				filter: filter
				);

			return translationContext.CreatePlaceholder(translationContext.CurrentSelectQuery, function, methodCall);
		}

		static LambdaExpression SimplifyEntityLambda(LambdaExpression lambda, int parameterIndex, Expression contextExpression)
		{
			var paramToReplace = lambda.Parameters[parameterIndex];
			var newBody = lambda.Body.Transform(e =>
			{
				if (e == paramToReplace)
				{
					if (contextExpression is ContextRefExpression contextRefExpression)
					{
						var contextTyped = contextRefExpression.WithType(e.Type);
						return contextTyped;
					}
				}
				return e;
			});

			var newParameters = lambda.Parameters.ToList();
			newParameters.RemoveAt(parameterIndex);

			return Expression.Lambda(newBody, newParameters);
		}

		public virtual Expression? TranslateRowNumber(ITranslationContext translationContext, MethodCallExpression methodCall, TranslationFlags translationFlags)
		{
			var factory = translationContext.ExpressionFactory;
			var dbDataType = factory.GetDbDataType(methodCall.Type);

			return TranslateWindowFunction(translationContext, methodCall, null, 1, dbDataType, "ROW_NUMBER");
		}

		public virtual Expression? TranslatePercentileCont(ITranslationContext translationContext, MethodCallExpression methodCall, TranslationFlags translationFlags)
		{
			var enumerableContext = translationContext.GetAggregationContext(methodCall.Arguments[0]);
			if (enumerableContext == null)
				return translationContext.CreateErrorExpression(methodCall.Arguments[0], "Enumerable context is not discoverable.", methodCall.Type);

			var argumentExpr = methodCall.Arguments[1];
			if (!translationContext.TranslateToSqlExpression(argumentExpr, out var argumentSql))
				return translationContext.CreateErrorExpression(argumentExpr, type : methodCall.Type);

			var builderLambda = methodCall.Arguments[2].UnwrapLambda();

			builderLambda = SimplifyEntityLambda(builderLambda, 0, enumerableContext);

			if (!CollectWindowFunctionInformation(
				    translationContext,
				    methodCall.Type,
				    null,
				    builderLambda.Body,
				    out var information,
				    out var error))
				return error;

			if (information.OrderBy!.Length != 1)
				return translationContext.CreateErrorExpression(methodCall.Arguments[2], "Expected single order by expression", methodCall.Type);

			List<SqlWindowOrderItem> withinGroupOrder = new();
			if (!TranslateOrderItems(translationContext, methodCall.Type, information.OrderBy, withinGroupOrder, out var orderError))
				return orderError;

			List<ISqlExpression>? partitionBy = null;
			if (information.PartitionBy != null)
			{
				partitionBy ??= new();
				if (!TranslatePartitionBy(translationContext, methodCall.Type, information.PartitionBy, partitionBy, out var partitionError))
					return partitionError;
			}

			var functionType = translationContext.GetDbDataType(withinGroupOrder[0].Expression);

			var windowFunction = translationContext.ExpressionFactory.WindowFunction(
				functionType,
				"PERCENTILE_CONT",
				[new SqlFunctionArgument(argumentSql, Sql.AggregateModifier.None)],
				[true],
				withinGroup : withinGroupOrder,
				partitionBy : partitionBy,
				isAggregate: true
			);

			return translationContext.CreatePlaceholder(translationContext.GetAggregationSelectQuery(enumerableContext), windowFunction, methodCall);
		}

		public virtual Expression? TransformPercentileCont(ITranslationContext translationContext, MethodCallExpression methodCall, TranslationFlags translationFlags)
		{
			return WindowFunctionHelpers.BuildAggregateExecuteExpression(methodCall); 
		}

		public virtual Expression? TranslateSum(ITranslationContext translationContext, MethodCallExpression methodCall, TranslationFlags translationFlags)
		{
			var dbDataType = translationContext.ExpressionFactory.GetDbDataType(methodCall.Type);

			return TranslateWindowFunction(translationContext, methodCall, 1, 2, dbDataType, "SUM");
		}
	}
}
