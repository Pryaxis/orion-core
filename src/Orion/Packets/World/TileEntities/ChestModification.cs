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

using Orion.World.Tiles;

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Specifies the chest modification in a <see cref="ChestModificationPacket"/>.
    /// </summary>
    public enum ChestModification : byte {
        /// <summary>
        /// Indicates that a <see cref="BlockType.Chests"/> should be placed.
        /// </summary>
        PlaceChest = 0,

        /// <summary>
        /// Indicates that a <see cref="BlockType.Chests"/> should be broken.
        /// </summary>
        BreakChest = 1,

        /// <summary>
        /// Indicates that a <see cref="BlockType.Dressers"/> should be placed.
        /// </summary>
        PlaceDresser = 2,

        /// <summary>
        /// Indicates that a <see cref="BlockType.Dressers"/> should be broken.
        /// </summary>
        BreakDresser = 3,

        /// <summary>
        /// Indicates that a <see cref="BlockType.Chests2"/> should be placed.
        /// </summary>
        PlaceChest2 = 4,

        /// <summary>
        /// Indicates that a <see cref="BlockType.Chests2"/> should be removed.
        /// </summary>
        BreakChest2 = 5
    }
}
