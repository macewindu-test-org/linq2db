﻿using System.Linq;
using System.Linq.Expressions;

using JetBrains.Annotations;

using LinqToDB.Internal.DataProvider;
using LinqToDB.Internal.Linq;
using LinqToDB.Internal.SqlProvider;
using LinqToDB.Mapping;

namespace LinqToDB.DataProvider.Oracle
{
	public interface IOracleSpecificTable<out TSource> : ITable<TSource>
		where TSource : notnull
	{
	}

	sealed class OracleSpecificTable<TSource> : DatabaseSpecificTable<TSource>, IOracleSpecificTable<TSource>
		where TSource : notnull
	{
		public OracleSpecificTable(ITable<TSource> table) : base(table)
		{
		}
	}

	public interface IOracleSpecificQueryable<out TSource> : IQueryable<TSource>
	{
	}

	sealed class OracleSpecificQueryable<TSource> : DatabaseSpecificQueryable<TSource>, IOracleSpecificQueryable<TSource>
	{
		public OracleSpecificQueryable(IQueryable<TSource> queryable) : base(queryable)
		{
		}
	}

	public static class OracleSpecificExtensions
	{
		[LinqTunnel, Pure, IsQueryable]
		[Sql.QueryExtension(null, Sql.QueryExtensionScope.None, typeof(NoneExtensionBuilder))]
		public static IOracleSpecificTable<TSource> AsOracle<TSource>(this ITable<TSource> table)
			where TSource : notnull
		{
			var newTable = new Table<TSource>(table.DataContext,
				Expression.Call(
					null,
					MethodHelper.GetMethodInfo(AsOracle, table),
					table.Expression)
			);

			return new OracleSpecificTable<TSource>(newTable);
		}

		[LinqTunnel, Pure, IsQueryable]
		[Sql.QueryExtension(null, Sql.QueryExtensionScope.None, typeof(NoneExtensionBuilder))]
		public static IOracleSpecificQueryable<TSource> AsOracle<TSource>(this IQueryable<TSource> source)
			where TSource : notnull
		{
			var currentSource = source.ProcessIQueryable();

			return new OracleSpecificQueryable<TSource>(currentSource.Provider.CreateQuery<TSource>(
				Expression.Call(
					null,
					MethodHelper.GetMethodInfo(AsOracle, source),
					currentSource.Expression)));
		}
	}
}
