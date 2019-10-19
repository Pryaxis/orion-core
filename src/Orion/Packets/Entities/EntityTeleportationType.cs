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

using Orion.Items;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Specifies an entity's teleportation type in an <see cref="EntityTeleportationPacket"/>.
    /// </summary>
    public enum EntityTeleportationType : byte {
        /// <summary>
        /// A player should be teleported.
        /// </summary>
        Player = 0,

        /// <summary>
        /// An NPC should be teleported.
        /// </summary>
        Npc = 1,

        /// <summary>
        /// A player should be teleported to another player via a <see cref="ItemType.WormholePotion"/>.
        /// </summary>
        PlayerToPlayer = 2
    }
}
