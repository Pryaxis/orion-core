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

using System;
using Orion.Core.Entities;

namespace Orion.Core.Projectiles
{
    /// <summary>
    /// Represents a Terraria projectile.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe: i.e., each operation on the projectile should be atomic.
    /// </remarks>
    public interface IProjectile : IEntity
    {
        /// <summary>
        /// Gets the projectile's ID.
        /// </summary>
        /// <value>The projectile's ID.</value>
        public ProjectileId Id { get; }

        /// <summary>
        /// Gets or sets the projectile's damage.
        /// </summary>
        /// <value>The projectile's damage.</value>
        public int Damage { get; set; }

        /// <summary>
        /// Gets or sets the projectile's knockback.
        /// </summary>
        /// <value>The projectile's knockback.</value>
        public float Knockback { get; set; }

        /// <summary>
        /// Gets the projectile's AI values.
        /// </summary>
        /// <value>The AI values.</value>
        public Span<float> AiValues { get; }

        /// <summary>
        /// Sets the projectile's <paramref name="id"/>. This will update the projectile accordingly. 
        /// </summary>
        /// <param name="id">The projectile ID to set the projectile to.</param>
        public void SetId(ProjectileId id);
    }
}
