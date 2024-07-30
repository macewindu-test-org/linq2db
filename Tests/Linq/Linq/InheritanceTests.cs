﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Linq;
using LinqToDB.Mapping;

using NUnit.Framework;

namespace Tests.Linq
{
	using Model;

	using Org.BouncyCastle.Crypto.Agreement;

	[TestFixture]
	public class InheritanceTests : TestBase
	{
		[Test]
		public void Test1([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(ParentInheritance, db.ParentInheritance);
		}

		[Test]
		public void Test2([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(ParentInheritance, db.ParentInheritance.Select(p => p));
		}

		[Test]
		public void Test3([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in    ParentInheritance where p is ParentInheritance1 select p,
					from p in db.ParentInheritance where p is ParentInheritance1 select p);
		}

		[Test]
		public void Test4([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in    ParentInheritance where !(p is ParentInheritanceNull) select p,
					from p in db.ParentInheritance where !(p is ParentInheritanceNull) select p);
		}

		[Test]
		public void Test5([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in    ParentInheritance where p is ParentInheritanceValue select p,
					from p in db.ParentInheritance where p is ParentInheritanceValue select p);
		}

		[Test]
		public void Test6([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			{
				var q = from p in db.ParentInheritance2 where p is ParentInheritance12 select p;
				var _ = q.ToList();
			}
		}

		[Test]
		public void Test7([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in    ParentInheritance where p is ParentInheritanceBase select p,
					from p in db.ParentInheritance where p is ParentInheritanceBase select p);
		}

		[Test]
		public void Test8([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					   ParentInheritance.OfType<ParentInheritance1>(),
					db.ParentInheritance.OfType<ParentInheritance1>());
		}

		[Test]
		public void Test9([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					   ParentInheritance
						.Where(p => p.ParentID == 1 || p.ParentID == 2 || p.ParentID == 4)
						.OfType<ParentInheritanceNull>(),
					db.ParentInheritance
						.Where(p => p.ParentID == 1 || p.ParentID == 2 || p.ParentID == 4)
						.OfType<ParentInheritanceNull>());
		}

		[Test]
		public void Test10([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					   ParentInheritance.OfType<ParentInheritanceValue>(),
					db.ParentInheritance.OfType<ParentInheritanceValue>());
		}

		[Test]
		public void Test11([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			{
				var q = from p in db.ParentInheritance3 where p is ParentInheritance13 select p;
				var _ = q.ToList();
			}
		}

		[Test]
		public void Test12([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in    ParentInheritance1 where p.ParentID == 1 select p,
					from p in db.ParentInheritance1 where p.ParentID == 1 select p);
		}

		//[Test]
		public void Test13([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in    ParentInheritance4
					join c in    Child on p.ParentID equals c.ParentID
					select p,
					from p in db.ParentInheritance4
					join c in db.Child on p.ParentID equals c.ParentID
					select p);
		}

		[Test]
		public void TestGetBaseClass([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			{
				var q = db.GetTable<ParentInheritanceBase3>()
					.Where(x => x is ParentInheritance13)
					.ToList();
				Assert.AreEqual(2, q.Count);
			}
		}

		[Test]
		public void TypeCastAsTest1([NorthwindDataContext] string context)
		{
			using (var db = new NorthwindDB(context))
			{
				var dd = GetNorthwindAsList(context);
				AreEqual(
					dd.DiscontinuedProduct.ToList()
						.Select(p => p as Northwind.Product)
						.Select(p => p == null ? "NULL" : p.ProductName),
					db.DiscontinuedProduct
						.Select(p => p as Northwind.Product)
						.Select(p => p == null ? "NULL" : p.ProductName));
			}
		}

		[Test]
		public void TypeCastAsTest11([NorthwindDataContext] string context)
		{
			using (var db = new NorthwindDB(context))
			{
				var dd = GetNorthwindAsList(context);
				AreEqual(
					dd.DiscontinuedProduct.ToList()
						.Select(p => new { p = p as Northwind.Product })
						.Select(p => p.p == null ? "NULL" : p.p.ProductName),
					db.DiscontinuedProduct
						.Select(p => new { p = p as Northwind.Product })
						.Select(p => p.p == null ? "NULL" : p.p.ProductName));
			}
		}

		[Test]
		public void TypeCastAsTest2([NorthwindDataContext] string context)
		{
			using (var db = new NorthwindDB(context))
			{
				var dd = GetNorthwindAsList(context);
				AreEqual(
					dd.Product.ToList()
						.Select(p => p as Northwind.DiscontinuedProduct)
						.Select(p => p == null ? "NULL" : p.ProductName),
					db.Product
						.Select(p => p as Northwind.DiscontinuedProduct)
						.Select(p => p == null ? "NULL" : p.ProductName));
			}
		}

		[Test]
		public void FirstOrDefault([NorthwindDataContext] string context)
		{
			using (var db = new NorthwindDB(context))
			{
				var dd = GetNorthwindAsList(context);
				Assert.AreEqual(
					dd.DiscontinuedProduct.FirstOrDefault()!.ProductID,
					db.DiscontinuedProduct.FirstOrDefault()!.ProductID);
			}
		}

		[Test]
		public void Cast1([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					   ParentInheritance.OfType<ParentInheritance1>().Cast<ParentInheritanceBase>(),
					db.ParentInheritance.OfType<ParentInheritance1>().Cast<ParentInheritanceBase>());
		}

		[Test]
		public async Task Cast1Async([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					      ParentInheritance.OfType<ParentInheritance1>().Cast<ParentInheritanceBase>(),
					await db.ParentInheritance.OfType<ParentInheritance1>().Cast<ParentInheritanceBase>().ToListAsync());
		}

		sealed class ParentEx : Parent
		{
			[NotColumn]
			public bool Field1;

			public static void Test(InheritanceTests inheritance, string context)
			{
				using (var db = inheritance.GetDataContext(context))
					inheritance.AreEqual(
						inheritance.Parent.Select(p => new ParentEx { Field1 = true, ParentID = p.ParentID, Value1 = p.Value1 }).Cast<Parent>(),
								 db.Parent.Select(p => new ParentEx { Field1 = true, ParentID = p.ParentID, Value1 = p.Value1 }).Cast<Parent>());
			}
		}

		[Test]
		public void Cast2([DataSources] string context)
		{
			ParentEx.Test(this, context);
		}

		[Table("Person", IsColumnAttributeRequired = false)]
		sealed class PersonEx : Person
		{
		}

		[Test]
		public void SimplTest()
		{
			using (var db = new DataConnection())
				Assert.AreEqual(1, db.GetTable<PersonEx>().Where(_ => _.FirstName == "John").Select(_ => _.ID).Single());
		}

		[InheritanceMapping(Code = 1, Type = typeof(Parent222))]
		[Table("Parent")]
		public class Parent111
		{
			[Column(IsDiscriminator = true)]
			public int ParentID;
		}

		[Column("Value1", "Value.ID")]
		public class Parent222 : Parent111
		{
			public Value111 Value = null!;
		}

		public class Value111
		{
			public int ID;
		}

		[Test]
		public void InheritanceMappingIssueTest()
		{
			using (var db = new DataConnection())
			{
				var q1 = db.GetTable<Parent222>();
				var q  = q1.Where(_ => _.Value.ID == 1);

				var sql = ((IExpressionQuery<Parent222>)q).SqlText;
				Assert.IsNotEmpty(sql);
			}
		}

		[Table(Name = "Child", IsColumnAttributeRequired = false)]
		[InheritanceMapping(Code = 1, IsDefault = true, Type = typeof(MyChildBase))]
		[InheritanceMapping(Code = 11, Type = typeof(MyChild11))]
		[InheritanceMapping(Code = 21, Type = typeof(MyChild21))]
		public class MyChildBase
		{
			[Column(IsDiscriminator = true)]
			public int ChildID { get; set; }
		}

		public class MyChildBase_11_21 : MyChildBase { }
		public class MyChild11 : MyChildBase_11_21 { }
		public class MyChild21 : MyChildBase_11_21 { }

		[Test]
		public void InheritanceMappingIssue106Test([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			{
				var childIDs = db.GetTable<MyChildBase_11_21>().AsEnumerable()
					.Select(ch => ch.ChildID)
					.OrderBy(x => x)
					.ToList();

				Assert.IsTrue(childIDs.SequenceEqual(new [] {11, 21} ), "{0}: {1}, {2}", childIDs.Count, childIDs[0], childIDs[1]);
			}
		}

		[Test]
		public void ReferenceNavigation([NorthwindDataContext] string context)
		{
			using (var db = new NorthwindDB(context))
			{
				var result =
					from od in db.OrderDetail
					where od.Product.Category!.CategoryName == "Seafood"
					select new { od.Order, od.Product };

				var list = result.ToList();

				Assert.AreEqual(330, list.Count);

				foreach (var item in list)
				{
					Assert.IsNotNull(item);
					Assert.IsNotNull(item.Order);
					Assert.IsNotNull(item.Product);
					Assert.IsTrue(
						 item.Product.Discontinued && item.Product is Northwind.DiscontinuedProduct ||
						!item.Product.Discontinued && item.Product is Northwind.ActiveProduct);
				}
			}
		}

		[Test]
		public void TypeCastIsChildConditional1([NorthwindDataContext] string context)
		{
			using (var db = new NorthwindDB(context))
			{
				var result   = db.Product.         Select(x => x is Northwind.DiscontinuedProduct ? x : null).ToList();
				var expected = db.Product.ToList().Select(x => x is Northwind.DiscontinuedProduct ? x : null).ToList();

				Assert.That(result.Count,                    Is.GreaterThan(0));
				Assert.That(expected.Count,                  Is.EqualTo(result.Count));
				Assert.That(result.Contains(null),           Is.True);
				Assert.That(result.Select(x => x == null ? (int?)null : x.ProductID).Except(expected.Select(x => x == null ? (int?)null : x.ProductID)).Count(), Is.EqualTo(0));
			}
		}

		[Test]
		public void TypeCastIsChildConditional2([NorthwindDataContext] string context)
		{
			using (var db = new NorthwindDB(context))
			{
				var result   = db.Product.         Select(x => x is Northwind.DiscontinuedProduct);
				var expected = db.Product.ToList().Select(x => x is Northwind.DiscontinuedProduct);

				var list = result.ToList();

				Assert.Greater(list.Count, 0);
				Assert.AreEqual(expected.Count(), list.Count);
				Assert.IsTrue(list.Except(expected).Count() == 0);
			}
		}

		[Test]
		public void TypeCastIsChild([NorthwindDataContext] string context)
		{
			using (var db = new NorthwindDB(context))
			{
				var dd = GetNorthwindAsList(context);

				var result   = db.Product.Where(x => x is Northwind.DiscontinuedProduct).ToList();
				var expected = dd.Product.Where(x => x is Northwind.DiscontinuedProduct).ToList();

				Assert.Greater(result.Count, 0);
				Assert.AreEqual(result.Count, expected.Count);
			}
		}

		#region Models for Test14

		interface IChildTest14
		{
			int ChildID { get; set; }
		}

		[Table(Name="Child")]
		sealed class ChildTest14 : IChildTest14
		{
			[PrimaryKey] public int ChildID { get; set; }

		}

		T? FindById<T>(IQueryable<T> queryable, int id)
			where T : class, IChildTest14
		{
			return queryable.Where(x => x.ChildID == id).FirstOrDefault();
		}

		#endregion

		[Test]
		public void Test14([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			{
				var q = db.GetTable<ChildTest14>().Select(c => new ChildTest14 { ChildID = c.ChildID });
				FindById(q, 10);
			}
		}

		[Test]
		public void Test15([NorthwindDataContext] string context)
		{
			using (var db = new NorthwindDB(context))
			{
				var dd = GetNorthwindAsList(context);

				var result   = db.DiscontinuedProduct.Select(p => p).ToList();
				var expected = dd.DiscontinuedProduct.Select(p => p).ToList();

				Assert.That(result.Count, Is.Not.EqualTo(0).And.EqualTo(expected.Count));
			}
		}

		[Test]
		public void Test16([NorthwindDataContext] string context)
		{
			using (var db = new NorthwindDB(context))
			{
				var dd = GetNorthwindAsList(context);

				var result   = db.DiscontinuedProduct.ToList();
				var expected = dd.DiscontinuedProduct.ToList();

				Assert.That(result.Count, Is.Not.EqualTo(0).And.EqualTo(expected.Count));
			}
		}

		public enum TypeCodeEnum
		{
			Base,
			A,
			A1,
			A2,
		}

		[Table(Name="LinqDataTypes")]
		public abstract class InheritanceBase
		{
			[Column] public Guid GuidValue { get; set; }

			[Column("ID")]
			public virtual TypeCodeEnum TypeCode => TypeCodeEnum.Base;
		}

		[InheritanceMapping(Code = TypeCodeEnum.A1, Type = typeof(InheritanceA1), IsDefault = false)]
		[InheritanceMapping(Code = TypeCodeEnum.A2, Type = typeof(InheritanceA2), IsDefault = true)]
		public abstract class InheritanceA : InheritanceBase
		{
			[Association(CanBeNull = true, ThisKey = "GuidValue", OtherKey = "GuidValue")]
			public List<InheritanceB> Bs { get; set; } = null!;

			[Column("ID", IsDiscriminator = true)]
			public override TypeCodeEnum TypeCode => TypeCodeEnum.A;
		}

		public class InheritanceA1 : InheritanceA
		{
			[Column("ID", IsDiscriminator = true)]
			public override TypeCodeEnum TypeCode => TypeCodeEnum.A1;
		}

		public class InheritanceA2 : InheritanceA
		{
			[Column("ID", IsDiscriminator = true)]
			public override TypeCodeEnum TypeCode => TypeCodeEnum.A2;
		}

		public class InheritanceB : InheritanceBase
		{
		}

		[Table(Name="LinqDataTypes")]
		public class InheritanceAssociation
		{
			[Column] public Guid GuidValue { get; set; }

			[Association(CanBeNull = true, ThisKey = "GuidValue", OtherKey = "GuidValue")]
			public InheritanceA1? A1 { get; set; }

			[Association(CanBeNull = true, ThisKey = "GuidValue", OtherKey = "GuidValue")]
			public InheritanceA2? A2 { get; set; }
		}

		[Test]
		public void GuidTest()
		{
			using (var db = new DataConnection())
			{
				var list = db.GetTable<InheritanceA>().Where(a => a.Bs.Any()).ToList();
			}
		}

		[Test]
		public void QuerySyntaxSimpleTest([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			{
				// db.GetTable<Parent111>().OfType<Parent222>().ToList(); - it's work!!!
				(from p in db.GetTable<Parent111>().OfType<Parent222>() select p).ToList();
			}
		}

		[Table("Person")]
		[InheritanceMapping(Code = 1, Type = typeof(Test17John))]
		[InheritanceMapping(Code = 2, Type = typeof(Test17Tester))]
		public class Test17Person
		{
			[Column(IsDiscriminator = true)]
			public int PersonID { get; set; }
		}

		public class Test17John : Test17Person
		{
			public string FirstName { get; set; } = null!;
		}

		public class Test17Tester : Test17Person
		{
			public string LastName { get; set; } = null!;
		}

		[Test]
		public void Test17([DataSources(false)] string data)
		{
			using (var context = GetDataContext(data))
			{
				var db = (TestDataConnection)context;
				db.GetTable<Test17Person>().OfType<Test17John>().ToList();
				Assert.False(db.LastQuery!.ToLowerInvariant().Contains("lastname"), "Why select LastName field??");
			}
		}

		[Table(Name="Person")]
		[InheritanceMapping(Code = Gender.Male,   Type = typeof(Test18Male))]
		[InheritanceMapping(Code = Gender.Female, Type = typeof(Test18Female))]
		public class Test18Person
		{
			[PrimaryKey, Identity, SequenceName("PERSONID")] public int    PersonID { get; set; }
			[Column(IsDiscriminator = true)]                 public Gender Gender   { get; set; }
		}

		public class Test18Male : Test18Person
		{
			[Column] public string FirstName { get; set; } = null!;
		}

		public class Test18Female : Test18Person
		{
			[Column] public string FirstName { get; set; } = null!;
			[Column] public string LastName  { get; set; } = null!;
		}

		[Test]
		public void Test18([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			{
				var ids = Enumerable.Range(0, 10).ToList();
				var q   =
					from p1 in db.GetTable<Test18Person>()
					where ids.Contains(p1.PersonID)
					join p2 in db.GetTable<Test18Person>() on p1.PersonID equals p2.PersonID
					select p1;

				var list = q.Distinct().OfType<Test18Female>().ToList();
			}
		}

		[Test]
		public async Task Test18Async([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			{
				var ids = Enumerable.Range(0, 10).ToList();
				var q   =
					from p1 in db.GetTable<Test18Person>()
					where ids.Contains(p1.PersonID)
					join p2 in db.GetTable<Test18Person>() on p1.PersonID equals p2.PersonID
					select p1;

				var list = await q.Distinct().OfType<Test18Female>().ToListAsync();
			}
		}

		[Test]
		public void Test19([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			{
				var ids = Enumerable.Range(0, 10).ToList();
				var q   =
					from p1 in db.GetTable<Test18Person>()
					where ids.Contains(p1.PersonID)
					join p2 in db.GetTable<Test18Person>() on p1.PersonID equals p2.PersonID
					select p1;

				IQueryable iq   = q.Distinct();
				var        list = iq.OfType<Test18Female>().ToList();
			}
		}

		[ActiveIssue("https://github.com/ClickHouse/ClickHouse/issues/37999", Configuration = ProviderName.ClickHouseMySql)]
		[Test]
		public void InheritanceAssociationTest([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			{
				var result = db.GetTable<InheritanceAssociation>().Select(ia =>
					new
					{
						TC1 = ia.A1!.TypeCode,
						TC2 = ia.A2!.TypeCode
					});

				var items = db.GetTable<LinqDataTypes>().ToList();
				var expected = items.Select(ia =>
					new
					{
						TC1 = items.Where(i => i.ID == ia.ID).Select(i => (TypeCodeEnum)i.ID).FirstOrDefault(i => i == TypeCodeEnum.A1),
						TC2 = items.Where(i => i.ID == ia.ID).Select(i => (TypeCodeEnum)i.ID).FirstOrDefault(i => i != TypeCodeEnum.A1)
					});

				AreEqual(expected, result);
			}
		}

		#region issue 2429

		public abstract class Root
		{
			public abstract int Value { get; set; }
			public abstract int GetValue();
		}

		[Table]
		public class BaseTable : Root
		{
			[PrimaryKey, NotNull  ] public int Id { get; set; }
			[Column(nameof(Value))] public int BaseValue { get; set; }

			private static Expression<Func<BaseTable, int>> GeValueImpl() => e => e.BaseValue;

			[ExpressionMethod(nameof(GeValueImpl), IsColumn = true)]
			public override int Value { get => BaseValue; set => BaseValue = value; }

			[ExpressionMethod(nameof(GeValueImpl))]
			public override int GetValue() => BaseValue;

			public static readonly BaseTable[] Data = new []
			{
				new BaseTable() { Id = 1, Value = 100 }
			};
		}

		[Table]
		public class BaseTable2
		{
			[PrimaryKey, NotNull] public         int Id { get; set; }
			[Column             ] public virtual int Value { get; set; }

			[ExpressionMethod(nameof(GetBaseTableOverrideImpl), IsColumn = true)]
			public virtual int GetValue() => Value;

			private static Expression<Func<BaseTable2, int>> GetBaseTableOverrideImpl() => e => e.Value;

			public static readonly BaseTable2[] Data = new []
			{
				new BaseTable2() { Id = 1, Value = 100 }
			};
		}

		public class DerivedTable2 : BaseTable2
		{
			private static Expression<Func<DerivedTable2, int>> GetDerivedTableOverrideImpl() => e => e.BaseValue * -1;

			[Column(nameof(Value))] public int BaseValue { get; set; }

			[ExpressionMethod(nameof(GetDerivedTableOverrideImpl), IsColumn = true)]
			public override int Value { get; set; }

			[ExpressionMethod(nameof(GetDerivedTableOverrideImpl))]
			public override int GetValue() => BaseValue * -1;
		}

		[Test]
		public void Issue2429PropertiesTest1([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			using (db.CreateLocalTable(BaseTable.Data))
			{
					var baseTableRecordById = db.GetTable<BaseTable>().FirstOrDefault(x => x.Id == 1);
					Assert.AreEqual(1, baseTableRecordById?.Id);
					Assert.AreEqual(100, baseTableRecordById?.Value);

					var baseTableRecordWithValuePredicate = db.GetTable<BaseTable>().FirstOrDefault(x => x.Id == 1 && x.Value == 100);
					Assert.AreEqual(1, baseTableRecordWithValuePredicate?.Id);
					Assert.AreEqual(100, baseTableRecordWithValuePredicate?.Value);
			}
		}

		[Test]
		public void Issue2429MethodsTest1([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			using (db.CreateLocalTable(BaseTable.Data))
			{
				var baseTableRecordById = db.GetTable<BaseTable>().FirstOrDefault(x => x.Id == 1);
				Assert.AreEqual(1, baseTableRecordById?.Id);
				Assert.AreEqual(100, baseTableRecordById?.Value);

				var baseTableRecordWithValuePredicate = db.GetTable<BaseTable>().FirstOrDefault(x => x.Id == 1 && x.GetValue() == 100);
				Assert.AreEqual(1, baseTableRecordWithValuePredicate?.Id);
				Assert.AreEqual(100, baseTableRecordWithValuePredicate?.Value);
			}
		}

		[ActiveIssue(Details = "Expression 'x.BaseValue' is not a Field. (Invalid mappings?)")]
		[Test]
		public void Issue2429PropertiesTest2([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			using (db.CreateLocalTable(BaseTable2.Data))
			{
				var baseTableRecord    = db.GetTable<BaseTable2>().FirstOrDefault(x => x.Id == 1 && x.Value == 100);
				//var derivedTableRecord = db.GetTable<DerivedTable2>().FirstOrDefault(x => x.Id == 1 && x.Value == (100 * -1 ));
				var derivedTableRecord = db.GetTable<BaseTable2>().OfType<DerivedTable2>().FirstOrDefault(x => x.Id == 1 && x.Value == (100 * -1 ));

				Assert.AreEqual(1, baseTableRecord?.Id);
				Assert.AreEqual(100, baseTableRecord?.Value);

				Assert.AreEqual(1, derivedTableRecord?.Id);
				Assert.AreEqual(100, derivedTableRecord?.Value * -1);
			}
		}

		[ActiveIssue(Details = "Expression 'x.BaseValue' is not a Field. (Invalid mappings?)")]
		[Test]
		public void Issue2429MethodsTest2([DataSources] string context)
		{
			using (var db = GetDataContext(context))
			using (db.CreateLocalTable(BaseTable2.Data))
			{
				var baseTableRecord    = db.GetTable<BaseTable2>().FirstOrDefault(x => x.Id == 1 && x.GetValue() == 100);
				//var derivedTableRecord = db.GetTable<DerivedTable2>().FirstOrDefault(x => x.Id == 1 && x.GetValue() == (100 * -1 ));
				var derivedTableRecord = db.GetTable<BaseTable2>().OfType<DerivedTable2>().FirstOrDefault(x => x.Id == 1 && x.GetValue() == (100 * -1 ));

				Assert.AreEqual(1, baseTableRecord?.Id);
				Assert.AreEqual(100, baseTableRecord?.Value);

				Assert.AreEqual(1, derivedTableRecord?.Id);
				Assert.AreEqual(100, derivedTableRecord?.Value * -1);
			}
		}
		#endregion

		#region issue4280

		[InheritanceMapping(Code = "DISPLAY", Type = typeof(Issue4280T1))]
		[InheritanceMapping(Code = "TV", Type = typeof(Issue4280T2))]
		[Table("Issue4280")]
		public abstract class Issue4280Base
		{
			[PrimaryKey                    ] public          int     Id           { get; set; }
			[Column                        ] public          string? SerialNumber { get; set; }
			[Column(IsDiscriminator = true)] public abstract string  DeviceType   { get; set; }
		}

		public class Issue4280T1 : Issue4280Base
		{
			public override string DeviceType { get; set; } = "DISPLAY";
		}

		public class Issue4280T2 : Issue4280Base
		{
			public override string DeviceType { get; set; } = "TV";

			[Column]
			public string? Location { get; set; }
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4280")]
		public void TestIssue4280AsBase([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4280Base>();

			var displayDevice = new Issue4280T1 { Id = 1, SerialNumber = "Disp00001" };
			var tvDevice      = new Issue4280T2 { Id = 2, SerialNumber = "TV00001", Location = "Something" };

			db.Insert<Issue4280Base>(tvDevice);
			db.Insert<Issue4280Base>(displayDevice);

			var data = tb.OrderBy(r => r.Id).ToArray();

			Assert.AreEqual(2          , data.Length);
			Assert.That(data[0]        , Is.InstanceOf<Issue4280T1>());
			Assert.That(data[1]        , Is.InstanceOf<Issue4280T2>());
			Assert.AreEqual("Disp00001", data[0].SerialNumber);
			Assert.AreEqual("TV00001"  , data[1].SerialNumber);
			Assert.AreEqual("Something", ((Issue4280T2)data[1]).Location);

			displayDevice.SerialNumber = "Disp00002";
			tvDevice.SerialNumber      = "TV00002";
			tvDevice.Location          = "Anything";

			db.Update<Issue4280Base>(tvDevice);
			db.Update<Issue4280Base>(displayDevice);

			data = tb.OrderBy(r => r.Id).ToArray();

			Assert.AreEqual(2          , data.Length);
			Assert.That(data[0]        , Is.InstanceOf<Issue4280T1>());
			Assert.That(data[1]        , Is.InstanceOf<Issue4280T2>());
			Assert.AreEqual("Disp00002", data[0].SerialNumber);
			Assert.AreEqual("TV00002"  , data[1].SerialNumber);
			Assert.AreEqual("Anything" , ((Issue4280T2)data[1]).Location);
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4280")]
		public void TestIssue4280AsIs([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4280Base>();

			var displayDevice = new Issue4280T1 { Id = 1, SerialNumber = "Disp00001" };
			var tvDevice      = new Issue4280T2 { Id = 2, SerialNumber = "TV00001", Location = "Something" };

			db.Insert(tvDevice);
			db.Insert(displayDevice);

			var data = tb.OrderBy(r => r.Id).ToArray();

			Assert.AreEqual(2          , data.Length);
			Assert.That(data[0]        , Is.InstanceOf<Issue4280T1>());
			Assert.That(data[1]        , Is.InstanceOf<Issue4280T2>());
			Assert.AreEqual("Disp00001", data[0].SerialNumber);
			Assert.AreEqual("TV00001"  , data[1].SerialNumber);
			Assert.AreEqual("Something", ((Issue4280T2)data[1]).Location);

			displayDevice.SerialNumber = "Disp00002";
			tvDevice.SerialNumber      = "TV00002";
			tvDevice.Location          = "Anything";

			db.Update(tvDevice);
			db.Update(displayDevice);

			data = tb.OrderBy(r => r.Id).ToArray();

			Assert.AreEqual(2          , data.Length);
			Assert.That(data[0]        , Is.InstanceOf<Issue4280T1>());
			Assert.That(data[1]        , Is.InstanceOf<Issue4280T2>());
			Assert.AreEqual("Disp00002", data[0].SerialNumber);
			Assert.AreEqual("TV00002"  , data[1].SerialNumber);
			Assert.AreEqual("Anything" , ((Issue4280T2)data[1]).Location);
		}
		#endregion

		#region issue 4460

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4460")]
		public void Issue4460Test_MustFindRecord([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4460Base>();

			db.Insert(new Issue4460GrandChild() { Id = 1, Name = "Tom", Surname = "Black" });

			var items = db.GetTable<Issue4460GrandChild>().ToList();

			Assert.That(items, Has.Count.EqualTo(1));
			Assert.That(items[0], Is.InstanceOf<Issue4460GrandChild>());
			var gc = (Issue4460GrandChild)items[0];
			Assert.That(gc.Name, Is.EqualTo("Tom"));
			Assert.That(gc.Surname, Is.EqualTo("Black"));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4460")]
		public void Issue4460Test_MustTypeResultProperly([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4460Base>();

			db.Insert(new Issue4460GrandChild() { Id = 1, Name = "Tom", Surname = "Black" });

			var items = db.GetTable<Issue4460Child>().ToList();

			Assert.That(items, Has.Count.EqualTo(1));
			Assert.That(items[0], Is.InstanceOf<Issue4460GrandChild>());
			var gc = (Issue4460GrandChild)items[0];
			Assert.That(gc.Name, Is.EqualTo("Tom"));
			Assert.That(gc.Surname, Is.EqualTo("Black"));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4460")]
		public void Issue4460Test_MustInsertAllFields([DataSources] string context, [Values] BulkCopyType copyType)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4460Base>();

			var items = new Issue4460Base[]
			{
				new Issue4460GrandChild() { Id = 1, Name = "Tom", Surname = "Black" }
			};

			tb.BulkCopy(new BulkCopyOptions() { BulkCopyType = copyType }, items);

			var res = tb.ToList();

			Assert.That(res, Has.Count.EqualTo(1));
			Assert.That(res[0], Is.InstanceOf<Issue4460GrandChild>());
			var gc = (Issue4460GrandChild)res[0];
			Assert.That(gc.Name, Is.EqualTo("Tom"));
			Assert.That(gc.Surname, Is.EqualTo("Black"));
		}

		[Table("Issue4460Table")]
		[InheritanceMapping(Code = "Child", IsDefault = true, Type = typeof(Issue4460Child))]
		[InheritanceMapping(Code = "GrandChild", Type = typeof(Issue4460GrandChild))]
		abstract class Issue4460Base
		{
			[Column] public int Id { get; set; }
			[Column(IsDiscriminator = true)] public string? Code { get; set; }
		}

		[Table]
		class Issue4460Child : Issue4460Base
		{
			[Column] public string? Name { get; set; }
		}

		abstract class Issue4460Abstract : Issue4460Child
		{
			[Column] public string? Surname { get; set; }
		}

		[Table("Issue4460Table")]
		class Issue4460GrandChild : Issue4460Abstract
		{ }

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4460")]
		public void Issue4460Test_PropertiesWithSameNameMapped([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4460TicketBase>();

			db.Insert(new Issue4460TicketChild() { Id = 1, Code = "Code1" });
			db.Insert(new Issue4460TicketChild2() { Id = 2, Code = "Code2", Price = 123 });

			var res = tb.OrderBy(r => r.Id).ToArray();

			Assert.That(res, Has.Length.EqualTo(2));

			Assert.That(res[0], Is.InstanceOf<Issue4460TicketChild>());
			var child = (Issue4460TicketChild)res[0];
			Assert.That(child.Code, Is.EqualTo("Code1"));

			Assert.That(res[1], Is.InstanceOf<Issue4460TicketChild2>());
			var child2 = (Issue4460TicketChild2)res[1];
			Assert.That(child2.Code, Is.EqualTo("Code2"));
			Assert.That(child2.Price, Is.EqualTo(123));
		}

		[Table("Tickets")]
		[InheritanceMapping(Code = "TicketChild", IsDefault = true, Type = typeof(Issue4460TicketChild))]
		[InheritanceMapping(Code = "TicketChild2", Type = typeof(Issue4460TicketChild2))]
		abstract class Issue4460TicketBase
		{
			[Column(IsDiscriminator = true)] public string? EventCode { get; set; }
			[Column] public int Id { get; set; }
		}

		class Issue4460TicketChild : Issue4460TicketBase
		{
			[Column("TicketChildCode")] public string? Code { get; set; }
		}

		class Issue4460TicketChild2 : Issue4460TicketBase
		{
			[Column("TicketChild2Code")] public string? Code { get; set; }
			[Column(CanBeNull = true)] public int Price { get; set; }
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4460")]
		public void Issue4460Test_CodeFilter([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4460_3_Base>();

			db.Insert(new Issue4460_3_Child() { Id = 1, Name = "Jane" });
			db.Insert(new Issue4460_3_Child2() { Id = 2, Age = 10 });

			var result = db.GetTable<Issue4460_3_Base>().Where(e => e.Code != "Child").ToArray();

			Assert.That(result, Has.Length.EqualTo(1));
			Assert.That(result[0], Is.InstanceOf<Issue4460_3_Child2>());
			var record = (Issue4460_3_Child2)result[0];
			Assert.That(record.Age, Is.EqualTo(10));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4460")]
		public void Issue4460Test_InterfaceFilter([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4460_3_Base>();

			db.Insert(new Issue4460_3_Child() { Id = 1, Name = "Jane" });
			db.Insert(new Issue4460_3_Child2() { Id = 2, Age = 10 });

			var result = db.GetTable<Issue4460_3_Base>().Where(e => !(e is IChild)).ToArray();

			Assert.That(result, Has.Length.EqualTo(1));
			Assert.That(result[0], Is.InstanceOf<Issue4460_3_Child2>());
			var record = (Issue4460_3_Child2)result[0];
			Assert.That(record.Age, Is.EqualTo(10));
		}

		[Table(Name = "Base")]
		[InheritanceMapping(Code = "Base", IsDefault = true, Type = typeof(Issue4460_3_Base))]
		[InheritanceMapping(Code = "Child", Type = typeof(Issue4460_3_Child))]
		[InheritanceMapping(Code = "Child2", Type = typeof(Issue4460_3_Child2))]
		class Issue4460_3_Base
		{
			[Column(IsDiscriminator = true)] public string? Code { get; set; }
			[Column] public int Id { get; set; }
		}

		interface IChild
		{
			string? Name { get; }
		}

		[Table(Name = "Base")]
		class Issue4460_3_Child : Issue4460_3_Base, IChild
		{
			[Column(CanBeNull = true)] public string? Name { get; set; }

			public Issue4460_3_Child()
			{
				Code = "Child";
			}
		}

		[Table(Name = "Base")]
		class Issue4460_3_Child2 : Issue4460_3_Base
		{
			[Column(CanBeNull = true)] public int Age { get; set; }

			public Issue4460_3_Child2()
			{
				Code = "Child2";
			}
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4460")]
		public void Issue4460Test_ConditionTranslation([DataSources] string context, [Values] bool additionalFlag)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4460_4_Base>();

			db.Insert(new Issue4460_4_Child() { Id = 1, Name = "Jane" });
			db.Insert(new Issue4460_4_Child2() { Id = 2, Age = 10 });

			var res = db.GetTable<Issue4460_4_Base>()
				.OrderBy(r => r.Id)
				.Where(e => e is Issue4460_4_BaseChild ? additionalFlag || e.Id != default : e.Id != default).ToArray();

			Assert.That(res, Has.Length.EqualTo(2));

			Assert.That(res[0], Is.InstanceOf<Issue4460_4_Child>());
			var child = (Issue4460_4_Child)res[0];
			Assert.That(child.Code, Is.EqualTo("Child"));
			Assert.That(child.Name, Is.EqualTo("Jane"));

			Assert.That(res[1], Is.InstanceOf<Issue4460_4_Child2>());
			var child2 = (Issue4460_4_Child2)res[1];
			Assert.That(child2.Code, Is.EqualTo("Child2"));
			Assert.That(child2.Age, Is.EqualTo(10));
		}

		[Table(Name = "Base")]
		[InheritanceMapping(Code = "Base", IsDefault = true, Type = typeof(Issue4460_4_Base))]
		[InheritanceMapping(Code = "BaseChild", Type = typeof(Issue4460_4_BaseChild))]
		[InheritanceMapping(Code = "Child", Type = typeof(Issue4460_4_Child))]
		[InheritanceMapping(Code = "Child2", Type = typeof(Issue4460_4_Child2))]
		class Issue4460_4_Base
		{
			[Column(IsDiscriminator = true)] public string? Code { get; set; }
			[Column] public int Id { get; set; }
		}

		[Table(Name = "Base")]
		class Issue4460_4_BaseChild : Issue4460_4_Base
		{
			[Column(CanBeNull = true)] public string? Name { get; set; }

			public Issue4460_4_BaseChild()
			{
				Code = "BaseChild";
			}
		}

		[Table(Name = "Base")]
		class Issue4460_4_Child : Issue4460_4_BaseChild
		{
			[Column(CanBeNull = true)] public bool IsMale { get; set; }

			public Issue4460_4_Child()
			{
				Code = "Child";
			}
		}

		[Table(Name = "Base")]
		class Issue4460_4_Child2 : Issue4460_4_BaseChild
		{
			[Column(CanBeNull = true)] public int Age { get; set; }

			public Issue4460_4_Child2()
			{
				Code = "Child2";
			}
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4460")]
		public void Issue4460Test_ScalarProjection([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4460_5_Base>();

			db.Insert(new Issue4460_5_Child() { Id = 1, Name = "Jane" });
			db.Insert(new Issue4460_5_Child2() { Id = 2, Age = 10 });

			var res = db.GetTable<Issue4460_5_Base>().Where(e => (e.Code == "Child") || (e.Code == "Child2"))
					.Cast<Issue4460_5_BaseChild>().Select(x => x.Name).ToArray();

			Assert.That(res, Has.Length.EqualTo(2));

			Assert.That(res[0], Is.EqualTo("Jane"));
			Assert.That(res[0], Is.Null);
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4460")]
		public void Issue4460Test_ObjectProjection([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<Issue4460_5_Base>();

			db.Insert(new Issue4460_5_Child() { Id = 1, Name = "Jane" });
			db.Insert(new Issue4460_5_Child2() { Id = 2, Age = 10 });

			var res = db.GetTable<Issue4460_5_Base>().Where(e => (e.Code == "Child") || (e.Code == "Child2"))
					.Cast<Issue4460_5_BaseChild>().Select(x => new { x.Name }).ToArray();

			Assert.That(res, Has.Length.EqualTo(2));

			Assert.That(res[0].Name, Is.EqualTo("Jane"));
			Assert.That(res[0].Name, Is.Null);
		}

		[Table(Name = "Base")]
		[InheritanceMapping(Code = "Child", IsDefault = true, Type = typeof(Issue4460_5_Child))]
		[InheritanceMapping(Code = "GrandChild", Type = typeof(Issue4460_5_Child2))]
		public class Issue4460_5_Base
		{
			[Column(IsDiscriminator = true)] public string? Code { get; set; }
			[Column] public int Id { get; set; }
		}

		[Table(Name = "Base")]
		public class Issue4460_5_BaseChild : Issue4460_5_Base
		{
			[Column(CanBeNull = true)] public string? Name { get; set; }

			public Issue4460_5_BaseChild()
			{
				Code = "BaseChild";
			}
		}

		[Table(Name = "Base")]
		public class Issue4460_5_Child : Issue4460_5_BaseChild
		{
			[Column(CanBeNull = true)] public bool IsMale { get; set; }

			public Issue4460_5_Child()
			{
				Code = "Child";
			}
		}

		[Table(Name = "Base")]
		public class Issue4460_5_Child2 : Issue4460_5_BaseChild
		{
			[Column(CanBeNull = true)] public int Age { get; set; }

			public Issue4460_5_Child2()
			{
				Code = "Child2";
			}
		}
		#endregion

		#region Issue 4364
		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test1([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			var result = db.GetTable<Issue4364_IntermediateThing>().OrderBy(r => r.Id).ToArray();

			AssertIssue4364(result);
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test2([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			var result = db.GetTable<Issue4364_BaseThing>().OrderBy(r => r.Id).ToArray();

			Assert.That(result, Has.Length.EqualTo(4));

			Assert.That(result[0], Is.InstanceOf<Issue4364_ConcreteBaseThingAlpha>());
			var item1 = (Issue4364_ConcreteBaseThingAlpha)(object)result[0]!;
			Assert.That(item1.Id, Is.EqualTo(1));
			Assert.That(item1.Type, Is.EqualTo(1));
			Assert.That(item1.BaseField, Is.EqualTo(2));

			Assert.That(result[1], Is.InstanceOf<Issue4364_ConcreteBaseThingBeta>());
			var item2 = (Issue4364_ConcreteBaseThingBeta)(object)result[1]!;
			Assert.That(item2.Id, Is.EqualTo(2));
			Assert.That(item2.Type, Is.EqualTo(2));
			Assert.That(item2.BaseField, Is.EqualTo(3));
			Assert.That(item2.ConcreteField, Is.EqualTo(4));

			Assert.That(result[2], Is.InstanceOf<Issue4364_ConcreteIntermediateThingOne>());
			var item3 = (Issue4364_ConcreteIntermediateThingOne)(object)result[2]!;
			Assert.That(item3.Id, Is.EqualTo(3));
			Assert.That(item3.Type, Is.EqualTo(101));
			Assert.That(item3.BaseField, Is.EqualTo(4));
			Assert.That(item3.ConcreteField, Is.EqualTo(5));
			Assert.That(item3.IntermediateField, Is.EqualTo(6));

			Assert.That(result[3], Is.InstanceOf<Issue4364_ConcreteIntermediateThingTwo>());
			var item4 = (Issue4364_ConcreteIntermediateThingTwo)(object)result[3]!;
			Assert.That(item4.Id, Is.EqualTo(4));
			Assert.That(item4.Type, Is.EqualTo(102));
			Assert.That(item3.BaseField, Is.EqualTo(5));
			Assert.That(item4.IntermediateField, Is.EqualTo(6));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test3([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			var result = db.GetTable<Issue4364_BaseThing>().OfType<Issue4364_IntermediateThing>().OrderBy(r => r.Id).ToArray();

			AssertIssue4364(result);
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test4([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			var result = db.GetTable<Issue4364_BaseThing>().Where(x => x.Type == 101 || x.Type == 102).Cast<Issue4364_IntermediateThing>().OrderBy(r => r.Id).ToArray();

			AssertIssue4364(result);
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test5([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			var result = db.GetTable<Issue4364_BaseThing>().Where(x => x.Type == 101 || x.Type == 102).Select(x => (Issue4364_IntermediateThing)x).OrderBy(r => r.Id).ToArray();

			AssertIssue4364(result);
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test6([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			var result = db.GetTable<Issue4364_BaseThing>().Where(x => x.Type == 101 || x.Type == 102).OrderBy(r => r.Id).ToArray();

			AssertIssue4364(result);
		}

		static void TestUpdateAndFind(IQueryable<Issue4364_IntermediateThing> table)
		{
			var query = table.Where(x => x.Id == 3);

			query.Set(y => y.IntermediateField, 333).Update();

			var item = query.Single();

			Assert.That(item, Is.InstanceOf<Issue4364_ConcreteIntermediateThingOne>());

			var item3 = (Issue4364_ConcreteIntermediateThingOne)item;
			Assert.That(item3.Id, Is.EqualTo(3));
			Assert.That(item3.Type, Is.EqualTo(101));
			Assert.That(item3.BaseField, Is.EqualTo(4));
			Assert.That(item3.ConcreteField, Is.EqualTo(5));
			Assert.That(item3.IntermediateField, Is.EqualTo(333));
		}

		static void TestJoinedAll(IDataContext db, IQueryable<Issue4364_BaseThing> table)
		{
			var result =
				(
					from b in table
					join i in db.GetTable<Issue4364_Interaction>() on b.Id equals i.ThingId
					join p in db.GetTable<Issue4364_Person>() on i.PersonId equals p.Id
					orderby b.Id
					select new
					{
						b.Type,
						p.FullName
					}
				)
				.ToArray();

			Assert.That(result, Has.Length.EqualTo(4));

			Assert.That(result[0].Type, Is.EqualTo(1));
			Assert.That(result[0].FullName, Is.EqualTo("Person 4"));

			Assert.That(result[1].Type, Is.EqualTo(2));
			Assert.That(result[1].FullName, Is.EqualTo("Person 1"));

			Assert.That(result[2].Type, Is.EqualTo(101));
			Assert.That(result[2].FullName, Is.EqualTo("Person 2"));

			Assert.That(result[3].Type, Is.EqualTo(102));
			Assert.That(result[3].FullName, Is.EqualTo("Person 3"));
		}

		static void TestJoined<T>(IDataContext db, IQueryable<T> table)
			where T: Issue4364_BaseThing
		{
			var result =
				(
					from b in table
					join i in db.GetTable<Issue4364_Interaction>() on b.Id equals i.ThingId
					join p in db.GetTable<Issue4364_Person>() on i.PersonId equals p.Id
					orderby b.Id
					select new
					{
						b.Type,
						p.FullName
					}
				)
				.ToArray();

			Assert.That(result, Has.Length.EqualTo(2));

			Assert.That(result[0].Type, Is.EqualTo(101));
			Assert.That(result[0].FullName, Is.EqualTo("Person 2"));

			Assert.That(result[1].Type, Is.EqualTo(102));
			Assert.That(result[1].FullName, Is.EqualTo("Person 3"));
		}

		static void TestUpdateAndFindBase(IQueryable<Issue4364_BaseThing> table)
		{
			var query = table.Where(x => x.Id == 3);

			query.Set(y => ((Issue4364_IntermediateThing)y).IntermediateField, 333).Update();

			var item = query.Single();

			Assert.That(item, Is.InstanceOf<Issue4364_ConcreteIntermediateThingOne>());

			var item3 = (Issue4364_ConcreteIntermediateThingOne)item;
			Assert.That(item3.Id, Is.EqualTo(3));
			Assert.That(item3.Type, Is.EqualTo(101));
			Assert.That(item3.BaseField, Is.EqualTo(4));
			Assert.That(item3.ConcreteField, Is.EqualTo(5));
			Assert.That(item3.IntermediateField, Is.EqualTo(333));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test11([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			TestUpdateAndFindBase(db.GetTable<Issue4364_BaseThing>());
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test12([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);
			using var tp = db.CreateLocalTable(Issue4364_Person.TestData);
			using var ti = db.CreateLocalTable(Issue4364_Interaction.TestData);

			TestJoinedAll(db, db.GetTable<Issue4364_BaseThing>());
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test21([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			TestUpdateAndFind(db.GetTable<Issue4364_IntermediateThing>());
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test22([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);
			using var tp = db.CreateLocalTable(Issue4364_Person.TestData);
			using var ti = db.CreateLocalTable(Issue4364_Interaction.TestData);

			TestJoined(db, db.GetTable<Issue4364_IntermediateThing>());
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test31([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			TestUpdateAndFind(db.GetTable<Issue4364_BaseThing>().OfType<Issue4364_IntermediateThing>());
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test32([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);
			using var tp = db.CreateLocalTable(Issue4364_Person.TestData);
			using var ti = db.CreateLocalTable(Issue4364_Interaction.TestData);

			TestJoined(db, db.GetTable<Issue4364_BaseThing>().OfType<Issue4364_IntermediateThing>());
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test41([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			TestUpdateAndFind(db.GetTable<Issue4364_BaseThing>().Where(x => x.Type == 101 || x.Type == 102).Cast<Issue4364_IntermediateThing>());
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test42([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);
			using var tp = db.CreateLocalTable(Issue4364_Person.TestData);
			using var ti = db.CreateLocalTable(Issue4364_Interaction.TestData);

			TestJoined(db, db.GetTable<Issue4364_BaseThing>().Where(x => x.Type == 101 || x.Type == 102).Cast<Issue4364_IntermediateThing>());
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test51([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			TestUpdateAndFindBase(db.GetTable<Issue4364_BaseThing>().Where(x => x.Type == 101 || x.Type == 102));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test52([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);
			using var tp = db.CreateLocalTable(Issue4364_Person.TestData);
			using var ti = db.CreateLocalTable(Issue4364_Interaction.TestData);

			TestJoined(db, db.GetTable<Issue4364_BaseThing>().Where(x => x.Type == 101 || x.Type == 102));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test61([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);

			TestUpdateAndFind(db.GetTable<Issue4364_BaseThing>().Where(x => x.Type == 101 || x.Type == 102).Select(x => (Issue4364_IntermediateThing)x));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4364")]
		public void Issue4364Test62([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable(Issue4364_BaseThing.TestData);
			using var tp = db.CreateLocalTable(Issue4364_Person.TestData);
			using var ti = db.CreateLocalTable(Issue4364_Interaction.TestData);

			TestJoined(db, db.GetTable<Issue4364_BaseThing>().Where(x => x.Type == 101 || x.Type == 102).Select(x => (Issue4364_IntermediateThing)x));
		}

		static void AssertIssue4364<T>(T[] result)
		{
			Assert.That(result, Has.Length.EqualTo(2));

			Assert.That(result[0], Is.InstanceOf<Issue4364_ConcreteIntermediateThingOne>());
			var item3 = (Issue4364_ConcreteIntermediateThingOne)(object)result[0]!;
			Assert.That(item3.Id, Is.EqualTo(3));
			Assert.That(item3.Type, Is.EqualTo(101));
			Assert.That(item3.BaseField, Is.EqualTo(4));
			Assert.That(item3.ConcreteField, Is.EqualTo(5));
			Assert.That(item3.IntermediateField, Is.EqualTo(6));

			Assert.That(result[1], Is.InstanceOf<Issue4364_ConcreteIntermediateThingTwo>());
			var item4 = (Issue4364_ConcreteIntermediateThingTwo)(object)result[1]!;
			Assert.That(item4.Id, Is.EqualTo(4));
			Assert.That(item4.Type, Is.EqualTo(102));
			Assert.That(item3.BaseField, Is.EqualTo(5));
			Assert.That(item4.IntermediateField, Is.EqualTo(6));
		}

		[Table(IsColumnAttributeRequired = false)]
		[InheritanceMapping(Code = 1, Type = typeof(Issue4364_ConcreteBaseThingAlpha))]
		[InheritanceMapping(Code = 2, Type = typeof(Issue4364_ConcreteBaseThingBeta))]
		[InheritanceMapping(Code = 101, Type = typeof(Issue4364_ConcreteIntermediateThingOne))]
		[InheritanceMapping(Code = 102, Type = typeof(Issue4364_ConcreteIntermediateThingTwo))]
		abstract class Issue4364_BaseThing
		{
			[PrimaryKey] public int Id { get; set; }
			[Column(IsDiscriminator = true)] public int Type { get; set; }
			public int BaseField { get; set; }

			public static readonly Issue4364_BaseThing[] TestData = new Issue4364_BaseThing[]
			{
				new Issue4364_ConcreteBaseThingAlpha()
				{
					Id = 1,
					Type = 1,
					BaseField = 2
				},
				new Issue4364_ConcreteBaseThingBeta()
				{
					Id = 2,
					Type = 2,
					BaseField = 3,
					ConcreteField = 4
				},
				new Issue4364_ConcreteIntermediateThingOne()
				{
					Id = 3,
					Type = 101,
					BaseField = 4,
					ConcreteField = 5,
					IntermediateField = 6
				},
				new Issue4364_ConcreteIntermediateThingTwo()
				{
					Id = 4,
					Type = 102,
					BaseField = 5,
					IntermediateField = 6
				}
			};
		}

		class Issue4364_ConcreteBaseThingAlpha : Issue4364_BaseThing
		{
		}

		class Issue4364_ConcreteBaseThingBeta : Issue4364_BaseThing
		{
			// TODO: remove when fixed, nullable added due to Issue4364Test_CreateTableWithNullableRequiredFields
			[Nullable] public int ConcreteField { get; set; }
		}

		abstract class Issue4364_IntermediateThing : Issue4364_BaseThing
		{
			// TODO: remove when fixed, nullable added due to Issue4364Test_CreateTableWithNullableRequiredFields
			[Nullable] public int IntermediateField { get; set; }
		}

		class Issue4364_ConcreteIntermediateThingOne : Issue4364_IntermediateThing
		{
			// TODO: remove when fixed, nullable added due to Issue4364Test_CreateTableWithNullableRequiredFields
			[Nullable] public int ConcreteField { get; set; }
		}

		class Issue4364_ConcreteIntermediateThingTwo : Issue4364_IntermediateThing
		{
		}

		[Table(IsColumnAttributeRequired = false)]
		class Issue4364_Person
		{
			[PrimaryKey] public int Id { get; set; }
			[NotNull] public string FullName { get; set; } = null!;

			public static readonly Issue4364_Person[] TestData = new[]
			{
				new Issue4364_Person() { Id = 1, FullName = "Person 1" },
				new Issue4364_Person() { Id = 2, FullName = "Person 2" },
				new Issue4364_Person() { Id = 3, FullName = "Person 3" },
				new Issue4364_Person() { Id = 4, FullName = "Person 4" },
				new Issue4364_Person() { Id = 5, FullName = "Person 5" },
			};
		}

		[Table(IsColumnAttributeRequired = false)]
		class Issue4364_Interaction
		{
			[PrimaryKey] public int Id { get; set; }
			public int PersonId { get; set; }
			public int ThingId { get; set; }

			public static readonly Issue4364_Interaction[] TestData = new[]
			{
				new Issue4364_Interaction() { Id = 1, PersonId = 2, ThingId = 3 },
				new Issue4364_Interaction() { Id = 2, PersonId = 3, ThingId = 4 },
				new Issue4364_Interaction() { Id = 3, PersonId = 4, ThingId = 1 },
				new Issue4364_Interaction() { Id = 4, PersonId = 1, ThingId = 2 },
			};
		}
		#endregion

		#region Issue 4364 extra tests
		[Test]
		public void Issue4364Test_InsertByConcreteType([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<CreateTableBase>();

			db.Insert(new CreateTable1() { Id = 1, Type = 1, Field1 = 1 });
			db.Insert(new CreateTable2() { Id = 2, Type = 2, Field2 = 2 });

			var result = tb.OrderBy(r => r.Id).ToArray();

			Assert.That(result, Has.Length.EqualTo(1));
			Assert.That(result[0], Is.InstanceOf<CreateTable1>());
			Assert.That(result[0].Id, Is.EqualTo(1));
			Assert.That(result[0].Type, Is.EqualTo(1));
			Assert.That(((CreateTable1)result[0]).Field1, Is.EqualTo(1));


			Assert.That(result[1], Is.InstanceOf<CreateTable2>());
			Assert.That(result[1].Id, Is.EqualTo(2));
			Assert.That(result[1].Type, Is.EqualTo(2));
			Assert.That(((CreateTable2)result[1]).Field2, Is.EqualTo(2));
		}

		[Test]
		public void Issue4364Test_CreateTableWithNullableRequiredFields([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var tb = db.CreateLocalTable<CreateTableBase>();

			tb.Insert(() => new CreateTable1() { Id = 1, Type = 1, Field1 = 1});
			tb.Insert(() => new CreateTable2() { Id = 2, Type = 2, Field2 = 2});

			// TODO: those asserts could be incorrect, depends on our fixed model
			var column = db.MappingSchema.GetEntityDescriptor(typeof(CreateTable1)).Columns.Single(c => c.ColumnName == "Field1");
			Assert.That(column.CanBeNull, Is.False);
			column = db.MappingSchema.GetEntityDescriptor(typeof(CreateTable1)).Columns.Single(c => c.ColumnName == "Field2");
			Assert.That(column.CanBeNull, Is.False);
		}

		[Table]
		[InheritanceMapping(Code = 1, Type = typeof(CreateTable1))]
		[InheritanceMapping(Code = 2, Type = typeof(CreateTable2))]
		abstract class CreateTableBase
		{
			[Column] public int Id { get; set; }
			[Column(IsDiscriminator = true)]  public int Type { get; set; }
		}

		[Table]
		sealed class CreateTable1 : CreateTableBase
		{
			[Column] public int Field1 { get; set; }
		}

		[Table]
		sealed class CreateTable2 : CreateTableBase
		{
			[Column] public int Field2 { get; set; }
		}
		#endregion
	}
}
