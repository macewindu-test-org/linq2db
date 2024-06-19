﻿using System.Linq;

using LinqToDB;
using LinqToDB.Mapping;
using LinqToDB.SchemaProvider;

using NUnit.Framework;

namespace Tests.Linq
{
	using Model;

	[TestFixture]
	public class ArrayTests : TestBase
	{
		enum SimpleEnum
		{
			Value1,
			Value2,
			Value3,
		}

		[Table]
		sealed class ArrayTable
		{
			[Column] public int[]?        Numbers     { get; set; }
			[Column] public Gender[]?     StringEnums { get; set; }
			[Column] public SimpleEnum[]? IntEnums    { get; set; }
		}

		[Test]
		public void CreateTable([DataSources(false)] string context)
		{
			using var db = GetDataConnection(context);
			using var tb = db.CreateLocalTable<ArrayTable>();

			var schema = db.DataProvider.GetSchemaProvider().GetSchema(db, new GetSchemaOptions()
			{
				LoadTable = t => t.Name == nameof(ArrayTable)
			});

			var tableSchema = schema.Tables.Single();

			Assert.That(tableSchema.Columns, Has.Count.EqualTo(3));

			var column = tableSchema.Columns.Single(c => c.ColumnName == nameof(ArrayTable.Numbers));
			Assert.That(column.SystemType, Is.EqualTo(typeof(int[])));
			Assert.That(column.IsNullable, Is.True);

			column = tableSchema.Columns.Single(c => c.ColumnName == nameof(ArrayTable.StringEnums));
			Assert.That(column.SystemType, Is.EqualTo(typeof(string[])));
			Assert.That(column.IsNullable, Is.True);

			column = tableSchema.Columns.Single(c => c.ColumnName == nameof(ArrayTable.IntEnums));
			Assert.That(column.SystemType, Is.EqualTo(typeof(int[])));
			Assert.That(column.IsNullable, Is.True);
		}

		[Test]
		public void InsertArray([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<ArrayTable>();

			var record = new ArrayTable()
			{
				Numbers     = [-1, 0, 5],
				StringEnums = [Gender.Unknown, Gender.Male, Gender.Female],
				IntEnums    = [SimpleEnum.Value2, SimpleEnum.Value3, SimpleEnum.Value1],
			};

			db.Insert(record);

			var result = tb.Single();

			Assert.That(result.Numbers    , Is.EqualTo(record.Numbers));
			Assert.That(result.StringEnums, Is.EqualTo(record.StringEnums));
			Assert.That(result.IntEnums   , Is.EqualTo(record.IntEnums));
		}

		[Test]
		public void UpdateArray([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<ArrayTable>();

			var record = new ArrayTable()
			{
				Numbers     = [-1, 0, 5],
				StringEnums = [Gender.Unknown, Gender.Male, Gender.Female],
				IntEnums    = [SimpleEnum.Value2, SimpleEnum.Value3, SimpleEnum.Value1],
			};

			db.Insert(record);

			tb
				.Set(r => r.Numbers, [4, 5, 6])
				.Set(r => r.StringEnums, r => null)
				.Set(r => r.IntEnums, r => new[] { SimpleEnum.Value2, SimpleEnum.Value1, SimpleEnum.Value1 })
				.Update();

			var result = tb.Single();

			Assert.That(result.Numbers    , Is.EqualTo(new[] { 4, 5, 6 }));
			Assert.That(result.StringEnums, Is.Null);
			Assert.That(result.IntEnums   , Is.EqualTo(new[] { SimpleEnum.Value2, SimpleEnum.Value1, SimpleEnum.Value1 }));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/1660")]
		public void CollectionContainsMapping([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<ArrayTable>();

			var record = new ArrayTable()
			{
				Numbers     = [-1, 0, 5],
				StringEnums = [Gender.Unknown, Gender.Male, Gender.Female],
				IntEnums    = [SimpleEnum.Value2, SimpleEnum.Value3, SimpleEnum.Value1],
			};

			db.Insert(record);

			var result = tb.Where(r => r.Numbers!.Contains(5)).Single();

			Assert.That(result.Numbers, Is.EqualTo(record.Numbers));
			Assert.That(result.StringEnums, Is.EqualTo(record.StringEnums));
			Assert.That(result.IntEnums, Is.EqualTo(record.IntEnums));

			result = tb.Where(r => r.Numbers!.Contains(6)).SingleOrDefault();

			Assert.That(result, Is.Null);
		}
	}
}
