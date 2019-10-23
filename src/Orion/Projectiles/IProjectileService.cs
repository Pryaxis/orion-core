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
using Orion.Utils;

namespace Orion.Projectiles {
    /// <summary>
    /// Represents a projectile service. Provides access to projectile-related properties and methods in a thread-safe
    /// manner unless specified otherwise.
    /// </summary>
    public interface IProjectileService {
        /// <summary>
        /// Gets the projectiles. All projectiles are returned, regardless of whether or not they are actually active.
        /// </summary>
        /// <value>The projectiles.</value>
        IReadOnlyArray<IProjectile> Projectiles { get; }

        /// <summary>
        /// Spawns and returns a projectile with the given <paramref name="type"/> at the specified
        /// <paramref name="position"/> with the <paramref name="velocity"/>, <paramref name="damage"/>,
        /// <paramref name="knockback"/>, and <paramref name="aiValues"/>.
        /// </summary>
        /// <param name="type">The projectile type.</param>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        /// <param name="knockback">The knockback.</param>
        /// <param name="aiValues">
        /// The AI values, or <see langword="null"/> for none. If not <see langword="null"/>, this should have length
        /// 2. These AI values are values that control type-specific behavior.
        /// </param>
        /// <returns>The resulting projectile, or <see langword="null"/> if none was spawned.</returns>
        /// <remarks>Implementations of this method are not required to be thread-safe.</remarks>
        /// <exception cref="ArgumentException">
        /// <paramref name="aiValues"/> is not <see langword="null"/> and does not have length 2.
        /// </exception>
        IProjectile? SpawnProjectile(
            ProjectileType type, Vector2 position, Vector2 velocity, int damage, float knockback,
            float[]? aiValues = null);
    }
}
