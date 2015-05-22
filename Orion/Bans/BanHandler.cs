using System.Data;
using MySql.Data.MySqlClient;
using Orion.Extensions;
using Orion.SQL;

namespace Orion.Bans
{
	public sealed class BanHandler
	{
		private Orion _orion;
		public readonly IDbConnection database;

		public BanHandler(Orion orion)
		{
			_orion = orion;
			database = orion.Database;

			SqlTable table = new SqlTable("Bans",
				new SqlColumn("ID", MySqlDbType.Int32) {Primary = true, AutoIncrement = true},
				new SqlColumn("Username", MySqlDbType.VarChar, 32) {Unique = true},
				new SqlColumn("UUID", MySqlDbType.VarChar, 128),
				new SqlColumn("IP", MySqlDbType.Text, 20),
				new SqlColumn("Expiration", MySqlDbType.Int64)
				);

			SqlTableCreator creator = new SqlTableCreator(database,
				database.GetSqlType() == SqlType.Sqlite
					? (IQueryBuilder)new SqliteQueryCreator()
					: new MysqlQueryCreator());
			creator.EnsureTableStructure(table);
		}
	}
}