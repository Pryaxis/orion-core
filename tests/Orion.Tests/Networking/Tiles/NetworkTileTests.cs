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
using Orion.World.Tiles;
using Xunit;

namespace Orion.Networking.Tiles {
    public class NetworkTileTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var tile = new NetworkTile();

            tile.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetBlockType_MarksAsDirty() {
            var tile = new NetworkTile();
            tile.BlockType = BlockType.Stone;

            tile.ShouldBeDirty();
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

            tile.ShouldBeDirty();
        }

        [Fact]
        public void SetWallType_NullValue_ThrowsArgumentNullException() {
            var tile = new NetworkTile();
            Action action = () => tile.WallType = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
