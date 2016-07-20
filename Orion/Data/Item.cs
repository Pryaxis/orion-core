using Microsoft.Xna.Framework;
using Orion.Interfaces;

namespace Orion.Data
{
	/// <summary>
	/// Wraps a Terraria item.
	/// </summary>
	public class Item : IItem
	{
		/// <summary>
		/// Gets the item damage.
		/// </summary>
		public int Damage => WrappedItem.damage;

		/// <summary>
		/// Gets the item maximum stack size.
		/// </summary>
		public int MaxStack => WrappedItem.maxStack;

		/// <summary>
		/// Gets the item name.
		/// </summary>
		public string Name => WrappedItem.name;

		/// <summary>
		/// Gets or sets the item position.
		/// </summary>
		public Vector2 Position
		{
			get { return WrappedItem.position; }
			set { WrappedItem.position = value; }
		}

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
		/// Gets or sets the item velocity.
		/// </summary>
		public Vector2 Velocity
		{
			get { return WrappedItem.velocity; }
			set { WrappedItem.velocity = value; }
		}

		/// <summary>
		/// Gets the wrapped Terraria item.
		/// </summary>
		public Terraria.Item WrappedItem { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Item"/> class wrapping the specified Terraria item.
		/// </summary>
		/// <param name="terrariaItem">The Terraria item to wrap.</param>
		public Item(Terraria.Item terrariaItem)
		{
			WrappedItem = terrariaItem;
		}
	}
}
