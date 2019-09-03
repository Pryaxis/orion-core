using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Framework;
using Orion.Projectiles.Events;

namespace Orion.Projectiles {
    /// <summary>
    /// Provides a mechanism for managing projectiles.
    /// </summary>
    public interface IProjectileService : IReadOnlyList<IProjectile>, IService {
        /// <summary>
        /// Occurs when a projectile is having its defaults set.
        /// </summary>
        event EventHandler<ProjectileSettingDefaultsEventArgs> ProjectileSettingDefaults;

        /// <summary>
        /// Occurs when a projectile had its defaults set.
        /// </summary>
        event EventHandler<ProjectileSetDefaultsEventArgs> ProjectileSetDefaults;

        /// <summary>
        /// Occurs when a projectile is being updated.
        /// </summary>
        event EventHandler<ProjectileUpdatingEventArgs> ProjectileUpdating;

        /// <summary>
        /// Occurs when a projectile's AI is being updated.
        /// </summary>
        event EventHandler<ProjectileUpdatingEventArgs> ProjectileUpdatingAi;

        /// <summary>
        /// Occurs when a projectile's AI was updated.
        /// </summary>
        event EventHandler<ProjectileUpdatedEventArgs> ProjectileUpdatedAi;

        /// <summary>
        /// Occurs when a projectile was updated.
        /// </summary>
        event EventHandler<ProjectileUpdatedEventArgs> ProjectileUpdated;

        /// <summary>
        /// Occurs when a projectile is being removed.
        /// </summary>
        event EventHandler<ProjectileRemovingEventArgs> ProjectileRemoving;

        /// <summary>
        /// Occurs when a projectile was removed.
        /// </summary>
        event EventHandler<ProjectileRemovedEventArgs> ProjectileRemoved;

        /// <summary>
        /// Spawns a projectile with the specified type at the position with the velocity, damage, knockback, and AI
        /// values.
        /// </summary>
        /// <param name="type">The projectile type.</param>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        /// <param name="knockback">The knockback.</param>
        /// <param name="aiValues">The AI values, or <c>null</c> for none. If not <c>null</c>, this should have length 2.</param>
        /// <returns>The resulting projectile.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="aiValues"/> is not <c>null</c> and does not have length 2.
        /// </exception>
        IProjectile SpawnProjectile(ProjectileType type, Vector2 position, Vector2 velocity, int damage,
                                    float knockback, float[] aiValues = null);
    }
}
