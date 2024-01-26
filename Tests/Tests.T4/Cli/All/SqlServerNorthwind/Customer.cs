// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB;
using LinqToDB.Mapping;
using LinqToDB.Tools.Comparers;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.All.SqlServerNorthwind
{
	[Table("Customers")]
	public class Customer : IEquatable<Customer>
	{
		[Column("CustomerID"  , DataType = DataType.NChar   , DbType = "nchar(5)"    , Length = 5 , IsPrimaryKey = true)] public SqlString  CustomerId   { get; set; } // nchar(5)
		[Column("CompanyName" , DataType = DataType.NVarChar, DbType = "nvarchar(40)", Length = 40                     )] public SqlString  CompanyName  { get; set; } // nvarchar(40)
		[Column("ContactName" , DataType = DataType.NVarChar, DbType = "nvarchar(30)", Length = 30                     )] public SqlString? ContactName  { get; set; } // nvarchar(30)
		[Column("ContactTitle", DataType = DataType.NVarChar, DbType = "nvarchar(30)", Length = 30                     )] public SqlString? ContactTitle { get; set; } // nvarchar(30)
		[Column("Address"     , DataType = DataType.NVarChar, DbType = "nvarchar(60)", Length = 60                     )] public SqlString? Address      { get; set; } // nvarchar(60)
		[Column("City"        , DataType = DataType.NVarChar, DbType = "nvarchar(15)", Length = 15                     )] public SqlString? City         { get; set; } // nvarchar(15)
		[Column("Region"      , DataType = DataType.NVarChar, DbType = "nvarchar(15)", Length = 15                     )] public SqlString? Region       { get; set; } // nvarchar(15)
		[Column("PostalCode"  , DataType = DataType.NVarChar, DbType = "nvarchar(10)", Length = 10                     )] public SqlString? PostalCode   { get; set; } // nvarchar(10)
		[Column("Country"     , DataType = DataType.NVarChar, DbType = "nvarchar(15)", Length = 15                     )] public SqlString? Country      { get; set; } // nvarchar(15)
		[Column("Phone"       , DataType = DataType.NVarChar, DbType = "nvarchar(24)", Length = 24                     )] public SqlString? Phone        { get; set; } // nvarchar(24)
		[Column("Fax"         , DataType = DataType.NVarChar, DbType = "nvarchar(24)", Length = 24                     )] public SqlString? Fax          { get; set; } // nvarchar(24)

		#region IEquatable<T> support
		private static readonly IEqualityComparer<Customer> _equalityComparer = ComparerBuilder.GetEqualityComparer<Customer>(c => c.CustomerId);

		public bool Equals(Customer? other)
		{
			return _equalityComparer.Equals(this, other!);
		}

		public override int GetHashCode()
		{
			return _equalityComparer.GetHashCode(this);
		}

		public override bool Equals(object? obj)
		{
			return Equals(obj as Customer);
		}
		#endregion

		#region Associations
		/// <summary>
		/// FK_CustomerCustomerDemo_Customers backreference
		/// </summary>
		[Association(ThisKey = nameof(CustomerId), OtherKey = nameof(CustomerCustomerDemo.CustomerId))]
		public IEnumerable<CustomerCustomerDemo> CustomerCustomerDemos { get; set; } = null!;

		/// <summary>
		/// FK_Orders_Customers backreference
		/// </summary>
		[Association(ThisKey = nameof(CustomerId), OtherKey = nameof(Order.CustomerId))]
		public IEnumerable<Order> Orders { get; set; } = null!;
		#endregion
	}
}
