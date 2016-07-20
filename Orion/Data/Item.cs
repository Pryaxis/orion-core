using Orion.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace Orion.Data
{
	/// <summary>
	/// Wraps a Terraria item.
	/// </summary>
	public class Item : Entity, IItem
	{
		private static readonly ConditionalWeakTable<Terraria.Item, Item> Cache
			= new ConditionalWeakTable<Terraria.Item, Item>();

		/// <summary>
		/// Gets the item damage.
		/// </summary>
		public int Damage => WrappedItem.damage;

		/// <summary>
		/// Gets the item maximum stack size.
		/// </summary>
		public int MaxStack => WrappedItem.maxStack;

		/// <summary>
		/// Gets or sets the item prefix.
		/// </summary>
		public byte Prefix
		{
			get { return WrappedItem.prefix; }
			set { WrappedItem.prefix = value; }
		}

		/// <summary>
		/// Gets or sets the item stack size.
		/// </summary>
		public int Stack
		{
			get { return WrappedItem.stack; }
			set { WrappedItem.stack = value; }
		}

		/// <summary>
		/// Gets the item type ID.
		/// </summary>
		public int Type => WrappedItem.netID;

		/// <summary>
		/// Gets the wrapped Terraria entity.
		/// </summary>
		public override Terraria.Entity WrappedEntity => WrappedItem;

		/// <summary>
		/// Gets the wrapped Terraria item.
		/// </summary>
		public Terraria.Item WrappedItem { get; }
		
		private Item(Terraria.Item terrariaItem)
		{
			WrappedItem = terrariaItem;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Item"/> class wrapping the specified Terraria item. If this method
		/// is called multiple times on the same Terraria item, then the same <see cref="Item"/> will be returned.
		/// </summary>
		/// <param name="terrariaItem">The Terraria item to wrap.</param>
		/// <returns>An <see cref="Item"/> that wraps <paramref name="terrariaItem"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaItem"/> was null.</exception>
		public static Item Wrap(Terraria.Item terrariaItem)
		{
			if (terrariaItem == null)
			{
				throw new ArgumentNullException(nameof(terrariaItem));
			}

			Item item;
			if (!Cache.TryGetValue(terrariaItem, out item))
			{
				item = new Item(terrariaItem);
				Cache.Add(terrariaItem, item);
			}
			return item;
		}
	}
}
