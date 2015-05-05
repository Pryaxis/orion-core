using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Orion.Utilities
{
	/// <summary>
	/// A class for ordered caching of objects
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class OrderedCache<T> : IEnumerable<T>
	{
		/// <summary>
		/// Cached objects
		/// </summary>
		private List<CacheObject> _cache;
		/// <summary>
		/// Max size of the cache
		/// </summary>
		private readonly int _maxSize;
		/// <summary>
		/// Whether or not the cache needs sorting
		/// </summary>
		private bool _sort;

		private int _clearCount;
		private int _flushInterval;

		public int MaxSize { get { return _maxSize; } }

		/// <summary>
		/// How often in milliseconds the cache will be flushed
		/// </summary>
		public int FlushInterval
		{
			private get {return _flushInterval;}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value", "FlushInterval cannot be less than 1.");
				}
				_flushInterval = value;
			}
		}

		/// <summary>
		/// How many objects will be cleared from the cache each flush
		/// </summary>
		public int ClearCount
		{
			private get { return _clearCount; }
			set
			{
				if (value < 0 || value > _maxSize)
				{
					throw new ArgumentOutOfRangeException("value",
						"ClearCount cannot be less than 0 or greater than the size of the cache.");
				}

				_clearCount = value;
			}
		}

		public delegate bool FlushCallBack(IEnumerable<T> values);

		/// <summary>
		/// Event that is raised when the cache is flushed
		/// </summary>
		public event FlushCallBack FlushEvent;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="maxSize">Max size of the cache</param>
		public OrderedCache(int maxSize)
		{
			_maxSize = maxSize;
			_cache = new List<CacheObject>(maxSize);

			ThreadPool.QueueUserWorkItem(WaitAndFlush);
		}

		/// <summary>
		/// Get or set a CacheObject through indexing
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public T this[int index]
		{
			get
			{
				if (index >= _cache.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}

				_cache[index].count++;
				_sort = true;

				return _cache[index].value;
			}

			set
			{
				if (index >= _cache.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}

				_cache[index].value = value;
			}
		}

		/// <summary>
		/// Sorts the cache based on the number of times each stored object has been accessed
		/// </summary>
		/// <param name="newState"></param>
		public void Sort(bool newState = false)
		{
			if (_sort)
			{
				_cache.Sort(Comparison);
			}
			_sort = newState;
		}

		/// <summary>
		/// Removes the oldest value in the cache and returns it
		/// </summary>
		/// <returns></returns>
		public T Pop()
		{
			Sort();

			T obj = _cache[_cache.Count - 1].value;
			_cache.RemoveAt(_cache.Count - 1);
			return obj;
		}

		/// <summary>
		/// Pushes a new value to the bottom of the cache
		/// </summary>
		/// <param name="value"></param>
		public void Push(T value)
		{
			Sort();

			if (_cache.Count > _maxSize)
			{
				_cache.RemoveAt(0);
			}

			_cache.Insert(0, new CacheObject { value = value, count = 0 });
		}

		/// <summary>
		/// Pushes multiple new values to the bottom of the cache
		/// </summary>
		/// <param name="values"></param>
		public void PushMany(List<T> values)
		{
			Sort();

			if (_cache.Count + values.Count > _maxSize)
			{
				_cache.RemoveRange(0, (_cache.Count + values.Count) - _maxSize);
			}

			_cache.InsertRange(0, values.Select(val => new CacheObject { value = val, count = 0 }));
		}

		public T FirstOrDefault(Predicate<T> predicate)
		{
			for (int i = 0; i < _cache.Count; i++)
			{
				if (predicate(_cache[i].value))
				{
					_sort = true;
					_cache[i].count++;
					return _cache[i].value;
				}
			}

			return default(T);
		}

		public IEnumerable<T> Where(Predicate<T> predicate)
		{
			List<T> wheres = new List<T>();

			//This is MUCH faster than using linq
			for (int i = 0; i < _cache.Count; i++)
			{
				if (predicate(_cache[i].value))
				{
					wheres.Add(_cache[i].value);
					_cache[i].count++;
				}
			}

			Sort();

			return wheres;
		}

		/// <summary>
		/// Flushes the cache and raises the OnFlush event
		/// </summary>
		/// <param name="clear">number of objects to clear</param>
		public void Flush(int clear = 10)
		{
			if (_cache.Count == 0)
			{
				OnFlush(_cache.Select(c => c.value));
				return;
			}

			if (clear >= _cache.Count)
			{
				clear = _cache.Count - 1;
			}

			Sort();

			if (OnFlush(_cache.Select(c => c.value)))
			{
				_cache.RemoveRange(0, clear);
			}
		}

		private bool OnFlush(IEnumerable<T> values)
		{
			if (FlushEvent == null)
			{
				return false;
			}

			return FlushEvent(values);
		}

		/// <summary>
		/// ThreadPool worker object that flushes the cache every <see cref="FlushInterval"/>
		/// </summary>
		/// <param name="sender"></param>
		private void WaitAndFlush(object sender)
		{
			Thread.Sleep(FlushInterval);
			Flush(ClearCount);
		}

		private int Comparison(CacheObject o1, CacheObject o2)
		{
			if (o1 == null)
			{
				if (o2 == null)
				{
					return 0;
				}

				return -1;
			}

			if (o2 == null)
			{
				return 1;
			}

			if (o1.count > o2.count)
			{
				return 1;
			}

			if (o1.count < o2.count)
			{
				return -1;
			}

			return 0;
		}

		/// <summary>
		/// Container for an object handled by the cache
		/// </summary>
		private sealed class CacheObject : IComparable
		{
			public T value;
			public int count;

			public int CompareTo(object other)
			{
				CacheObject obj = other as CacheObject;
				if (obj == null)
				{
					return 0;
				}

				if (obj.count > count)
				{
					return -1;
				}
				if (obj.count < count)
				{
					return 1;
				}
				if (obj.count == count)
				{
					return 0;
				}

				return 0;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _cache.Select(obj => obj.value).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}