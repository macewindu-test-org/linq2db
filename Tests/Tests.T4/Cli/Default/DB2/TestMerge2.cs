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

namespace Cli.Default.DB2
{
	[Table("TestMerge2")]
	public class TestMerge2
	{
		[Column("Id"             , IsPrimaryKey = true)] public int       Id              { get; set; } // INTEGER
		[Column("Field1"                              )] public int?      Field1          { get; set; } // INTEGER
		[Column("Field2"                              )] public int?      Field2          { get; set; } // INTEGER
		[Column("Field3"                              )] public int?      Field3          { get; set; } // INTEGER
		[Column("Field4"                              )] public int?      Field4          { get; set; } // INTEGER
		[Column("Field5"                              )] public int?      Field5          { get; set; } // INTEGER
		[Column("FieldInt64"                          )] public long?     FieldInt64      { get; set; } // BIGINT
		[Column("FieldBoolean"                        )] public short?    FieldBoolean    { get; set; } // SMALLINT
		[Column("FieldString"                         )] public string?   FieldString     { get; set; } // VARCHAR(20)
		[Column("FieldNString"                        )] public string?   FieldNString    { get; set; } // VARCHAR(80)
		[Column("FieldChar"                           )] public char?     FieldChar       { get; set; } // CHARACTER(1)
		[Column("FieldNChar"                          )] public string?   FieldNChar      { get; set; } // CHARACTER(4)
		[Column("FieldFloat"                          )] public float?    FieldFloat      { get; set; } // REAL
		[Column("FieldDouble"                         )] public double?   FieldDouble     { get; set; } // DOUBLE
		[Column("FieldDateTime"                       )] public DateTime? FieldDateTime   { get; set; } // TIMESTAMP
		[Column("FieldBinary"                         )] public byte[]?   FieldBinary     { get; set; } // VARCHAR (20) FOR BIT DATA
		[Column("FieldGuid"                           )] public byte[]?   FieldGuid       { get; set; } // CHAR (16) FOR BIT DATA
		[Column("FieldDecimal"                        )] public decimal?  FieldDecimal    { get; set; } // DECIMAL(24,10)
		[Column("FieldDate"                           )] public DateTime? FieldDate       { get; set; } // DATE
		[Column("FieldTime"                           )] public TimeSpan? FieldTime       { get; set; } // TIME
		[Column("FieldEnumString"                     )] public string?   FieldEnumString { get; set; } // VARCHAR(20)
		[Column("FieldEnumNumber"                     )] public int?      FieldEnumNumber { get; set; } // INTEGER
	}
}
