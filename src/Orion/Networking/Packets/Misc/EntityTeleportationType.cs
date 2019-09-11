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

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Specifies the teleportation type of an <see cref="EntityTeleportationPacket"/>.
    /// </summary>
    public enum EntityTeleportationType {
        /// <summary>
        /// Indicates that a player should be teleported.
        /// </summary>
        TeleportPlayer = 0,

        /// <summary>
        /// Indicates that an NPC should be teleported.
        /// </summary>
        TeleportNpc = 1,

        /// <summary>
        /// Indicates that a player should be teleported to another player.
        /// </summary>
        TeleportPlayerToOtherPlayer = 2
    }
}
