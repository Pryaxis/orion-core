using System;

namespace Orion.Items
{
	/// <summary>
	/// Wraps an array of Terraria item instances.
	/// </summary>
	public class ItemArray : IItemArray
	{
		private readonly IItem[] _iitemArray;

		/// <inheritdoc/>
		/// <remarks>
		/// The <see cref="IItem"/> instances are cached in an array. Calling this method multiple times will result
		/// in the same <see cref="IItem"/> instances as long as the wrapped Terraria item array remains unchanged.
		/// </remarks>
		public IItem this[int index]
		{
			get
			{
				if (index < 0 || index >= Length)
				{
					throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range.");
				}

				if (_iitemArray[index]?.WrappedItem != WrappedItemArray[index])
				{
					_iitemArray[index] = new Item(WrappedItemArray[index]);
				}
				return _iitemArray[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(nameof(value));
				}
				if (index < 0 || index >= Length)
				{
					throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range.");
				}

				_iitemArray[index] = value;
				WrappedItemArray[index] = value.WrappedItem;
			}
		}

		/// <inheritdoc/>
		public int Length => WrappedItemArray.Length;

		/// <inheritdoc/>
		public Terraria.Item[] WrappedItemArray { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemArray"/> class wrapping the specified array of Terraria
		/// item instances.
		/// </summary>
		/// <param name="terrariaItemArray">The array of Terraria item instances.</param>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaItemArray"/> was null.</exception>
		public ItemArray(Terraria.Item[] terrariaItemArray)
		{
			if (terrariaItemArray == null)
			{
				throw new ArgumentNullException(nameof(terrariaItemArray));
			}

			_iitemArray = new IItem[terrariaItemArray.Length];
			WrappedItemArray = terrariaItemArray;
		}
	}
}
