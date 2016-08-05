using System;
using Microsoft.Xna.Framework;
using Orion.Projectiles;

namespace Orion.Items
{
	/// <summary>
	/// Provides a wrapper around a Terraria item instance.
	/// </summary>
	public interface IItem
	{
		/// <summary>
		/// Gets or sets the item's ammo type.
		/// </summary>
		int AmmoType { get; set; }

		/// <summary>
		/// Gets the item's axe power.
		/// </summary>
		int AxePower { get; }

		/// <summary>
		/// Gets or sets the item's color.
		/// </summary>
		Color Color { get; set; }

		/// <summary>
		/// Gets or sets the item's damage.
		/// </summary>
		int Damage { get; set; }

		/// <summary>
		/// Gets the item's hammer power.
		/// </summary>
		int HammerPower { get; }

		/// <summary>
		/// Gets or sets the item's height.
		/// </summary>
		int Height { get; set; }

		/// <summary>
		/// Gets or sets the item's knockback.
		/// </summary>
		float Knockback { get; set; }

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
		/// Gets the item's <see cref="ItemPrefix"/>.
		/// </summary>
		ItemPrefix Prefix { get; }

		/// <summary>
		/// Gets or sets the item's projectile speed.
		/// </summary>
		float ProjectileSpeed { get; set; }

		/// <summary>
		/// Gets or sets the item's <see cref="ProjectileType"/>.
		/// </summary>
		ProjectileType ProjectileType { get; set; }

		/// <summary>
		/// Gets or sets the item's scale.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> was negative.</exception>
		float Scale { get; set; }

		/// <summary>
		/// Gets or sets the item's stack size.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> was negative.</exception>
		int StackSize { get; set; }

		/// <summary>
		/// Gets the item's <see cref="ItemType"/>.
		/// </summary>
		ItemType Type { get; }

		/// <summary>
		/// Gets or sets the item's used ammo type.
		/// </summary>
		int UseAmmoType { get; set; }

		/// <summary>
		/// Gets or sets the item's use animation time.
		/// </summary>
		int UseAnimationTime { get; set; }

		/// <summary>
		/// Gets or sets the item's use time.
		/// </summary>
		int UseTime { get; set; }

		/// <summary>
		/// Gets or sets the item's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets or sets the item's width.
		/// </summary>
		int Width { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria item instance.
		/// </summary>
		Terraria.Item WrappedItem { get; }

		/// <summary>
		/// Sets the item's defaults using an <see cref="ItemType"/>.
		/// </summary>
		/// <param name="type">The <see cref="ItemType"/>.</param>
		void SetDefaults(ItemType type);

		/// <summary>
		/// Tries to set the item's prefix using an <see cref="ItemPrefix"/>.
		/// </summary>
		/// <param name="prefix">The <see cref="ItemPrefix"/>.</param>
		void SetPrefix(ItemPrefix prefix);
	}
}
