using Orion.Permissions;
using Orion.Utilities;
using System.Collections.Generic;

namespace Orion.Grouping
{
	public class ParentCollection : IntCollection
	{
		public ParentCollection()
			:base()
		{

		}

		public ParentCollection(string parents)
			:base(parents)
		{

		}

		public ParentCollection(List<int> parents)
			:base(parents)
		{

		}

		/// <summary>
		/// Checks if any of the groups in this collection have the given permission
		/// </summary>
		/// <param name="permission"></param>
		/// <returns></returns>
		public bool HasPermission(string permission)
		{
			foreach (int id in _values)
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
			foreach (int id in _values)
			{
				if (GroupHandler.Groups[id].HasPermission(permission))
				{
					return true;
				}
			}

			return false;
		}
	}
}
