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
	[Table("TestMerge2")]
	public class TestMerge2 : IEquatable<TestMerge2>
	{
		[Column("Id"             , DataType = LinqToDB.DataType.Int32         , DbType = "int"              , IsPrimaryKey = true            )] public SqlInt32        Id              { get; set; } // int
		[Column("Field1"         , DataType = LinqToDB.DataType.Int32         , DbType = "int"                                               )] public SqlInt32?       Field1          { get; set; } // int
		[Column("Field2"         , DataType = LinqToDB.DataType.Int32         , DbType = "int"                                               )] public SqlInt32?       Field2          { get; set; } // int
		[Column("Field3"         , DataType = LinqToDB.DataType.Int32         , DbType = "int"                                               )] public SqlInt32?       Field3          { get; set; } // int
		[Column("Field4"         , DataType = LinqToDB.DataType.Int32         , DbType = "int"                                               )] public SqlInt32?       Field4          { get; set; } // int
		[Column("Field5"         , DataType = LinqToDB.DataType.Int32         , DbType = "int"                                               )] public SqlInt32?       Field5          { get; set; } // int
		[Column("FieldInt64"     , DataType = LinqToDB.DataType.Int64         , DbType = "bigint"                                            )] public SqlInt64?       FieldInt64      { get; set; } // bigint
		[Column("FieldBoolean"   , DataType = LinqToDB.DataType.Boolean       , DbType = "bit"                                               )] public SqlBoolean?     FieldBoolean    { get; set; } // bit
		[Column("FieldString"    , DataType = LinqToDB.DataType.VarChar       , DbType = "varchar(20)"      , Length       = 20              )] public SqlString?      FieldString     { get; set; } // varchar(20)
		[Column("FieldNString"   , DataType = LinqToDB.DataType.NVarChar      , DbType = "nvarchar(20)"     , Length       = 20              )] public SqlString?      FieldNString    { get; set; } // nvarchar(20)
		[Column("FieldChar"      , DataType = LinqToDB.DataType.Char          , DbType = "char(1)"          , Length       = 1               )] public SqlString?      FieldChar       { get; set; } // char(1)
		[Column("FieldNChar"     , DataType = LinqToDB.DataType.NChar         , DbType = "nchar(1)"         , Length       = 1               )] public SqlString?      FieldNChar      { get; set; } // nchar(1)
		[Column("FieldFloat"     , DataType = LinqToDB.DataType.Single        , DbType = "real"                                              )] public SqlSingle?      FieldFloat      { get; set; } // real
		[Column("FieldDouble"    , DataType = LinqToDB.DataType.Double        , DbType = "float"                                             )] public SqlDouble?      FieldDouble     { get; set; } // float
		[Column("FieldDateTime"  , DataType = LinqToDB.DataType.DateTime      , DbType = "datetime"                                          )] public SqlDateTime?    FieldDateTime   { get; set; } // datetime
		[Column("FieldDateTime2" , DataType = LinqToDB.DataType.DateTimeOffset, DbType = "datetimeoffset(7)", Precision    = 7               )] public DateTimeOffset? FieldDateTime2  { get; set; } // datetimeoffset(7)
		[Column("FieldBinary"    , DataType = LinqToDB.DataType.VarBinary     , DbType = "varbinary(20)"    , Length       = 20              )] public SqlBinary?      FieldBinary     { get; set; } // varbinary(20)
		[Column("FieldGuid"      , DataType = LinqToDB.DataType.Guid          , DbType = "uniqueidentifier"                                  )] public SqlGuid?        FieldGuid       { get; set; } // uniqueidentifier
		[Column("FieldDecimal"   , DataType = LinqToDB.DataType.Decimal       , DbType = "decimal(24, 10)"  , Precision    = 24  , Scale = 10)] public SqlDecimal?     FieldDecimal    { get; set; } // decimal(24, 10)
		[Column("FieldDate"      , DataType = LinqToDB.DataType.Date          , DbType = "date"                                              )] public SqlDateTime?    FieldDate       { get; set; } // date
		[Column("FieldTime"      , DataType = LinqToDB.DataType.Time          , DbType = "time(7)"          , Precision    = 7               )] public TimeSpan?       FieldTime       { get; set; } // time(7)
		[Column("FieldEnumString", DataType = LinqToDB.DataType.VarChar       , DbType = "varchar(20)"      , Length       = 20              )] public SqlString?      FieldEnumString { get; set; } // varchar(20)
		[Column("FieldEnumNumber", DataType = LinqToDB.DataType.Int32         , DbType = "int"                                               )] public SqlInt32?       FieldEnumNumber { get; set; } // int

		#region IEquatable<T> support
		private static readonly IEqualityComparer<TestMerge2> _equalityComparer = ComparerBuilder.GetEqualityComparer<TestMerge2>(c => c.Id);

		public bool Equals(TestMerge2? other)
		{
			return _equalityComparer.Equals(this, other!);
		}

		public override int GetHashCode()
		{
			return _equalityComparer.GetHashCode(this);
		}

		public override bool Equals(object? obj)
		{
			return Equals(obj as TestMerge2);
		}
		#endregion
	}
}
