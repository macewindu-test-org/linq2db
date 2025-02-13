﻿//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591
#nullable enable

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Mapping;

using Microsoft.SqlServer.Types;

namespace ModelDataContext
{
	public partial class TestDataDB
	{
		#region Tables

		public ITable<AllType>                 AllTypes                 { get { return _dataContext.GetTable<AllType>(); } }
		public ITable<AllTypes2>               AllTypes2                { get { return _dataContext.GetTable<AllTypes2>(); } }
		public ITable<Child>                   Children                 { get { return _dataContext.GetTable<Child>(); } }
		public ITable<CollatedTable>           CollatedTables           { get { return _dataContext.GetTable<CollatedTable>(); } }
		public ITable<DataType>                DataTypes                { get { return _dataContext.GetTable<DataType>(); } }
		public ITable<DecimalOverflow>         DecimalOverflows         { get { return _dataContext.GetTable<DecimalOverflow>(); } }
		public ITable<Doctor>                  Doctors                  { get { return _dataContext.GetTable<Doctor>(); } }
		public ITable<GrandChild>              GrandChildren            { get { return _dataContext.GetTable<GrandChild>(); } }
		public ITable<GuidID>                  GuidIds                  { get { return _dataContext.GetTable<GuidID>(); } }
		public ITable<GuidID2>                 GuidID2                  { get { return _dataContext.GetTable<GuidID2>(); } }
		public ITable<IndexTable>              IndexTables              { get { return _dataContext.GetTable<IndexTable>(); } }
		public ITable<IndexTable2>             IndexTable2              { get { return _dataContext.GetTable<IndexTable2>(); } }
		public ITable<InheritanceChild>        InheritanceChildren      { get { return _dataContext.GetTable<InheritanceChild>(); } }
		public ITable<InheritanceParent>       InheritanceParents       { get { return _dataContext.GetTable<InheritanceParent>(); } }
		public ITable<Issue1115>               Issue1115                { get { return _dataContext.GetTable<Issue1115>(); } }
		public ITable<Issue1144>               Issue1144                { get { return _dataContext.GetTable<Issue1144>(); } }
		public ITable<LinqDataType>            LinqDataTypes            { get { return _dataContext.GetTable<LinqDataType>(); } }
		public ITable<Member>                  Members                  { get { return _dataContext.GetTable<Member>(); } }
		public ITable<NameTest>                NameTests                { get { return _dataContext.GetTable<NameTest>(); } }
		/// <summary>
		/// This is Parent table
		/// </summary>
		public ITable<Parent>                  Parents                  { get { return _dataContext.GetTable<Parent>(); } }
		public ITable<ParentChildView>         ParentChildViews         { get { return _dataContext.GetTable<ParentChildView>(); } }
		public ITable<ParentView>              ParentViews              { get { return _dataContext.GetTable<ParentView>(); } }
		public ITable<Patient>                 Patients                 { get { return _dataContext.GetTable<Patient>(); } }
		public ITable<Person>                  People                   { get { return _dataContext.GetTable<Person>(); } }
		public ITable<Provider>                Providers                { get { return _dataContext.GetTable<Provider>(); } }
		public ITable<SameTableName>           SameTableNames           { get { return _dataContext.GetTable<SameTableName>(); } }
		public ITable<SqlType>                 SqlTypes                 { get { return _dataContext.GetTable<SqlType>(); } }
		public ITable<TestIdentity>            TestIdentities           { get { return _dataContext.GetTable<TestIdentity>(); } }
		public ITable<TestMerge1>              TestMerge1               { get { return _dataContext.GetTable<TestMerge1>(); } }
		public ITable<TestMerge2>              TestMerge2               { get { return _dataContext.GetTable<TestMerge2>(); } }
		public ITable<TestMergeIdentity>       TestMergeIdentities      { get { return _dataContext.GetTable<TestMergeIdentity>(); } }
		public ITable<TestSchemaSameTableName> TestSchemaSameTableNames { get { return _dataContext.GetTable<TestSchemaSameTableName>(); } }
		public ITable<TestSchemaX>             TestSchemaX              { get { return _dataContext.GetTable<TestSchemaX>(); } }
		public ITable<TestSchemaY>             TestSchemaY              { get { return _dataContext.GetTable<TestSchemaY>(); } }

		#endregion

		#region Schemas

		public TestSchemaSchema.DataContext TestSchema { get; set; } = null!;

		public void InitSchemas()
		{
			TestSchema = new TestSchemaSchema.DataContext(_dataContext);
		}

		#endregion

		#region .ctor

		public TestDataDB(IDataContext dataContext)
		{
			_dataContext = dataContext;
		}

		private readonly IDataContext _dataContext;

		#endregion
	}

	[Table(Schema="dbo", Name="AllTypes")]
	public partial class AllType
	{
		[Column(),                                                                 PrimaryKey, Identity] public int             ID                       { get; set; } // int
		[Column("bigintDataType"),                                                 Nullable            ] public long?           BigintDataType           { get; set; } // bigint
		[Column("numericDataType"),                                                Nullable            ] public decimal?        NumericDataType          { get; set; } // numeric(18, 1)
		[Column("bitDataType"),                                                    Nullable            ] public bool?           BitDataType              { get; set; } // bit
		[Column("smallintDataType"),                                               Nullable            ] public short?          SmallintDataType         { get; set; } // smallint
		[Column("decimalDataType"),                                                Nullable            ] public decimal?        DecimalDataType          { get; set; } // decimal(18, 1)
		[Column("smallmoneyDataType"),                                             Nullable            ] public decimal?        SmallmoneyDataType       { get; set; } // smallmoney
		[Column("intDataType"),                                                    Nullable            ] public int?            IntDataType              { get; set; } // int
		[Column("tinyintDataType"),                                                Nullable            ] public byte?           TinyintDataType          { get; set; } // tinyint
		[Column("moneyDataType"),                                                  Nullable            ] public decimal?        MoneyDataType            { get; set; } // money
		[Column("floatDataType"),                                                  Nullable            ] public double?         FloatDataType            { get; set; } // float
		[Column("realDataType"),                                                   Nullable            ] public float?          RealDataType             { get; set; } // real
		[Column("datetimeDataType"),                                               Nullable            ] public DateTime?       DatetimeDataType         { get; set; } // datetime
		[Column("smalldatetimeDataType"),                                          Nullable            ] public DateTime?       SmalldatetimeDataType    { get; set; } // smalldatetime
		[Column("charDataType"),                                                   Nullable            ] public char?           CharDataType             { get; set; } // char(1)
		[Column("char20DataType"),                                                 Nullable            ] public string?         Char20DataType           { get; set; } // char(20)
		[Column("varcharDataType"),                                                Nullable            ] public string?         VarcharDataType          { get; set; } // varchar(20)
		[Column("textDataType"),                                                   Nullable            ] public string?         TextDataType             { get; set; } // text
		[Column("ncharDataType"),                                                  Nullable            ] public string?         NcharDataType            { get; set; } // nchar(20)
		[Column("nvarcharDataType"),                                               Nullable            ] public string?         NvarcharDataType         { get; set; } // nvarchar(20)
		[Column("ntextDataType"),                                                  Nullable            ] public string?         NtextDataType            { get; set; } // ntext
		[Column("binaryDataType"),                                                 Nullable            ] public byte[]?         BinaryDataType           { get; set; } // binary(1)
		[Column("varbinaryDataType"),                                              Nullable            ] public byte[]?         VarbinaryDataType        { get; set; } // varbinary(1)
		[Column("imageDataType"),                                                  Nullable            ] public byte[]?         ImageDataType            { get; set; } // image
		[Column("timestampDataType",        SkipOnInsert=true, SkipOnUpdate=true), Nullable            ] public byte[]?         TimestampDataType        { get; set; } // timestamp
		[Column("uniqueidentifierDataType"),                                       Nullable            ] public Guid?           UniqueidentifierDataType { get; set; } // uniqueidentifier
		[Column("sql_variantDataType"),                                            Nullable            ] public object?         SqlVariantDataType       { get; set; } // sql_variant
		[Column("nvarchar_max_DataType"),                                          Nullable            ] public string?         NvarcharMaxDataType      { get; set; } // nvarchar(max)
		[Column("varchar_max_DataType"),                                           Nullable            ] public string?         VarcharMaxDataType       { get; set; } // varchar(max)
		[Column("varbinary_max_DataType"),                                         Nullable            ] public byte[]?         VarbinaryMaxDataType     { get; set; } // varbinary(max)
		[Column("xmlDataType"),                                                    Nullable            ] public string?         XmlDataType              { get; set; } // xml
		[Column("datetime2DataType"),                                              Nullable            ] public DateTime?       Datetime2DataType        { get; set; } // datetime2(7)
		[Column("datetimeoffsetDataType"),                                         Nullable            ] public DateTimeOffset? DatetimeoffsetDataType   { get; set; } // datetimeoffset(7)
		[Column("datetimeoffset0DataType"),                                        Nullable            ] public DateTimeOffset? Datetimeoffset0DataType  { get; set; } // datetimeoffset(0)
		[Column("datetimeoffset1DataType"),                                        Nullable            ] public DateTimeOffset? Datetimeoffset1DataType  { get; set; } // datetimeoffset(1)
		[Column("datetimeoffset2DataType"),                                        Nullable            ] public DateTimeOffset? Datetimeoffset2DataType  { get; set; } // datetimeoffset(2)
		[Column("datetimeoffset3DataType"),                                        Nullable            ] public DateTimeOffset? Datetimeoffset3DataType  { get; set; } // datetimeoffset(3)
		[Column("datetimeoffset4DataType"),                                        Nullable            ] public DateTimeOffset? Datetimeoffset4DataType  { get; set; } // datetimeoffset(4)
		[Column("datetimeoffset5DataType"),                                        Nullable            ] public DateTimeOffset? Datetimeoffset5DataType  { get; set; } // datetimeoffset(5)
		[Column("datetimeoffset6DataType"),                                        Nullable            ] public DateTimeOffset? Datetimeoffset6DataType  { get; set; } // datetimeoffset(6)
		[Column("datetimeoffset7DataType"),                                        Nullable            ] public DateTimeOffset? Datetimeoffset7DataType  { get; set; } // datetimeoffset(7)
		[Column("dateDataType"),                                                   Nullable            ] public DateTime?       DateDataType             { get; set; } // date
		[Column("timeDataType"),                                                   Nullable            ] public TimeSpan?       TimeDataType             { get; set; } // time(7)
	}

	[Table(Schema="dbo", Name="AllTypes2")]
	public partial class AllTypes2
	{
		[Column(),                         PrimaryKey, Identity] public int             ID                     { get; set; } // int
		[Column("dateDataType"),           Nullable            ] public DateTime?       DateDataType           { get; set; } // date
		[Column("datetimeoffsetDataType"), Nullable            ] public DateTimeOffset? DatetimeoffsetDataType { get; set; } // datetimeoffset(7)
		[Column("datetime2DataType"),      Nullable            ] public DateTime?       Datetime2DataType      { get; set; } // datetime2(7)
		[Column("timeDataType"),           Nullable            ] public TimeSpan?       TimeDataType           { get; set; } // time(7)
		[Column("hierarchyidDataType"),    Nullable            ] public SqlHierarchyId? HierarchyidDataType    { get; set; } // hierarchyid
		[Column("geographyDataType"),      Nullable            ] public SqlGeography?   GeographyDataType      { get; set; } // geography
		[Column("geometryDataType"),       Nullable            ] public SqlGeometry?    GeometryDataType       { get; set; } // geometry
	}

	[Table(Schema="dbo", Name="Child")]
	public partial class Child
	{
		[Column(),      Nullable            ] public int? ParentID { get; set; } // int
		/// <summary>
		/// This ChildID column
		/// </summary>
		[Column(),      Nullable            ] public int? ChildID  { get; set; } // int
		[Column("_ID"), PrimaryKey, Identity] public int  Id       { get; set; } // int
	}

	[Table(Schema="dbo", Name="CollatedTable")]
	public partial class CollatedTable
	{
		[Column, NotNull] public int    Id              { get; set; } // int
		[Column, NotNull] public string CaseSensitive   { get; set; } = null!; // nvarchar(20)
		[Column, NotNull] public string CaseInsensitive { get; set; } = null!; // nvarchar(20)
	}

	[Table(Schema="dbo", Name="DataType")]
	public partial class DataType
	{
		[Column("id"), NotNull] public int Id { get; set; } // int
	}

	[Table(Schema="dbo", Name="DecimalOverflow")]
	public partial class DecimalOverflow
	{
		[PrimaryKey, NotNull    ] public decimal  Decimal1 { get; set; } // decimal(38, 20)
		[Column,        Nullable] public decimal? Decimal2 { get; set; } // decimal(31, 2)
		[Column,        Nullable] public decimal? Decimal3 { get; set; } // decimal(38, 36)
		[Column,        Nullable] public decimal? Decimal4 { get; set; } // decimal(29, 0)
		[Column,        Nullable] public decimal? Decimal5 { get; set; } // decimal(38, 38)
	}

	[Table(Schema="dbo", Name="Doctor")]
	public partial class Doctor
	{
		[PrimaryKey, NotNull] public int    PersonID { get; set; } // int
		[Column,     NotNull] public string Taxonomy { get; set; } = null!; // nvarchar(50)

		#region Associations

		/// <summary>
		/// FK_Doctor_Person (dbo.Person)
		/// </summary>
		[Association(ThisKey=nameof(PersonID), OtherKey=nameof(ModelDataContext.Person.PersonID), CanBeNull=false)]
		public Person Person { get; set; } = null!;

		#endregion
	}

	[Table(Schema="dbo", Name="GrandChild")]
	public partial class GrandChild
	{
		[Column(),      Nullable            ] public int? ParentID     { get; set; } // int
		[Column(),      Nullable            ] public int? ChildID      { get; set; } // int
		[Column(),      Nullable            ] public int? GrandChildID { get; set; } // int
		[Column("_ID"), PrimaryKey, Identity] public int  Id           { get; set; } // int
	}

	[Table(Schema="dbo", Name="GuidID")]
	public partial class GuidID
	{
		[PrimaryKey, NotNull    ] public Guid ID     { get; set; } // uniqueidentifier
		[Column,        Nullable] public int? Field1 { get; set; } // int
	}

	[Table(Schema="dbo", Name="GuidID2")]
	public partial class GuidID2
	{
		[PrimaryKey, NotNull] public Guid ID { get; set; } // uniqueidentifier
	}

	[Table(Schema="dbo", Name="IndexTable")]
	public partial class IndexTable
	{
		[PrimaryKey(2), NotNull] public int PKField1    { get; set; } // int
		[PrimaryKey(1), NotNull] public int PKField2    { get; set; } // int
		[Column,        NotNull] public int UniqueField { get; set; } // int
		[Column,        NotNull] public int IndexField  { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_Patient2_IndexTable_BackReference (dbo.IndexTable2)
		/// </summary>
		[Association(ThisKey=nameof(PKField2) + ", " + nameof(PKField1), OtherKey=nameof(ModelDataContext.IndexTable2.PKField2) + ", " + nameof(ModelDataContext.IndexTable2.PKField1), CanBeNull=true)]
		public IndexTable2? Patient { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="IndexTable2")]
	public partial class IndexTable2
	{
		[PrimaryKey(2), NotNull] public int PKField1 { get; set; } // int
		[PrimaryKey(1), NotNull] public int PKField2 { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_Patient2_IndexTable (dbo.IndexTable)
		/// </summary>
		[Association(ThisKey=nameof(PKField2) + ", " + nameof(PKField1), OtherKey=nameof(ModelDataContext.IndexTable.PKField2) + ", " + nameof(ModelDataContext.IndexTable.PKField1), CanBeNull=false)]
		public IndexTable Patient2IndexTable { get; set; } = null!;

		#endregion
	}

	[Table(Schema="dbo", Name="InheritanceChild")]
	public partial class InheritanceChild
	{
		[PrimaryKey, NotNull    ] public int     InheritanceChildId  { get; set; } // int
		[Column,     NotNull    ] public int     InheritanceParentId { get; set; } // int
		[Column,        Nullable] public int?    TypeDiscriminator   { get; set; } // int
		[Column,        Nullable] public string? Name                { get; set; } // nvarchar(50)
	}

	[Table(Schema="dbo", Name="InheritanceParent")]
	public partial class InheritanceParent
	{
		[PrimaryKey, NotNull    ] public int     InheritanceParentId { get; set; } // int
		[Column,        Nullable] public int?    TypeDiscriminator   { get; set; } // int
		[Column,        Nullable] public string? Name                { get; set; } // nvarchar(50)
	}

	[Table(Schema="dbo", Name="Issue1115")]
	public partial class Issue1115
	{
		[Column("id"), PrimaryKey, NotNull] public SqlHierarchyId Id { get; set; } // hierarchyid
	}

	[Table(Schema="dbo", Name="Issue1144")]
	public partial class Issue1144
	{
		/// <summary>
		/// Column description
		/// </summary>
		[Column("id"), PrimaryKey, NotNull] public int Id { get; set; } // int
	}

	[Table(Schema="dbo", Name="LinqDataTypes")]
	public partial class LinqDataType
	{
		[Column("_ID"), PrimaryKey, Identity] public int       Id             { get; set; } // int
		[Column(),      Nullable            ] public int?      ID             { get; set; } // int
		[Column(),      Nullable            ] public decimal?  MoneyValue     { get; set; } // decimal(10, 4)
		[Column(),      Nullable            ] public DateTime? DateTimeValue  { get; set; } // datetime
		[Column(),      Nullable            ] public DateTime? DateTimeValue2 { get; set; } // datetime2(7)
		[Column(),      Nullable            ] public bool?     BoolValue      { get; set; } // bit
		[Column(),      Nullable            ] public Guid?     GuidValue      { get; set; } // uniqueidentifier
		[Column(),      Nullable            ] public byte[]?   BinaryValue    { get; set; } // varbinary(5000)
		[Column(),      Nullable            ] public short?    SmallIntValue  { get; set; } // smallint
		[Column(),      Nullable            ] public int?      IntValue       { get; set; } // int
		[Column(),      Nullable            ] public long?     BigIntValue    { get; set; } // bigint
		[Column(),      Nullable            ] public string?   StringValue    { get; set; } // nvarchar(50)
	}

	[Table(Schema="dbo", Name="Member")]
	public partial class Member
	{
		[PrimaryKey, Identity] public int    MemberId { get; set; } // int
		[Column,     NotNull ] public string Alias    { get; set; } = null!; // nvarchar(50)

		#region Associations

		/// <summary>
		/// FK_Provider_Member_BackReference (dbo.Provider)
		/// </summary>
		[Association(ThisKey=nameof(MemberId), OtherKey=nameof(ModelDataContext.Provider.ProviderId), CanBeNull=true)]
		public Provider? Provider { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Name.Test")]
	public partial class NameTest
	{
		[Column("Name.Test"), Nullable] public int? NameTestColumn { get; set; } // int
	}

	/// <summary>
	/// This is Parent table
	/// </summary>
	[Table(Schema="dbo", Name="Parent")]
	public partial class Parent
	{
		[Column(),      Nullable            ] public int? ParentID { get; set; } // int
		[Column(),      Nullable            ] public int? Value1   { get; set; } // int
		[Column("_ID"), PrimaryKey, Identity] public int  Id       { get; set; } // int
	}

	[Table(Schema="dbo", Name="ParentChildView", IsView=true)]
	public partial class ParentChildView
	{
		[Column, Nullable] public int? ParentID { get; set; } // int
		[Column, Nullable] public int? Value1   { get; set; } // int
		[Column, Nullable] public int? ChildID  { get; set; } // int
	}

	[Table(Schema="dbo", Name="ParentView", IsView=true)]
	public partial class ParentView
	{
		[Column(),      Nullable] public int? ParentID { get; set; } // int
		[Column(),      Nullable] public int? Value1   { get; set; } // int
		[Column("_ID"), Identity] public int  Id       { get; set; } // int
	}

	[Table(Schema="dbo", Name="Patient")]
	public partial class Patient
	{
		[PrimaryKey, NotNull] public int    PersonID  { get; set; } // int
		[Column,     NotNull] public string Diagnosis { get; set; } = null!; // nvarchar(256)

		#region Associations

		/// <summary>
		/// FK_Patient_Person (dbo.Person)
		/// </summary>
		[Association(ThisKey=nameof(PersonID), OtherKey=nameof(ModelDataContext.Person.PersonID), CanBeNull=false)]
		public Person Person { get; set; } = null!;

		#endregion
	}

	[Table(Schema="dbo", Name="Person")]
	public partial class Person
	{
		[PrimaryKey, Identity   ] public int     PersonID   { get; set; } // int
		[Column,     NotNull    ] public string  FirstName  { get; set; } = null!; // nvarchar(50)
		[Column,     NotNull    ] public string  LastName   { get; set; } = null!; // nvarchar(50)
		[Column,        Nullable] public string? MiddleName { get; set; } // nvarchar(50)
		[Column,     NotNull    ] public char    Gender     { get; set; } // char(1)

		#region Associations

		/// <summary>
		/// FK_Doctor_Person_BackReference (dbo.Doctor)
		/// </summary>
		[Association(ThisKey=nameof(PersonID), OtherKey=nameof(ModelDataContext.Doctor.PersonID), CanBeNull=true)]
		public Doctor? Doctor { get; set; }

		/// <summary>
		/// FK_Patient_Person_BackReference (dbo.Patient)
		/// </summary>
		[Association(ThisKey=nameof(PersonID), OtherKey=nameof(ModelDataContext.Patient.PersonID), CanBeNull=true)]
		public Patient? Patient { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Provider")]
	public partial class Provider
	{
		[PrimaryKey, NotNull] public int    ProviderId { get; set; } // int
		[Column,     NotNull] public string Test       { get; set; } = null!; // nvarchar(max)

		#region Associations

		/// <summary>
		/// FK_Provider_Member (dbo.Member)
		/// </summary>
		[Association(ThisKey=nameof(ProviderId), OtherKey=nameof(ModelDataContext.Member.MemberId), CanBeNull=false)]
		public Member Member { get; set; } = null!;

		#endregion
	}

	[Table(Schema="dbo", Name="SameTableName")]
	public partial class SameTableName
	{
		[Column("id"), Nullable] public int? Id { get; set; } // int
	}

	[Table(Schema="dbo", Name="SqlTypes")]
	public partial class SqlType
	{
		[PrimaryKey, NotNull    ] public int             ID  { get; set; } // int
		[Column,        Nullable] public SqlHierarchyId? HID { get; set; } // hierarchyid
	}

	[Table(Schema="dbo", Name="TestIdentity")]
	public partial class TestIdentity
	{
		[PrimaryKey, Identity] public int ID { get; set; } // int
	}

	[Table(Schema="dbo", Name="TestMerge1")]
	public partial class TestMerge1
	{
		[PrimaryKey, NotNull    ] public int             Id              { get; set; } // int
		[Column,        Nullable] public int?            Field1          { get; set; } // int
		[Column,        Nullable] public int?            Field2          { get; set; } // int
		[Column,        Nullable] public int?            Field3          { get; set; } // int
		[Column,        Nullable] public int?            Field4          { get; set; } // int
		[Column,        Nullable] public int?            Field5          { get; set; } // int
		[Column,        Nullable] public long?           FieldInt64      { get; set; } // bigint
		[Column,        Nullable] public bool?           FieldBoolean    { get; set; } // bit
		[Column,        Nullable] public string?         FieldString     { get; set; } // varchar(20)
		[Column,        Nullable] public string?         FieldNString    { get; set; } // nvarchar(20)
		[Column,        Nullable] public char?           FieldChar       { get; set; } // char(1)
		[Column,        Nullable] public char?           FieldNChar      { get; set; } // nchar(1)
		[Column,        Nullable] public float?          FieldFloat      { get; set; } // real
		[Column,        Nullable] public double?         FieldDouble     { get; set; } // float
		[Column,        Nullable] public DateTime?       FieldDateTime   { get; set; } // datetime
		[Column,        Nullable] public DateTimeOffset? FieldDateTime2  { get; set; } // datetimeoffset(7)
		[Column,        Nullable] public byte[]?         FieldBinary     { get; set; } // varbinary(20)
		[Column,        Nullable] public Guid?           FieldGuid       { get; set; } // uniqueidentifier
		[Column,        Nullable] public decimal?        FieldDecimal    { get; set; } // decimal(24, 10)
		[Column,        Nullable] public DateTime?       FieldDate       { get; set; } // date
		[Column,        Nullable] public TimeSpan?       FieldTime       { get; set; } // time(7)
		[Column,        Nullable] public string?         FieldEnumString { get; set; } // varchar(20)
		[Column,        Nullable] public int?            FieldEnumNumber { get; set; } // int
	}

	[Table(Schema="dbo", Name="TestMerge2")]
	public partial class TestMerge2
	{
		[PrimaryKey, NotNull    ] public int             Id              { get; set; } // int
		[Column,        Nullable] public int?            Field1          { get; set; } // int
		[Column,        Nullable] public int?            Field2          { get; set; } // int
		[Column,        Nullable] public int?            Field3          { get; set; } // int
		[Column,        Nullable] public int?            Field4          { get; set; } // int
		[Column,        Nullable] public int?            Field5          { get; set; } // int
		[Column,        Nullable] public long?           FieldInt64      { get; set; } // bigint
		[Column,        Nullable] public bool?           FieldBoolean    { get; set; } // bit
		[Column,        Nullable] public string?         FieldString     { get; set; } // varchar(20)
		[Column,        Nullable] public string?         FieldNString    { get; set; } // nvarchar(20)
		[Column,        Nullable] public char?           FieldChar       { get; set; } // char(1)
		[Column,        Nullable] public char?           FieldNChar      { get; set; } // nchar(1)
		[Column,        Nullable] public float?          FieldFloat      { get; set; } // real
		[Column,        Nullable] public double?         FieldDouble     { get; set; } // float
		[Column,        Nullable] public DateTime?       FieldDateTime   { get; set; } // datetime
		[Column,        Nullable] public DateTimeOffset? FieldDateTime2  { get; set; } // datetimeoffset(7)
		[Column,        Nullable] public byte[]?         FieldBinary     { get; set; } // varbinary(20)
		[Column,        Nullable] public Guid?           FieldGuid       { get; set; } // uniqueidentifier
		[Column,        Nullable] public decimal?        FieldDecimal    { get; set; } // decimal(24, 10)
		[Column,        Nullable] public DateTime?       FieldDate       { get; set; } // date
		[Column,        Nullable] public TimeSpan?       FieldTime       { get; set; } // time(7)
		[Column,        Nullable] public string?         FieldEnumString { get; set; } // varchar(20)
		[Column,        Nullable] public int?            FieldEnumNumber { get; set; } // int
	}

	[Table(Schema="dbo", Name="TestMergeIdentity")]
	public partial class TestMergeIdentity
	{
		[PrimaryKey, Identity] public int  Id    { get; set; } // int
		[Column,     Nullable] public int? Field { get; set; } // int
	}

	[Table(Schema="dbo", Name="TestSchema_SameTableName")]
	public partial class TestSchemaSameTableName
	{
		[Column("id"), Nullable] public int? Id { get; set; } // int
	}

	[Table(Schema="dbo", Name="TestSchemaX")]
	public partial class TestSchemaX
	{
		[PrimaryKey, NotNull] public int TestSchemaXID { get; set; } // int
		[Column,     NotNull] public int Field1        { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_TestSchemaY_TestSchemaX_BackReference (dbo.TestSchemaY)
		/// </summary>
		[Association(ThisKey=nameof(TestSchemaXID), OtherKey=nameof(ModelDataContext.TestSchemaY.TestSchemaXID), CanBeNull=true)]
		public IEnumerable<TestSchemaY> TestSchemaY { get; set; } = null!;

		/// <summary>
		/// FK_TestSchemaY_OtherID_BackReference (dbo.TestSchemaY)
		/// </summary>
		[Association(ThisKey=nameof(TestSchemaXID), OtherKey=nameof(ModelDataContext.TestSchemaY.TestSchemaXID), CanBeNull=true)]
		public IEnumerable<TestSchemaY> TestSchemaYOtherIds { get; set; } = null!;

		/// <summary>
		/// FK_TestSchemaY_ParentTestSchemaX_BackReference (dbo.TestSchemaY)
		/// </summary>
		[Association(ThisKey=nameof(TestSchemaXID), OtherKey=nameof(ModelDataContext.TestSchemaY.ParentTestSchemaXID), CanBeNull=true)]
		public IEnumerable<TestSchemaY> TestSchemaYParentTestSchemaX { get; set; } = null!;

		#endregion
	}

	[Table(Schema="dbo", Name="TestSchemaY")]
	public partial class TestSchemaY
	{
		[Column, NotNull] public int TestSchemaXID       { get; set; } // int
		[Column, NotNull] public int ParentTestSchemaXID { get; set; } // int
		[Column, NotNull] public int OtherID             { get; set; } // int

		#region Associations

		/// <summary>
		/// FK_TestSchemaY_OtherID (dbo.TestSchemaX)
		/// </summary>
		[Association(ThisKey=nameof(TestSchemaXID), OtherKey=nameof(ModelDataContext.TestSchemaX.TestSchemaXID), CanBeNull=false)]
		public TestSchemaX FkTestSchemaYOtherID { get; set; } = null!;

		/// <summary>
		/// FK_TestSchemaY_ParentTestSchemaX (dbo.TestSchemaX)
		/// </summary>
		[Association(ThisKey=nameof(ParentTestSchemaXID), OtherKey=nameof(ModelDataContext.TestSchemaX.TestSchemaXID), CanBeNull=false)]
		public TestSchemaX ParentTestSchemaX { get; set; } = null!;

		/// <summary>
		/// FK_TestSchemaY_TestSchemaX (dbo.TestSchemaX)
		/// </summary>
		[Association(ThisKey=nameof(TestSchemaXID), OtherKey=nameof(ModelDataContext.TestSchemaX.TestSchemaXID), CanBeNull=false)]
		public TestSchemaX TestSchemaX { get; set; } = null!;

		#endregion
	}

	public static partial class TableExtensions
	{
		public static AllType? Find(this ITable<AllType> table, int ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static AllTypes2? Find(this ITable<AllTypes2> table, int ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Child? Find(this ITable<Child> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static DecimalOverflow? Find(this ITable<DecimalOverflow> table, decimal Decimal1)
		{
			return table.FirstOrDefault(t =>
				t.Decimal1 == Decimal1);
		}

		public static Doctor? Find(this ITable<Doctor> table, int PersonID)
		{
			return table.FirstOrDefault(t =>
				t.PersonID == PersonID);
		}

		public static GrandChild? Find(this ITable<GrandChild> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static GuidID? Find(this ITable<GuidID> table, Guid ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static GuidID2? Find(this ITable<GuidID2> table, Guid ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static IndexTable? Find(this ITable<IndexTable> table, int PKField1, int PKField2)
		{
			return table.FirstOrDefault(t =>
				t.PKField1 == PKField1 &&
				t.PKField2 == PKField2);
		}

		public static IndexTable2? Find(this ITable<IndexTable2> table, int PKField1, int PKField2)
		{
			return table.FirstOrDefault(t =>
				t.PKField1 == PKField1 &&
				t.PKField2 == PKField2);
		}

		public static InheritanceChild? Find(this ITable<InheritanceChild> table, int InheritanceChildId)
		{
			return table.FirstOrDefault(t =>
				t.InheritanceChildId == InheritanceChildId);
		}

		public static InheritanceParent? Find(this ITable<InheritanceParent> table, int InheritanceParentId)
		{
			return table.FirstOrDefault(t =>
				t.InheritanceParentId == InheritanceParentId);
		}

		public static Issue1115? Find(this ITable<Issue1115> table, SqlHierarchyId Id)
		{
			return table.FirstOrDefault(t =>
				(bool)(t.Id == Id));
		}

		public static Issue1144? Find(this ITable<Issue1144> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static LinqDataType? Find(this ITable<LinqDataType> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Member? Find(this ITable<Member> table, int MemberId)
		{
			return table.FirstOrDefault(t =>
				t.MemberId == MemberId);
		}

		public static Parent? Find(this ITable<Parent> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Patient? Find(this ITable<Patient> table, int PersonID)
		{
			return table.FirstOrDefault(t =>
				t.PersonID == PersonID);
		}

		public static Person? Find(this ITable<Person> table, int PersonID)
		{
			return table.FirstOrDefault(t =>
				t.PersonID == PersonID);
		}

		public static Provider? Find(this ITable<Provider> table, int ProviderId)
		{
			return table.FirstOrDefault(t =>
				t.ProviderId == ProviderId);
		}

		public static SqlType? Find(this ITable<SqlType> table, int ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static TestIdentity? Find(this ITable<TestIdentity> table, int ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static TestMerge1? Find(this ITable<TestMerge1> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static TestMerge2? Find(this ITable<TestMerge2> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static TestMergeIdentity? Find(this ITable<TestMergeIdentity> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static TestSchemaX? Find(this ITable<TestSchemaX> table, int TestSchemaXID)
		{
			return table.FirstOrDefault(t =>
				t.TestSchemaXID == TestSchemaXID);
		}
	}

	public static partial class TestSchemaSchema
	{
		public partial class DataContext
		{
			public ITable<SameTableName> SameTableNames { get { return _dataContext.GetTable<SameTableName>(); } }
			public ITable<TestSchemaA>   TestSchemaA    { get { return _dataContext.GetTable<TestSchemaA>(); } }
			public ITable<TestSchemaB>   TestSchemaB    { get { return _dataContext.GetTable<TestSchemaB>(); } }

			private readonly IDataContext _dataContext;

			public DataContext(IDataContext dataContext)
			{
				_dataContext = dataContext;
			}
		}

		[Table(Schema="TestSchema", Name="SameTableName")]
		public partial class SameTableName
		{
			[Column("id"), Nullable] public int? Id { get; set; } // int
		}

		[Table(Schema="TestSchema", Name="TestSchemaA")]
		public partial class TestSchemaA
		{
			[PrimaryKey, NotNull] public int TestSchemaAID { get; set; } // int
			[Column,     NotNull] public int Field1        { get; set; } // int

			#region Associations

			/// <summary>
			/// FK_TestSchema_TestSchemaBY_TargetTestSchemaA2_BackReference (TestSchema.TestSchemaB)
			/// </summary>
			[Association(ThisKey=nameof(TestSchemaAID), OtherKey=nameof(ModelDataContext.TestSchemaSchema.TestSchemaB.TargetTestSchemaAId), CanBeNull=true)]
			public IEnumerable<TestSchemaSchema.TestSchemaB> FkTestSchemaTestSchemaBYTargetTestSchemaA2BackReferences { get; set; } = null!;

			/// <summary>
			/// FK_TestSchema_TestSchemaBY_OriginTestSchemaA_BackReference (TestSchema.TestSchemaB)
			/// </summary>
			[Association(ThisKey=nameof(TestSchemaAID), OtherKey=nameof(ModelDataContext.TestSchemaSchema.TestSchemaB.OriginTestSchemaAID), CanBeNull=true)]
			public IEnumerable<TestSchemaSchema.TestSchemaB> TestSchemaBYOriginTestSchemaA { get; set; } = null!;

			/// <summary>
			/// FK_TestSchema_TestSchemaBY_TargetTestSchemaA_BackReference (TestSchema.TestSchemaB)
			/// </summary>
			[Association(ThisKey=nameof(TestSchemaAID), OtherKey=nameof(ModelDataContext.TestSchemaSchema.TestSchemaB.TargetTestSchemaAID), CanBeNull=true)]
			public IEnumerable<TestSchemaSchema.TestSchemaB> TestSchemaBYTargetTestSchemaA { get; set; } = null!;

			#endregion
		}

		[Table(Schema="TestSchema", Name="TestSchemaB")]
		public partial class TestSchemaB
		{
			[Column(),                          PrimaryKey, NotNull] public int TestSchemaBID       { get; set; } // int
			[Column(),                                      NotNull] public int OriginTestSchemaAID { get; set; } // int
			[Column(),                                      NotNull] public int TargetTestSchemaAID { get; set; } // int
			[Column("Target_Test_Schema_A_ID"),             NotNull] public int TargetTestSchemaAId { get; set; } // int

			#region Associations

			/// <summary>
			/// FK_TestSchema_TestSchemaBY_TargetTestSchemaA (TestSchema.TestSchemaA)
			/// </summary>
			[Association(ThisKey=nameof(TargetTestSchemaAID), OtherKey=nameof(ModelDataContext.TestSchemaSchema.TestSchemaA.TestSchemaAID), CanBeNull=false)]
			public TestSchemaSchema.TestSchemaA FKTargetTestSchemaA { get; set; } = null!;

			/// <summary>
			/// FK_TestSchema_TestSchemaBY_OriginTestSchemaA (TestSchema.TestSchemaA)
			/// </summary>
			[Association(ThisKey=nameof(OriginTestSchemaAID), OtherKey=nameof(ModelDataContext.TestSchemaSchema.TestSchemaA.TestSchemaAID), CanBeNull=false)]
			public TestSchemaSchema.TestSchemaA OriginTestSchemaA { get; set; } = null!;

			/// <summary>
			/// FK_TestSchema_TestSchemaBY_TargetTestSchemaA2 (TestSchema.TestSchemaA)
			/// </summary>
			[Association(ThisKey=nameof(TargetTestSchemaAId), OtherKey=nameof(ModelDataContext.TestSchemaSchema.TestSchemaA.TestSchemaAID), CanBeNull=false)]
			public TestSchemaSchema.TestSchemaA TargetTestSchemaA { get; set; } = null!;

			#endregion
		}

		#region Table Extensions

		public static TestSchemaA? Find(this ITable<TestSchemaA> table, int TestSchemaAID)
		{
			return table.FirstOrDefault(t =>
				t.TestSchemaAID == TestSchemaAID);
		}

		public static TestSchemaB? Find(this ITable<TestSchemaB> table, int TestSchemaBID)
		{
			return table.FirstOrDefault(t =>
				t.TestSchemaBID == TestSchemaBID);
		}

		#endregion
	}
}
