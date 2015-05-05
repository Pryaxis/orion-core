using System.Collections.Generic;
using System.Data;
using Orion.Extensions;

namespace Orion.SQL
{
	public class SqlTableEditor
	{
		private IDbConnection database;
		private IQueryBuilder creator;

		public SqlTableEditor(IDbConnection db, IQueryBuilder provider)
		{
			database = db;
			creator = provider;
		}

		public void UpdateValues(string table, List<SqlValue> values, List<SqlValue> wheres)
		{
			database.Query(creator.UpdateValue(table, values, wheres));
		}

		public void InsertValues(string table, List<SqlValue> values)
		{
			database.Query(creator.InsertValues(table, values));
		}

		public List<object> ReadColumn(string table, string column, List<SqlValue> wheres)
		{
			List<object> values = new List<object>();

			using (QueryResult reader = database.QueryReader(creator.ReadColumn(table, wheres)))
			{
				while (reader.Read())
					values.Add(reader.Reader.Get<object>(column));
			}

			return values;
		}
	}
}