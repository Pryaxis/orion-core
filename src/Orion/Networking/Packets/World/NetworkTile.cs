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

using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Represents a tile that is transmitted over the network.
    /// </summary>
    public sealed class NetworkTile : Tile {
        /// <inheritdoc />
        public override BlockType BlockType { get; set; }

        /// <inheritdoc />
        public override WallType WallType { get; set; }

        /// <inheritdoc />
        public override byte LiquidAmount { get; set; }

        /// <inheritdoc />
        public override short TileHeader { get; set; }

        /// <inheritdoc />
        public override byte TileHeader2 { get; set; }

        /// <inheritdoc />
        public override byte TileHeader3 { get; set; }

        /// <inheritdoc />
        public override byte TileHeader4 { get; set; }

        /// <inheritdoc />
        public override short BlockFrameX { get; set; }

        /// <inheritdoc />
        public override short BlockFrameY { get; set; }
    }
}
