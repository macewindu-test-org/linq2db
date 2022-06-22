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

namespace Cli.Default.MySql
{
	[Table("linqdatatypes")]
	public class Linqdatatype
	{
		[Column("ID"            )] public int?      Id             { get; set; } // int
		[Column("MoneyValue"    )] public decimal?  MoneyValue     { get; set; } // decimal(10,4)
		[Column("DateTimeValue" )] public DateTime? DateTimeValue  { get; set; } // datetime(3)
		[Column("DateTimeValue2")] public DateTime? DateTimeValue2 { get; set; } // datetime
		[Column("BoolValue"     )] public bool?     BoolValue      { get; set; } // tinyint(1)
		[Column("GuidValue"     )] public string?   GuidValue      { get; set; } // char(36)
		[Column("BinaryValue"   )] public byte[]?   BinaryValue    { get; set; } // varbinary(5000)
		[Column("SmallIntValue" )] public short?    SmallIntValue  { get; set; } // smallint
		[Column("IntValue"      )] public int?      IntValue       { get; set; } // int
		[Column("BigIntValue"   )] public long?     BigIntValue    { get; set; } // bigint
		[Column("StringValue"   )] public string?   StringValue    { get; set; } // varchar(50)
	}
}
