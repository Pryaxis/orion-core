using System;
using System.Collections;
using System.Collections.Generic;

namespace Orion.Collections
{
	public class IntCollection : ICollection
	{
		internal List<int> _values;

		public int Count
		{
			get
			{
				return ((ICollection)_values).Count;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)_values).IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return ((ICollection)_values).SyncRoot;
			}
		}

		public IntCollection()
		{
			_values = new List<int>();
		}

		public IntCollection(string values)
		{
			_values = new List<int>();

			foreach (string parent in values.Split(','))
			{
				int ID;
				if (!Int32.TryParse(parent, out ID))
				{
					continue;
				}
				_values.Add(ID);
			}
		}

		public IntCollection(List<int> values)
		{
			_values = values;
		}

		public void Add(int parent)
		{
			_values.Add(parent);
		}

		public void Remove(int parent)
		{
			_values.Remove(parent);
		}

		public void CopyTo(Array array, int index)
		{
			((ICollection)_values).CopyTo(array, index);
		}

		public IEnumerator GetEnumerator()
		{
			return ((ICollection)_values).GetEnumerator();
		}
	}
}
