using Microsoft.Xna.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a wrapper around a Terraria projectile.
	/// </summary>
	public interface IProjectile
	{
		/// <summary>
		/// Gets the projectile's damage.
		/// </summary>
		int Damage { get; }

		/// <summary>
		/// Gets the projectile's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the projectile's type ID.
		/// </summary>
		int Type { get; }

		/// <summary>
		/// Gets or sets the projectile's position in the world.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets or sets the projectile's velocity.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria Projectile.
		/// </summary>
		Terraria.Projectile WrappedProjectile { get; }
	}
}
