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

namespace Cli.All.SqlServer.Azure
{
	[Table("GuidID")]
	public class GuidId : IEquatable<GuidId>
	{
		[Column("ID"    , DataType = LinqToDB.DataType.Guid , DbType = "uniqueidentifier", IsPrimaryKey = true)] public SqlGuid   Id     { get; set; } // uniqueidentifier
		[Column("Field1", DataType = LinqToDB.DataType.Int32, DbType = "int"                                  )] public SqlInt32? Field1 { get; set; } // int

		#region IEquatable<T> support
		private static readonly IEqualityComparer<GuidId> _equalityComparer = ComparerBuilder.GetEqualityComparer<GuidId>(c => c.Id);

		public bool Equals(GuidId? other)
		{
			return _equalityComparer.Equals(this, other!);
		}

		public override int GetHashCode()
		{
			return _equalityComparer.GetHashCode(this);
		}

		public override bool Equals(object? obj)
		{
			return Equals(obj as GuidId);
		}
		#endregion
	}
}
