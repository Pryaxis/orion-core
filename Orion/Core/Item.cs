using System;
using Microsoft.Xna.Framework;
using Orion.Interfaces;

namespace Orion.Core
{
	/// <summary>
	/// Wraps a Terraria item.
	/// </summary>
	public class Item : IItem
	{
		/// <summary>
		/// Gets the item's axe power.
		/// </summary>
		public int AxePower => WrappedItem.axe;

		/// <summary>
		/// Gets the item's damage.
		/// </summary>
		public int Damage => WrappedItem.damage;

		/// <summary>
		/// Gets the item's hammer power.
		/// </summary>
		public int HammerPower => WrappedItem.hammer;

		/// <summary>
		/// Gets the item's maximum stack size.
		/// </summary>
		public int MaxStackSize => WrappedItem.maxStack;

		/// <summary>
		/// Gets the item's name.
		/// </summary>
		public string Name => WrappedItem.name;

		/// <summary>
		/// Gets the item's pickaxe power.
		/// </summary>
		public int PickaxePower => WrappedItem.pick;

		/// <summary>
		/// Gets or sets the item's position in the world.
		/// </summary>
		public Vector2 Position
		{
			get { return WrappedItem.position; }
			set { WrappedItem.position = value; }
		}

		/// <summary>
		/// Gets the item's prefix.
		/// </summary>
		public int Prefix => WrappedItem.prefix;

		/// <summary>
		/// Gets the projectile type that the item creates.
		/// </summary>
		public int Projectile => WrappedItem.shoot;

		/// <summary>
		/// Gets or sets the item's stack size.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="value"/> was negative or greater than <see cref="IItem.MaxStackSize"/>.
		/// </exception>
		public int StackSize
		{
			get { return WrappedItem.stack; }
			set
			{
				if (value < 0 || value > MaxStackSize)
				{
					throw new ArgumentOutOfRangeException(nameof(value));
				}

				WrappedItem.stack = value;
			}
		}

		/// <summary>
		/// Gets the item's type.
		/// </summary>
		public int Type => WrappedItem.netID;

		/// <summary>
		/// Gets or sets the item's velocity in the world.
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
		/// <exception cref="ArgumentNullException"><paramref name="terrariaItem"/> was null.</exception>
		public Item(Terraria.Item terrariaItem)
		{
			if (terrariaItem == null)
			{
				throw new ArgumentNullException(nameof(terrariaItem));
			}

			WrappedItem = terrariaItem;
		}

		/// <summary>
		/// Sets the item's defaults to the specified type's.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="type"/> was an invalid type.</exception>
		public void SetDefaults(int type)
		{
			if (type < 0 || type > Terraria.Main.maxItemTypes)
			{
				throw new ArgumentOutOfRangeException(nameof(type));
			}

			WrappedItem.SetDefaults(type);
		}

		/// <summary>
		/// Sets the item's prefix.
		/// </summary>
		/// <param name="prefix">The prefix.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="prefix"/> was an invalid prefix.</exception>
		public void SetPrefix(int prefix)
		{
			if (prefix < 0 || prefix > Terraria.Item.maxPrefixes)
			{
				throw new ArgumentOutOfRangeException(nameof(prefix));
			}

			WrappedItem.Prefix(prefix);
		}
	}
}
