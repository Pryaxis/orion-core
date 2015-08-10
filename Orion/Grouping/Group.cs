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

		/// <summary>Default constructor for a Group object; holds no data.</summary>
		/// <returns>A Group object.</returns>
		public Group()
		{
			ID = -1;
			Name = "";
			Permissions = new PermissionCollection();
			Parents = new ParentCollection();
			ChatInfo = new ChatInfo();
		}

		public Group(string name, PermissionCollection permissions, ParentCollection parents, Color chatColor,
			string prefix, string suffix)
		{
			Name = name;
			Permissions = permissions;
			Parents = parents;
			ChatInfo = new ChatInfo(prefix, suffix, chatColor);
		}
		
		public void LoadFromQuery(QueryResult result)
		{
			ID = result.Get<int>("ID");
			Name = result.Get<string>("Name");
			Permissions = new PermissionCollection(result.Get<string>("Permissions"));
			Parents = new ParentCollection(result.Get<string>("Parents"));
			Color chatColor = result.Get<string>("ChatColor").ToColor();
			string prefix = result.Get<string>("Prefix");
			string suffix = result.Get<string>("Suffix");
			ChatInfo = new ChatInfo(prefix, suffix, chatColor);
		}
	}
}
