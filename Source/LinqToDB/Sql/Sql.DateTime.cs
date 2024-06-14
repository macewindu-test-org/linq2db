﻿using System;
using System.Globalization;

namespace LinqToDB
{
	using SqlQuery;
	using Expressions;

	using PN = ProviderName;

	public partial class Sql
	{
		[Enum]
		public enum DateParts
		{
			Year        =  0,
			Quarter     =  1,
			Month       =  2,
			DayOfYear   =  3,
			Day         =  4,
			/// <summary>
			/// This date part behavior depends on used database and also depends on where if calculated - in C# code or in database.
			/// Eeach database could have own week numbering logic, see notes below.
			///
			/// Current implementation uses following schemas per-provider:
			/// C# evaluation:
			/// <para>
			/// <c>CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.Value, CalendarWeekRule.FirstDay, DayOfWeek.Sunday)</c>
			/// </para>
			/// Databases:
			/// <list type="bullet">
			/// <item>US numbering schema used by:
			/// <list type="bullet">
			/// <item>MS Access</item>
			/// <item>SQL CE</item>
			/// <item>SQL Server</item>
			/// <item>SAP/Sybase ASE</item>
			/// <item>Informix</item>
			/// </list>
			/// </item>
			/// <item>US 0-based numbering schema used by MySQL database</item>
			/// <item>ISO numbering schema with incorrect numbering of first week used by SAP HANA database</item>
			/// <item>ISO numbering schema with proper numbering of first week used by:
			/// <list type="bullet">
			/// <item>Firebird</item>
			/// <item>PostgreSQL</item>
			/// <item>ClickHouse</item>
			/// </list>
			/// </item>
			/// <item>Primitive (each 7 days counted as week) numbering schema:
			/// <list type="bullet">
			/// <item>DB2</item>
			/// <item>Oracle</item>
			/// </list>
			/// </item>
			/// <item>SQLite numbering logic cannot be classified by human being</item>
			/// </list>
			/// </summary>
			Week        =  5,
			WeekDay     =  6,
			Hour        =  7,
			Minute      =  8,
			Second      =  9,
			Millisecond = 10,
			Microsecond = 11,
			Nanosecond  = 12,
			Tick        = 13,
		}

		#region DatePart

		public static int? DatePart([SqlQueryDependent] DateParts part, [ExprParameter] DateTime? date)
		{
			return (int?)DatePartLong(part, date);
		}

		public static long? DatePartLong([SqlQueryDependent] DateParts part, [ExprParameter] DateTime? date)
		{
			if (date == null)
				return null;

			return part switch
			{
				DateParts.Year          => date.Value.Year,
				DateParts.Quarter       => (date.Value.Month - 1) / 3 + 1,
				DateParts.Month         => date.Value.Month,
				DateParts.DayOfYear     => date.Value.DayOfYear,
				DateParts.Day           => date.Value.Day,
				DateParts.Week          => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.Value, CalendarWeekRule.FirstDay, DayOfWeek.Sunday),
				DateParts.WeekDay       => ((int)date.Value.DayOfWeek + 1 + DateFirst + 6) % 7 + 1,
				DateParts.Hour          => date.Value.Hour,
				DateParts.Minute        => date.Value.Minute,
				DateParts.Second        => date.Value.Second,
				DateParts.Millisecond   => date.Value.Millisecond,
#if NET7_0_OR_GREATER
				DateParts.Microsecond   => date.Value.Microsecond,
				DateParts.Nanosecond    => date.Value.Nanosecond,
				DateParts.Tick          => date.Value.Ticks,
#else
				DateParts.Microsecond   => date.Value.Ticks / 10,
				DateParts.Nanosecond    => date.Value.Ticks * 100,
				DateParts.Tick          => date.Value.Ticks,
#endif
				_                           => throw new InvalidOperationException(),
			};
		}

#endregion DatePart

		#region DateAdd

		public static DateTime? DateAdd([SqlQueryDependent] DateParts part, double? number, DateTime? date)
		{
			if (number == null || date == null)
				return null;

			return part switch
			{
				DateParts.Year          => date.Value.AddYears((int)number),
				DateParts.Quarter       => date.Value.AddMonths((int)number * 3),
				DateParts.Month         => date.Value.AddMonths((int)number),
				DateParts.DayOfYear     => date.Value.AddDays(number.Value),
				DateParts.Day           => date.Value.AddDays(number.Value),
				DateParts.Week          => date.Value.AddDays(number.Value * 7),
				DateParts.WeekDay       => date.Value.AddDays(number.Value),
				DateParts.Hour          => date.Value.AddHours(number.Value),
				DateParts.Minute        => date.Value.AddMinutes(number.Value),
				DateParts.Second        => date.Value.AddSeconds(number.Value),
				DateParts.Millisecond   => date.Value.AddMilliseconds(number.Value),
#if NET7_0_OR_GREATER
				DateParts.Microsecond   => date.Value.AddMicroseconds(number.Value),
#else
				DateParts.Microsecond   => date.Value.AddTicks((long)number.Value * 10000),
#endif
				DateParts.Nanosecond    => date.Value.AddTicks((long)number.Value / 100),
				DateParts.Tick          => date.Value.AddTicks((long)number.Value),
				_                       => throw new InvalidOperationException(),
			};
		}

		#endregion

		#region DateDiff

		sealed class DateDiffBuilder : IExtensionCallBuilder
		{
			public static string DatePartToStr(DateParts part)
			{
				return part switch
				{
					DateParts.Year => "year",
					DateParts.Quarter => "quarter",
					DateParts.Month => "month",
					DateParts.DayOfYear => "dayofyear",
					DateParts.Day => "day",
					DateParts.Week => "week",
					DateParts.WeekDay => "weekday",
					DateParts.Hour => "hour",
					DateParts.Minute => "minute",
					DateParts.Second => "second",
					DateParts.Millisecond => "millisecond",
					DateParts.Microsecond => "microsecond",
					DateParts.Nanosecond => "nanosecond",
					_ => throw new InvalidOperationException($"Unexpected datepart: {part}")
				};
			}

			public void Build(ISqExtensionBuilder builder)
			{
				var part      = builder.GetValue<DateParts>(0);
				var startdate = builder.GetExpression(1);
				var endDate   = builder.GetExpression(2);

				if (startdate is null || endDate is null)
				{
					builder.IsConvertible = false;
					return;
				}

				var partSql   = new SqlExpression(DatePartToStr(part), Precedence.Primary);

				builder.ResultExpression = new SqlFunction(typeof(int), builder.Expression, partSql, startdate, endDate);
			}
		}

		sealed class DateDiffBuilderSapHana : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var part       = builder.GetValue<DateParts>(0);
				var startdate  = builder.GetExpression(1);
				var endDate    = builder.GetExpression(2);
				var divider    = 1;

				if (startdate is null || endDate is null)
				{
					builder.IsConvertible = false;
					return;
				}

				string funcName;
				switch (part)
				{
					case DateParts.Day        : funcName = "Days_Between";                     break;
					case DateParts.Hour       : funcName = "Seconds_Between"; divider = 3600;  break;
					case DateParts.Minute     : funcName = "Seconds_Between"; divider = 60;    break;
					case DateParts.Second     : funcName = "Seconds_Between";                  break;
					case DateParts.Millisecond: funcName = "Nano100_Between"; divider = 10000; break;
					case DateParts.Microsecond: funcName = "Nano100_Between"; divider = 10;    break;
					case DateParts.Tick:        funcName = "Nano100_Between";                  break;
					case DateParts.Nanosecond : funcName = "Nano100_Between";                  break;
					default:
						throw new InvalidOperationException($"Unexpected datepart: {part}");
				}

				ISqlExpression func = new SqlFunction(typeof(int), funcName, startdate, endDate);
				if (divider != 1)
					func = builder.Div(func, divider);

				if (part == DateParts.Nanosecond)
					func = builder.Mul(func, 100);

				builder.ResultExpression = func;
			}
		}

		sealed class DateDiffBuilderDB2 : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var part       = builder.GetValue<DateParts>(0);
				var startDate  = builder.GetExpression(1);
				var endDate    = builder.GetExpression(2);

				if (startDate is null || endDate is null)
				{
					builder.IsConvertible = false;
					return;
				}

				var secondsExpr = builder.Mul<int>(builder.Sub<int>(
						new SqlFunction(typeof(int), "Days", endDate),
						new SqlFunction(typeof(int), "Days", startDate)),
					new SqlValue(86400));

				var midnight = builder.Sub<int>(
					new SqlFunction(typeof(int), "MIDNIGHT_SECONDS", endDate),
					new SqlFunction(typeof(int), "MIDNIGHT_SECONDS", startDate));

				var resultExpr = builder.Add<int>(secondsExpr, midnight);

				switch (part)
				{
					case DateParts.Day         : resultExpr = builder.Div(resultExpr, 86400); break;
					case DateParts.Hour        : resultExpr = builder.Div(resultExpr, 3600);  break;
					case DateParts.Minute      : resultExpr = builder.Div(resultExpr, 60);    break;
					case DateParts.Second      : break;
					case DateParts.Millisecond :
						resultExpr = builder.Add<int>(
							builder.Mul(resultExpr, 1000),
							builder.Div(
								builder.Sub<int>(
									new SqlFunction(typeof(int), "MICROSECOND", endDate),
									new SqlFunction(typeof(int), "MICROSECOND", startDate)),
								1000));
						break;
					case DateParts.Microsecond :
						resultExpr = builder.Add<int>(
							builder.Mul(resultExpr, 1000_000),
							builder.Sub<int>(
								new SqlFunction(typeof(int), "MICROSECOND", endDate),
								new SqlFunction(typeof(int), "MICROSECOND", startDate))
							);
						break;
					default:
						throw new InvalidOperationException($"Unexpected datepart: {part}");
				}

				builder.ResultExpression = resultExpr;
			}
		}

		sealed class DateDiffBuilderSQLite : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var part = builder.GetValue<DateParts>(0);
				var startDate = builder.GetExpression(1);
				var endDate = builder.GetExpression(2);

				if (startDate is null || endDate is null)
				{
					builder.IsConvertible = false;
					return;
				}

				var expStr = "round((julianday({1}) - julianday({0}))";
				expStr += part switch
				{
					DateParts.Day         => ")",
					DateParts.Hour        => " * 24)",
					DateParts.Minute      => " * 1440)",
					DateParts.Second      => " * 86400)",
					DateParts.Millisecond => " * 86400000)",
					_                     => throw new InvalidOperationException($"Unexpected datepart: {part}"),
				};
				builder.ResultExpression = new SqlExpression(typeof(int), expStr, startDate, endDate );
			}
		}

		sealed class DateDiffBuilderPostgreSql : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var part = builder.GetValue<DateParts>(0);
				var startDate = builder.GetExpression(1)!;
				var endDate = builder.GetExpression(2)!;
				var expStr = part switch
				{
					DateParts.Year        => "(DATE_PART('year', {1}::date) - DATE_PART('year', {0}::date))",
					DateParts.Month       => "((DATE_PART('year', {1}::date) - DATE_PART('year', {0}::date)) * 12 + (DATE_PART('month', {1}'::date) - DATE_PART('month', {0}::date)))",
					DateParts.Week        => "TRUNC(DATE_PART('day', {1}::timestamp - {0}::timestamp) / 7)",
					DateParts.Day         => "EXTRACT(EPOCH FROM ({1}::timestamp - {0}::timestamp)) / 86400",
					DateParts.Hour        => "EXTRACT(EPOCH FROM ({1}::timestamp - {0}::timestamp)) / 3600",
					DateParts.Minute      => "EXTRACT(EPOCH FROM ({1}::timestamp - {0}::timestamp)) / 60",
					DateParts.Second      => "EXTRACT(EPOCH FROM ({1}::timestamp - {0}::timestamp))",
					DateParts.Millisecond => "ROUND(EXTRACT(EPOCH FROM ({1}::timestamp - {0}::timestamp)) * 1000)",
					_                     => throw new InvalidOperationException($"Unexpected datepart: {part}"),
				};
				builder.ResultExpression = new SqlExpression(typeof(int), expStr, Precedence.Multiplicative, startDate, endDate);
			}
		}

		sealed class DateDiffBuilderAccess : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var part = builder.GetValue<DateParts>(0);
				var startDate = builder.GetExpression(1);
				var endDate = builder.GetExpression(2);

				if (startDate is null || endDate is null)
				{
					builder.IsConvertible = false;
					return;
				}

				var expStr = "DATEDIFF('";

#pragma warning disable CA2208 // Instantiate argument exceptions correctly
				expStr += part switch
				{
					DateParts.Year        => "yyyy",
					DateParts.Quarter     => "q",
					DateParts.Month       => "m",
					DateParts.DayOfYear   => "y",
					DateParts.Day         => "d",
					DateParts.WeekDay     => "w",
					DateParts.Week        => "ww",
					DateParts.Hour        => "h",
					DateParts.Minute      => "n",
					DateParts.Second      => "s",
					DateParts.Millisecond => "s",
					_                     => throw new InvalidOperationException($"Unexpected datepart: {part}"),
				};
#pragma warning restore CA2208 // Instantiate argument exceptions correctly

				expStr += "', {0}, {1})";

				if (part == DateParts.Millisecond)
					expStr += " * 1000";

				builder.ResultExpression = new SqlExpression(typeof(int), expStr, startDate, endDate);
			}
		}

		sealed class DateDiffBuilderOracle : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var part = builder.GetValue<DateParts>(0);
				var startDate = builder.GetExpression(1);
				var endDate = builder.GetExpression(2);

				if (startDate is null || endDate is null)
				{
					builder.IsConvertible = false;
					return;
				}

				var expStr = part switch
				{
					// DateParts.Year        => "({1} - {0}) / 365",
					// DateParts.Month       => "({1} - {0}) / 30",
					DateParts.Week        => "(CAST ({1} as DATE) - CAST ({0} as DATE)) / 7",
					DateParts.Day         => "(CAST ({1} as DATE) - CAST ({0} as DATE))",
					DateParts.Hour        => "(CAST ({1} as DATE) - CAST ({0} as DATE)) * 24",
					DateParts.Minute      => "(CAST ({1} as DATE) - CAST ({0} as DATE)) * 1440",
					DateParts.Second      => "(CAST ({1} as DATE) - CAST ({0} as DATE)) * 86400",

					// this is tempting to use but leads to precision loss on big intervals
					//DateParts.Millisecond => "1000 * (EXTRACT(SECOND FROM CAST ({1} as TIMESTAMP) - CAST ({0} as TIMESTAMP)) + (CAST ({1} as DATE) - CAST ({0} as DATE)) * 86400)",

					// could be really ugly on big start/end expressions
					DateParts.Millisecond => "1000 * (EXTRACT(SECOND FROM CAST ({1} as TIMESTAMP) - CAST ({0} as TIMESTAMP))"
					+ " + 60 * (EXTRACT(MINUTE FROM CAST ({1} as TIMESTAMP) - CAST ({0} as TIMESTAMP))"
					+ " + 60 * (EXTRACT(HOUR FROM CAST ({1} as TIMESTAMP) - CAST ({0} as TIMESTAMP))"
					+ " + 24 * EXTRACT(DAY FROM CAST ({1} as TIMESTAMP) - CAST ({0} as TIMESTAMP)))))",
					_                     => throw new InvalidOperationException($"Unexpected datepart: {part}"),
				};
				builder.ResultExpression = new SqlExpression(typeof(int), expStr, Precedence.Multiplicative, startDate, endDate);
			}
		}

		sealed class DateDiffBuilderClickHouse : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var part       = builder.GetValue<DateParts>(0);
				var startDate  = builder.GetExpression(1);
				var endDate    = builder.GetExpression(2);

				if (startDate is null || endDate is null)
				{
					builder.IsConvertible = false;
					return;
				}

				string? unit = null;
				switch (part)
				{
					case DateParts.Year   : unit = "year"   ; break;
					case DateParts.Quarter: unit = "quarter"; break;
					case DateParts.Month  : unit = "month"  ; break;
					case DateParts.Week   : unit = "week"   ; break;
					case DateParts.Day    : unit = "day"    ; break;
					case DateParts.Hour   : unit = "hour"   ; break;
					case DateParts.Minute : unit = "minute" ; break;
					case DateParts.Second : unit = "second" ; break;

					case DateParts.Millisecond:
						builder.ResultExpression = new SqlExpression(
							typeof(long?),
							"toUnixTimestamp64Milli(toDateTime64({1}, 3)) - toUnixTimestamp64Milli(toDateTime64({0}, 3))",
							Precedence.Subtraction,
							startDate,
							endDate);
						break;

					default:
						throw new InvalidOperationException($"Unexpected datepart: {part}");
				}

				if (unit != null)
					builder.ResultExpression = new SqlFunction(typeof(int), "date_diff", new SqlValue(unit), startDate, endDate);
			}
		}

		[CLSCompliant(false)]
		[Extension(                  "DateDiff",      BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.SqlServer,     "DateDiff_Big" , BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.SqlServer2005, "DateDiff"     , BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.SqlServer2008, "DateDiff"     , BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.SqlServer2012, "DateDiff"     , BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.SqlServer2014, "DateDiff"     , BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.MySql,         "TIMESTAMPDIFF", BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.DB2,           "",              BuilderType = typeof(DateDiffBuilderDB2))]
		[Extension(PN.SapHana,       "",              BuilderType = typeof(DateDiffBuilderSapHana))]
		[Extension(PN.SQLite,        "",              BuilderType = typeof(DateDiffBuilderSQLite))]
		[Extension(PN.Oracle,        "",              BuilderType = typeof(DateDiffBuilderOracle))]
		[Extension(PN.PostgreSQL,    "",              BuilderType = typeof(DateDiffBuilderPostgreSql))]
		[Extension(PN.Access,        "",              BuilderType = typeof(DateDiffBuilderAccess))]
		[Extension(PN.ClickHouse,    "",              BuilderType = typeof(DateDiffBuilderClickHouse))]
		public static int? DateDiff(DateParts part, DateTime? startDate, DateTime? endDate)
		{
			if (startDate == null || endDate == null)
				return null;

			return part switch
			{
				DateParts.Day         => (int)(endDate - startDate).Value.TotalDays,
				DateParts.Hour        => (int)(endDate - startDate).Value.TotalHours,
				DateParts.Minute      => (int)(endDate - startDate).Value.TotalMinutes,
				DateParts.Second      => (int)(endDate - startDate).Value.TotalSeconds,
				DateParts.Millisecond => (int)(endDate - startDate).Value.TotalMilliseconds,
#if NET7_0_OR_GREATER
				DateParts.Microsecond => (int)(endDate - startDate).Value.TotalMicroseconds,
				DateParts.Nanosecond  => (int)(endDate - startDate).Value.TotalNanoseconds,
#endif
				_ => throw new InvalidOperationException(),
			};
		}

				[CLSCompliant(false)]
		[Extension(                  "DateDiff",      BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.SqlServer,     "DateDiff_Big" , BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.SqlServer2005, "DateDiff"     , BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.SqlServer2008, "DateDiff"     , BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.SqlServer2012, "DateDiff"     , BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.SqlServer2014, "DateDiff"     , BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.MySql,         "TIMESTAMPDIFF", BuilderType = typeof(DateDiffBuilder))]
		[Extension(PN.DB2,           "",              BuilderType = typeof(DateDiffBuilderDB2))]
		[Extension(PN.SapHana,       "",              BuilderType = typeof(DateDiffBuilderSapHana))]
		[Extension(PN.SQLite,        "",              BuilderType = typeof(DateDiffBuilderSQLite))]
		[Extension(PN.Oracle,        "",              BuilderType = typeof(DateDiffBuilderOracle))]
		[Extension(PN.PostgreSQL,    "",              BuilderType = typeof(DateDiffBuilderPostgreSql))]
		[Extension(PN.Access,        "",              BuilderType = typeof(DateDiffBuilderAccess))]
		[Extension(PN.ClickHouse,    "",              BuilderType = typeof(DateDiffBuilderClickHouse))]
		public static long? DateDiffLong(DateParts part, DateTime? startDate, DateTime? endDate)
		{
			if (startDate == null || endDate == null)
				return null;

			return part switch
			{
				DateParts.Day         => (long)(endDate - startDate).Value.TotalDays,
				DateParts.Hour        => (long)(endDate - startDate).Value.TotalHours,
				DateParts.Minute      => (long)(endDate - startDate).Value.TotalMinutes,
				DateParts.Second      => (long)(endDate - startDate).Value.TotalSeconds,
				DateParts.Millisecond => (long)(endDate - startDate).Value.TotalMilliseconds,
#if NET7_0_OR_GREATER
				DateParts.Microsecond => (long)(endDate - startDate).Value.TotalMicroseconds,
				DateParts.Nanosecond  => (long)(endDate - startDate).Value.TotalNanoseconds,
#endif
				_ => throw new InvalidOperationException(),
			};
		}
		#endregion

				#region DateDiffInterval

		sealed class DateDiffIntervalBuilder : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startdate = builder.GetExpression(0);
				var endDate   = builder.GetExpression(1);

				builder.ResultExpression = new SqlExpression(typeof(long), builder.Expression + "(nanosecond, {0}, {1}) / 100", startdate!, endDate!);
			}
		}

		sealed class DateDiffIntervalBuilderSapHana : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startdate  = builder.GetExpression(0);
				var endDate    = builder.GetExpression(1);

				builder.ResultExpression = new SqlFunction(typeof(long), "Nano100_Between", startdate!, endDate!);
			}
		}

		sealed class DateDiffIntervalBuilderMySql : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startdate = builder.GetExpression(0);
				var endDate   = builder.GetExpression(1);

				builder.ResultExpression = new SqlExpression(typeof(long), builder.Expression + "(MICROSECOND, {0}, {1}) * 10", startdate!, endDate!);
			}
		}

		sealed class DateDiffIntervalBuilderSybase : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startdate = builder.GetExpression(0);
				var endDate   = builder.GetExpression(1);

				builder.ResultExpression = new SqlExpression(typeof(long), builder.Expression + "(microsecond, {0}, {1}) * 10", startdate!, endDate!);
			}
		}

		sealed class DateDiffIntervalBuilderFirebird : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startdate = builder.GetExpression(0);
				var endDate   = builder.GetExpression(1);

				builder.ResultExpression = new SqlExpression(typeof(long), "Cast(DateDiff(millisecond, {0}, {1}) * 10000 as BIGINT)", startdate!, endDate!);
			}
		}

		sealed class DateDiffIntervalBuilderDB2 : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startDate  = builder.GetExpression(0);
				var endDate    = builder.GetExpression(1);

				var secondsExpr = builder.Mul<int>(builder.Sub<int>(
						new SqlFunction(typeof(int), "Days", endDate!),
						new SqlFunction(typeof(int), "Days", startDate!)),
					new SqlValue(86400));

				var midnight = builder.Sub<int>(
					new SqlFunction(typeof(int), "MIDNIGHT_SECONDS", endDate!),
					new SqlFunction(typeof(int), "MIDNIGHT_SECONDS", startDate!));

				var resultExpr = builder.Add<int>(secondsExpr, midnight);

				resultExpr = builder.Add<TimeSpan>(
					builder.Mul(resultExpr, 10000000),
					builder.Mul(
						builder.Sub<int>(
							new SqlFunction(typeof(int), "MICROSECOND", endDate!),
							new SqlFunction(typeof(int), "MICROSECOND", startDate!)),
						10));

				builder.ResultExpression = resultExpr;
			}
		}

		sealed class DateDiffIntervalBuilderSQLite : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startDate = builder.GetExpression(0);
				var endDate = builder.GetExpression(1);

				var expStr = "cast(round((julianday({1}) - julianday({0})) * 864000000000) as INTEGER)";
				builder.ResultExpression = new SqlExpression(typeof(long), expStr, startDate!, endDate!);
			}
		}

		sealed class DateDiffIntervalBuilderPostgreSql : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startDate = builder.GetExpression(0);
				var endDate = builder.GetExpression(1);
				var expStr =  "({1}::timestamp - {0}::timestamp)";
				builder.ResultExpression = new SqlExpression(typeof(long), expStr, Precedence.Multiplicative, startDate!, endDate!);
			}
		}

		sealed class DateDiffIntervalBuilderAccess : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startDate = builder.GetExpression(0);
				var endDate = builder.GetExpression(1);

				var expStr = "DATEDIFF('s', {0}, {1}) * 10000000";

				builder.ResultExpression = new SqlExpression(typeof(long), expStr, startDate!, endDate!);
			}
		}

		sealed class DateDiffIntervalBuilderOracle : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startDate = builder.GetExpression(0);
				var endDate = builder.GetExpression(1);
				// In Oracle, subtracting two DATE returns the number days between them (could be converted to INTERVAL with NUMTODSINTERVAL).
				// Subtracting two TIMESTAMP returns an INTERVAL.
				// Unfortunately, it's not possible to know based on C# type if an expression was mapped to `DATE` or `TIMESTAMP` in DB :(
				var expStr = "(CAST ({1} as TIMESTAMP) - CAST ({0} as TIMESTAMP))";
				builder.ResultExpression = new SqlExpression(typeof(long), expStr, startDate!, endDate!);
			}
		}

		sealed class DateDiffIntervalBuilderClickHouse : IExtensionCallBuilder
		{
			public void Build(ISqExtensionBuilder builder)
			{
				var startDate  = builder.GetExpression(0);
				var endDate    = builder.GetExpression(1);

				builder.ResultExpression = new SqlExpression(
					typeof(long?),
					"((toUnixTimestamp64Nano(toDateTime64({1}, 3)) - toUnixTimestamp64Nano(toDateTime64({0}, 3))) / 100)",
					Precedence.Subtraction,
					startDate!,
					endDate!);
			}
		}

		[Extension(                 "DateDiff",      BuilderType = typeof(DateDiffIntervalBuilder))]
		[Extension(PN.SqlServer,    "DateDiff_Big",  BuilderType = typeof(DateDiffIntervalBuilder))]
		[Extension(PN.SqlCe,        "DateDiff",      BuilderType = typeof(DateDiffIntervalBuilder))]
		[Extension(PN.SqlServer2005,"DateDiff",      BuilderType = typeof(DateDiffIntervalBuilder))]
		[Extension(PN.SqlServer2008,"DateDiff",      BuilderType = typeof(DateDiffIntervalBuilder))]
		[Extension(PN.SqlServer2012,"DateDiff",      BuilderType = typeof(DateDiffIntervalBuilder))]
		[Extension(PN.SqlServer2014,"DateDiff",      BuilderType = typeof(DateDiffIntervalBuilder))]
		[Extension(PN.MySql,        "TIMESTAMPDIFF", BuilderType = typeof(DateDiffIntervalBuilderMySql))]
		[Extension(PN.DB2,          "",              BuilderType = typeof(DateDiffIntervalBuilderDB2))]
		[Extension(PN.SapHana,      "",              BuilderType = typeof(DateDiffIntervalBuilderSapHana))]
		[Extension(PN.Firebird,		"",              BuilderType = typeof(DateDiffIntervalBuilderFirebird))]
		[Extension(PN.Sybase,		"DateDiff",      BuilderType = typeof(DateDiffIntervalBuilderSybase))]
		[Extension(PN.SQLite,       "",              BuilderType = typeof(DateDiffIntervalBuilderSQLite))]
		[Extension(PN.Oracle,       "",              BuilderType = typeof(DateDiffIntervalBuilderOracle))]
		[Extension(PN.PostgreSQL,   "",              BuilderType = typeof(DateDiffIntervalBuilderPostgreSql))]
		[Extension(PN.Access,       "",              BuilderType = typeof(DateDiffIntervalBuilderAccess))]
		[Extension(PN.ClickHouse,   "",              BuilderType = typeof(DateDiffIntervalBuilderClickHouse))]
		/* Returns the Native Database Interval type, or the Timespan Ticks (100ns) */		
		internal static TimeSpan? DateDiffInterval(DateTime? startDate, DateTime? endDate)
		{
			if (startDate == null || endDate == null)
				return null;

			return endDate - startDate;
		}

		#endregion
	}
}
