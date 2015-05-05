using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Orion.SQL
{
	public interface IQueryBuilder
	{
		string CreateTable(SqlTable table);
		string AlterTable(SqlTable from, SqlTable to);
		string DbTypeToString(MySqlDbType type, int? length);
		string UpdateValue(string table, List<SqlValue> values, List<SqlValue> wheres);
		string InsertValues(string table, List<SqlValue> values);
		string ReadColumn(string table, List<SqlValue> wheres);
		string DeleteRow(string table, List<SqlValue> wheres);
		string RenameTable(string from, string to);
	}
}