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

using System.ComponentModel;
using Orion.World.Tiles;

namespace Orion.World.Events {
    /// <summary>
    /// Provides data for the <see cref="IWorldService.UpdatingHardmodeBlock"/> handlers.
    /// </summary>
    public sealed class UpdatingHardmodeBlockEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the block's X coordinate.
        /// </summary>
        public int BlockX { get; }

        /// <summary>
        /// Gets the block's Y coordinate.
        /// </summary>
        public int BlockY { get; }

        /// <summary>
        /// Gets or sets the reuslting <see cref="World.Tiles.BlockType"/>.
        /// </summary>
        public BlockType BlockType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatingHardmodeBlockEventArgs"/> class with the specified
        /// coordinates and block type.
        /// </summary>
        /// <param name="blockX">The X coordinate.</param>
        /// <param name="blockY">The Y coordinate.</param>
        /// <param name="type">The new block type.</param>
        public UpdatingHardmodeBlockEventArgs(int blockX, int blockY, BlockType type) {
            BlockX = blockX;
            BlockY = blockY;
            BlockType = type;
        }
    }
}
