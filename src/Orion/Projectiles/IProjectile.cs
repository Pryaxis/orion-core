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

using Orion.Entities;

namespace Orion.Projectiles {
    /// <summary>
    /// Represents a Terraria projectile.
    /// </summary>
    public interface IProjectile : IEntity {
        /// <summary>
        /// Gets the projectile's <see cref="ProjectileType"/>.
        /// </summary>
        ProjectileType Type { get; }

        /// <summary>
        /// Gets or sets the projectile's damage.
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        /// Gets or sets the projectile's knockback.
        /// </summary>
        float Knockback { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the projectile is hostile.
        /// </summary>
        bool IsHostile { get; set; }

        /// <summary>
        /// Applies the given <see cref="ProjectileType"/> to the projectile. This will update all of the properties accordingly.
        /// </summary>
        /// <param name="type">The type.</param>
        void ApplyType(ProjectileType type);

        /// <summary>
        /// Removes the projectile.
        /// </summary>
        void Remove();
    }
}
