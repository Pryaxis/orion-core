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

using System.Diagnostics.CodeAnalysis;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Specifies an entity action in a <see cref="EntityActionPacket"/>.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum EntityAction : byte {
        /// <summary>
        /// Indicates that Skeletron should be spawned. This is caused by talking to the Old Man.
        /// </summary>
        PlayerSpawnSkeletron = 1,

        /// <summary>
        /// Indicates that a grappling sound should be played.
        /// </summary>
        PlayerGrappleSound = 2,

        /// <summary>
        /// Indicates that the Enchanted Sundial should be used.
        /// </summary>
        PlayerUseSundial = 3,

        /// <summary>
        /// Indicates that mimic smoke should be created.
        /// </summary>
        NpcCreateMimicSmoke = 4
    }
}
