using System;
using Microsoft.Xna.Framework;

namespace Orion.Entities.Item
{
	/// <summary>
	/// Wraps a Terraria item.
	/// </summary>
	public class Item : IItem
	{
		/// <inheritdoc/>
		public int AxePower => WrappedItem.axe;

		/// <inheritdoc/>
		public int Damage => WrappedItem.damage;

		/// <inheritdoc/>
		public int HammerPower => WrappedItem.hammer;

		/// <inheritdoc/>
		public int MaxStackSize => WrappedItem.maxStack;

		/// <inheritdoc/>
		public string Name => WrappedItem.name;

		/// <inheritdoc/>
		public int PickaxePower => WrappedItem.pick;

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedItem.position; }
			set { WrappedItem.position = value; }
		}

		/// <inheritdoc/>
		public int Prefix => WrappedItem.prefix;

		/// <inheritdoc/>
		public int Projectile => WrappedItem.shoot;

		/// <inheritdoc/>
		public int StackSize
		{
			get { return WrappedItem.stack; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value));
				}

				WrappedItem.stack = value;
			}
		}

		/// <inheritdoc/>
		public int Type => WrappedItem.netID;

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedItem.velocity; }
			set { WrappedItem.velocity = value; }
		}

		/// <inheritdoc/>
		public Terraria.Item WrappedItem { get; }

		/// <inheritdoc/>
		public Item(Terraria.Item terrariaItem)
		{
			if (terrariaItem == null)
			{
				throw new ArgumentNullException(nameof(terrariaItem));
			}

			WrappedItem = terrariaItem;
		}

		/// <inheritdoc/>
		public void SetDefaults(int type)
		{
			if (type < 0 || type > Terraria.Main.maxItemTypes)
			{
				throw new ArgumentOutOfRangeException(nameof(type));
			}

			WrappedItem.SetDefaults(type);
		}

		/// <inheritdoc/>
		/// <remarks>
		/// If <paramref name="prefix"/> is not applicable to the item type, then a randomly chosen prefix will be
		/// used.
		/// </remarks>
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
