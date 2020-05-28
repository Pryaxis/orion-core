// Copyright (c) 2020 Pryaxis & Orion Contributors
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

using Moq;
using Orion.Players;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Events.World.Tiles {
    public class BlockPlaceEventTests {
        [Fact]
        public void Id_Get() {
            var player = new Mock<IPlayer>().Object;
            var evt = new BlockPlaceEvent(player, 0, 0, BlockId.Torches, 0, false);

            Assert.Equal(BlockId.Torches, evt.Id);
        }

        [Fact]
        public void Style_Get() {
            var player = new Mock<IPlayer>().Object;
            var evt = new BlockPlaceEvent(player, 0, 0, BlockId.Torches, 1, false);

            Assert.Equal(1, evt.Style);
        }

        [Fact]
        public void IsReplacement_Get() {
            var player = new Mock<IPlayer>().Object;
            var evt = new BlockPlaceEvent(player, 0, 0, BlockId.Torches, 1, true);

            Assert.True(evt.IsReplacement);
        }
    }
}
