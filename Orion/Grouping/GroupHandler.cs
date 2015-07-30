using System.Data;
using MySql.Data.MySqlClient;
using Orion.Exceptions;
using Orion.Extensions;
using Orion.SQL;
using Orion.UserAccounts;

namespace Orion.Grouping
{
	public class GroupHandler
	{
		private Orion _core;
		public readonly IDbConnection database;

		public GroupHandler(Orion core)
		{
			_core = core;
			database = core.Database;

			SqlTable table = new SqlTable("Groups",
				new SqlColumn("ID", MySqlDbType.Int32) { Primary = true, AutoIncrement = true },
				new SqlColumn("Name", MySqlDbType.VarChar, 32) { Unique = true },
				new SqlColumn("Permissions", MySqlDbType.VarChar, 128),
				new SqlColumn("ChatColor", MySqlDbType.VarChar, 128),
				new SqlColumn("Prefix", MySqlDbType.Text),
				new SqlColumn("Suffix", MySqlDbType.Text)
				);

			SqlTableCreator creator = new SqlTableCreator(database,
				database.GetSqlType() == SqlType.Sqlite
					? (IQueryBuilder)new SqliteQueryCreator()
					: new MysqlQueryCreator());
			creator.EnsureTableStructure(table);
		}

		public void SetAccountGroup(UserAccount account, string groupName)
		{
			Group g = GetGroupFromName(groupName);
			if (g != null)
			{
				account.Group = g;
			}
			else
			{
				throw new GroupNotFoundException(groupName);
			}
		}
		
		public Group GetGroupFromName(string name)
		{
			using (QueryResult result = database.QueryReader("SELECT * FROM Groups WHERE Name=@0", name))
			{
				if (result.Read())
				{
					Group g = new Group();
					g.LoadFromQuery(result);
					return g;
				}
			}

			return null;
		}

		public Group GetGroupFromID(int ID)
		{
			using (QueryResult result = database.QueryReader("SELECT * FROM Groups WHERE ID=@0", ID))
			{
				if (result.Read())
				{
					Group g = new Group();
					g.LoadFromQuery(result);
					return g;
				}
			}

			return null;
		}
	}
}
