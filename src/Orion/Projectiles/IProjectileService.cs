// Copyright (c) 2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using Microsoft.Xna.Framework;
using Orion.Events;
using Orion.Events.Projectiles;
using Orion.Utils;

namespace Orion.Projectiles {
    /// <summary>
    /// Represents a projectile service. Provides access to projectile-related events and methods.
    /// </summary>
    public interface IProjectileService : IService {
        /// <summary>
        /// Gets the projectiles.
        /// </summary>
        IReadOnlyArray<IProjectile> Projectiles { get; }

        /// <summary>
        /// Gets or sets the event handlers that occur when a projectile's defaults are being set. This event can be
        /// canceled.
        /// </summary>
        EventHandlerCollection<ProjectileSetDefaultsEventArgs>? ProjectileSetDefaults { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that occur when a projectile is updating. This event can be canceled.
        /// </summary>
        EventHandlerCollection<ProjectileUpdateEventArgs>? ProjectileUpdate { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that occur when a projectile is being removed. This event can be canceled.
        /// </summary>
        EventHandlerCollection<ProjectileRemoveEventArgs>? ProjectileRemove { get; set; }

        /// <summary>
        /// Spawns and returns a projectile with the given projectile type at the specified position with the velocity,
        /// damage, knockback, and AI values.
        /// </summary>
        /// <param name="projectileType">The projectile type.</param>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        /// <param name="knockback">The knockback.</param>
        /// <param name="aiValues">
        /// The AI values, or <see langword="null" /> for none. If not <see langword="null" />, this should have length
        /// 2.
        /// </param>
        /// <returns>The resulting projectile, or <see langword="null" /> if none was spawned.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="aiValues"/> is not <see langword="null" /> and does not have length 2.
        /// </exception>
        IProjectile? SpawnProjectile(ProjectileType projectileType, Vector2 position, Vector2 velocity, int damage,
                                     float knockback, float[]? aiValues = null);
    }
}
