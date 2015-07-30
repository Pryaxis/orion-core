using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Permissions
{
	/// <summary>
	/// A collection of <see cref="Permission"></see>s
	/// </summary>
	public class PermissionCollection : ICollection
	{
		private List<Permission> _permissions;

		public int Count
		{
			get
			{
				return ((ICollection)_permissions).Count;
			}
		}

		public object SyncRoot
		{
			get
			{
				return ((ICollection)_permissions).SyncRoot;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)_permissions).IsSynchronized;
			}
		}

		public Permission this[int index]
		{
			get
			{
				return _permissions[index];
			}
			set
			{
				_permissions[index] = value;
			}
		}

		/// <summary>
		/// Creates a new PermissionCollection using a comma-separated string of permissions
		/// </summary>
		/// <param name="permissions"></param>
		public PermissionCollection(string permissions)
		{
			_permissions = new List<Permission>();

			foreach (string permission in permissions.Split(','))
			{
				_permissions.Add(new Permission(permission));
			}
		}

		/// <summary>
		/// Creates an empty PermissionCollection
		/// </summary>
		public PermissionCollection()
		{
			_permissions = new List<Permission>();
		}

		/// <summary>
		/// Creates a new PermissionCollection using a list of strings
		/// </summary>
		/// <param name="permissions"></param>
		public PermissionCollection(List<string> permissions)
		{
			_permissions = new List<Permission>();

			foreach (string permission in permissions)
			{
				_permissions.Add(new Permission(permission));
			}
		}

		/// <summary>
		/// Adds a new permission to the collection
		/// </summary>
		/// <param name="permission"></param>
		public void Add(string permission)
		{
			_permissions.Add(new Permission(permission));
		}

		/// <summary>
		/// Removes a permission from the collection
		/// </summary>
		/// <param name="permission"></param>
		public void Remove(string permission)
		{
			_permissions.RemoveAll(p => p.Equals(permission));
		}

		/// <summary>
		/// Removes a permission from the collection
		/// </summary>
		/// <param name="permission"></param>
		public void Remove(Permission permission)
		{
			_permissions.RemoveAll(p => p.Equals(permission));
		}

		/// <summary>
		/// Determines whether or not the collection contains a permission
		/// </summary>
		/// <param name="permission"></param>
		/// <returns></returns>
		public bool Contains(string permission)
		{
			return _permissions.Any(p => p.Equals(permission));
		}

		/// <summary>
		/// Determines whether or not the collection contains a permission
		/// </summary>
		/// <param name="permission"></param>
		/// <returns></returns>
		public bool Contains(Permission permission)
		{
			return _permissions.Any(p => p.Equals(permission));
		}

		public bool Negated(string permission)
		{
			return _permissions.Any(p => p.Equals(permission) && p.Negate);
		}

		public bool Negated(Permission permission)
		{
			return _permissions.Any(p => p.Equals(permission) && p.Negate);
		}

		public void CopyTo(Array array, int index)
		{
			((ICollection)_permissions).CopyTo(array, index);
		}

		public IEnumerator GetEnumerator()
		{
			return ((ICollection)_permissions).GetEnumerator();
		}

		public override string ToString()
		{
			return String.Join(",", _permissions.Select(p => p.ToString()));
		}
	}
}
