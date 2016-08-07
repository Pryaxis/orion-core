using Microsoft.Xna.Framework;

namespace Orion.Projectiles
{
	/// <summary>
	/// Provides a wrapper around a Terraria projectile instance.
	/// </summary>
	public interface IProjectile
	{
		/// <summary>
		/// Gets the projectile's damage.
		/// </summary>
		int Damage { get; }

		/// <summary>
		/// Gets a value indicating whether the projectile is hostile.
		/// </summary>
		bool IsHostile { get; }

		/// <summary>
		/// Gets the projectile's name.
		/// </summary>
		string Name { get; }

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
