using System;
using System.Collections.Generic;
using Orion.Framework;
using Orion.Projectiles.Events;

namespace Orion.Projectiles
{
	/// <summary>
	/// Provides a mechanism for managing projectiles.
	/// </summary>
	public interface IProjectileService : ISharedService
	{
		/// <summary>
		/// Occurs after a projectile was killed.
		/// </summary>
		event EventHandler<ProjectileKilledEventArgs> ProjectileKilled;

		/// <summary>
		/// Occurs after a projectile is being killed.
		/// </summary>
		event EventHandler<ProjectileKillingEventArgs> ProjectileKilling;

		/// <summary>
		/// Occurs after a projectile has had its defaults set.
		/// </summary>
		event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;

		/// <summary>
		/// Occurs when a projectile is having its defaults set.
		/// </summary>
		event EventHandler<ProjectileSettingDefaultsEventArgs> ProjectileSettingDefaults;

		/// <summary>
		/// Returns all projectiles in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of projectiles.</returns>
		IEnumerable<IProjectile> FindProjectiles(Predicate<IProjectile> predicate = null);
	}
}
