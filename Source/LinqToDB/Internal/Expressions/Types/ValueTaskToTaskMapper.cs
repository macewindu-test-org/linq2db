﻿using System.Linq.Expressions;

namespace LinqToDB.Internal.Expressions.Types
{
	public class ValueTaskToTaskMapper : ICustomMapper
	{
		bool ICustomMapper.CanMap(Expression expression)
		{
			return (!expression.Type.IsGenericType && expression.Type.FullName == "System.Threading.Tasks.ValueTask")
				|| (expression.Type.IsGenericType && expression.Type.GetGenericTypeDefinition().FullName == "System.Threading.Tasks.ValueTask`1");
		}

		Expression ICustomMapper.Map(Expression expression)
		{
			return Expression.Call(expression, "AsTask", []);
		}
	}
}
