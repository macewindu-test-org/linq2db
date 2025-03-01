﻿using System;
using System.Linq.Expressions;

using LinqToDB.Concurrency;
using LinqToDB.Mapping;
using LinqToDB.Model;

namespace LinqToDB.Mapping
{
	/// <summary>
	/// Defines optimistic lock column value generation strategy for update.
	/// Used with <see cref="ConcurrencyExtensions" /> extensions.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public abstract class OptimisticLockPropertyBaseAttribute : MappingAttribute
	{
		protected OptimisticLockPropertyBaseAttribute()
		{
		}

		/// <summary>
		/// Returns expression for new value for optimistic lock column on successful update.
		/// Should return <c>null</c> if value generated by database.
		/// </summary>
		/// <param name="column">Column descriptor.</param>
		/// <param name="record">Current record variable.</param>
		public abstract LambdaExpression? GetNextValue(ColumnDescriptor column, ParameterExpression record);

		public override string GetObjectID() => $".{Configuration}.";
	}
}
