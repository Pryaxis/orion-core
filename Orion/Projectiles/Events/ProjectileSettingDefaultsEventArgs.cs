using System;
using System.ComponentModel;

namespace Orion.Projectiles.Events
{
	/// <summary>
	/// Provides data for the see <see cref="IProjectileService.ProjectileSettingDefaults"/> event.
	/// </summary>
	public class ProjectileSettingDefaultsEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the <see cref="IProjectile"/> instance that is having its defaults set.
		/// </summary>
		public IProjectile Projectile { get; }

		/// <summary>
		/// Gets or sets the <see cref="ProjectileType"/> that the <see cref="IProjectile"/> instance is having its
		/// defaults set to.
		/// </summary>
		public ProjectileType Type { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileSettingDefaultsEventArgs"/> class.
		/// </summary>
		/// <param name="projectile">The <see cref="IProjectile"/> instance that is having its defaults set.</param>
		/// <param name="type">
		/// The <see cref="ProjectileType"/> that the <see cref="IProjectile"/> instance is having its defaults set to.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
		public ProjectileSettingDefaultsEventArgs(IProjectile projectile, ProjectileType type)
		{
			if (projectile == null)
			{
				throw new ArgumentNullException(nameof(projectile));
			}

			Projectile = projectile;
			Type = type;
		}
	}
}
