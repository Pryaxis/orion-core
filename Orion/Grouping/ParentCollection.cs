using Orion.Permissions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Orion.Grouping
{
	public class ParentCollection : ICollection
	{
		private List<int> _parents;

		public int Count
		{
			get
			{
				return ((ICollection)_parents).Count;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)_parents).IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return ((ICollection)_parents).SyncRoot;
			}
		}

		public ParentCollection()
		{
			_parents = new List<int>();
		}

		public ParentCollection(string parents)
		{
			_parents = new List<int>();

			foreach (string parent in parents.Split(','))
			{
				int ID;
				if (!Int32.TryParse(parent, out ID))
				{
					continue;
				}
				_parents.Add(ID);
			}
		}

		public ParentCollection(List<int> parents)
		{
			_parents = parents;
		}

		public void Add(int parent)
		{
			_parents.Add(parent);
		}

		public void Remove(int parent)
		{
			_parents.Remove(parent);
		}

		/// <summary>
		/// Checks if any of the groups in this collection have the given permission
		/// </summary>
		/// <param name="permission"></param>
		/// <returns></returns>
		public bool HasPermission(string permission)
		{
			foreach (int id in _parents)
			{
				if (GroupHandler.Groups[id].HasPermission(permission))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Checks if any of the groups in this collection have the given permission
		/// </summary>
		/// <param name="permission"></param>
		/// <returns></returns>
		public bool HasPermission(Permission permission)
		{
			foreach (int id in _parents)
			{
				if (GroupHandler.Groups[id].HasPermission(permission))
				{
					return true;
				}
			}

			return false;
		}

		public void CopyTo(Array array, int index)
		{
			((ICollection)_parents).CopyTo(array, index);
		}

		public IEnumerator GetEnumerator()
		{
			return ((ICollection)_parents).GetEnumerator();
		}
	}
}
