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

using Orion.Entities;
using Orion.Utils;
using TerrariaProjectile = Terraria.Projectile;

namespace Orion.Projectiles {
    /// <summary>
    /// Represents a Terraria projectile.
    /// </summary>
    /// <remarks>
    /// Projectiles can be short from items and from NPCs. They may either be friendly or hostile. <para/>
    /// 
    /// There are two types of projectiles:
    /// <list type="bullet">
    /// <item>
    /// </item>
    /// <item>
    /// <description>Projectiles which are not active.</description>
    /// </item>
    /// <item>
    /// <description>Projectiles which are active in the world.</description>
    /// </item>
    /// </list>
    /// 
    /// Care must be taken to differentiate the two using the <see cref="IEntity.IsActive"/> property.
    /// </remarks>
    public interface IProjectile : IEntity, IWrapping<TerrariaProjectile> {
        /// <summary>
        /// Gets the projectile's type.
        /// </summary>
        /// <value>The projectile's type.</value>
        /// <remarks>
        /// The projectile's type controls many aspects of the projectile such as its behavior. To set the projectile's
        /// type, use the <see cref="SetType(ProjectileType)"/> method.
        /// </remarks>
        ProjectileType Type { get; }

        /// <summary>
        /// Sets the projectile's <paramref name="type"/>. This will update the projectile accordingly.
        /// </summary>
        /// <param name="type">The projectile type.</param>
        void SetType(ProjectileType type);
    }
}
