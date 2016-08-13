using System;

namespace Orion.Projectiles.Events
{
	/// <summary>
	/// Provides data for the <see cref="IProjectileService.ProjectileUpdated"/> event.
	/// </summary>
	public class ProjectileUpdatedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the projectile that was updated.
		/// </summary>
		public IProjectile Projectile { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileUpdatedEventArgs"/> class. 
		/// </summary>
		/// <param name="projectile">The projectile that was updated.</param>
		/// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
		public ProjectileUpdatedEventArgs(IProjectile projectile)
		{
			if (projectile == null)
			{
				throw new ArgumentNullException(nameof(projectile));
			}

			Projectile = projectile;
		}
	}
}
