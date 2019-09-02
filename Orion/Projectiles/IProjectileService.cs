using System;
using System.Collections.Generic;
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
    }
}
