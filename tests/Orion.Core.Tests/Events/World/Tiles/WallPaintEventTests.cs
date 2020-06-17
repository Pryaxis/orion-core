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

using System;
using Moq;
using Orion.Core.Players;
using Orion.Core.World;
using Orion.Core.World.Tiles;
using Xunit;

namespace Orion.Core.Events.World.Tiles
{
    public class WallPaintEventTests
    {
        [Fact]
        public void Ctor_NullWorld_ThrowsArgumentNullException()
        {
            var player = Mock.Of<IPlayer>();

            Assert.Throws<ArgumentNullException>(() => new WallPaintEvent(null!, player, 256, 100, PaintColor.Red));
        }

        [Fact]
        public void Color_Get()
        {
            var world = Mock.Of<IWorld>();
            var player = Mock.Of<IPlayer>();
            var evt = new WallPaintEvent(world, player, 256, 100, PaintColor.Red);

            Assert.Equal(PaintColor.Red, evt.Color);
        }
    }
}
