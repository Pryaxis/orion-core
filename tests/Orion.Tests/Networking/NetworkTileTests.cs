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

using System;
using FluentAssertions;
using Orion.Networking.Tiles;
using Orion.World;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Networking {
    public class NetworkTileTests {
        [Fact]
        public void SetBlockType_MarksAsDirty() {
            var tile = new NetworkTile();
            tile.BlockType = BlockType.Stone;

            tile.IsDirty.Should().BeTrue();
        }

        [Fact]
        public void SetBlockType_NullValue_ThrowsArgumentNullException() {
            var tile = new NetworkTile();
            Action action = () => tile.BlockType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetWallType_MarksAsDirty() {
            var tile = new NetworkTile();
            tile.WallType = WallType.Stone;

            tile.IsDirty.Should().BeTrue();
        }

        [Fact]
        public void SetWallType_NullValue_ThrowsArgumentNullException() {
            var tile = new NetworkTile();
            Action action = () => tile.WallType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetLiquidAmount_MarksAsDirty() {
            var tile = new NetworkTile();
            tile.LiquidAmount = 0;

            tile.IsDirty.Should().BeTrue();
        }

        [Fact]
        public void SetTileHeader_MarksAsDirty() {
            var tile = new NetworkTile();
            tile.TileHeader = 0;

            tile.IsDirty.Should().BeTrue();
        }

        [Fact]
        public void SetTileHeader2_MarksAsDirty() {
            var tile = new NetworkTile();
            tile.TileHeader2 = 0;

            tile.IsDirty.Should().BeTrue();
        }

        [Fact]
        public void SetTileHeader3_MarksAsDirty() {
            var tile = new NetworkTile();
            tile.TileHeader3 = 0;

            tile.IsDirty.Should().BeTrue();
        }

        [Fact]
        public void SetTileHeader4_MarksAsDirty() {
            var tile = new NetworkTile();
            tile.TileHeader4 = 0;

            tile.IsDirty.Should().BeTrue();
        }

        [Fact]
        public void SetBlockFrameX_MarksAsDirty() {
            var tile = new NetworkTile();
            tile.BlockFrameX = 0;

            tile.IsDirty.Should().BeTrue();
        }

        [Fact]
        public void SetBlockFrameY_MarksAsDirty() {
            var tile = new NetworkTile();
            tile.BlockFrameY = 0;

            tile.IsDirty.Should().BeTrue();
        }
    }
}
