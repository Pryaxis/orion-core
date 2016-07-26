using System;
using System.Collections.Generic;
using Orion.Events.Projectile;
using Orion.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a mechanism for managing <see cref="IProjectile"/>s.
	/// </summary>
	public interface IProjectileService : IService
	{
		/// <summary>
		/// Occurs after a projectile had its defaults set.
		/// </summary>
		event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;

		/// <summary>
		/// Occurs when a projectile is having its defaults set.
		/// </summary>
		event EventHandler<ProjectileSettingDefaultsEventArgs> ProjectileSettingDefaults;

		/// <summary>
		/// Finds all <see cref="IProjectile"/>s in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of <see cref="IProjectile"/>s that match the predicate.</returns>
		IEnumerable<IProjectile> Find(Predicate<IProjectile> predicate = null);
	}
}
