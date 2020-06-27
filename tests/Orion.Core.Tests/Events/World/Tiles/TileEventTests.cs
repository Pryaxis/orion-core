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
using Orion.Core.Players;
using Orion.Core.World;
using Xunit;

namespace Orion.Core.Events.World.Tiles
{
    public class TileEventTests
    {
        [Fact]
        public void Player_Get()
        {
            var world = Mock.Of<IWorld>();
            var player = Mock.Of<IPlayer>();
            var evt = new Mock<TileEvent>(world, player, 123, 456).Object;

            Assert.Same(player, evt.Player);
        }

        [Fact]
        public void X_Get()
        {
            var world = Mock.Of<IWorld>();
            var player = Mock.Of<IPlayer>();
            var evt = new Mock<TileEvent>(world, player, 123, 456).Object;

            Assert.Equal(123, evt.X);
        }

        [Fact]
        public void Y_Get()
        {
            var world = Mock.Of<IWorld>();
            var player = Mock.Of<IPlayer>();
            var evt = new Mock<TileEvent>(world, player, 123, 456).Object;

            Assert.Equal(456, evt.Y);
        }
    }
}
