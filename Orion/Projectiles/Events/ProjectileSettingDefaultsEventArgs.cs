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
		/// Gets or sets the type for the <see cref="IProjectile"/> instance is having its defaults set to.
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileSettingDefaultsEventArgs"/> class.
		/// </summary>
		/// <param name="projectile">The <see cref="IProjectile"/> instance that is having its defaults set.</param>
		/// <param name="type">
		/// The type that the <see cref="IProjectile"/> instance is having its defaults set to.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="projectile"/> was null.</exception>
		public ProjectileSettingDefaultsEventArgs(IProjectile projectile, int type)
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
