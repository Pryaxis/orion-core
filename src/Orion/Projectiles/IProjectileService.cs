// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using Orion.Hooks;
using Orion.Projectiles.Events;
using Orion.Utils;

namespace Orion.Projectiles {
    /// <summary>
    /// Represents a service that manages projectiles. Provides projectile-related hooks and methods.
    /// </summary>
    public interface IProjectileService : IReadOnlyArray<IProjectile>, IService {
        /// <summary>
        /// Gets or sets the hook handlers that occur when a projectile is having its defaults set. This hook can be
        /// handled.
        /// </summary>
        HookHandlerCollection<SettingProjectileDefaultsEventArgs> SettingProjectileDefaults { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a projectile had its defaults set.
        /// </summary>
        HookHandlerCollection<SetProjectileDefaultsEventArgs> SetProjectileDefaults { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a projectile is being updated. This hook can be handled.
        /// </summary>
        HookHandlerCollection<UpdatingProjectileEventArgs> UpdatingProjectile { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a projectile's AI is being updated. This hook can be handled.
        /// </summary>
        HookHandlerCollection<UpdatingProjectileEventArgs> UpdatingProjectileAi { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a projectile's AI was updated.
        /// </summary>
        HookHandlerCollection<UpdatedProjectileEventArgs> UpdatedProjectileAi { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a projectile was updated.
        /// </summary>
        HookHandlerCollection<UpdatedProjectileEventArgs> UpdatedProjectile { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a projectile is being removed. This hook can be handled.
        /// </summary>
        HookHandlerCollection<RemovingProjectileEventArgs> RemovingProjectile { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when a projectile was removed.
        /// </summary>
        HookHandlerCollection<RemovedProjectileEventArgs> RemovedProjectile { get; set; }

        /// <summary>
        /// Spawns and returns a projectile with the given <see cref="ProjectileType"/> at the specified position with
        /// the velocity, damage, knockback, and AI values.
        /// </summary>
        /// <param name="type">The <see cref="ProjectileType"/>.</param>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        /// <param name="knockback">The knockback.</param>
        /// <param name="aiValues">
        /// The AI values, or <c>null</c> for none. If not <c>null</c>, this should have length 2.
        /// </param>
        /// <returns>The resulting <see cref="IProjectile"/> instance, or <c>null</c> if none was spawned.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="aiValues"/> is not <c>null</c> and does not have length 2.
        /// </exception>
        IProjectile SpawnProjectile(ProjectileType type, Vector2 position, Vector2 velocity, int damage,
                                    float knockback, float[] aiValues = null);
    }
}
