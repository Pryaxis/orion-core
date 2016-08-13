using Microsoft.Xna.Framework;

namespace Orion.Projectiles
{
	/// <summary>
	/// Provides a wrapper around a Terraria projectile instance.
	/// </summary>
	public interface IProjectile
	{
		/// <summary>
		/// Gets or sets the projectile's damage.
		/// </summary>
		int Damage { get; set; }

		/// <summary>
		/// Gets or sets the projectile's height in pixels.
		/// </summary>
		int Height { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the projectile is hostile.
		/// </summary>
		bool IsHostile { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the projectile is magic.
		/// </summary>
		bool IsMagic { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the projectile is melee.
		/// </summary>
		bool IsMelee { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the projectile is a minion.
		/// </summary>
		bool IsMinion { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the projectile is ranged.
		/// </summary>
		bool IsRanged { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the projectile is thrown.
		/// </summary>
		bool IsThrown { get; set; }

		/// <summary>
		/// Gets or sets the projectile's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the projectile's position in the world.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets the projectile's <see cref="ProjectileType"/>.
		/// </summary>
		ProjectileType Type { get; }

		/// <summary>
		/// Gets or sets the projectile's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets or sets the projectile's width in pixels.
		/// </summary>
		int Width { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria projectile instance.
		/// </summary>
		Terraria.Projectile WrappedProjectile { get; }

		/// <summary>
		/// Sets the projectile's defaults using a <see cref="ProjectileType"/>.
		/// </summary>
		/// <param name="type">The <see cref="ProjectileType"/>.</param>
		void SetDefaults(ProjectileType type);
	}
}
