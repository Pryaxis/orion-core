using System.Data;
using MySql.Data.MySqlClient;
using Orion.Exceptions;
using Orion.Extensions;
using Orion.SQL;
using Orion.UserAccounts;
using Orion.Collections;
using Orion.Permissions;

namespace Orion.Grouping
{
	/// <summary>
	/// Provides methods to manipulate <see cref="Group"></see>s
	/// </summary>
	public class GroupHandler
	{
		private Orion _core;
		/// <summary>
		/// Orion's database connection
		/// </summary>
		public readonly IDbConnection database;

		public GroupCollection Groups;

		/// <summary>
		/// Creates a new GroupHandler instance and ensures that the database structure is correct
		/// </summary>
		/// <param name="core"></param>
		public GroupHandler(Orion core)
		{
			_core = core;
			database = core.Database;

			SqlTable table = new SqlTable("Groups",
				new SqlColumn("ID", MySqlDbType.Int32) { Primary = true, AutoIncrement = true },
				new SqlColumn("Name", MySqlDbType.VarChar, 32) { Unique = true },
				new SqlColumn("Parents", MySqlDbType.Text),
				new SqlColumn("Permissions", MySqlDbType.VarChar, 128),
				new SqlColumn("ChatColor", MySqlDbType.VarChar, 128),
				new SqlColumn("Prefix", MySqlDbType.Text),
				new SqlColumn("Suffix", MySqlDbType.Text));

			SqlTableCreator creator = new SqlTableCreator(database,
				database.GetSqlType() == SqlType.Sqlite
					? (IQueryBuilder)new SqliteQueryCreator()
					: new MysqlQueryCreator());
			creator.EnsureTableStructure(table);

			using (QueryResult result = database.QueryReader("SELECT Max(ID) from Groups"))
			{
				if (result.Read())
				{
					Groups = new GroupCollection(result.Get<int>("ID"));
				}
				else
				{
					throw new FatalException(Strings.FatalGroupInitializationException);
				}
			}

			using (QueryResult result = database.QueryReader("SELECT * FROM Groups"))
			{
				while (result.Read())
				{
					int ID = result.Get<int>("ID");
					Groups[ID].LoadFromQuery(result);
				}
			}
		}

		public bool HasPermission(Group group, string permission)
		{
			//Negated override contains
			if (group.Permissions.Negated(permission))
			{
				return false;
			}

			//Contains overrides parents
			if (group.Permissions.Contains(permission))
			{
				return true;
			}

			bool contains = false;
			foreach (int id in group.Parents)
			{
				//Any parent negate overrides any parent contains
				//So all parents MUST be checked for negated before a true can be returned
				if (Groups[id].Permissions.Negated(permission))
				{
					return false;
				}
				if (Groups[id].Permissions.Contains(permission))
				{
					contains = true;
				}
			}

			return contains;
		}

		public bool HasPermission(Group group, Permission permission)
		{
			return HasPermission(group, permission.ToString());
		}

		/// <summary>
		/// Sets a UserAccount's group based on the provided group value.
		/// <see cref="group"></see> can be group name, group ID, or a <see cref="Group"></see> object
		/// </summary>
		/// <param name="account">UserAccount to modify</param>
		/// <param name="group">name, ID, or <see cref="Group"></see> object</param>
		public void SetAccountGroup(UserAccount account, object group)
		{
			if (account == null)
			{
				throw new SetUserGroupException(Strings.SetUserGroupInvalidAccountException, "account");
			}

			if (group is string)
			{
				SetAccountGroup(account, group.ToString());
			}
			else if (group is int)
			{
				SetAccountGroup(account, (int)group);
			}
			else if (group is Group)
			{
				SetAccountGroup(account, (Group)group);
			}
			else
			{
				throw new SetUserGroupException(Strings.SetUserGroupException, "group");
			}
		}

		internal void SetAccountGroup(UserAccount account, string name)
		{
			Group g = GetGroupFromName(name);
			if (g != null)
			{
				SetAccountGroup(account, g);
			}
			else
			{
				throw new SetUserGroupException(Strings.SetUserGroupInvalidNameException, "name");
			}
		}

		internal void SetAccountGroup(UserAccount account, int id)
		{
			Group g = GetGroupFromID(id);
			if (g != null)
			{
				SetAccountGroup(account, g);
			}
			else
			{
				throw new SetUserGroupException(Strings.SetUserGroupInvalidIDException, "id");
			}
		}

		internal void SetAccountGroup(UserAccount account, Group group)
		{
			//TODO
		}

		/// <summary>
		/// Returns a <see cref="Group"></see>'s ID.
		/// Returns -1 if no groups match the provided name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public int GetGroupID(string name)
		{
			using (QueryResult result = database.QueryReader("SELECT ID FROM Groups WHERE Name=@0", name))
			{
				if (result.Read())
				{
					return result.Get<int>("ID");
				}
			}

			return -1;
		}

		/// <summary>
		/// Gets a group object based on its name (string) or ID (int)
		/// Returns null if no matches are found.
		/// </summary>
		/// <param name="nameOrID">string or int value corresponding to the group's name or ID respectively</param>
		/// <returns>Group object or null</returns>
		public Group GetGroup(object nameOrID)
		{
			if (nameOrID is string)
			{
				//Load a group using its Name as the identifier
				return GetGroupFromName(nameOrID.ToString());
			}
			else if (nameOrID is int)
			{
				//Load a group using its ID as the identifier
				return GetGroupFromID((int)nameOrID);
			}

			//Can't load from anything that isn't int or string
			return null;
		}

		/// <summary>
		/// Finds and returns a Group object based on the provided name.
		/// Returns null if no matches are found
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		internal Group GetGroupFromName(string name)
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

		/// <summary>
		/// Finds and returns a Group object based on the provided ID.
		/// Returns null if no matches are found
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		internal Group GetGroupFromID(int ID)
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
