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
using Orion.Core.DataStructures;
using Orion.Core.Events.Projectiles;

namespace Orion.Core.Projectiles
{
    /// <summary>
    /// Represents a projectile service. Provides access to projectiles and publishes projectile-related events.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe.
    /// 
    /// The projectile service is responsible for publishing the following projectile-related events:
    /// <list type="bullet">
    /// <item><description><see cref="ProjectileDefaultsEvent"/></description></item>
    /// <item><description><see cref="ProjectileTickEvent"/></description></item>
    /// </list>
    /// </remarks>
    [Service(ServiceScope.Singleton)]
    public interface IProjectileService : IReadOnlyList<IProjectile>
    {
        /// <summary>
        /// Spawns a projectile with the given <paramref name="id"/> at the specified <paramref name="position"/> with
        /// the specified <paramref name="velocity"/>, <paramref name="damage"/>, and <paramref name="knockback"/>.
        /// Returns the resulting projectile.
        /// </summary>
        /// <param name="id">The projectile ID to spawn.</param>
        /// <param name="position">The position to spawn the projectile at.</param>
        /// <param name="velocity">The projectile velocity.</param>
        /// <param name="damage">The projectile damage.</param>
        /// <param name="knockback">The projectile knockback.</param>
        /// <returns>The resulting projectile.</returns>
        IProjectile Spawn(ProjectileId id, Vector2f position, Vector2f velocity, int damage, float knockback);
    }
}
