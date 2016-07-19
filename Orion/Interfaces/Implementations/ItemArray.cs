using System;
using System.Runtime.CompilerServices;

namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Wraps a Terraria item array.
	/// </summary>
	public class ItemArray : IItemArray
	{
		private static readonly ConditionalWeakTable<Terraria.Item[], ItemArray> Cache
			= new ConditionalWeakTable<Terraria.Item[], ItemArray>();

		/// <summary>
		/// Gets the wrapped Terraria item array.
		/// </summary>
		public Terraria.Item[] WrappedItemArray { get; }
		
		private ItemArray(Terraria.Item[] terrariaItemArray)
		{
			WrappedItemArray = terrariaItemArray;
		}

		/// <summary>
		/// Gets or sets the <see cref="IItem"/> at the specified index.
		/// </summary>
		/// <param name="index">The index to retrieve or modify.</param>
		/// <returns>The <see cref="IItem"/> at <paramref name="index"/>.</returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		public IItem this[int index]
		{
			get { return Item.Wrap(WrappedItemArray[index]); }
			set { WrappedItemArray[index] = value.WrappedItem; }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ItemArray"/> class wrapping the specified Terraria item array. If
		/// this method is called multiple times on the same Terraria item array, then the same <see cref="ItemArray"/>
		/// will be returned.
		/// </summary>
		/// <param name="terrariaItemArray">The Terraria item array to wrap.</param>
		/// <returns>An <see cref="ItemArray"/> that wraps <paramref name="terrariaItemArray"/>.</returns>
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
