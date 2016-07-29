using System;
using System.Collections.Generic;
using Orion.Framework;
using Orion.Projectiles.Events;

namespace Orion.Projectiles
{
	/// <summary>
	/// Provides a mechanism for managing <see cref="IProjectile"/> instances.
	/// </summary>
	public interface IProjectileService : IService
	{
		/// <summary>
		/// Occurs after a <see cref="IProjectile"/> instance had its defaults set.
		/// </summary>
		event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;

		/// <summary>
		/// Occurs when a <see cref="IProjectile"/> instance is having its defaults set.
		/// </summary>
		event EventHandler<ProjectileSettingDefaultsEventArgs> ProjectileSettingDefaults;

		/// <summary>
		/// Finds all <see cref="IProjectile"/> instances in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of <see cref="IProjectile"/> instances.</returns>
		IEnumerable<IProjectile> Find(Predicate<IProjectile> predicate = null);
	}
}
