using System;
using System.Runtime.CompilerServices;

namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Wraps a <see cref="Terraria.Item"/> array.
	/// </summary>
	public class ItemArray : IItemArray
	{
		private static readonly ConditionalWeakTable<Terraria.Item[], ItemArray> Cache
			= new ConditionalWeakTable<Terraria.Item[], ItemArray>();

		/// <summary>
		/// Gets the wrapped <see cref="Terraria.Item"/> array.
		/// </summary>
		public Terraria.Item[] WrappedItemArray { get; }
		
		private ItemArray(Terraria.Item[] terrariaItemArray)
		{
			WrappedItemArray = terrariaItemArray;
		}

		/// <summary>
		/// Gets or sets the <see cref="IItem"/> at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The <see cref="IItem"/> at the specified index.</returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		public IItem this[int index]
		{
			get { return Item.Wrap(WrappedItemArray[index]); }
			set { WrappedItemArray[index] = value.WrappedItem; }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ItemArray"/> class wrapping the specified <see cref="Terraria.Item"/>
		/// array. If this method is called multiple times on the same <see cref="Terraria.Item"/> array, then the same
		/// <see cref="ItemArray"/> will be returned.
		/// </summary>
		/// <param name="terrariaItemArray">The <see cref="Terraria.Item"/> array.</param>
		/// <returns>An <see cref="ItemArray"/> wrapping <paramref name="terrariaItemArray"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaItemArray"/> was null.</exception>
		public static ItemArray Wrap(Terraria.Item[] terrariaItemArray)
		{
			if (terrariaItemArray == null)
			{
				throw new ArgumentNullException(nameof(terrariaItemArray));
			}

			ItemArray itemArray;
			if (!Cache.TryGetValue(terrariaItemArray, out itemArray))
			{
				itemArray = new ItemArray(terrariaItemArray);
				Cache.Add(terrariaItemArray, itemArray);
			}
			return itemArray;
		}
	}
}
