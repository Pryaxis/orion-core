using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using Orion.Exceptions;
using Orion.Extensions;
using Orion.SQL;
using Orion.Utilities;

namespace Orion.UserAccounts
{
	public sealed class UserAccountHandler
	{
		private Orion _core;

		public readonly IDbConnection database;
		/// <summary>
		/// Ordered cache of user accounts
		/// </summary>
		public OrderedCache<UserAccount> UserCache { get; private set; } 

		public UserAccountHandler(Orion core)
		{
			_core = core;
			database = core.Database;

			SqlTable table = new SqlTable("Users",
				new SqlColumn("ID", MySqlDbType.Int32) {Primary = true, AutoIncrement = true},
				new SqlColumn("Username", MySqlDbType.VarChar, 32) {Unique = true},
				new SqlColumn("Password", MySqlDbType.VarChar, 128),
				new SqlColumn("UUID", MySqlDbType.VarChar, 128),
				new SqlColumn("Usergroup", MySqlDbType.Text),
				new SqlColumn("Registered", MySqlDbType.Text),
				new SqlColumn("LastAccessed", MySqlDbType.Text),
				new SqlColumn("KnownIPs", MySqlDbType.Text)
				);

			SqlTableCreator creator = new SqlTableCreator(database,
				database.GetSqlType() == SqlType.Sqlite
					? (IQueryBuilder) new SqliteQueryCreator()
					: new MysqlQueryCreator());
			creator.EnsureTableStructure(table);

			UserCache = new OrderedCache<UserAccount>(core.Config.MaxUserCacheSize);
			UserCache.FlushEvent += OnFlush;
			UserCache.RemoveEvent += OnRemove;
		}

		private void OnRemove(UserAccount user)
		{
			Sync(user);
		}

		private bool OnFlush(IEnumerable<UserAccount> users)
		{
			foreach (UserAccount user in users)
			{
				Sync(user);
			}

			return true;
		}

		/// <summary>
		/// Syncs an existing user's state to the database
		/// </summary>
		/// <param name="user">user object to sync</param>
		/// <returns>true if successfully syncd</returns>
		/// <exception cref="UserNotFoundException">Thrown if the user does not exist in the database</exception>
		/// <exception cref="UserHandlerException">Thrown if an unexpected exception occurs</exception>
		public bool Sync(UserAccount user)
		{
			try
			{
				if (database.Query("UPDATE Users SET Password = @0, Usergroup = @1, UUID = @2 WHERE ID = @3",
					user.Password, user.Group.Name, user.UUID, user.ID) == 0)
				{
					throw new UserNotFoundException(user.Name);
				}

				return true;
			}
			catch (Exception ex)
			{
				throw new UserHandlerException(
					String.Format("UserHandler.Sync returned an error for user {0} ({1})", user.Name, user.ID), ex);
			}
		}

		/// <summary>
		/// Sets password, group, and/or uuid of a user
		/// </summary>
		/// <param name="id">User's ID</param>
		/// <param name="password">new password. Null to skip</param>
		/// <param name="group">new Group. Null to skip</param>
		/// <param name="uuid">new UUID. Null to skip</param>
		/// <returns></returns>
		public bool SetParameters(int id, string password = null, string group = null, string uuid = null)
		{
			if (id < 0)
			{
				throw new ArgumentOutOfRangeException("id", Strings.UserIDLessThanZero);
			}

			//Don't want people setting everything to null
			if (string.IsNullOrEmpty(password) && string.IsNullOrEmpty(group) && string.IsNullOrEmpty(uuid))
			{
				throw new UserHandlerException(Strings.UserHandlerSetParameterInvalidArgs);
			}

			//If we can find the user in the cache, we can simply update that object and let it sync when the cache flushes.
			UserAccount cacheUser = UserCache.FirstOrDefault(o => o.ID == id);
			if (cacheUser != null)
			{
				if (!string.IsNullOrEmpty(password))
				{
					cacheUser.Password = password;
				}
				if (!string.IsNullOrEmpty(group))
				{
					_core.Groups.SetAccountGroup(cacheUser, group);
				}
				if (!string.IsNullOrEmpty(uuid))
				{
					cacheUser.UUID = uuid;
				}

				return true;
			}
			
			Dictionary<string, string> data = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(password))
			{
				data.Add("Password", password);
			}
			if (!string.IsNullOrEmpty(group))
			{
				data.Add("Usergroup", group);
			}
			if (!string.IsNullOrEmpty(uuid))
			{
				data.Add("UUID", uuid);
			}

			try
			{
				if (database.Query(String.Format("UPDATE Users SET {0} WHERE ID = @0",
					string.Join(" AND ", data.Select(kv => kv.Key + " = " + kv.Value))), uuid) == 0)
				{
					throw new UserNotFoundException(GetUserById(id).Name);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw new UserHandlerException(String.Format(Strings.UserHandlerSetParameterUserIDError, id), ex);
			}
		}

		/// <summary>
		/// Gets all users from the database with optional limits on how many to retrieve at once
		/// </summary>
		/// <param name="name"></param>
		/// <param name="group"></param>
		/// <param name="useCache">true (default) to grab users from the cache instead of from the database. 
		/// If no users are retrieved from the cache, the database will be queried</param>
		/// <param name="count">number of users to retrieve. 0 for all</param>
		/// <param name="offset">number of users to skip. 0 for none</param>
		/// <returns>List of users</returns>
		public List<UserAccount> GetUsers(string name = null, string group = null, 
			bool useCache = true, int count = 0, int offset = 0)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}

			List<UserAccount> ret = new List<UserAccount>();

			if (useCache)
			{
				if (string.IsNullOrEmpty(name))
				{
					if (string.IsNullOrEmpty(group))
					{
						ret = UserCache.Skip(offset).Where(u => u != null).ToList();
					}
					else
					{
						ret = UserCache.Skip(offset).Where(u =>
							string.Equals(u.GroupName, group, StringComparison.InvariantCultureIgnoreCase)).ToList();
					}
				}
				else if (string.IsNullOrEmpty(group))
				{
					UserAccount user = UserCache.Skip(offset).FirstOrDefault(u => u.Name == name);
					if (user == null)
					{
						ret = UserCache.Skip(offset).Where(u => u.Name.Contains(name)).ToList();
					}
					else
					{
						ret =  new List<UserAccount>{user};
					}
				}

				if (ret.Count > 0)
				{
					return ret;
				}
			}

			string query = "SELECT * FROM Users";

			if (!string.IsNullOrEmpty(name))
			{
				query += " WHERE Username LIKE @0";
			}
			if (!string.IsNullOrEmpty(group))
			{
				if (!string.IsNullOrEmpty(name))
				{
					query += " AND Usergroup = @1";
				}
				else
				{
					query += " WHERE Usergroup = @0";
				}
			}

			if (count > 0)
			{
				query += String.Format(" LIMIT {0},{1}", offset, count);
			}

			using (QueryResult result = database.QueryReader(query))
			{
				while (result.Read())
				{
					UserAccount user = new UserAccount();
					user.LoadFromQuery(result);
					user.Group = _core.Groups.GetGroupFromName(user.GroupName);
					UserCache.Push(user);
					ret.Add(user);
				}
			}

			return ret;
		}

		/// <summary>
		/// Gets all users from the database or cache that match <see cref="name"/>, 
		/// with optional limits on how many to retrieve at once
		/// </summary>
		/// <param name="name">string to search for</param>
		/// <param name="count">number of users to retrieve. 0 for all</param>
		/// <param name="offset">number of users to skip. 0 for none</param>
		/// <returns>List of users</returns>
		public List<UserAccount> GetUsersByName(string name, int count = 0, int offset = 0)
		{
			return GetUsers(name, null, true, count, offset);
		}

		/// <summary>
		/// Gets all users in group <see cref="group"/> from the database or the cache
		/// </summary>
		/// <param name="group">group that users are in</param>
		/// <param name="count">number of users to retrieve. 0 for all</param>
		/// <param name="offset">number of users to skip. 0 for none</param>
		/// <returns>List of users</returns>
		public List<UserAccount> GetUsersByGroup(string group, int count = 0, int offset = 0)
		{
			return GetUsers(null, group, true, count, offset);
		}

		/// <summary>
		/// Gets a user from the database with ID <see cref="id"/>
		/// </summary>
		/// <param name="id">User ID</param>
		/// <returns>User</returns>
		public UserAccount GetUserById(int id)
		{
			UserAccount user = null;
			using (QueryResult result = database.QueryReader("SELECT * FROM Users WHERE ID = @0", id))
			{
				if (result.Read())
				{
					user = new UserAccount();
					user.LoadFromQuery(result);
					UserCache.Push(user);
				}
			}

			if (user != null)
			{
				user.Group = _core.Groups.GetGroupFromName(user.GroupName);
			}

			return user;
		}
	}
}