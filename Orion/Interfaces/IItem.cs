using System;
using Microsoft.Xna.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a wrapper around a Terraria item.
	/// </summary>
	public interface IItem
	{
		/// <summary>
		/// Gets the item's axe power.
		/// </summary>
		int AxePower { get; }
		
		/// <summary>
		/// Gets the item's damage.
		/// </summary>
		int Damage { get; }

		/// <summary>
		/// Gets the item's hammer power.
		/// </summary>
		int HammerPower { get; }
		
		/// <summary>
		/// Gets the item's maximum stack size.
		/// </summary>
		int MaxStackSize { get; }

		/// <summary>
		/// Gets the item's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the item's pickaxe power.
		/// </summary>
		int PickaxePower { get; }

		/// <summary>
		/// Gets or sets the item's position in the world.
		/// </summary>
		Vector2 Position { get; set; }
		
		/// <summary>
		/// Gets or sets the item's prefix.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="value"/> was greater than the number of prefixes.
		/// </exception>
		byte Prefix { get; set; }
		
		/// <summary>
		/// Gets the projectile type ID that the item creates.
		/// </summary>
		int Projectile { get; }
		
		/// <summary>
		/// Gets or sets the item's stack size.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="value"/> was negative or greater than <see cref="MaxStackSize"/>.
		/// </exception>
		int StackSize { get; set; }
		
		/// <summary>
		/// Gets or sets the item's type ID.
		/// </summary>
		int Type { get; }
		
		/// <summary>
		/// Gets or sets the item's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }
		
		/// <summary>
		/// Gets the wrapped Terraria item.
		/// </summary>
		Terraria.Item WrappedItem { get; }

		/// <summary>
		/// Sets the item's defaults to the specified type ID.
		/// </summary>
		/// <param name="type">The type ID.</param>
		void SetDefaults(int type);
	}
}
