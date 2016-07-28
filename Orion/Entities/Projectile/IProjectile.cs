using System;
using Microsoft.Xna.Framework;

namespace Orion.Entities.Projectile
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
		/// Gets the projectile's type.
		/// </summary>
		int Type { get; }

		/// <summary>
		/// Gets or sets the projectile's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria Projectile instance.
		/// </summary>
		Terraria.Projectile WrappedProjectile { get; }

		/// <summary>
		/// Sets the projectile's defaults to the type's.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="type"/> was an invalid type.</exception>
		void SetDefaults(int type);
	}
}
