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
		
		public delegate void RemoveCallBack(T value);
		public delegate bool FlushCallBack(IEnumerable<T> values);

		/// <summary>
		/// Fired when the cache is flushed
		/// </summary>
		public event FlushCallBack FlushEvent;

		/// <summary>
		/// Fired when a CacheObject is removed from the cache due to size constraints
		/// </summary>
		public event RemoveCallBack RemoveEvent;

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
				if (RemoveEvent != null)
				{
					RemoveEvent(_cache[0].value);
				}

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

			//Removes values from the cache if the new additions will overflow the container size
			if (_cache.Count + values.Count > _maxSize)
			{
				//if something is hooked into the RemoveEvent we need to fire it for each removal
				if (RemoveEvent != null)
				{
					for (int i = (_cache.Count + values.Count) - _maxSize - 1; i >= 0; i--)
					{
						RemoveEvent(_cache[i].value);
						_cache.RemoveAt(i);
					}
				}
				//Otherwise we can just remove the range
				else
				{
					_cache.RemoveRange(0, (_cache.Count + values.Count) - _maxSize);
				}
			}

			_cache.InsertRange(0, values.Select(val => new CacheObject { value = val, count = 0 }));
		}

		/// <summary>
		/// Returns the first element of the sequence that satisfies a condition, or a default value if
		/// none is found
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Filters a sequence of values based on a predicate
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
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
				clear = _cache.Count;
			}

			Sort();

			if (OnFlush(_cache.Select(c => c.value)))
			{
				_cache.RemoveRange(0, clear);
			}
		}

		/// <summary>
		/// Raises the FlushEvent with the given IEnumerable of values
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Compares two cache objects to determine their order
		/// </summary>
		/// <param name="o1"></param>
		/// <param name="o2"></param>
		/// <returns></returns>
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

			return o1.CompareTo(o2);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _cache.Select(obj => obj.value).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Container for an object handled by the cache
		/// </summary>
		private sealed class CacheObject : IComparable
		{
			public T value;
			/// <summary>
			/// Number of times this object has been accessed
			/// </summary>
			public int count;

			public int CompareTo(object other)
			{
				CacheObject obj = other as CacheObject;

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
	}
}