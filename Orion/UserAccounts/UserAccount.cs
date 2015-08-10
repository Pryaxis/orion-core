using Orion.SQL;
using Orion.Grouping;
using Orion.Extensions;
using Orion.Permissions;
using Orion.Collections;

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
		public Group Group { get; set; }

		/// <summary>
		/// Used internally to load Group
		/// </summary>
		internal int GroupID { get; set; }

		/// <summary>
		/// The user's Chat data
		/// </summary>
		public ChatInfo ChatInfo { get; set; }

		/// <summary>
		/// The user's permissions
		/// </summary>
		public PermissionCollection Permissions { get; set; }

		/// <summary>The unix epoch corresponding to the registration date of the user.</summary>
		public string Registered { get; set; }

		/// <summary>The unix epoch corresponding to the last access date of the user.</summary>
		public string LastAccessed { get; set; }

		/// <summary>A JSON serialized list of known IP addresses for a user.</summary>
		public string KnownIps { get; set; }

		/// <summary>Constructor for the user object, assuming you define everything yourself.</summary>
		/// <param name="name">The user's name.</param>
		/// <param name="pass">The user's password hash.</param>
		/// <param name="uuid">The user's UUID.</param>
		/// <param name="group">The user's group.</param>
		/// <param name="registered">The unix epoch for the registration date.</param>
		/// <param name="last">The unix epoch for the last access date.</param>
		/// <param name="known">The known IPs for the user, serialized as a JSON object</param>
		/// <returns>A completed user object.</returns>
		public UserAccount(string name, string pass, string uuid, Group group, PermissionCollection permissions, string registered, string last, string known)
		{
			Name = name;
			Password = pass;
			UUID = uuid;
			Group = group;
			GroupID = group.ID;
			ChatInfo = Group.ChatInfo;
			Permissions = permissions;
			Registered = registered;
			LastAccessed = last;
			KnownIps = known;
		}

		/// <summary>Default constructor for a UserAccount object; holds no data.</summary>
		/// <returns>A UserAccount object.</returns>
		public UserAccount()
		{
			Name = "";
			Password = "";
			UUID = "";
			Group = new Group();
			GroupID = -1;
			ChatInfo = Group.ChatInfo;
			Permissions = new PermissionCollection();
			Registered = "";
			LastAccessed = "";
			KnownIps = "";
		}

		/// <summary>
		/// Used internally to load a User Account from a database query
		/// </summary>
		/// <param name="result"></param>
		internal void LoadFromQuery(QueryResult result)
		{
			ID = result.Get<int>("ID");
			GroupID = result.Get<int>("GroupID");
			Password = result.Get<string>("Password");
			UUID = result.Get<string>("UUID");
			Name = result.Get<string>("Username");
			Registered = result.Get<string>("Registered");
			LastAccessed = result.Get<string>("LastAccessed");
			KnownIps = result.Get<string>("KnownIps");

			Color chatColor = result.Get<string>("ChatColor").ToColor();
			string prefix = result.Get<string>("Prefix");
			string suffix = result.Get<string>("Suffix");
			ChatInfo = new ChatInfo(prefix, suffix, chatColor);
		}
	}
}