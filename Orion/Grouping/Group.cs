using Orion.Extensions;
using Orion.SQL;
using Orion.Permissions;

namespace Orion.Grouping
{
	public class Group
	{
		public int ID { get; private set; }
		public string Name { get; private set; }
		public PermissionCollection Permissions { get; private set; }
		public Color ChatColor { get; private set; }
		public string Prefix { get; private set; }
		public string Suffix { get; private set; }

		/// <summary>Default constructor for a Group object; holds no data.</summary>
		/// <returns>A Group object.</returns>
		public Group()
		{
			ID = -1;
			Name = "";
			Permissions = new PermissionCollection();
			ChatColor = Color.White;
			Prefix = "";
			Suffix = "";
		}

		public Group(string name, PermissionCollection permissions, Color chatColor, string prefix, string suffix)
		{
			Name = name;
			Permissions = permissions;
			ChatColor = chatColor;
			Prefix = prefix;
			Suffix = suffix;
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
			ChatColor = result.Get<string>("ChatColor").ToColor();
			Prefix = result.Get<string>("Prefix");
			Suffix = result.Get<string>("Suffix");
		}
	}
}
