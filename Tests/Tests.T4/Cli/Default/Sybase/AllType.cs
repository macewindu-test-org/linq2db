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

namespace Cli.Default.Sybase
{
	[Table("AllTypes")]
	public class AllType
	{
		[Column("ID"                   , IsIdentity   = true, SkipOnInsert = true, SkipOnUpdate = true)] public int       Id                    { get; set; } // int
		[Column("bigintDataType"                                                                      )] public long?     BigintDataType        { get; set; } // bigint
		[Column("uBigintDataType"                                                                     )] public ulong?    UBigintDataType       { get; set; } // ubigint
		[Column("numericDataType"                                                                     )] public decimal?  NumericDataType       { get; set; } // numeric(18, 1)
		[Column("bitDataType"                                                                         )] public bool      BitDataType           { get; set; } // bit
		[Column("smallintDataType"                                                                    )] public short?    SmallintDataType      { get; set; } // smallint
		[Column("uSmallintDataType"                                                                   )] public ushort?   USmallintDataType     { get; set; } // usmallint
		[Column("decimalDataType"                                                                     )] public decimal?  DecimalDataType       { get; set; } // decimal(18, 1)
		[Column("smallmoneyDataType"                                                                  )] public decimal?  SmallmoneyDataType    { get; set; } // smallmoney
		[Column("intDataType"                                                                         )] public int?      IntDataType           { get; set; } // int
		[Column("uIntDataType"                                                                        )] public uint?     UIntDataType          { get; set; } // uint
		[Column("tinyintDataType"                                                                     )] public sbyte?    TinyintDataType       { get; set; } // tinyint
		[Column("moneyDataType"                                                                       )] public decimal?  MoneyDataType         { get; set; } // money
		[Column("floatDataType"                                                                       )] public double?   FloatDataType         { get; set; } // float
		[Column("realDataType"                                                                        )] public float?    RealDataType          { get; set; } // real
		[Column("datetimeDataType"                                                                    )] public DateTime? DatetimeDataType      { get; set; } // datetime
		[Column("smalldatetimeDataType"                                                               )] public DateTime? SmalldatetimeDataType { get; set; } // smalldatetime
		[Column("dateDataType"                                                                        )] public DateTime? DateDataType          { get; set; } // date
		[Column("timeDataType"                                                                        )] public TimeSpan? TimeDataType          { get; set; } // time
		[Column("charDataType"                                                                        )] public char?     CharDataType          { get; set; } // char(1)
		[Column("char20DataType"                                                                      )] public string?   Char20DataType        { get; set; } // char(20)
		[Column("varcharDataType"                                                                     )] public string?   VarcharDataType       { get; set; } // varchar(20)
		[Column("textDataType"                                                                        )] public string?   TextDataType          { get; set; } // text
		[Column("ncharDataType"                                                                       )] public string?   NcharDataType         { get; set; } // nchar(20)
		[Column("nvarcharDataType"                                                                    )] public string?   NvarcharDataType      { get; set; } // nvarchar(20)
		[Column("ntextDataType"                                                                       )] public string?   NtextDataType         { get; set; } // unitext
		[Column("binaryDataType"                                                                      )] public byte[]?   BinaryDataType        { get; set; } // binary(1)
		[Column("varbinaryDataType"                                                                   )] public byte[]?   VarbinaryDataType     { get; set; } // varbinary(1)
		[Column("imageDataType"                                                                       )] public byte[]?   ImageDataType         { get; set; } // image
		[Column("timestampDataType"    , SkipOnInsert = true, SkipOnUpdate = true                     )] public byte[]?   TimestampDataType     { get; set; } // timestamp
	}
}
