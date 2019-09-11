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
    /// Specifies the type of action in a <see cref="MiscActionPacket"/>.
    /// </summary>
    public enum MiscAction : byte {
        /// <summary>
        /// Indicates that Skeletron should be spawned.
        /// </summary>
        SpawnSkeletron = 1,

        /// <summary>
        /// Indicates that a Whoopie Cushion should be used.
        /// </summary>
        WhoopieCushion = 2,

        /// <summary>
        /// Indicates that the sundial should be used.
        /// </summary>
        UseSundial = 3,

        /// <summary>
        /// Indicates that mimic smoke should be created.
        /// </summary>
        CreateMimicSmoke = 4
    }
}
