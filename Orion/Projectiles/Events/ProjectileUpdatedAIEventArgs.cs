using System;

namespace Orion.Projectiles.Events
{
	/// <summary>
	/// Provides data for the <see cref="IProjectileService.ProjectileUpdatedAI"/> event.
	/// </summary>
	public class ProjectileUpdatedAIEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the projectile for which the AI was updated.
		/// </summary>
		public IProjectile Projectile { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileUpdatedAIEventArgs"/> class. 
		/// </summary>
		/// <param name="projectile">The projectile for which the AI was updated.</param>
		/// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
		public ProjectileUpdatedAIEventArgs(IProjectile projectile)
		{
			if (projectile == null)
			{
				throw new ArgumentNullException(nameof(projectile));
			}

			Projectile = projectile;
		}
	}
}
