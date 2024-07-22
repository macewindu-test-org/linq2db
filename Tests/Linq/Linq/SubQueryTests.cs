﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using LinqToDB;

using NUnit.Framework;

namespace Tests.Linq
{
	using LinqToDB.Linq;
	using LinqToDB.Mapping;
	using Model;

	[TestFixture]
	public class SubQueryTests : TestBase
	{
		[Test]
		public void Test1([DataSources(TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in Parent
					where p.ParentID != 5
					select (from ch in Child where ch.ParentID == p.ParentID select ch.ChildID).Max(),
					from p in db.Parent
					where p.ParentID != 5
					select (from ch in db.Child where ch.ParentID == p.ParentID select ch.ChildID).Max());
		}

		[Test]
		public void Test2([DataSources(TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in Parent
					where p.ParentID != 5
					select (from ch in Child where ch.ParentID == p.ParentID && ch.ChildID > 1 select ch.ChildID).Max(),
					from p in db.Parent
					where p.ParentID != 5
					select (from ch in db.Child where ch.ParentID == p.ParentID && ch.ChildID > 1 select ch.ChildID).Max());
		}

		[Test]
		public void Test3([DataSources(TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in Parent
					where p.ParentID != 5
					select (from ch in Child where ch.ParentID == p.ParentID && ch.ChildID == ch.ParentID * 10 + 1 select ch.ChildID).SingleOrDefault()
					,
					from p in db.Parent
					where p.ParentID != 5
					select (from ch in db.Child where ch.ParentID == p.ParentID && ch.ChildID == ch.ParentID * 10 + 1 select ch.ChildID).SingleOrDefault());
		}

		[Test]
		public void Test4([DataSources(TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in Parent
					where p.ParentID != 5
					select (from ch in Child where ch.ParentID == p.ParentID && ch.ChildID == ch.ParentID * 10 + 1 select ch.ChildID).FirstOrDefault(),
					from p in db.Parent
					where p.ParentID != 5
					select (from ch in db.Child where ch.ParentID == p.ParentID && ch.ChildID == ch.ParentID * 10 + 1 select ch.ChildID).FirstOrDefault());
		}

		static int _testValue = 3;

		[Test]
		public void Test5([DataSources(TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
			{
				IEnumerable<int> ids = new[] { 1, 2 };

				var eids = Parent
					.Where(p => ids.Contains(p.ParentID))
					.Select(p => p.Value1 == null ? p.ParentID : p.ParentID + 1)
					.Distinct();

				var expected = eids.Select(id =>
					new
					{
						id,
						Count1 = Child.Where(p => p.ParentID == id).Count(),
						Count2 = Child.Where(p => p.ParentID == id && p.ParentID == _testValue).Count(),
					});

				var rids   = db.Parent
					.Where(p => ids.Contains(p.ParentID))
					.Select(p => p.Value1 == null ? p.ParentID : p.ParentID + 1)
					.Distinct();

				var result = rids.Select(id =>
					new
					{
						id,
						Count1 = db.Child.Where(p => p.ParentID == id).Count(),
						Count2 = db.Child.Where(p => p.ParentID == id && p.ParentID == _testValue).Count(),
					});

				AreEqual(expected, result);
			}
		}

		[Test]
		public void Test6([DataSources(TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
			{
				var id = 2;
				var b  = false;

				var q = Child.Where(c => c.ParentID == id).OrderBy(c => c.ChildID);
				q = b
					? q.OrderBy(m => m.ParentID)
					: q.OrderByDescending(m => m.ParentID);

				var gc = GrandChild;
				var expected = q.Select(c => new
				{
					ID     = c.ChildID,
					c.ParentID,
					Sum    = gc.Where(g => g.ChildID == c.ChildID && g.GrandChildID > 0).Sum(g => (int)g.ChildID! * g.GrandChildID),
					Count1 = gc.Count(g => g.ChildID == c.ChildID && g.GrandChildID > 0)
				});

				var r = db.Child.Where(c => c.ParentID == id).OrderBy(c => c.ChildID);
				r = b
					? r.OrderBy(m => m.ParentID)
					: r.OrderByDescending(m => m.ParentID);

				var rgc = db.GrandChild;
				var result = r.Select(c => new
				{
					ID     = c.ChildID,
					c.ParentID,
					Sum    = rgc.Where(g => g.ChildID == c.ChildID && g.GrandChildID > 0).Sum(g => (int)g.ChildID! * g.GrandChildID),
					Count1 = rgc.Count(g => g.ChildID == c.ChildID && g.GrandChildID > 0),
				});

				AreEqual(expected, result);
			}
		}

		[Test]
		public void Test7([DataSources(TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from c in    Child select new { Count =    GrandChild.Where(g => g.ChildID == c.ChildID).Count() },
					from c in db.Child select new { Count = db.GrandChild.Where(g => g.ChildID == c.ChildID).Count() });
		}

		[Test]
		public void Test8([DataSources(TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
			{
				var parent  =
					from p in db.Parent
					where p.ParentID == 1
					select p.ParentID;

				var chilren =
					from c in db.Child
					where parent.Contains(c.ParentID)
					select c;

				var chs1 = chilren.ToList();

				parent  =
					from p in db.Parent
					where p.ParentID == 2
					select p.ParentID;

				chilren =
					from c in db.Child
					where parent.Contains(c.ParentID)
					select c;

				var chs2 = chilren.ToList();

				Assert.AreEqual(chs2.Count, chs2.Except(chs1).Count());
			}
		}

		[Test]
		public void ObjectCompare([DataSources(ProviderName.Access)] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in Parent
					from c in
						from c in
							from c in Child select new Child { ParentID = c.ParentID, ChildID = c.ChildID + 1, Parent = c.Parent }
						where c.ChildID > 0
						select c
					where p == c.Parent
					select new { p.ParentID, c.ChildID },
					from p in db.Parent
					from c in
						from c in
							from c in db.Child select new Child { ParentID = c.ParentID, ChildID = c.ChildID + 1, Parent = c.Parent }
						where c.ChildID > 0
						select c
					where p == c.Parent
					select new { p.ParentID, c.ChildID });
		}

		[Test]
		public void Contains1([DataSources(
			TestProvName.AllInformix,
			TestProvName.AllClickHouse,
			TestProvName.AllSybase,
			TestProvName.AllSapHana,
			TestProvName.AllAccess,
			TestProvName.AllOracle,
			TestProvName.AllMySql,
			ProviderName.DB2)]
			string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in Parent
					where (from p1 in Parent where p1.Value1 == p.Value1 select p.ParentID).Take(3).Contains(p.ParentID)
					select p,
					from p in db.Parent
					where (from p1 in db.Parent where p1.Value1 == p.Value1 select p.ParentID).Take(3).Contains(p.ParentID)
					select p);
		}

		[Test]
		public void Contains2([DataSources(
			TestProvName.AllClickHouse,
			TestProvName.AllMySql,
			TestProvName.AllSybase,
			TestProvName.AllSapHana,
			TestProvName.AllAccess,
			TestProvName.AllOracle,
			ProviderName.DB2)]
			string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in Parent
					where (from p1 in Parent where p1.Value1 == p.Value1 select p1.ParentID).Take(3).Contains(p.ParentID)
					select p,
					from p in db.Parent
					where (from p1 in db.Parent where p1.Value1 == p.Value1 select p1.ParentID).Take(3).Contains(p.ParentID)
					select p);
		}

		[Test]
		public void SubSub1([DataSources(
			TestProvName.AllClickHouse,
			ProviderName.SqlCe, ProviderName.Access, ProviderName.DB2,
			TestProvName.AllOracle)]
			string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p1 in
						from p2 in Parent
						select new { p2, ID = p2.ParentID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.Children
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).Count()
					},
					from p1 in
						from p2 in db.Parent
						select new { p2, ID = p2.ParentID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.Children
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).Count()
					});
		}

		[Test]
		public void SubSub2([DataSources(
			TestProvName.AllAccess,
			TestProvName.AllClickHouse,
			ProviderName.DB2,
			TestProvName.AllOracle,
			TestProvName.AllSybase,
			TestProvName.AllInformix)]
			string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p1 in
						from p2 in Parent
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.p2.Children
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c.c.ParentID + 1 into c
							where c < p1.ID
							select c
						).FirstOrDefault()
					},
					from p1 in
						from p2 in db.Parent
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.p2.Children
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c.c.ParentID + 1 into c
							where c < p1.ID
							select c
						).FirstOrDefault()
					});
		}

		//[Test]
		public void SubSub201([DataSources] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p1 in
						from p2 in Parent
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.p2.Children
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select new { c.c, ID = c.c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).FirstOrDefault()
					},
					from p1 in
						from p2 in db.Parent
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.p2.Children
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select new { c.c, ID = c.c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).FirstOrDefault()
					});
		}

		[Test]
		public void SubSub21([DataSources(
			ProviderName.SqlCe, ProviderName.DB2,
			TestProvName.AllClickHouse,
			TestProvName.AllOracle,
			ProviderName.Access)]
			string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p1 in
						from p2 in Parent
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.p2.Children
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select new { c.c, ID = c.c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).Count()
					},
					from p1 in
						from p2 in db.Parent
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.p2.Children
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select new { c.c, ID = c.c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).Count()
					});
		}

		[Test]
		public void SubSub211([DataSources(
			ProviderName.SqlCe, ProviderName.Access, ProviderName.DB2,
			TestProvName.AllClickHouse,
			TestProvName.AllOracle)]
			string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p1 in
						from p2 in Parent
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.p2.Children
							from g in c.GrandChildren
							select new { g, ID = g.ParentID + 1 } into c
							where c.ID < p1.ID
							select new { c.g, ID = c.g.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).Count()
					},
					from p1 in
						from p2 in db.Parent
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.p2.Children
							from g in c.GrandChildren
							select new { g, ID = g.ParentID + 1 } into c
							where c.ID < p1.ID
							select new { c.g, ID = c.g.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).Count()
					});
		}

		[Test]
		public void SubSub212([DataSources(
			ProviderName.SqlCe, TestProvName.AllAccess, ProviderName.DB2,
			TestProvName.AllClickHouse,
			TestProvName.AllOracle)]
			string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p1 in
						from p2 in Child
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.p2.Parent!.GrandChildren
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select new { c.c, ID = c.c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).Count()
					},
					from p1 in
						from p2 in db.Child
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in p1.p2.p2.Parent!.GrandChildren
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select new { c.c, ID = c.c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).Count()
					});
		}

		[Test]
		public void SubSub22([DataSources(
			ProviderName.SqlCe, ProviderName.Access, ProviderName.DB2,
			TestProvName.AllClickHouse,
			TestProvName.AllOracle, TestProvName.AllSapHana)]
			string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p1 in
						from p2 in Parent
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in Child
							where p1.p2.p2.ParentID == c.ParentID
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select new { c.c, ID = c.c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).Count()
					},
					from p1 in
						from p2 in db.Parent
						select new { p2, ID = p2.ParentID + 1 } into p3
						where p3.ID > 0
						select new { p2 = p3, ID = p3.ID + 1 }
					where p1.ID > 0
					select new
					{
						Count =
						(
							from c in db.Child
							where p1.p2.p2.ParentID == c.ParentID
							select new { c, ID = c.ParentID + 1 } into c
							where c.ID < p1.ID
							select new { c.c, ID = c.c.ParentID + 1 } into c
							where c.ID < p1.ID
							select c
						).Count()
					});
		}

		[Test]
		public void Count1([DataSources(ProviderName.SqlCe, TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in
						from p in Parent
						select new
						{
							p.ParentID,
							Sum = p.Children.Where(t => t.ParentID > 0).Sum(t => t.ParentID) / 2,
						}
					where p.Sum > 1
					select p,
					from p in
						from p in db.Parent
						select new
						{
							p.ParentID,
							Sum = p.Children.Where(t => t.ParentID > 0).Sum(t => t.ParentID) / 2,
						}
					where p.Sum > 1
					select p);
		}

		[Test]
		public void Count2([DataSources(ProviderName.SqlCe, TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in
						from p in Parent
						select new Parent
						{
							ParentID = p.ParentID,
							Value1   = p.Children.Where(t => t.ParentID > 0).Sum(t => t.ParentID) / 2,
						}
					where p.Value1 > 1
					select p,
					from p in
						from p in db.Parent
						select new Parent
						{
							ParentID = p.ParentID,
							Value1   = p.Children.Where(t => t.ParentID > 0).Sum(t => t.ParentID) / 2,
						}
					where p.Value1 > 1
					select p);
		}

		[Test]
		public void Count3([DataSources(ProviderName.SqlCe, TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
				AreEqual(
					from p in
						from p in Parent
						select new
						{
							p.ParentID,
							Sum = p.Children.Sum(t => t.ParentID) / 2,
						}
					where p.Sum > 1
					select p,
					from p in
						from p in db.Parent
						select new
						{
							p.ParentID,
							Sum = p.Children.Sum(t => t.ParentID) / 2,
						}
					where p.Sum > 1
					select p);
		}

		[Test]
		public void Issue1601([DataSources(false)] string context)
		{
			using (var db = GetDataConnection(context))
			{
				var query = from q in db.Types
							let x = db.Types.Sum(y => y.MoneyValue)
							select new
							{
								Y1 = x < 0 ? 9 : x + 8,
								Y2 = Math.Round(x + x)
							};

				query.ToList();

				Assert.AreEqual(2, System.Text.RegularExpressions.Regex.Matches(db.LastQuery!, "Types").Count);
			}
		}

		[Table]
		sealed class Contract_Distributor_Agent
		{
			[Column] public int Agent_Id { get; set; }
			[Column] public int Distributor_Id { get; set; }
			[Column] public int Contract_Id { get; set; }
			[Column] public string? Distributor_Type_Code { get; set; }
			[Column] public string? Distributor_Agent_Type_Prefix { get; set; }
			[Column] public string? Represents_Type_Prefix { get; set; }

			public static readonly Contract_Distributor_Agent[] Data = new[]
			{
				new Contract_Distributor_Agent() { Agent_Id = 1, Distributor_Id = 1, Contract_Id = 198827882, Distributor_Type_Code = "CC", Distributor_Agent_Type_Prefix = "OFFICE", Represents_Type_Prefix = "REPRESENTS" }
			};
		}

		[Table]
		sealed class Agent
		{
			[Column] public int Agent_Id { get; set; }
			[Column] public string? First_Name { get; set; }
			[Column] public string? Last_Name { get; set; }

			public static readonly Agent[] Data = new[]
			{
				new Agent() { Agent_Id = 1, First_Name = "x", Last_Name = "x" }
			};
		}

		[Table]
		sealed class Distributor
		{
			[Column] public int Distributor_Id { get; set; }
			[Column] public string? Type_Code { get; set; }
			[Column] public string? Distributor_Name { get; set; }

			public static readonly Distributor[] Data = new[]
			{
				new Distributor() { Distributor_Id = 1, Type_Code = "RE", Distributor_Name = "x" }
			};
		}

		[Table]
		sealed class Distributor_Commercial_Propert
		{
			[Column] public int Distributor_Id { get; set; }
			[Column] public int Commercial_Property_Id { get; set; }
			[Column] public string? Distributor_Type_Code { get; set; }

			public static readonly Distributor_Commercial_Propert[] Data = new[]
			{
				new Distributor_Commercial_Propert() { Distributor_Id = 1, Commercial_Property_Id = 1, Distributor_Type_Code = "RE" }
			};
		}

		[Table]
		sealed class Commercial_Property
		{
			[Column              ] public int     Commercial_Property_Id { get; set; }
			[Column(Length = 100)] public string? Street_Number          { get; set; }
			[Column(Length = 100)] public string? Street_Name            { get; set; }
			[Column(Length = 100)] public string? State                  { get; set; }
			[Column(Length = 100)] public string? Zip_Code               { get; set; }
			[Column(Length = 100)] public string? Zip_Plus_4             { get; set; }
			[Column(Length = 100)] public string? City_Code              { get; set; }

			public static readonly Commercial_Property[] Data = new[]
			{
				new Commercial_Property() { Commercial_Property_Id = 1, Street_Number = "x", Street_Name = "x", State = "x", Zip_Code = "x", Zip_Plus_4 = "x", City_Code = "x" }
			};
		}

		[Table]
		sealed class Contract_Dates
		{
			[Column] public int Contract_Id { get; set; }
			[Column] public string? Type_Code { get; set; }
			[Column] public string? Effective_Date { get; set; }

			public static readonly Contract_Dates[] Data = new[]
			{
				new Contract_Dates() { Contract_Id = 198827882, Type_Code = "ESTCOE", Effective_Date = "x" }
			};
		}

		[Table]
		sealed class Cities
		{
			[Column] public string? City_Code { get; set; }
			[Column] public string? City_Name { get; set; }

			public static readonly Cities[] Data = new[]
			{
				new Cities() { City_Code = "x", City_Name = "Urupinsk" }
			};
		}

		[Test]
		public void Issue383Test1([DataSources(false, TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
			using (db.CreateLocalTable(Contract_Distributor_Agent.Data))
			using (db.CreateLocalTable(Agent.Data))
			using (db.CreateLocalTable(Distributor.Data))
			using (db.CreateLocalTable(Distributor_Commercial_Propert.Data))
			using (db.CreateLocalTable(Commercial_Property.Data))
			using (db.CreateLocalTable(Contract_Dates.Data))
			using (db.CreateLocalTable(Cities.Data))
			{
				var query = from cda in db.GetTable<Contract_Distributor_Agent>()
							join a in db.GetTable<Agent>() on cda.Agent_Id equals a.Agent_Id
							join d in db.GetTable<Distributor>() on cda.Distributor_Id equals d.Distributor_Id
							join dcp in db.GetTable<Distributor_Commercial_Propert>() on d.Distributor_Id equals dcp.Distributor_Id
							join cp in db.GetTable<Commercial_Property>() on dcp.Commercial_Property_Id equals cp.Commercial_Property_Id
							join cd in db.GetTable<Contract_Dates>() on cda.Contract_Id equals cd.Contract_Id
							where cda.Contract_Id == 198827882
								 && cda.Distributor_Type_Code == "CC"
								 && cda.Distributor_Agent_Type_Prefix == "OFFICE"
								 && cda.Represents_Type_Prefix == "REPRESENTS"
								 && cd.Type_Code == "ESTCOE"
								 && d.Type_Code == "RE"
								 && dcp.Distributor_Type_Code == "RE"
							select new
							{
								a.First_Name,
								a.Last_Name,
								d.Distributor_Name,
								cp.Street_Number,
								cp.Street_Name,
								City_Name = (from c in db.GetTable<Cities>()
											 where c.City_Code == cp.City_Code
											 select new { c.City_Name }),
								cp.State,
								cp.Zip_Code,
								cp.Zip_Plus_4,
								cd.Effective_Date
							};

				var res = query.ToList();

				Assert.AreEqual(1, res.Count);
				Assert.AreEqual("Urupinsk", res[0].City_Name.Single().City_Name);
			}
		}

		[Test]
		public void Issue383Test2([DataSources(false, TestProvName.AllClickHouse)] string context)
		{
			using (var db = GetDataContext(context))
			using (db.CreateLocalTable(Contract_Distributor_Agent.Data))
			using (db.CreateLocalTable(Agent.Data))
			using (db.CreateLocalTable(Distributor.Data))
			using (db.CreateLocalTable(Distributor_Commercial_Propert.Data))
			using (db.CreateLocalTable(Commercial_Property.Data))
			using (db.CreateLocalTable(Contract_Dates.Data))
			using (db.CreateLocalTable(Cities.Data))
			{
				var query = from cda in db.GetTable<Contract_Distributor_Agent>()
							join a in db.GetTable<Agent>() on cda.Agent_Id equals a.Agent_Id
							join d in db.GetTable<Distributor>() on cda.Distributor_Id equals d.Distributor_Id
							join dcp in db.GetTable<Distributor_Commercial_Propert>() on d.Distributor_Id equals dcp.Distributor_Id
							join cp in db.GetTable<Commercial_Property>() on dcp.Commercial_Property_Id equals cp.Commercial_Property_Id
							join cd in db.GetTable<Contract_Dates>() on cda.Contract_Id equals cd.Contract_Id
							where cda.Contract_Id == 198827882
								 && cda.Distributor_Type_Code == "CC"
								 && cda.Distributor_Agent_Type_Prefix == "OFFICE"
								 && cda.Represents_Type_Prefix == "REPRESENTS"
								 && cd.Type_Code == "ESTCOE"
								 && d.Type_Code == "RE"
								 && dcp.Distributor_Type_Code == "RE"
							select new
							{
								a.First_Name,
								a.Last_Name,
								d.Distributor_Name,
								cp.Street_Number,
								cp.Street_Name,
								City_Name = (from c in db.GetTable<Cities>()
											 where c.City_Code == cp.City_Code
											 select c.City_Name).Single(),
								cp.State,
								cp.Zip_Code,
								cp.Zip_Plus_4,
								cd.Effective_Date
							};

				var res = query.ToList();

				Assert.AreEqual(1, res.Count);
				Assert.AreEqual("Urupinsk", res[0].City_Name);
			}
		}

		#region Issue 1700
		[Test(Description = "https://github.com/linq2db/linq2db/issues/1700")]
		public void TestOuterApplySubFunction([DataSources(TestProvName.AllAccess, TestProvName.AllClickHouse)] string context)
		{
			var groupId = 5;

			using var db = GetDataContext(context);
			using var t1 = db.CreateLocalTable<Item>();
			using var t2 = db.CreateLocalTable<ItemAppType>();
			using var t3 = db.CreateLocalTable<AppType>();
			using var t4 = db.CreateLocalTable<AppSubType>();

			var items     = db.GetTable<Item>().AsQueryable();
			var itemTypes = db.GetTable<ItemAppType>().AsQueryable();
			var types     = db.GetTable<AppType>().AsQueryable();
			var subTypes  = db.GetTable<AppSubType>().AsQueryable();

			var data = (
				from item in items.Where(i => i.GroupId == groupId)
				let itemSubTypeDescription = SubFunction(itemTypes, types, subTypes, item)
				select new { item.ItemId, Description1 = item.Description, Description2 = itemSubTypeDescription.Description });

			var all_items = data.ToList();
		}

		[Table]
		class ItemAppType
		{
			[Column] public int AppTypeId { get; set; }
			[Column] public int ItemId  { get; set; }
		}

		[Table]
		class Item
		{
			[Column] public int GroupId { get; set; }
			[Column] public int ItemId  { get; set; }
			[Column] public string? Description { get; set; }
		}

		[Table]
		class AppType
		{
			[Column] public int AppTypeId { get; set; }
			[Column] public DateTime CreatedDate { get; set; }
		}

		[Table]
		class AppSubType
		{
			[Column] public int AppTypeId { get; set; }
			[Column] public int AppSubTypeId { get; set; }
			[Column] public string? Description { get; set; }
			[Column] public DateTime CreatedDate { get; set; }
		}

		[ExpressionMethod(nameof(SubFunctionImpl))]
		static TSome SubFunction(IQueryable<ItemAppType> itemTypes, IQueryable<AppType> types, IQueryable<AppSubType> subTypes, Item item)
		{
			throw new NotImplementedException();
		}

		public class TSome
		{
			public int      AppSubTypeId           { get; set; }
			public string?  Description            { get; set; }
			public DateTime MaxSubtypeCreatedDate  { get; set; }
			public DateTime MaxTypeCreatedDate     { get; set; }
			public int      MaxTypeId              { get; set; }
			public int      CountDistinctTypeId    { get; set; }
			public int      CountDistinctSubTypeId { get; set; }
		}

		static Expression<Func<IQueryable<ItemAppType>, IQueryable<AppType>, IQueryable<AppSubType>, Item, TSome?>> SubFunctionImpl()
		{
			return (itemTypes, types, subTypes, item) => (
					from sub in
						from itemtype in itemTypes
						from type in types.LeftJoin(t => t.AppTypeId == itemtype.AppTypeId)
						from subtype in subTypes.LeftJoin(u => u.AppTypeId == type.AppTypeId)
						where itemtype.ItemId == item.ItemId
							  && type.AppTypeId == itemtype.AppTypeId
							  && subtype.AppTypeId == type.AppTypeId
						select new
						{
							subtype.Description,
							subtype.AppSubTypeId,
							subtypeCreatedDate = subtype.CreatedDate,
							typeCreatedDate    = type.CreatedDate,
							type.AppTypeId
						}
					group sub by new { sub.Description, sub.AppSubTypeId }
					into grpby
					select new TSome
					{
						AppSubTypeId           = grpby.Key.AppSubTypeId,
						Description            = grpby.Key.Description,
						MaxSubtypeCreatedDate  = grpby.Max(i => i.subtypeCreatedDate),
						MaxTypeCreatedDate     = grpby.Max(i => i.typeCreatedDate),
						MaxTypeId              = grpby.Max(i => i.AppTypeId),
						CountDistinctTypeId    = grpby.CountExt(i => i.AppTypeId, Sql.AggregateModifier.Distinct),
						CountDistinctSubTypeId = grpby.CountExt(i => i.AppSubTypeId, Sql.AggregateModifier.Distinct)
					}
				)
				.OrderByDescending(ord1 => ord1.CountDistinctTypeId)
				.ThenByDescending(ord2 => ord2.MaxSubtypeCreatedDate)
				.ThenByDescending(ord3 => ord3.MaxTypeCreatedDate)
				.ThenByDescending(ord4 => ord4.MaxTypeId)
				.FirstOrDefault();
		}
		#endregion

		[Test]
		public void Issue3295Test1([DataSources] string context)
		{
			using var db = GetDataContext(context);

			var query = (from x in db.Person
						 let status = db.Patient.FirstOrDefault(y => y.PersonID == x.ID)
						 select new
						 {
							 Id = status != null ? status.PersonID : x.ID,
							 StatusName = status != null ? status.Diagnosis : "abc",
						 }).Where(x => x.StatusName == "abc");

			query.ToArray();
		}

		[Test]
		public void Issue3295Test2([DataSources] string context)
		{
			using var db = GetDataContext(context);

			var expected = Parent
				.Where(x => x.Children.Where(y => y.ChildID == 11).Select(y => y.ParentID).FirstOrDefault() == 0)
				.Count();

			var actual = db.Parent
				.Where(x => x.Children.Where(y => y.ChildID == 11).Select(y => y.ParentID).FirstOrDefault() == 0)
				.Count();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/3334")]
		public void Issue3334Test([DataSources] string context)
		{
			using var db = GetDataContext(context);

			var subquery = db.GetTable<Person>();

			var query = db.GetTable<Person>()
					.Select(entity1 => new
					{
						Entity1 = entity1,
						Entity2 = subquery.FirstOrDefault(entity2 => entity2.ID == entity1.ID)
					})
					.GroupJoin(db.GetTable<Person>(),
						x => x.Entity2!.ID,
						x => x.ID,
						(x, y) => x);

			var result = query.FirstOrDefault();
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/3365")]
		public void Issue3365Test([DataSources] string context)
		{
			using var db = GetDataContext(context);

			var query = db.Child.Select(x => new
			{
				Assignee = x.GrandChildren.Select(a => a.ParentID).FirstOrDefault()
			});

			var orderedQuery = query.OrderBy(x => x.Assignee);

			orderedQuery.ToArray();
		}

		#region Issue 4458
		[Table]
		sealed class Issue4458Item
		{
			[PrimaryKey, NotNull] public string Id { get; set; } = null!;

			public static readonly Issue4458Item[] Data =
			[
				new Issue4458Item() { Id = "1", },
				new Issue4458Item() { Id = "2", },
				new Issue4458Item() { Id = "3", }
			];
		}

		[Table]
		sealed class WarehouseStock
		{
			[PrimaryKey, NotNull] public string ItemId { get; set; } = null!;
			[Column] public int QuantityAvailable { get; set; }
			[Column(CanBeNull = false)] public string WarehouseId { get; set; } = null!;

			public static readonly WarehouseStock[] Data =
			[
				new WarehouseStock()
				{
					ItemId = "1",
					QuantityAvailable = 10,
					WarehouseId = "A",
				}
			];
		}

		[Table]
		sealed class Review
		{
			[PrimaryKey, NotNull] public string ItemId { get; set; } = null!;
			[PrimaryKey, NotNull] public string UserId { get; set; } = null!;
			[Column] public int Score { get; set; }

			public static readonly Review[] Data =
			[
				new Review()
				{
					ItemId = "1",
					UserId = "1",
					Score = 100,
				},
				new Review()
				{
					ItemId = "1",
					UserId = "2",
					Score = 90,
				},
				new Review()
				{
					ItemId = "2",
					UserId = "1",
					Score = 89,
				},
				new Review()
				{
					ItemId = "2",
					UserId = "4",
					Score = 93,
				},
				new Review()
				{
					ItemId = "3",
					UserId = "5",
					Score = 91,
				}
			];
		}

		sealed class ItemStockSummary
		{
			public string ItemId { get; set; } = null!;
			public int TotalAvailable { get; set; }
			public IEnumerable<Review> Reviews { get; set; } = null!;
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4458")]
		public void Issue4458Test1([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var t1 = db.CreateLocalTable(Issue4458Item.Data);
			using var t2 = db.CreateLocalTable(WarehouseStock.Data);
			using var t3 = db.CreateLocalTable(Review.Data);

			var query = from item in t1
						from stock in t2
						.LeftJoin(s => s.ItemId == item.Id)
						.GroupBy(s => s.ItemId)
						select new ItemStockSummary()
						{
							ItemId = item.Id,
							TotalAvailable = stock.Sum(s => s.QuantityAvailable),
							Reviews = t3.Where(r => r.ItemId == item.Id)
						};

			var filteredByScore = query.Where(i => i.Reviews.Any(r => r.Score > 95));

			var result = filteredByScore.ToArray();
			Assert.That(result, Has.Length.EqualTo(1));
			Assert.That(result[0].ItemId, Is.EqualTo("1"));
			Assert.That(result[0].TotalAvailable, Is.EqualTo(20));
			Assert.That(result[0].Reviews.Count(), Is.EqualTo(2));
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4458")]
		public void Issue4458Test2([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var t1 = db.CreateLocalTable(Issue4458Item.Data);
			using var t2 = db.CreateLocalTable(WarehouseStock.Data);
			using var t3 = db.CreateLocalTable(Review.Data);

			var query = from item in t1
						from stock in t2
						.LeftJoin(s => s.ItemId == item.Id)
						.GroupBy(s => s.ItemId)
						select new ItemStockSummary()
						{
							ItemId = item.Id,
							TotalAvailable = stock.Sum(s => s.QuantityAvailable),
							Reviews = t3.Where(r => r.ItemId == item.Id)
						};

			var filteredByScore = query.Where(i => t3.Where(r => r.ItemId == i.ItemId).Any(r => r.Score > 95));

			var result = filteredByScore.ToArray();
			Assert.That(result, Has.Length.EqualTo(1));
			Assert.That(result[0].ItemId, Is.EqualTo("1"));
			Assert.That(result[0].TotalAvailable, Is.EqualTo(20));
			Assert.That(result[0].Reviews.Count(), Is.EqualTo(2));
		}
		#endregion

		#region Issue 4347
		[Table]
		sealed record TransactionEntity
		{
			[PrimaryKey]
			public Guid Id { get; set; }

			[Column]
			public DateTime ValidOn { get; set; }

			[Association(ThisKey = nameof(Id), OtherKey = nameof(LineEntity.TransactionId))]
			public List<LineEntity> Lines { get; set; } = null!;
		}

		[Table]
		sealed record LineEntity
		{
			[PrimaryKey]
			public Guid Id { get; set; }

			[Column]
			public Guid TransactionId { get; set; }

			[Column]
			public decimal Amount { get; set; }

			[Column]
			public string Currency { get; set; } = null!;

			[Association(ThisKey = nameof(TransactionId), OtherKey = nameof(TransactionEntity.Id), CanBeNull = false)]
			public TransactionEntity Transaction { get; set; } = null!;
		}

		sealed record TransactionDto
		{
			public Guid Id { get; set; }

			public DateTime ValidOn { get; set; }

			public IEnumerable<LineDto> Lines { get; set; } = Enumerable.Empty<LineDto>();

			[ExpressionMethod(nameof(FromEntityExpression))]
			public static TransactionDto FromEntity(TransactionEntity entity)
				=> FromEntityExpression().Compile()(entity);

			static Expression<Func<TransactionEntity, TransactionDto>> FromEntityExpression() =>
				entity => new TransactionDto
				{
					Id = entity.Id,
					ValidOn = entity.ValidOn,
					Lines = entity.Lines.Select(line => LineDto.FromEntity(line))
				};
		}

		sealed record LineDto
		{
			public Guid Id { get; set; }

			public decimal Amount { get; set; }

			public string Currency { get; set; } = null!;

			[ExpressionMethod(nameof(FromEntityExpression))]
			public static LineDto FromEntity(LineEntity entity)
				=> FromEntityExpression().Compile()(entity);

			static Expression<Func<LineEntity, LineDto>> FromEntityExpression()
				=> entity => new LineDto
				{
					Id = entity.Id,
					Amount = entity.Amount,
					Currency = entity.Currency
				};
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4347")]
		public void Issue4347Test1([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var t1 = db.CreateLocalTable<TransactionEntity>();
			using var t2 = db.CreateLocalTable<LineEntity>();

			var currencies = new[] { "A", "B" };

			var q = t1
				.Select(x => new
				{
					Entity = x,
					Dto = TransactionDto.FromEntity(x)
				})
				.Where(x => x.Dto.Lines.Select(y => y.Currency).Intersect(currencies).Any())
				.OrderBy(x => x.Dto.ValidOn)
				.Select(x => x.Dto)
				.ToList();
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4347")]
		public void Issue4347Test2([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var t1 = db.CreateLocalTable<TransactionEntity>();
			using var t2 = db.CreateLocalTable<LineEntity>();

			var currencies = new[] { "A", "B" };

			var q = t1
				.Select(x => new
				{
					Entity = x,
					Dto = TransactionDto.FromEntity(x)
				})
				.Where(x => x.Dto.Lines.Select(y => y.Currency).Intersect(currencies).Any())
				.Select(x => x.Dto)
				.ToList();
		}

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4347")]
		public void Issue4347Test3([DataSources] string context)
		{
			using var db = GetDataContext(context);
			using var t1 = db.CreateLocalTable<TransactionEntity>();
			using var t2 = db.CreateLocalTable<LineEntity>();

			var q = t1
				.Select(x => new
				{
					Entity = x,
					Dto = TransactionDto.FromEntity(x)
				})
				.OrderBy(x => x.Dto.ValidOn)
				.Select(x => x.Dto)
				.ToList();
		}
		#endregion

		#region Issue 4184

		[Test(Description = "https://github.com/linq2db/linq2db/issues/4184")]
		public void Issue4184Test([DataSources] string context)
		{
			using var db = GetDataContext(context);
			
			var subquery =
				from p in db.Person
				group p by p.ID
				into gpItem
				select new PcScanId(gpItem.Key, gpItem.Max(s => s.ID));

			var query =
				from ps in subquery
				join pc in db.Patient on ps.PcId equals pc.PersonID
				select new { pc, ps };

			Assert.That(() => query.ToArray(), Throws.Exception.InstanceOf<LinqException>());
		}

		private record PcScanId(int pcId, int scanId)
		{
			public int PcId = pcId;
			public int ScanId = scanId;
		}

		#endregion
	}
}
