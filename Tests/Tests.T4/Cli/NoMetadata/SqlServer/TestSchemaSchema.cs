// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;
using LinqToDB.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.NoMetadata.SqlServer
{
	public static partial class TestSchemaSchema
	{
		public partial class DataContext
		{
			private readonly IDataContext _dataContext;

			public ITable<SameTableName> SameTableNames => _dataContext.GetTable<SameTableName>();
			public ITable<TestSchemaA>   TestSchemaA    => _dataContext.GetTable<TestSchemaA>();
			public ITable<TestSchemaB>   TestSchemaB    => _dataContext.GetTable<TestSchemaB>();

			public DataContext(IDataContext dataContext)
			{
				_dataContext = dataContext;
			}

			#region Table Functions
			#region SchemaTableFunction
			private static readonly MethodInfo _schemaTableFunction = MemberHelper.MethodOf<DataContext>(ctx => ctx.SchemaTableFunction(default));

			public IQueryable<Parent> SchemaTableFunction(int? id)
			{
				return _dataContext.GetTable<Parent>(this, _schemaTableFunction, id);
			}
			#endregion
			#endregion
		}

		public class SameTableName
		{
			public int? Id { get; set; } // int
		}

		public class TestSchemaA
		{
			public int TestSchemaAid { get; set; } // int
			public int Field1        { get; set; } // int

			#region Associations
			/// <summary>
			/// FK_TestSchema_TestSchemaBY_OriginTestSchemaA backreference
			/// </summary>
			public IEnumerable<TestSchemaB> TestSchemaB { get; set; } = null!;

			/// <summary>
			/// FK_TestSchema_TestSchemaBY_TargetTestSchemaA backreference
			/// </summary>
			public IEnumerable<TestSchemaB> TestSchemaB1 { get; set; } = null!;

			/// <summary>
			/// FK_TestSchema_TestSchemaBY_TargetTestSchemaA2 backreference
			/// </summary>
			public IEnumerable<TestSchemaB> TestSchemaB2 { get; set; } = null!;
			#endregion
		}

		public class TestSchemaB
		{
			public int TestSchemaBid       { get; set; } // int
			public int OriginTestSchemaAid { get; set; } // int
			public int TargetTestSchemaAid { get; set; } // int
			public int TargetTestSchemaAId { get; set; } // int

			#region Associations
			/// <summary>
			/// FK_TestSchema_TestSchemaBY_OriginTestSchemaA
			/// </summary>
			public TestSchemaA OriginTestSchemaA { get; set; } = null!;

			/// <summary>
			/// FK_TestSchema_TestSchemaBY_TargetTestSchemaA
			/// </summary>
			public TestSchemaA TargetTestSchemaA { get; set; } = null!;

			/// <summary>
			/// FK_TestSchema_TestSchemaBY_TargetTestSchemaA2
			/// </summary>
			public TestSchemaA TargetTestSchemaA1 { get; set; } = null!;
			#endregion
		}

		#region Stored Procedures
		#region TestProcedure
		public static IEnumerable<TestProcedureResult> TestProcedure(this TestDataDB dataConnection)
		{
			return dataConnection.QueryProc(dataReader => new TestProcedureResult()
			{
				Column = Converter.ChangeTypeTo<int>(dataReader.GetValue(0), dataConnection.MappingSchema)
			}, "[TestSchema].[TestProcedure]");
		}

		public static Task<IEnumerable<TestProcedureResult>> TestProcedureAsync(this TestDataDB dataConnection, CancellationToken cancellationToken = default)
		{
			return dataConnection.QueryProcAsync(dataReader => new TestProcedureResult()
			{
				Column = Converter.ChangeTypeTo<int>(dataReader.GetValue(0), dataConnection.MappingSchema)
			}, "[TestSchema].[TestProcedure]", cancellationToken);
		}

		public partial class TestProcedureResult
		{
			public int Column { get; set; }
		}
		#endregion
		#endregion
	}
}
