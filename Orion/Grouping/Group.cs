using System.Data.Odbc;
using Orion.Extensions;
using Orion.SQL;
using Orion.Permissions;
using Orion.Collections;

namespace Orion.Grouping
{
	/// <summary>
	/// Class that assigns information to a set of <see cref="UserAccount"></see>s
	/// </summary>
	public class Group
	{
		/// <summary>
		/// Group ID
		/// </summary>
		public int ID { get; private set; }
		/// <summary>
		/// Group name
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Group permissions
		/// </summary>
		public PermissionCollection Permissions { get; private set; }
		/// <summary>
		/// Group parents
		/// </summary>
		public ParentCollection Parents { get; private set; }
		/// <summary>
		/// Group chat info
		/// </summary>
		public ChatInfo ChatInfo { get; private set; }

		/// <summary>
		/// Default constructor for a Group object; holds no data.
		/// </summary>
		public Group()
		{
			ID = -1;
			Name = "";
			Permissions = new PermissionCollection();
			Parents = new ParentCollection();
			ChatInfo = new ChatInfo();
		}

	    /// <summary>
	    /// Creates a group with various options.
	    /// </summary>
	    /// <param name="name">The name of the group.</param>
	    /// <param name="permissions">The permissions for the group.</param>
	    /// <param name="parents">The parents for the group.</param>
	    /// <param name="chatColor">The group's chat color as it appears in game.</param>
	    /// <param name="prefix">The group's prefix as it appears in game.</param>
	    /// <param name="suffix">The group's suffix as it appears in game.</param>
	    /// <param name="id">The group's ID.</param>
	    public Group(string name, PermissionCollection permissions = null, ParentCollection parents = null, Color? chatColor = null,
			string prefix = "", string suffix = "", int id = -1)
	    {
	        ID = id;
			Name = name;
            Permissions = permissions ?? new PermissionCollection();
			Parents = parents ?? new ParentCollection();
	        if (!chatColor.HasValue)
	            chatColor = Color.White;
			ChatInfo = new ChatInfo(prefix, suffix, chatColor.Value);
		}

        /// <summary>
        /// Creates a group with a specified name. The group will contain no permissions, parents, or special chat options.
        /// </summary>
        /// <param name="name">The name of the group.</param>
	    public Group(string name)
	    {
	        Name = name;
            Permissions = new PermissionCollection();
            Parents = new ParentCollection();
            ChatInfo = new ChatInfo();
        }
		
		public static Group LoadFromQuery(QueryResult result)
		{
			var id = result.Get<int>("ID");
			var name = result.Get<string>("Name");
			var permissions = new PermissionCollection(result.Get<string>("Permissions"));
			var parents = new ParentCollection(result.Get<string>("Parents"));
			Color chatColor = result.Get<string>("ChatColor").ToColor();
			string prefix = result.Get<string>("Prefix");
			string suffix = result.Get<string>("Suffix");
		    return new Group(name, permissions, parents, chatColor, prefix, suffix, id);
		}
	}
}
