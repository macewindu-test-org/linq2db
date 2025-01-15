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

using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace Default.Access.Odbc
{
	public partial class TestDataDB : LinqToDB.Data.DataConnection
	{
		#region Tables

		public ITable<AllType>             AllTypes             { get { return this.GetTable<AllType>(); } }
		public ITable<Child>               Children             { get { return this.GetTable<Child>(); } }
		public ITable<DataTypeTest>        DataTypeTests        { get { return this.GetTable<DataTypeTest>(); } }
		public ITable<Doctor>              Doctors              { get { return this.GetTable<Doctor>(); } }
		public ITable<Dual>                Duals                { get { return this.GetTable<Dual>(); } }
		public ITable<GrandChild>          GrandChildren        { get { return this.GetTable<GrandChild>(); } }
		public ITable<InheritanceChild>    InheritanceChildren  { get { return this.GetTable<InheritanceChild>(); } }
		public ITable<InheritanceParent>   InheritanceParents   { get { return this.GetTable<InheritanceParent>(); } }
		public ITable<LinqDataType>        LinqDataTypes        { get { return this.GetTable<LinqDataType>(); } }
		public ITable<LinqDataTypesQuery>  LinqDataTypesQueries { get { return this.GetTable<LinqDataTypesQuery>(); } }
		public ITable<LinqDataTypesQuery1> LinqDataTypesQuery1  { get { return this.GetTable<LinqDataTypesQuery1>(); } }
		public ITable<LinqDataTypesQuery2> LinqDataTypesQuery2  { get { return this.GetTable<LinqDataTypesQuery2>(); } }
		public ITable<Parent>              Parents              { get { return this.GetTable<Parent>(); } }
		public ITable<Patient>             Patients             { get { return this.GetTable<Patient>(); } }
		public ITable<PatientSelectAll>    PatientSelectAll     { get { return this.GetTable<PatientSelectAll>(); } }
		public ITable<Person>              People               { get { return this.GetTable<Person>(); } }
		public ITable<PersonSelectAll>     PersonSelectAll      { get { return this.GetTable<PersonSelectAll>(); } }
		public ITable<ScalarDataReader>    ScalarDataReaders    { get { return this.GetTable<ScalarDataReader>(); } }
		public ITable<TestIdentity>        TestIdentities       { get { return this.GetTable<TestIdentity>(); } }
		public ITable<TestMerge1>          TestMerge1           { get { return this.GetTable<TestMerge1>(); } }
		public ITable<TestMerge2>          TestMerge2           { get { return this.GetTable<TestMerge2>(); } }

		#endregion

		#region .ctor

		public TestDataDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public TestDataDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public TestDataDB(DataOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public TestDataDB(DataOptions<TestDataDB> options)
			: base(options.Options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();

		#endregion
	}

	[Table("AllTypes")]
	public partial class AllType
	{
		[Column(),                           Identity   ] public int       ID                       { get; set; } // COUNTER
		[Column("bitDataType"),              NotNull    ] public bool      BitDataType              { get; set; } // BIT
		[Column("smallintDataType"),            Nullable] public short?    SmallintDataType         { get; set; } // SMALLINT
		[Column("decimalDataType"),             Nullable] public decimal?  DecimalDataType          { get; set; } // DECIMAL(18, 0)
		[Column("intDataType"),                 Nullable] public int?      IntDataType              { get; set; } // INTEGER
		[Column("tinyintDataType"),             Nullable] public byte?     TinyintDataType          { get; set; } // BYTE
		[Column("moneyDataType"),               Nullable] public decimal?  MoneyDataType            { get; set; } // CURRENCY
		[Column("floatDataType"),               Nullable] public double?   FloatDataType            { get; set; } // DOUBLE
		[Column("realDataType"),                Nullable] public float?    RealDataType             { get; set; } // REAL
		[Column("datetimeDataType"),            Nullable] public DateTime? DatetimeDataType         { get; set; } // DATETIME
		[Column("charDataType"),                Nullable] public char?     CharDataType             { get; set; } // CHAR(1)
		[Column("char20DataType"),              Nullable] public string?   Char20DataType           { get; set; } // CHAR(20)
		[Column("varcharDataType"),             Nullable] public string?   VarcharDataType          { get; set; } // VARCHAR(20)
		[Column("textDataType"),                Nullable] public string?   TextDataType             { get; set; } // LONGCHAR
		[Column("ncharDataType"),               Nullable] public string?   NcharDataType            { get; set; } // CHAR(20)
		[Column("nvarcharDataType"),            Nullable] public string?   NvarcharDataType         { get; set; } // VARCHAR(20)
		[Column("ntextDataType"),               Nullable] public string?   NtextDataType            { get; set; } // LONGCHAR
		[Column("binaryDataType"),              Nullable] public byte[]?   BinaryDataType           { get; set; } // BINARY(10)
		[Column("varbinaryDataType"),           Nullable] public byte[]?   VarbinaryDataType        { get; set; } // VARBINARY(510)
		[Column("imageDataType"),               Nullable] public byte[]?   ImageDataType            { get; set; } // LONGBINARY
		[Column("oleObjectDataType"),           Nullable] public byte[]?   OleObjectDataType        { get; set; } // LONGBINARY
		[Column("uniqueidentifierDataType"),    Nullable] public Guid?     UniqueidentifierDataType { get; set; } // GUID
	}

	[Table("Child")]
	public partial class Child
	{
		[Column, Nullable] public int? ParentID { get; set; } // INTEGER
		[Column, Nullable] public int? ChildID  { get; set; } // INTEGER
	}

	[Table("DataTypeTest")]
	public partial class DataTypeTest
	{
		[Column(),            Identity] public int       DataTypeID { get; set; } // COUNTER
		[Column("Binary_"),   Nullable] public byte[]?   Binary     { get; set; } // LONGBINARY
		[Column("Boolean_"),  Nullable] public int?      Boolean    { get; set; } // INTEGER
		[Column("Byte_"),     Nullable] public byte?     Byte       { get; set; } // BYTE
		[Column("Bytes_"),    Nullable] public byte[]?   Bytes      { get; set; } // LONGBINARY
		[Column("Char_"),     Nullable] public char?     Char       { get; set; } // VARCHAR(1)
		[Column("DateTime_"), Nullable] public DateTime? DateTime   { get; set; } // DATETIME
		[Column("Decimal_"),  Nullable] public decimal?  Decimal    { get; set; } // CURRENCY
		[Column("Double_"),   Nullable] public double?   Double     { get; set; } // DOUBLE
		[Column("Guid_"),     Nullable] public Guid?     Guid       { get; set; } // GUID
		[Column("Int16_"),    Nullable] public short?    Int16      { get; set; } // SMALLINT
		[Column("Int32_"),    Nullable] public int?      Int32      { get; set; } // INTEGER
		[Column("Int64_"),    Nullable] public int?      Int64      { get; set; } // INTEGER
		[Column("Money_"),    Nullable] public decimal?  Money      { get; set; } // CURRENCY
		[Column("SByte_"),    Nullable] public byte?     SByte      { get; set; } // BYTE
		[Column("Single_"),   Nullable] public float?    Single     { get; set; } // REAL
		[Column("Stream_"),   Nullable] public byte[]?   Stream     { get; set; } // LONGBINARY
		[Column("String_"),   Nullable] public string?   String     { get; set; } // VARCHAR(50)
		[Column("UInt16_"),   Nullable] public short?    UInt16     { get; set; } // SMALLINT
		[Column("UInt32_"),   Nullable] public int?      UInt32     { get; set; } // INTEGER
		[Column("UInt64_"),   Nullable] public int?      UInt64     { get; set; } // INTEGER
		[Column("Xml_"),      Nullable] public string?   Xml        { get; set; } // LONGCHAR
	}

	[Table("Doctor")]
	public partial class Doctor
	{
		[Column, Nullable] public int?    PersonID { get; set; } // INTEGER
		[Column, Nullable] public string? Taxonomy { get; set; } // VARCHAR(50)
	}

	[Table("Dual")]
	public partial class Dual
	{
		[Column, Nullable] public string? Dummy { get; set; } // VARCHAR(10)
	}

	[Table("GrandChild")]
	public partial class GrandChild
	{
		[Column, Nullable] public int? ParentID     { get; set; } // INTEGER
		[Column, Nullable] public int? ChildID      { get; set; } // INTEGER
		[Column, Nullable] public int? GrandChildID { get; set; } // INTEGER
	}

	[Table("InheritanceChild")]
	public partial class InheritanceChild
	{
		[Column, Nullable] public int?    InheritanceChildId  { get; set; } // INTEGER
		[Column, Nullable] public int?    InheritanceParentId { get; set; } // INTEGER
		[Column, Nullable] public int?    TypeDiscriminator   { get; set; } // INTEGER
		[Column, Nullable] public string? Name                { get; set; } // VARCHAR(50)
	}

	[Table("InheritanceParent")]
	public partial class InheritanceParent
	{
		[Column, Nullable] public int?    InheritanceParentId { get; set; } // INTEGER
		[Column, Nullable] public int?    TypeDiscriminator   { get; set; } // INTEGER
		[Column, Nullable] public string? Name                { get; set; } // VARCHAR(50)
	}

	[Table("LinqDataTypes")]
	public partial class LinqDataType
	{
		[Column,    Nullable] public int?      ID             { get; set; } // INTEGER
		[Column,    Nullable] public decimal?  MoneyValue     { get; set; } // DECIMAL(10, 4)
		[Column,    Nullable] public DateTime? DateTimeValue  { get; set; } // DATETIME
		[Column,    Nullable] public DateTime? DateTimeValue2 { get; set; } // DATETIME
		[Column, NotNull    ] public bool      BoolValue      { get; set; } // BIT
		[Column,    Nullable] public Guid?     GuidValue      { get; set; } // GUID
		[Column,    Nullable] public byte[]?   BinaryValue    { get; set; } // LONGBINARY
		[Column,    Nullable] public short?    SmallIntValue  { get; set; } // SMALLINT
		[Column,    Nullable] public int?      IntValue       { get; set; } // INTEGER
		[Column,    Nullable] public int?      BigIntValue    { get; set; } // INTEGER
		[Column,    Nullable] public string?   StringValue    { get; set; } // VARCHAR(50)
	}

	[Table("LinqDataTypes Query", IsView=true)]
	public partial class LinqDataTypesQuery
	{
		[Column, Nullable] public DateTime? DateTimeValue { get; set; } // DATETIME
	}

	[Table("LinqDataTypes Query1", IsView=true)]
	public partial class LinqDataTypesQuery1
	{
		[Column, Nullable] public int? ID { get; set; } // INTEGER
	}

	[Table("LinqDataTypes Query2", IsView=true)]
	public partial class LinqDataTypesQuery2
	{
		[Column, Nullable] public int? ID { get; set; } // INTEGER
	}

	[Table("Parent")]
	public partial class Parent
	{
		[Column, Nullable] public int? ParentID { get; set; } // INTEGER
		[Column, Nullable] public int? Value1   { get; set; } // INTEGER
	}

	[Table("Patient")]
	public partial class Patient
	{
		[Column, Nullable] public int?    PersonID  { get; set; } // INTEGER
		[Column, Nullable] public string? Diagnosis { get; set; } // VARCHAR(255)
	}

	[Table("Patient_SelectAll", IsView=true)]
	public partial class PatientSelectAll
	{
		[Identity          ] public int     PersonID   { get; set; } // COUNTER
		[Column,   Nullable] public string? FirstName  { get; set; } // VARCHAR(50)
		[Column,   Nullable] public string? LastName   { get; set; } // VARCHAR(50)
		[Column,   Nullable] public string? MiddleName { get; set; } // VARCHAR(50)
		[Column,   Nullable] public char?   Gender     { get; set; } // VARCHAR(1)
		[Column,   Nullable] public string? Diagnosis  { get; set; } // VARCHAR(255)
	}

	[Table("Person")]
	public partial class Person
	{
		[Identity          ] public int     PersonID   { get; set; } // COUNTER
		[Column,   Nullable] public string? FirstName  { get; set; } // VARCHAR(50)
		[Column,   Nullable] public string? LastName   { get; set; } // VARCHAR(50)
		[Column,   Nullable] public string? MiddleName { get; set; } // VARCHAR(50)
		[Column,   Nullable] public char?   Gender     { get; set; } // VARCHAR(1)
	}

	[Table("Person_SelectAll", IsView=true)]
	public partial class PersonSelectAll
	{
		[Identity          ] public int     PersonID   { get; set; } // COUNTER
		[Column,   Nullable] public string? FirstName  { get; set; } // VARCHAR(50)
		[Column,   Nullable] public string? LastName   { get; set; } // VARCHAR(50)
		[Column,   Nullable] public string? MiddleName { get; set; } // VARCHAR(50)
		[Column,   Nullable] public char?   Gender     { get; set; } // VARCHAR(1)
	}

	[Table("Scalar_DataReader", IsView=true)]
	public partial class ScalarDataReader
	{
		[Column("intField"),    Nullable] public int?    IntField    { get; set; } // INTEGER
		[Column("stringField"), Nullable] public string? StringField { get; set; } // VARCHAR
	}

	[Table("TestIdentity")]
	public partial class TestIdentity
	{
		[Identity] public int ID { get; set; } // COUNTER
	}

	[Table("TestMerge1")]
	public partial class TestMerge1
	{
		[Column,    Nullable] public int?      Id              { get; set; } // INTEGER
		[Column,    Nullable] public int?      Field1          { get; set; } // INTEGER
		[Column,    Nullable] public int?      Field2          { get; set; } // INTEGER
		[Column,    Nullable] public int?      Field3          { get; set; } // INTEGER
		[Column,    Nullable] public int?      Field4          { get; set; } // INTEGER
		[Column,    Nullable] public int?      Field5          { get; set; } // INTEGER
		[Column, NotNull    ] public bool      FieldBoolean    { get; set; } // BIT
		[Column,    Nullable] public string?   FieldString     { get; set; } // VARCHAR(20)
		[Column,    Nullable] public string?   FieldNString    { get; set; } // VARCHAR(20)
		[Column,    Nullable] public char?     FieldChar       { get; set; } // CHAR(1)
		[Column,    Nullable] public char?     FieldNChar      { get; set; } // CHAR(1)
		[Column,    Nullable] public float?    FieldFloat      { get; set; } // REAL
		[Column,    Nullable] public double?   FieldDouble     { get; set; } // DOUBLE
		[Column,    Nullable] public DateTime? FieldDateTime   { get; set; } // DATETIME
		[Column,    Nullable] public byte[]?   FieldBinary     { get; set; } // VARBINARY(20)
		[Column,    Nullable] public Guid?     FieldGuid       { get; set; } // GUID
		[Column,    Nullable] public decimal?  FieldDecimal    { get; set; } // DECIMAL(24, 10)
		[Column,    Nullable] public DateTime? FieldDate       { get; set; } // DATETIME
		[Column,    Nullable] public DateTime? FieldTime       { get; set; } // DATETIME
		[Column,    Nullable] public string?   FieldEnumString { get; set; } // VARCHAR(20)
		[Column,    Nullable] public int?      FieldEnumNumber { get; set; } // INTEGER
	}

	[Table("TestMerge2")]
	public partial class TestMerge2
	{
		[Column,    Nullable] public int?      Id              { get; set; } // INTEGER
		[Column,    Nullable] public int?      Field1          { get; set; } // INTEGER
		[Column,    Nullable] public int?      Field2          { get; set; } // INTEGER
		[Column,    Nullable] public int?      Field3          { get; set; } // INTEGER
		[Column,    Nullable] public int?      Field4          { get; set; } // INTEGER
		[Column,    Nullable] public int?      Field5          { get; set; } // INTEGER
		[Column, NotNull    ] public bool      FieldBoolean    { get; set; } // BIT
		[Column,    Nullable] public string?   FieldString     { get; set; } // VARCHAR(20)
		[Column,    Nullable] public string?   FieldNString    { get; set; } // VARCHAR(20)
		[Column,    Nullable] public char?     FieldChar       { get; set; } // CHAR(1)
		[Column,    Nullable] public char?     FieldNChar      { get; set; } // CHAR(1)
		[Column,    Nullable] public float?    FieldFloat      { get; set; } // REAL
		[Column,    Nullable] public double?   FieldDouble     { get; set; } // DOUBLE
		[Column,    Nullable] public DateTime? FieldDateTime   { get; set; } // DATETIME
		[Column,    Nullable] public byte[]?   FieldBinary     { get; set; } // VARBINARY(20)
		[Column,    Nullable] public Guid?     FieldGuid       { get; set; } // GUID
		[Column,    Nullable] public decimal?  FieldDecimal    { get; set; } // DECIMAL(24, 10)
		[Column,    Nullable] public DateTime? FieldDate       { get; set; } // DATETIME
		[Column,    Nullable] public DateTime? FieldTime       { get; set; } // DATETIME
		[Column,    Nullable] public string?   FieldEnumString { get; set; } // VARCHAR(20)
		[Column,    Nullable] public int?      FieldEnumNumber { get; set; } // INTEGER
	}

	public static partial class TestDataDBStoredProcedures
	{
		#region PatientSelectByName

		public static IEnumerable<PatientSelectByNameResult> PatientSelectByName(this TestDataDB dataConnection, string? @firstName, string? @lastName)
		{
			var parameters = new []
			{
				new DataParameter("@firstName", @firstName, LinqToDB.DataType.VarChar, 255),
				new DataParameter("@lastName",  @lastName,  LinqToDB.DataType.VarChar, 255)
			};

			return dataConnection.QueryProc<PatientSelectByNameResult>("[Patient_SelectByName]", parameters);
		}

		public partial class PatientSelectByNameResult
		{
			public int     PersonID   { get; set; }
			public string? FirstName  { get; set; }
			public string? LastName   { get; set; }
			public string? MiddleName { get; set; }
			public string? Gender     { get; set; }
			public string? Diagnosis  { get; set; }
		}

		#endregion

		#region PersonDelete

		public static int PersonDelete(this TestDataDB dataConnection, int? @PersonID)
		{
			var parameters = new []
			{
				new DataParameter("@PersonID", @PersonID, LinqToDB.DataType.Int32)
			};

			return dataConnection.ExecuteProc("[Person_Delete]", parameters);
		}

		#endregion

		#region PersonInsert

		public static int PersonInsert(this TestDataDB dataConnection, string? @FirstName, string? @MiddleName, string? @LastName, string? @Gender)
		{
			var parameters = new []
			{
				new DataParameter("@FirstName",  @FirstName,  LinqToDB.DataType.VarChar, 255),
				new DataParameter("@MiddleName", @MiddleName, LinqToDB.DataType.VarChar, 255),
				new DataParameter("@LastName",   @LastName,   LinqToDB.DataType.VarChar, 255),
				new DataParameter("@Gender",     @Gender,     LinqToDB.DataType.VarChar, 255)
			};

			return dataConnection.ExecuteProc("[Person_Insert]", parameters);
		}

		#endregion

		#region PersonSelectByKey

		public static IEnumerable<PersonSelectByKeyResult> PersonSelectByKey(this TestDataDB dataConnection, int? @id)
		{
			var parameters = new []
			{
				new DataParameter("@id", @id, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<PersonSelectByKeyResult>("[Person_SelectByKey]", parameters);
		}

		public partial class PersonSelectByKeyResult
		{
			public int     PersonID   { get; set; }
			public string? FirstName  { get; set; }
			public string? LastName   { get; set; }
			public string? MiddleName { get; set; }
			public string? Gender     { get; set; }
		}

		#endregion

		#region PersonSelectByName

		public static IEnumerable<PersonSelectByNameResult> PersonSelectByName(this TestDataDB dataConnection, string? @firstName, string? @lastName)
		{
			var parameters = new []
			{
				new DataParameter("@firstName", @firstName, LinqToDB.DataType.VarChar, 255),
				new DataParameter("@lastName",  @lastName,  LinqToDB.DataType.VarChar, 255)
			};

			return dataConnection.QueryProc<PersonSelectByNameResult>("[Person_SelectByName]", parameters);
		}

		public partial class PersonSelectByNameResult
		{
			public int     PersonID   { get; set; }
			public string? FirstName  { get; set; }
			public string? LastName   { get; set; }
			public string? MiddleName { get; set; }
			public string? Gender     { get; set; }
		}

		#endregion

		#region PersonSelectListByName

		public static IEnumerable<PersonSelectListByNameResult> PersonSelectListByName(this TestDataDB dataConnection, string? @firstName, string? @lastName)
		{
			var parameters = new []
			{
				new DataParameter("@firstName", @firstName, LinqToDB.DataType.VarChar, 255),
				new DataParameter("@lastName",  @lastName,  LinqToDB.DataType.VarChar, 255)
			};

			return dataConnection.QueryProc<PersonSelectListByNameResult>("[Person_SelectListByName]", parameters);
		}

		public partial class PersonSelectListByNameResult
		{
			public int     PersonID   { get; set; }
			public string? FirstName  { get; set; }
			public string? LastName   { get; set; }
			public string? MiddleName { get; set; }
			public string? Gender     { get; set; }
		}

		#endregion

		#region PersonUpdate

		public static int PersonUpdate(this TestDataDB dataConnection, int? @id, int? @PersonID, string? @FirstName, string? @MiddleName, string? @LastName, string? @Gender)
		{
			var parameters = new []
			{
				new DataParameter("@id",         @id,         LinqToDB.DataType.Int32),
				new DataParameter("@PersonID",   @PersonID,   LinqToDB.DataType.Int32),
				new DataParameter("@FirstName",  @FirstName,  LinqToDB.DataType.VarChar, 255),
				new DataParameter("@MiddleName", @MiddleName, LinqToDB.DataType.VarChar, 255),
				new DataParameter("@LastName",   @LastName,   LinqToDB.DataType.VarChar, 255),
				new DataParameter("@Gender",     @Gender,     LinqToDB.DataType.VarChar, 255)
			};

			return dataConnection.ExecuteProc("[Person_Update]", parameters);
		}

		#endregion
	}
}

