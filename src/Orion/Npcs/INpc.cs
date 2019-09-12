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

namespace Orion.Npcs {
    /// <summary>
    /// Represents a Terraria NPC.
    /// </summary>
    public interface INpc : IEntity {
        /// <summary>
        /// Gets the NPC's <see cref="NpcType"/>.
        /// </summary>
        NpcType Type { get; }

        /// <summary>
        /// Gets or sets the NPC's health.
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets the NPC's maximum health.
        /// </summary>
        int MaxHealth { get; set; }

        /// <summary>
        /// Applies the given <see cref="NpcType"/> to the NPC. This will update all of the properties accordingly.
        /// </summary>
        /// <param name="type">The <see cref="NpcType"/>.</param>
        void ApplyType(NpcType type);

        /// <summary>
        /// Damages the NPC with the given damage, knockback, hit direction, and criticality.
        /// </summary>
        /// <param name="damage">The damage.</param>
        /// <param name="knockback">The knockback.</param>
        /// <param name="hitDirection">The hit direction.</param>
        /// <param name="isHitCritical">A value indicating whether the hit should be critical.</param>
        void Damage(int damage, float knockback, int hitDirection, bool isHitCritical = false);

        /// <summary>
        /// Kills the NPC.
        /// </summary>
        void Kill();

        /// <summary>
        /// Transforms the NPC to a new <see cref="NpcType"/>. This will update all of the properties accordingly.
        /// </summary>
        /// <param name="newType">The new <see cref="NpcType"/>.</param>
        void Transform(NpcType newType);
    }
}
