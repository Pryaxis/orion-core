using System;
using System.ComponentModel;

namespace Orion.Projectiles.Events
{
	/// <summary>
	/// Provides data for the <see cref="IProjectileService.ProjectileUpdating"/> event.
	/// </summary>
	public class ProjectileUpdatingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the projectile that is being updated.
		/// </summary>
		public IProjectile Projectile { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileUpdatingEventArgs"/> class. 
		/// </summary>
		/// <param name="projectile">The projectile that is being updated.</param>
		/// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
		public ProjectileUpdatingEventArgs(IProjectile projectile)
		{
			if (projectile == null)
			{
				throw new ArgumentNullException(nameof(projectile));
			}

			Projectile = projectile;
		}
	}
}
