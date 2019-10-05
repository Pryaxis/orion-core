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

using Orion.Utils;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Represents a (generalized) Terraria tile entity. Tile entities are tiles which require extra functionality.
    /// </summary>
    public interface ITileEntity : IAnnotatable {
        /// <summary>
        /// Gets the tile entity's type.
        /// </summary>
        TileEntityType Type { get; }

        /// <summary>
        /// Gets the tile entity's index. A value of -1 indicates that the tile entity is not "real". i.e., it is not
        /// tied to the game state.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets a value indicating whether the tile entity is active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets or sets the tile entity's X coordinate.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the tile entity's Y coordinate.
        /// </summary>
        int Y { get; set; }
    }
}
