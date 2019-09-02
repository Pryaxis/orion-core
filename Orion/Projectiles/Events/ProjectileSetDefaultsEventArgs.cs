using System;

namespace Orion.Projectiles.Events {
    /// <summary>
    /// Provides data for the see <see cref="IProjectileService.ProjectileSetDefaults"/> event.
    /// </summary>
    public sealed class ProjectileSetDefaultsEventArgs : EventArgs {
        /// <summary>
        /// Gets the projectile that had its defaults set.
        /// </summary>
        public IProjectile Projectile { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileSetDefaultsEventArgs"/> class with the specified
        /// projectile.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
        public ProjectileSetDefaultsEventArgs(IProjectile projectile) {
            Projectile = projectile ?? throw new ArgumentNullException(nameof(projectile));
        }
    }
}
