using System;
using Orion.Interfaces;

namespace Orion.Events.Projectile
{
	/// <summary>
	/// Provides data for the <see cref="IProjectileService.SetDefaults"/> event.
	/// </summary>
	public class ProjectileSetDefaultsEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the <see cref="IProjectile"/> that had its defaults set.
		/// </summary>
		public IProjectile Projectile { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileSetDefaultsEventArgs"/> class. 
		/// </summary>
		/// <param name="projectile">The <see cref="IProjectile"/> that had its defaults set.</param>
		/// <exception cref="ArgumentNullException"><paramref name="projectile"/> was null.</exception>
		public ProjectileSetDefaultsEventArgs(IProjectile projectile)
		{
			if (projectile == null)
			{
				throw new ArgumentNullException(nameof(projectile));
			}

			Projectile = projectile;
		}
	}
}
