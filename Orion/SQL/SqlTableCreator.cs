using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using Orion.Extensions;

namespace Orion.SQL
{
	public class SqlTableCreator
	{
		private IDbConnection database;
		private IQueryBuilder creator;

		public SqlTableCreator(IDbConnection db, IQueryBuilder provider)
		{
			database = db;
			creator = provider;
		}

		/// <summary>
		/// Ensures a table exists and matches the given structure.
		/// </summary>
		/// <param name="table">table to create</param>
		/// <returns>Returns true if the table was created</returns>
		public bool EnsureTableStructure(SqlTable table)
		{
			List<string> columns = GetColumns(table);
			if (columns.Count > 0)
			{
				if (!table.Columns.All(c => columns.Contains(c.Name)) || !columns.All(c => table.Columns.Any(c2 => c2.Name == c)))
				{
					SqlTable from = new SqlTable(table.Name, columns.Select(s => new SqlColumn(s, MySqlDbType.String)).ToList());
					database.Query(creator.AlterTable(from, table));
				}
			}
			else
			{
				database.Query(creator.CreateTable(table));
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns the names of the columns in the given table
		/// </summary>
		/// <param name="table">table to retrieve column names from</param>
		/// <returns>List of column names</returns>
		public List<string> GetColumns(SqlTable table)
		{
			List<string> ret = new List<string>();
			SqlType name = database.GetSqlType();
			if (name == SqlType.Sqlite)
			{
				using (QueryResult reader = database.QueryReader(String.Format("PRAGMA table_info({0})", table.Name)))
				{
					while (reader.Read())
						ret.Add(reader.Get<string>("name"));
				}
			}
			else if (name == SqlType.Mysql)
			{
				using (
					QueryResult reader =
						database.QueryReader(
							"SELECT COLUMN_NAME FROM information_schema.COLUMNS WHERE TABLE_NAME=@0 AND TABLE_SCHEMA=@1", table.Name,
							database.Database))
				{
					while (reader.Read())
						ret.Add(reader.Get<string>("COLUMN_NAME"));
				}
			}
			else
			{
				throw new NotSupportedException();
			}

			return ret;
		}

		/// <summary>
		/// Deletes a row from the database
		/// </summary>
		/// <param name="table"></param>
		/// <param name="wheres"></param>
		public void DeleteRow(string table, List<SqlValue> wheres)
		{
			database.Query(creator.DeleteRow(table, wheres));
		}
	}
}
