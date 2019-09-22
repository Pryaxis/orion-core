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

using JetBrains.Annotations;
using Orion.Networking.Packets.Entities;

namespace Orion.Networking.Entities {
    /// <summary>
    /// Specifies an entity teleportation type in a <see cref="EntityTeleportationPacket"/>.
    /// </summary>
    [PublicAPI]
    public enum EntityTeleportationType : byte {
        /// <summary>
        /// Indicates that a player should be teleported.
        /// </summary>
        Player = 0,

        /// <summary>
        /// Indicates that an NPC should be teleported.
        /// </summary>
        Npc = 1,

        /// <summary>
        /// Indicates that a player should be teleported to another player. This is caused by a Wormhole Potion.
        /// </summary>
        PlayerToPlayer = 2
    }
}
