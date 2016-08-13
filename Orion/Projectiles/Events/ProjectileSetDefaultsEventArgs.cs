using System;

namespace Orion.Projectiles.Events
{
	/// <summary>
	/// Provides data for the <see cref="IProjectileService.ProjectileSetDefaults"/> event.
	/// </summary>
	public class ProjectileSetDefaultsEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the projectile that had its defaults set.
		/// </summary>
		public IProjectile Projectile { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileSetDefaultsEventArgs"/> class. 
		/// </summary>
		/// <param name="projectile">The projectile that had its defaults set.</param>
		/// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
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
