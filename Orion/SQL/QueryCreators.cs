using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Orion.Extensions;

namespace Orion.SQL
{
	public class SqliteQueryCreator : GenericQueryCreator, IQueryBuilder
	{
		public override string CreateTable(SqlTable table)
		{
			IEnumerable<string> columns =
				table.Columns.Select(
					c =>
						String.Format("'{0}' {1} {2} {3} {4}", c.Name,
							DbTypeToString(c.Type, c.Length),
							c.Primary ? "PRIMARY KEY" : "",
							c.AutoIncrement ? "AUTOINCREMENT" : "",
							c.NotNull ? "NOT NULL" : ""));
			List<string> uniques = table.Columns.Where(c => c.Unique).Select(c => c.Name).ToList();
			return String.Format("CREATE TABLE {0} ({1} {2})", EscapeTableName(table.Name),
				string.Join(", ", columns),
				uniques.Any() ? String.Format(", UNIQUE({0})", string.Join(", ", uniques)) : "");
		}

		public override string RenameTable(string from, string to)
		{
			return String.Format("ALTER TABLE {0} RENAME TO {1}", from, to);
		}

		private static readonly Dictionary<MySqlDbType, string> TypesAsStrings = new Dictionary<MySqlDbType, string>
		{
			{MySqlDbType.VarChar, "TEXT"},
			{MySqlDbType.String, "TEXT"},
			{MySqlDbType.Text, "TEXT"},
			{MySqlDbType.TinyText, "TEXT"},
			{MySqlDbType.MediumText, "TEXT"},
			{MySqlDbType.LongText, "TEXT"},
			{MySqlDbType.Float, "REAL"},
			{MySqlDbType.Double, "REAL"},
			{MySqlDbType.Int32, "INTEGER"},
			{MySqlDbType.Blob, "BLOB"},
			{MySqlDbType.Int64, "BIGINT"},
		};

		public string DbTypeToString(MySqlDbType type, int? length)
		{
			string ret;
			if (TypesAsStrings.TryGetValue(type, out ret))
				return ret;
			throw new NotImplementedException(Enum.GetName(typeof (MySqlDbType), type));
		}

		protected override string EscapeTableName(string table)
		{
			return String.Format("`{0}`", table);
		}
	}

	public class MysqlQueryCreator : GenericQueryCreator, IQueryBuilder
	{
		public override string CreateTable(SqlTable table)
		{
			IEnumerable<string> columns =
				table.Columns.Select(
					c =>
						String.Format("{0} {1} {2} {3} {4}", c.Name, DbTypeToString(c.Type, c.Length), c.Primary ? "PRIMARY KEY" : "",
							c.AutoIncrement ? "AUTO_INCREMENT" : "", c.NotNull ? "NOT NULL" : ""));
			List<string> uniques = table.Columns.Where(c => c.Unique).Select(c => c.Name).ToList();
			return String.Format("CREATE TABLE {0} ({1} {2})", EscapeTableName(table.Name), string.Join(", ", columns),
				uniques.Any()
					? String.Format(", UNIQUE({0})", string.Join(", ", uniques))
					: "");
		}

		public override string RenameTable(string from, string to)
		{
			return String.Format("RENAME TABLE {0} TO {1}" ,from, to);
		}

		private static readonly Dictionary<MySqlDbType, string> TypesAsStrings = new Dictionary<MySqlDbType, string>
		{
			{MySqlDbType.VarChar, "VARCHAR"},
			{MySqlDbType.String, "CHAR"},
			{MySqlDbType.Text, "TEXT"},
			{MySqlDbType.TinyText, "TINYTEXT"},
			{MySqlDbType.MediumText, "MEDIUMTEXT"},
			{MySqlDbType.LongText, "LONGTEXT"},
			{MySqlDbType.Float, "FLOAT"},
			{MySqlDbType.Double, "DOUBLE"},
			{MySqlDbType.Int32, "INT"},
			{MySqlDbType.Int64, "BIGINT"},
		};

		public string DbTypeToString(MySqlDbType type, int? length)
		{
			string ret;
			if (TypesAsStrings.TryGetValue(type, out ret))
				return ret + (length != null ? String.Format("({0})", (int) length) : "");
			throw new NotImplementedException(Enum.GetName(typeof (MySqlDbType), type));
		}

		protected override string EscapeTableName(string table)
		{
			return String.Format("`{0}`", table);
		}
	}

	public abstract class GenericQueryCreator
	{
		protected static Random rand = new Random();
		protected abstract string EscapeTableName(string table);
		public abstract string CreateTable(SqlTable table);
		public abstract string RenameTable(string from, string to);

		/// <summary>
		/// Alter a table from source to destination
		/// </summary>
		/// <param name="from">Must have name and column names. Column types are not required</param>
		/// <param name="to">Must have column names and column types.</param>
		/// <returns></returns>
		public string AlterTable(SqlTable from, SqlTable to)
		{
			string rstr = rand.NextString(20);
			string escapedTable = EscapeTableName(from.Name);
			string tmpTable = EscapeTableName(String.Format("{0}_{1}", rstr, from.Name));
			string alter = RenameTable(escapedTable, tmpTable);
			string create = CreateTable(to);
			// combine all columns in the 'from' variable excluding ones that aren't in the 'to' variable.
			// exclude the ones that aren't in 'to' variable because if the column is deleted, why try to import the data?
			string columns = string.Join(", ", from.Columns.Where(c => to.Columns.Any(c2 => c2.Name == c.Name)).Select(c => c.Name));
			string insert = String.Format("INSERT INTO {0} ({1}) SELECT {1} FROM {2}", escapedTable, columns, tmpTable);
			string drop = String.Format("DROP TABLE {0}", tmpTable);
			return String.Format("{0}; {1}; {2}; {3};", alter, create, insert, drop);
		}

		public string DeleteRow(string table, List<SqlValue> wheres)
		{
			return String.Format("DELETE FROM {0} {1}", EscapeTableName(table), BuildWhere(wheres));
		}

		public string UpdateValue(string table, List<SqlValue> values, List<SqlValue> wheres)
		{
			if (0 == values.Count)
				throw new ArgumentException("No values supplied");

			return String.Format("UPDATE {0} SET {1} {2}", EscapeTableName(table),
				string.Join(", ", values.Select(v => v.Name + " = " + v.Value)), BuildWhere(wheres));
		}

		public string ReadColumn(string table, List<SqlValue> wheres)
		{
			return String.Format("SELECT * FROM {0} {1}", EscapeTableName(table), BuildWhere(wheres));
		}

		public string InsertValues(string table, List<SqlValue> values)
		{
			string namesStr = string.Join(", ", values.Select(v => v.Name));
			string valuesStr = string.Join(", ", values.Select(v => v.Value));

			return String.Format("INSERT INTO {0} ({1}) VALUES ({2})", EscapeTableName(table), namesStr, valuesStr);
		}

		protected static string BuildWhere(List<SqlValue> wheres)
		{
			if (0 == wheres.Count)
				return string.Empty;

			return String.Format("WHERE {0}", string.Join(", ", wheres.Select(v => v.Name + " = " + v.Value)));
		}
	}
}