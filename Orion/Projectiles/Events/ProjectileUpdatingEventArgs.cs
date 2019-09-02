using System;
using System.ComponentModel;

namespace Orion.Projectiles.Events {
    /// <summary>
    /// Provides data for the <see cref="IProjectileService.ProjectileUpdating"/> event.
    /// </summary>
    public sealed class ProjectileUpdatingEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the projectile that is being updated.
        /// </summary>
        public IProjectile Projectile { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileUpdatingEventArgs"/> class with the specified
        /// projectile.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
        public ProjectileUpdatingEventArgs(IProjectile projectile) {
            Projectile = projectile ?? throw new ArgumentNullException(nameof(projectile));
        }
    }
}
