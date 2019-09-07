using System;

namespace Orion.Projectiles.Events {
    /// <summary>
    /// Provides data for the <see cref="IProjectileService.UpdatedProjectile"/> event.
    /// </summary>
    public sealed class UpdatedProjectileEventArgs : EventArgs {
        /// <summary>
        /// Gets the projectile that was updated.
        /// </summary>
        public IProjectile Projectile { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatedProjectileEventArgs"/> class with the specified
        /// projectile.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
        public UpdatedProjectileEventArgs(IProjectile projectile) {
            Projectile = projectile ?? throw new ArgumentNullException(nameof(projectile));
        }
    }
}
