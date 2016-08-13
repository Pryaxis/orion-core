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
		/// Occurs when a projectile is being killed.
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
		/// Occurs after a projectile was updated.
		/// </summary>
		event EventHandler<ProjectileUpdatedEventArgs> ProjectileUpdated;

		/// <summary>
		/// Occurs after a projectile's AI was updated.
		/// </summary>
		event EventHandler<ProjectileUpdatedAIEventArgs> ProjectileUpdatedAI;

		/// <summary>
		/// Occurs when a projectile is being updated.
		/// </summary>
		event EventHandler<ProjectileUpdatingEventArgs> ProjectileUpdating;

		/// <summary>
		/// Occurs when a projectile's AI is being updated.
		/// </summary>
		event EventHandler<ProjectileUpdatingAIEventArgs> ProjectileUpdatingAI;

		/// <summary>
		/// Returns all projectiles in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of projectiles.</returns>
		IEnumerable<IProjectile> FindProjectiles(Predicate<IProjectile> predicate = null);
	}
}
