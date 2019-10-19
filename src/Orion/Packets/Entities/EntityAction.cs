﻿// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Npcs;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Specifies the entity action in a <see cref="EntityActionPacket"/>.
    /// </summary>
    public enum EntityAction : byte {
        /// <summary>
        /// Spawn skeletron on a player. This is caused by talking to the <see cref="NpcType.OldMan"/>.
        /// </summary>
        SpawnSkeletron = 1,

        /// <summary>
        /// Play a grappling sound on a player.
        /// </summary>
        PlayGrappleSound = 2,

        /// <summary>
        /// Use the <see cref="ItemType.EnchantedSundial"/>.
        /// </summary>
        UseSundial = 3,

        /// <summary>
        /// Create mimic smoke on an NPC.
        /// </summary>
        CreateMimicSmoke = 4
    }
}
