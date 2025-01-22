﻿//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;
using System.Linq;

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Mapping;

namespace ClickHouse.Client
{
	public partial class Testdb2DB : LinqToDB.Data.DataConnection
	{
		#region Tables

		public ITable<AllType>                 AllTypes                 { get { return this.GetTable<AllType>(); } }
		public ITable<Child>                   Children                 { get { return this.GetTable<Child>(); } }
		public ITable<CollatedTable>           CollatedTables           { get { return this.GetTable<CollatedTable>(); } }
		public ITable<Doctor>                  Doctors                  { get { return this.GetTable<Doctor>(); } }
		public ITable<GrandChild>              GrandChildren            { get { return this.GetTable<GrandChild>(); } }
		public ITable<InheritanceChild>        InheritanceChildren      { get { return this.GetTable<InheritanceChild>(); } }
		public ITable<InheritanceParent>       InheritanceParents       { get { return this.GetTable<InheritanceParent>(); } }
		public ITable<LinqDataType>            LinqDataTypes            { get { return this.GetTable<LinqDataType>(); } }
		public ITable<Parent>                  Parents                  { get { return this.GetTable<Parent>(); } }
		public ITable<Patient>                 Patients                 { get { return this.GetTable<Patient>(); } }
		public ITable<Person>                  People                   { get { return this.GetTable<Person>(); } }
		public ITable<ReplacingMergeTreeTable> ReplacingMergeTreeTables { get { return this.GetTable<ReplacingMergeTreeTable>(); } }
		public ITable<TestMerge1>              TestMerge1               { get { return this.GetTable<TestMerge1>(); } }
		public ITable<TestMerge2>              TestMerge2               { get { return this.GetTable<TestMerge2>(); } }

		#endregion

		#region .ctor

		public Testdb2DB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public Testdb2DB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public Testdb2DB(DataOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public Testdb2DB(DataOptions<Testdb2DB> options)
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
		[Column(                    SkipOnUpdate=true), PrimaryKey,  NotNull] public int     ID               { get; set; } // Int32
		[Column("intDataType"),                            Nullable         ] public int?    IntDataType      { get; set; } // Int32
		[Column("smallintDataType"),                       Nullable         ] public short?  SmallintDataType { get; set; } // Int16
		[Column("floatDataType"),                          Nullable         ] public float?  FloatDataType    { get; set; } // Float32
		[Column("doubleDataType"),                         Nullable         ] public double? DoubleDataType   { get; set; } // Float64
		[Column("ncharDataType"),                          Nullable         ] public string  NcharDataType    { get; set; } // FixedString(20)
		[Column("char20DataType"),                         Nullable         ] public string  Char20DataType   { get; set; } // FixedString(20)
		[Column("varcharDataType"),                        Nullable         ] public string  VarcharDataType  { get; set; } // String
		[Column("charDataType"),                           Nullable         ] public char?   CharDataType     { get; set; } // FixedString(1)
		[Column("bitDataType"),                            Nullable         ] public ulong?  BitDataType      { get; set; } // UInt64
	}

	[Table("Child")]
	public partial class Child
	{
		[Column, NotNull] public int ParentID { get; set; } // Int32
		[Column, NotNull] public int ChildID  { get; set; } // Int32
	}

	[Table("CollatedTable")]
	public partial class CollatedTable
	{
		[Column(SkipOnUpdate=true), PrimaryKey,  NotNull] public int    Id              { get; set; } // Int32
		[Column,                       Nullable         ] public string CaseSensitive   { get; set; } // String
		[Column,                       Nullable         ] public string CaseInsensitive { get; set; } // String
	}

	[Table("Doctor")]
	public partial class Doctor
	{
		[Column(SkipOnUpdate=true), PrimaryKey, NotNull] public int    PersonID { get; set; } // Int32
		[Column,                                NotNull] public string Taxonomy { get; set; } // String
	}

	[Table("GrandChild")]
	public partial class GrandChild
	{
		[Column, NotNull] public int ParentID     { get; set; } // Int32
		[Column, NotNull] public int ChildID      { get; set; } // Int32
		[Column, NotNull] public int GrandChildID { get; set; } // Int32
	}

	[Table("InheritanceChild")]
	public partial class InheritanceChild
	{
		[Column(SkipOnUpdate=true), PrimaryKey,  NotNull] public int    InheritanceChildId  { get; set; } // Int32
		[Column,                                 NotNull] public int    InheritanceParentId { get; set; } // Int32
		[Column,                       Nullable         ] public int?   TypeDiscriminator   { get; set; } // Int32
		[Column,                       Nullable         ] public string Name                { get; set; } // String
	}

	[Table("InheritanceParent")]
	public partial class InheritanceParent
	{
		[Column(SkipOnUpdate=true), PrimaryKey,  NotNull] public int    InheritanceParentId { get; set; } // Int32
		[Column,                       Nullable         ] public int?   TypeDiscriminator   { get; set; } // Int32
		[Column,                       Nullable         ] public string Name                { get; set; } // String
	}

	[Table("LinqDataTypes")]
	public partial class LinqDataType
	{
		[Column(SkipOnUpdate=true), PrimaryKey,  NotNull] public int             ID             { get; set; } // Int32
		[Column,                       Nullable         ] public decimal?        MoneyValue     { get; set; } // Decimal(18, 4)
		[Column,                       Nullable         ] public DateTimeOffset? DateTimeValue  { get; set; } // DateTime64(3)
		[Column,                       Nullable         ] public DateTimeOffset? DateTimeValue2 { get; set; } // DateTime64(7)
		[Column,                       Nullable         ] public bool?           BoolValue      { get; set; } // Bool
		[Column,                       Nullable         ] public Guid?           GuidValue      { get; set; } // UUID
		[Column,                       Nullable         ] public string          BinaryValue    { get; set; } // String
		[Column,                       Nullable         ] public short?          SmallIntValue  { get; set; } // Int16
		[Column,                       Nullable         ] public int?            IntValue       { get; set; } // Int32
		[Column,                       Nullable         ] public long?           BigIntValue    { get; set; } // Int64
		[Column,                       Nullable         ] public string          StringValue    { get; set; } // String
	}

	[Table("Parent")]
	public partial class Parent
	{
		[Column, NotNull    ] public int  ParentID { get; set; } // Int32
		[Column,    Nullable] public int? Value1   { get; set; } // Int32
	}

	[Table("Patient")]
	public partial class Patient
	{
		[Column(SkipOnUpdate=true), PrimaryKey, NotNull] public int    PersonID  { get; set; } // Int32
		[Column,                                NotNull] public string Diagnosis { get; set; } // String
	}

	[Table("Person")]
	public partial class Person
	{
		[Column(SkipOnUpdate=true), PrimaryKey,  NotNull] public int    PersonID   { get; set; } // Int32
		[Column,                                 NotNull] public string FirstName  { get; set; } // String
		[Column,                                 NotNull] public string LastName   { get; set; } // String
		[Column,                       Nullable         ] public string MiddleName { get; set; } // String
		[Column,                                 NotNull] public char   Gender     { get; set; } // FixedString(1)
	}

	[Table("ReplacingMergeTreeTable")]
	public partial class ReplacingMergeTreeTable
	{
		[Column(SkipOnUpdate=true), PrimaryKey, NotNull] public uint           ID { get; set; } // UInt32
		[Column,                                NotNull] public DateTimeOffset TS { get; set; } // DateTime
	}

	[Table("TestMerge1")]
	public partial class TestMerge1
	{
		[Column(SkipOnUpdate=true), PrimaryKey,  NotNull] public int             Id              { get; set; } // Int32
		[Column,                       Nullable         ] public int?            Field1          { get; set; } // Int32
		[Column,                       Nullable         ] public int?            Field2          { get; set; } // Int32
		[Column,                       Nullable         ] public int?            Field3          { get; set; } // Int32
		[Column,                       Nullable         ] public int?            Field4          { get; set; } // Int32
		[Column,                       Nullable         ] public int?            Field5          { get; set; } // Int32
		[Column,                       Nullable         ] public long?           FieldInt64      { get; set; } // Int64
		[Column,                       Nullable         ] public bool?           FieldBoolean    { get; set; } // Bool
		[Column,                       Nullable         ] public string          FieldString     { get; set; } // String
		[Column,                       Nullable         ] public string          FieldNString    { get; set; } // String
		[Column,                       Nullable         ] public char?           FieldChar       { get; set; } // FixedString(1)
		[Column,                       Nullable         ] public string          FieldNChar      { get; set; } // FixedString(2)
		[Column,                       Nullable         ] public float?          FieldFloat      { get; set; } // Float32
		[Column,                       Nullable         ] public double?         FieldDouble     { get; set; } // Float64
		[Column,                       Nullable         ] public DateTimeOffset? FieldDateTime   { get; set; } // DateTime64(3)
		[Column,                       Nullable         ] public DateTimeOffset? FieldDateTime2  { get; set; } // DateTime64(7)
		[Column,                       Nullable         ] public string          FieldBinary     { get; set; } // String
		[Column,                       Nullable         ] public Guid?           FieldGuid       { get; set; } // UUID
		[Column,                       Nullable         ] public decimal?        FieldDecimal    { get; set; } // Decimal(38, 10)
		[Column,                       Nullable         ] public DateTime?       FieldDate       { get; set; } // Date
		[Column,                       Nullable         ] public long?           FieldTime       { get; set; } // Int64
		[Column,                       Nullable         ] public string          FieldEnumString { get; set; } // String
		[Column,                       Nullable         ] public int?            FieldEnumNumber { get; set; } // Int32
	}

	[Table("TestMerge2")]
	public partial class TestMerge2
	{
		[Column(SkipOnUpdate=true), PrimaryKey,  NotNull] public int             Id              { get; set; } // Int32
		[Column,                       Nullable         ] public int?            Field1          { get; set; } // Int32
		[Column,                       Nullable         ] public int?            Field2          { get; set; } // Int32
		[Column,                       Nullable         ] public int?            Field3          { get; set; } // Int32
		[Column,                       Nullable         ] public int?            Field4          { get; set; } // Int32
		[Column,                       Nullable         ] public int?            Field5          { get; set; } // Int32
		[Column,                       Nullable         ] public long?           FieldInt64      { get; set; } // Int64
		[Column,                       Nullable         ] public bool?           FieldBoolean    { get; set; } // Bool
		[Column,                       Nullable         ] public string          FieldString     { get; set; } // String
		[Column,                       Nullable         ] public string          FieldNString    { get; set; } // String
		[Column,                       Nullable         ] public char?           FieldChar       { get; set; } // FixedString(1)
		[Column,                       Nullable         ] public string          FieldNChar      { get; set; } // FixedString(2)
		[Column,                       Nullable         ] public float?          FieldFloat      { get; set; } // Float32
		[Column,                       Nullable         ] public double?         FieldDouble     { get; set; } // Float64
		[Column,                       Nullable         ] public DateTimeOffset? FieldDateTime   { get; set; } // DateTime64(3)
		[Column,                       Nullable         ] public DateTimeOffset? FieldDateTime2  { get; set; } // DateTime64(7)
		[Column,                       Nullable         ] public string          FieldBinary     { get; set; } // String
		[Column,                       Nullable         ] public Guid?           FieldGuid       { get; set; } // UUID
		[Column,                       Nullable         ] public decimal?        FieldDecimal    { get; set; } // Decimal(38, 10)
		[Column,                       Nullable         ] public DateTime?       FieldDate       { get; set; } // Date
		[Column,                       Nullable         ] public long?           FieldTime       { get; set; } // Int64
		[Column,                       Nullable         ] public string          FieldEnumString { get; set; } // String
		[Column,                       Nullable         ] public int?            FieldEnumNumber { get; set; } // Int32
	}

	public static partial class TableExtensions
	{
		public static AllType Find(this ITable<AllType> table, int ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static CollatedTable Find(this ITable<CollatedTable> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Doctor Find(this ITable<Doctor> table, int PersonID)
		{
			return table.FirstOrDefault(t =>
				t.PersonID == PersonID);
		}

		public static InheritanceChild Find(this ITable<InheritanceChild> table, int InheritanceChildId)
		{
			return table.FirstOrDefault(t =>
				t.InheritanceChildId == InheritanceChildId);
		}

		public static InheritanceParent Find(this ITable<InheritanceParent> table, int InheritanceParentId)
		{
			return table.FirstOrDefault(t =>
				t.InheritanceParentId == InheritanceParentId);
		}

		public static LinqDataType Find(this ITable<LinqDataType> table, int ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static Patient Find(this ITable<Patient> table, int PersonID)
		{
			return table.FirstOrDefault(t =>
				t.PersonID == PersonID);
		}

		public static Person Find(this ITable<Person> table, int PersonID)
		{
			return table.FirstOrDefault(t =>
				t.PersonID == PersonID);
		}

		public static ReplacingMergeTreeTable Find(this ITable<ReplacingMergeTreeTable> table, uint ID)
		{
			return table.FirstOrDefault(t =>
				t.ID == ID);
		}

		public static TestMerge1 Find(this ITable<TestMerge1> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static TestMerge2 Find(this ITable<TestMerge2> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}
	}
}
