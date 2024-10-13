﻿using System.Text;
using System.Globalization;

namespace LinqToDB.DataProvider.Oracle
{
	using SqlQuery;
	using Mapping;
	using SqlProvider;
	using System;

	class Oracle23SqlBuilder : Oracle12SqlBuilder
	{
		public Oracle23SqlBuilder(IDataProvider? provider, MappingSchema mappingSchema, DataOptions dataOptions, ISqlOptimizer sqlOptimizer, SqlProviderFlags sqlProviderFlags)
			: base(provider, mappingSchema, dataOptions, sqlOptimizer, sqlProviderFlags)
		{
		}

		Oracle23SqlBuilder(BasicSqlBuilder parentBuilder) : base(parentBuilder)
		{
		}

		protected override ISqlBuilder CreateSqlBuilder()
		{
			return new Oracle23SqlBuilder(this) { HintBuilder = HintBuilder };
		}

		protected override string? FakeTable => null;

		protected override void BuildDropTableStatement(SqlDropTableStatement dropTable)
		{
			if (dropTable.Table!.IdentityFields.Count == 0)
			{
				BuildDropTableStatementIfExists(dropTable);
			}
			else
			{
				BuildTag(dropTable);

				var exists = dropTable.Table.TableOptions.HasDropIfExists() ? "IF EXISTS " : null;
				StringBuilder
					.Append(CultureInfo.InvariantCulture, $"DROP TRIGGER {exists}");

				AppendSchemaPrefix(StringBuilder, dropTable.Table!.TableName.Schema);
				Convert(StringBuilder, MakeIdentityTriggerName(dropTable.Table.TableName.Name), ConvertType.TriggerName);

				StringBuilder
					.AppendLine(";")
					.Append(CultureInfo.InvariantCulture, $"DROP SEQUENCE {exists}");

				AppendSchemaPrefix(StringBuilder, dropTable.Table!.TableName.Schema);
				Convert(StringBuilder, MakeIdentitySequenceName(dropTable.Table.TableName.Name), ConvertType.SequenceName);

				StringBuilder
					.AppendLine(";")
					.Append(CultureInfo.InvariantCulture, $"DROP TABLE {exists}");
				BuildPhysicalTable(dropTable.Table, null);
				StringBuilder
					.AppendLine(";")
					;
			}
		}

		protected override void BuildCreateTableCommand(SqlTable table)
		{
			base.BuildCreateTableCommand(table);

			if (table.TableOptions.HasCreateIfNotExists())
				StringBuilder.Append("IF NOT EXISTS ");
		}

		protected override void BuildStartCreateTableStatement(SqlCreateTableStatement createTable)
		{
			if (createTable.StatementHeader == null && (createTable.Table.TableOptions.HasCreateIfNotExists() || createTable.Table.TableOptions.HasIsTemporary()))
			{
				if (createTable.Table!.IdentityFields.Count > 0)
				{
					AppendIndent().AppendLine(@"BEGIN");

					Indent++;

					AppendIndent().AppendLine(@"EXECUTE IMMEDIATE '");
					Indent++;
				}
			}

			if (createTable.StatementHeader == null)
			{
				AppendIndent();
				BuildCreateTableCommand(createTable.Table!);
				BuildPhysicalTable(createTable.Table!, null);
			}
			else
			{
				var name = WithStringBuilder(
					static ctx =>
					{
						ctx.this_.BuildPhysicalTable(ctx.createTable.Table!, null);
					}, (this_: this, createTable));

				AppendIndent().AppendFormat(CultureInfo.InvariantCulture, createTable.StatementHeader, name);
			}
		}

		protected override void BuildEndCreateTableStatement(SqlCreateTableStatement createTable)
		{
			if (createTable.StatementFooter != null)
				AppendIndent().Append(createTable.StatementFooter);

			if (createTable.StatementHeader == null)
			{
				var table = createTable.Table;

				if (table.TableOptions.IsTemporaryOptionSet())
				{
					AppendIndent().AppendLine(table.TableOptions.HasIsTransactionTemporaryData()
						? "ON COMMIT DELETE ROWS"
						: "ON COMMIT PRESERVE ROWS");
				}

				if (createTable.Table!.IdentityFields.Count > 0)
				{
					Indent--;

					AppendIndent()
						.AppendLine("';");

					Indent--;

					StringBuilder
						.AppendLine("EXCEPTION")
						.AppendLine("\tWHEN OTHERS THEN")
						.AppendLine("\t\tIF SQLCODE != -955 THEN")
						.AppendLine("\t\t\tRAISE;")
						.AppendLine("\t\tEND IF;")
						.AppendLine("END;")
						;
				}
			}
		}
	}
}
