using System;
using System.Runtime.CompilerServices;

namespace Orion.Interfaces.Implementations
{
	/// <summary>
	/// Wraps a <see cref="Terraria.Item"/>.
	/// </summary>
	public class Item : Entity, IItem
	{
		private static readonly ConditionalWeakTable<Terraria.Item, Item> Cache
			= new ConditionalWeakTable<Terraria.Item, Item>();

		/// <summary>
		/// Gets the damage.
		/// </summary>
		public int Damage => WrappedItem.damage;

		/// <summary>
		/// Gets the maximum stack size.
		/// </summary>
		public int MaxStack => WrappedItem.maxStack;

		/// <summary>
		/// Gets or sets the prefix.
		/// </summary>
		public byte Prefix
		{
			get { return WrappedItem.prefix; }
			set { WrappedItem.prefix = value; }
		}

		/// <summary>
		/// Gets or sets the stack size.
		/// </summary>
		public int Stack
		{
			get { return WrappedItem.stack; }
			set { WrappedItem.stack = value; }
		}

		/// <summary>
		/// Gets the type ID.
		/// </summary>
		public int Type => WrappedItem.netID;

		/// <summary>
		/// Gets the wrapped <see cref="Terraria.Entity"/>.
		/// </summary>
		public override Terraria.Entity WrappedEntity => WrappedItem;

		/// <summary>
		/// Gets the wrapped <see cref="Terraria.Item"/>.
		/// </summary>
		public Terraria.Item WrappedItem { get; }
		
		private Item(Terraria.Item terrariaItem)
		{
			WrappedItem = terrariaItem;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Item"/> class wrapping the specified <see cref="Terraria.Item"/>.
		/// If this method is called multiple times on the same <see cref="Terraria.Item"/>, then the same
		/// <see cref="Item"/> will be returned.
		/// </summary>
		/// <param name="terrariaItem">The <see cref="Terraria.Item"/>.</param>
		/// <returns>An <see cref="Item"/> wrapping <paramref name="terrariaItem"/>.</returns>
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
