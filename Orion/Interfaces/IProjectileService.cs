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
		/// Occurs after a <see cref="IProjectile"/> has its defaults set.
		/// </summary>
		event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;

		/// <summary>
		/// Occurs before a <see cref="IProjectile"/> has its defaults set.
		/// </summary>
		event EventHandler<ProjectileSettingDefaultsEventArgs> ProjectileSettingDefaults;

		/// <summary>
		/// Finds all <see cref="IProjectile"/>s in the world matching the predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with, or null for none.</param>
		/// <returns>An enumerable collection of <see cref="IProjectile"/>s that match the predicate.</returns>
		IEnumerable<IProjectile> Find(Predicate<IProjectile> predicate = null);
	}
}
