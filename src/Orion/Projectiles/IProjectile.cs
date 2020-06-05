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

using Orion.Entities;

namespace Orion.Projectiles {
    /// <summary>
    /// Represents a Terraria projectile.
    /// </summary>
    public interface IProjectile : IEntity, IWrapping<Terraria.Projectile> {
        /// <summary>
        /// Gets the projectile's ID.
        /// </summary>
        /// <value>The projectile's ID.</value>
        ProjectileId Id { get; }

        /// <summary>
        /// Sets the projectile's <paramref name="id"/>. This will update the projectile accordingly. 
        /// </summary>
        /// <param name="id">The projectile ID.</param>
        void SetId(ProjectileId id);
    }
}
