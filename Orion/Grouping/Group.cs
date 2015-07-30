using Orion.Extensions;
using Orion.SQL;
using Orion.Permissions;

namespace Orion.Grouping
{
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
			ChatInfo = new ChatInfo();
		}

		public Group(string name, PermissionCollection permissions, Color chatColor, string prefix, string suffix)
		{
			Name = name;
			Permissions = permissions;
			ChatInfo = new ChatInfo(prefix, suffix, chatColor);
		}

		/// <summary>
		/// Checks if this group's permissions contains the given permission
		/// </summary>
		/// <param name="permission"></param>
		/// <returns></returns>
		public bool HasPermission(string permission)
		{
			if (Permissions.Negated(permission))
			{
				return false;
			}

			return Permissions.Contains(permission);
		}
		
		public void LoadFromQuery(QueryResult result)
		{
			ID = result.Get<int>("ID");
			Name = result.Get<string>("Name");
			Permissions = new PermissionCollection(result.Get<string>("Permissions"));

			Color chatColor = result.Get<string>("ChatColor").ToColor();
			string prefix = result.Get<string>("Prefix");
			string suffix = result.Get<string>("Suffix");
			ChatInfo = new ChatInfo(prefix, suffix, chatColor);
		}
	}
}
