// Copyright (c) 2020 Pryaxis & Orion Contributors
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

using System.Collections.Generic;
using Orion.Framework;
using Orion.Packets.DataStructures;

namespace Orion.Projectiles {
    /// <summary>
    /// Represents a projectile service. Provides access to projectiles and publishes projectile-related events.
    /// </summary>
    [Service(ServiceScope.Singleton)]
    public interface IProjectileService {
        /// <summary>
        /// Gets the projectiles.
        /// </summary>
        /// <value>The projectiles.</value>
        IReadOnlyList<IProjectile> Projectiles { get; }

        /// <summary>
        /// Spawns a projectile with the given <paramref name="id"/> at the specified <paramref name="position"/> with
        /// the <paramref name="velocity"/>, <paramref name="damage"/>, and <paramref name="knockback"/>.
        /// </summary>
        /// <param name="id">The projectile ID.</param>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        /// <param name="knockback">The knockback.</param>
        /// <returns>The resulting projectile, or <see langword="null"/> if none was spawned.</returns>
        IProjectile? SpawnProjectile(
            ProjectileId id, Vector2f position, Vector2f velocity, int damage, float knockback);
    }
}
