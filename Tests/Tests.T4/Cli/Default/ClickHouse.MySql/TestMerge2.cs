// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;
using System;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.Default.ClickHouse.MySql
{
	[Table("TestMerge2")]
	public class TestMerge2
	{
		[Column("Id"             , IsPrimaryKey = true, SkipOnUpdate = true)] public int             Id              { get; set; } // Int32
		[Column("Field1"                                                   )] public int?            Field1          { get; set; } // Int32
		[Column("Field2"                                                   )] public int?            Field2          { get; set; } // Int32
		[Column("Field3"                                                   )] public int?            Field3          { get; set; } // Int32
		[Column("Field4"                                                   )] public int?            Field4          { get; set; } // Int32
		[Column("Field5"                                                   )] public int?            Field5          { get; set; } // Int32
		[Column("FieldInt64"                                               )] public long?           FieldInt64      { get; set; } // Int64
		[Column("FieldBoolean"                                             )] public bool?           FieldBoolean    { get; set; } // Bool
		[Column("FieldString"                                              )] public string?         FieldString     { get; set; } // String
		[Column("FieldNString"                                             )] public string?         FieldNString    { get; set; } // String
		[Column("FieldChar"                                                )] public char?           FieldChar       { get; set; } // FixedString(1)
		[Column("FieldNChar"                                               )] public string?         FieldNChar      { get; set; } // FixedString(2)
		[Column("FieldFloat"                                               )] public float?          FieldFloat      { get; set; } // Float32
		[Column("FieldDouble"                                              )] public double?         FieldDouble     { get; set; } // Float64
		[Column("FieldDateTime"                                            )] public DateTimeOffset? FieldDateTime   { get; set; } // DateTime64(3)
		[Column("FieldDateTime2"                                           )] public DateTimeOffset? FieldDateTime2  { get; set; } // DateTime64(7)
		[Column("FieldBinary"                                              )] public string?         FieldBinary     { get; set; } // String
		[Column("FieldGuid"                                                )] public Guid?           FieldGuid       { get; set; } // UUID
		[Column("FieldDecimal"                                             )] public decimal?        FieldDecimal    { get; set; } // Decimal(38, 10)
		[Column("FieldDate"                                                )] public DateTime?       FieldDate       { get; set; } // Date
		[Column("FieldTime"                                                )] public long?           FieldTime       { get; set; } // Int64
		[Column("FieldEnumString"                                          )] public string?         FieldEnumString { get; set; } // String
		[Column("FieldEnumNumber"                                          )] public int?            FieldEnumNumber { get; set; } // Int32
	}
}
