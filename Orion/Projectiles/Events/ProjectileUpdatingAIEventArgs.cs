using System;
using System.ComponentModel;

namespace Orion.Projectiles.Events
{
	/// <summary>
	/// Provides data for the <see cref="IProjectileService.ProjectileUpdatingAI"/> event.
	/// </summary>
	public class ProjectileUpdatingAIEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the projectile for which the AI is being updated.
		/// </summary>
		public IProjectile Projectile { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileUpdatingAIEventArgs"/> class. 
		/// </summary>
		/// <param name="projectile">The projectile for which the AI is being updated.</param>
		/// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
		public ProjectileUpdatingAIEventArgs(IProjectile projectile)
		{
			if (projectile == null)
			{
				throw new ArgumentNullException(nameof(projectile));
			}

			Projectile = projectile;
		}
	}
}
