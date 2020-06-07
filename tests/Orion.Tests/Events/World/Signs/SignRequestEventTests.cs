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
using Orion.Players;
using Orion.World;
using Xunit;

namespace Orion.Events.World.Signs {
    public class SignRequestEventTests {
        [Fact]
        public void Ctor_NullWorld_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;

            Assert.Throws<ArgumentNullException>(() => new SignRequestEvent(null!, player, 123, 456));
        }

        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var world = new Mock<IWorld>().Object;

            Assert.Throws<ArgumentNullException>(() => new SignRequestEvent(world, null!, 123, 456));
        }

        [Fact]
        public void Player_Get() {
            var world = new Mock<IWorld>().Object;
            var player = new Mock<IPlayer>().Object;
            var evt = new SignRequestEvent(world, player, 123, 456);

            Assert.Same(player, evt.Player);
        }

        [Fact]
        public void X_Get() {
            var world = new Mock<IWorld>().Object;
            var player = new Mock<IPlayer>().Object;
            var evt = new SignRequestEvent(world, player, 123, 456);

            Assert.Equal(123, evt.X);
        }

        [Fact]
        public void Y_Get() {
            var world = new Mock<IWorld>().Object;
            var player = new Mock<IPlayer>().Object;
            var evt = new SignRequestEvent(world, player, 123, 456);

            Assert.Equal(456, evt.Y);
        }
    }
}
