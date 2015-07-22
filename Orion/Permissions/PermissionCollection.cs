using System.Collections.Generic;

namespace Orion.Permissions
{
	public class PermissionCollection
	{
		private List<Permission> _permissions;

		public PermissionCollection(string permissions)
		{
			_permissions = new List<Permission>();

			foreach (string permission in permissions.Split(','))
			{
				_permissions.Add(new Permission(permission));
			}
		}

		public PermissionCollection()
		{
			_permissions = new List<Permission>();
		}

		public PermissionCollection(List<string> permissions)
		{
			_permissions = new List<Permission>();

			foreach (string permission in permissions)
			{
				_permissions.Add(new Permission(permission));
			}
		}

		public void Add(string permission)
		{
			_permissions.Add(new Permission(permission));
		}

		public void Remove(string permission)
		{
			Permission p = new Permission(permission);
			_permissions.RemoveAll(perm => perm.Name == p.Name);
		}
	}
}
