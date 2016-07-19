using System;
using System.Runtime.CompilerServices;

namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Encapsulates an array of Terraria items.
	/// </summary>
	public class ItemArray : IItemArray
	{
		private readonly IItem[] _array;
		private readonly Terraria.Item[] _previousBacking;

		/// <summary>
		/// Gets the backing Terraria item array.
		/// </summary>
		public Terraria.Item[] Backing { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemArray"/> class encapsulating the specified Terraria item
		/// array.
		/// </summary>
		/// <param name="array">The Terraria item array.</param>
		public ItemArray(Terraria.Item[] array)
		{
			_array = new IItem[array.Length];
			for (int i = 0; i < array.Length; ++i)
			{
				_array[i] = new Item(array[i]);
			}
			Backing = array;
			_previousBacking = (Terraria.Item[])array.Clone();
		}

		/// <summary>
		/// Gets or sets the <see cref="IItem"/> at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>
		/// The <see cref="IItem"/> at the specified index. A new instance will be created if the underlying item is
		/// reassigned.
		/// </returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		public IItem this[int index]
		{
			get
			{
				if (!ReferenceEquals(Backing[index], _previousBacking[index]))
				{
					// If _backing[index] has been reassigned, then update.
					_previousBacking[index] = Backing[index];
					_array[index] = new Item(Backing[index]);
				}
				return _array[index];
			}
			set
			{
				_array[index] = value;
				Backing[index] = value.Backing;
				_previousBacking[index] = value.Backing;
			}
		}
	}
}
