using System.Dynamic;
using System.Collections.Generic;
using Orion.SQL;
using Orion.Permissions;

namespace Orion.UserAccounts
{
	public sealed class UserAccount
	{
		/// <summary>The database ID of the user.</summary>
		public int ID { get; set; }

		/// <summary>The user's name.</summary>
		public string Name { get; set; }

		/// <summary>The hashed password for the user.</summary>
		public string Password { get; internal set; }

		/// <summary>The user's saved Univerally Unique Identifier token.</summary>
		public string UUID { get; set; }

		/// <summary>The group object that the user is a part of.</summary>
		public string Group { get; set; }

		public PermissionCollection Permissions { get; set; }

		/// <summary>The unix epoch corresponding to the registration date of the user.</summary>
		public string Registered { get; set; }

		/// <summary>The unix epoch corresponding to the last access date of the user.</summary>
		public string LastAccessed { get; set; }

		/// <summary>A JSON serialized list of known IP addresses for a user.</summary>
		public string KnownIps { get; set; }

		public dynamic Extensions { get; private set; }

		/// <summary>Constructor for the user object, assuming you define everything yourself.</summary>
		/// <param name="name">The user's name.</param>
		/// <param name="pass">The user's password hash.</param>
		/// <param name="uuid">The user's UUID.</param>
		/// <param name="group">The user's group name.</param>
		/// <param name="registered">The unix epoch for the registration date.</param>
		/// <param name="last">The unix epoch for the last access date.</param>
		/// <param name="known">The known IPs for the user, serialized as a JSON object</param>
		/// <returns>A completed user object.</returns>
		public UserAccount(string name, string pass, string uuid, string group, PermissionCollection permissions, string registered, string last, string known)
		{
			Name = name;
			Password = pass;
			UUID = uuid;
			Group = group;
			Permissions = permissions;
			Registered = registered;
			LastAccessed = last;
			KnownIps = known;
			Extensions = new ExpandoObject();
		}

		/// <summary>Default constructor for a user object; holds no data.</summary>
		/// <returns>A user object.</returns>
		public UserAccount()
		{
			Name = "";
			Password = "";
			UUID = "";
			Group = "";
			Permissions = new PermissionCollection();
			Registered = "";
			LastAccessed = "";
			KnownIps = "";
		}

		public static UserAccount LoadFromQuery(QueryResult result)
		{
			UserAccount user = new UserAccount
			{
				ID = result.Get<int>("ID"),
				Group = result.Get<string>("Usergroup"),
				Password = result.Get<string>("Password"),
				UUID = result.Get<string>("UUID"),
				Name = result.Get<string>("Username"),
				Registered = result.Get<string>("Registered"),
				LastAccessed = result.Get<string>("LastAccessed"),
				KnownIps = result.Get<string>("KnownIps")
			};
			return user;
		}
	}
}