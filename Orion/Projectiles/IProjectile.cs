using Microsoft.Xna.Framework;
using Orion.Entities;

namespace Orion.Projectiles
{
	/// <summary>
	/// Provides a wrapper around a Terraria projectile instance.
	/// </summary>
	public interface IProjectile : IOrionEntity
	{
		/// <summary>
		/// Gets or sets the projectile's damage.
		/// </summary>
		int Damage { get; set; }

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
		/// Gets the projectile's <see cref="ProjectileType"/>.
		/// </summary>
		ProjectileType Type { get; }

		/// <summary>
		/// Gets the wrapped Terraria projectile instance.
		/// </summary>
		Terraria.Projectile WrappedProjectile { get; }

		/// <summary>
		/// Sets the projectile's defaults using the specified <see cref="ProjectileType"/>.
		/// </summary>
		/// <param name="type">The <see cref="ProjectileType"/>.</param>
		void SetDefaults(ProjectileType type);
	}
}
