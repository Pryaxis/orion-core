using System;
using System.ComponentModel;

namespace Orion.Projectiles.Events {
    /// <summary>
    /// Provides data for the see <see cref="IProjectileService.ProjectileSettingDefaults"/> event.
    /// </summary>
    public sealed class ProjectileSettingDefaultsEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the projectile that is having its defaults set.
        /// </summary>
        public IProjectile Projectile { get; }

        /// <summary>
        /// Gets or sets the <see cref="ProjectileType"/> that the projectile is having its defaults set to.
        /// </summary>
        public ProjectileType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileSettingDefaultsEventArgs"/> class with the specified
        /// projectile and type.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
        public ProjectileSettingDefaultsEventArgs(IProjectile projectile, ProjectileType type) {
            Projectile = projectile ?? throw new ArgumentNullException(nameof(projectile));
            Type = type;
        }
    }
}
