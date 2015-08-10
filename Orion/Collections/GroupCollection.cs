using System;
using System.Collections;

using Orion.Grouping;

namespace Orion.Collections
{
	public class GroupCollection : ICollection
	{
		private Group[] _groups;

		public int Count
		{
			get
			{
				return ((ICollection)_groups).Count;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return _groups.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return _groups.SyncRoot;
			}
		}

		public Group this[int index]
		{
			get
			{
				if (index < 0 || index > _groups.Length)
				{
					throw new IndexOutOfRangeException("index");
				}
				return _groups[index];
			}
			set
			{
				if (index < 0 || index > _groups.Length)
				{
					throw new IndexOutOfRangeException("index");
				}
				_groups[index] = value;
			}
		}

		public GroupCollection(int length)
		{
			_groups = new Group[length];

			//Initialize each index
			for (int i = 0; i < length; i++)
			{
				_groups[i] = new Group();
			}
		}

		public void CopyTo(Array array, int index)
		{
			_groups.CopyTo(array, index);
		}

		public IEnumerator GetEnumerator()
		{
			return _groups.GetEnumerator();
		}
	}
}
