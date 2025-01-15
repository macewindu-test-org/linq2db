// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB;
using LinqToDB.Mapping;
using LinqToDB.Tools.Comparers;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.All.Oracle
{
	[Table("Doctor")]
	public class Doctor : IEquatable<Doctor>
	{
		[Column("PersonID", DataType  = DataType.Decimal, DbType   = "NUMBER"         , Length = 22             , IsPrimaryKey = true)] public OracleDecimal PersonId { get; set; } // NUMBER
		[Column("Taxonomy", CanBeNull = false           , DataType = DataType.NVarChar, DbType = "NVARCHAR2(50)", Length       = 50  )] public string        Taxonomy { get; set; } = null!; // NVARCHAR2(50)

		#region IEquatable<T> support
		private static readonly IEqualityComparer<Doctor> _equalityComparer = ComparerBuilder.GetEqualityComparer<Doctor>(c => c.PersonId);

		public bool Equals(Doctor? other)
		{
			return _equalityComparer.Equals(this, other!);
		}

		public override int GetHashCode()
		{
			return _equalityComparer.GetHashCode(this);
		}

		public override bool Equals(object? obj)
		{
			return Equals(obj as Doctor);
		}
		#endregion

		#region Associations
		/// <summary>
		/// FK_Doctor_Person
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(PersonId), OtherKey = nameof(Oracle.Person.PersonId))]
		public Person Person { get; set; } = null!;
		#endregion
	}
}
