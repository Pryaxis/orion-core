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

using System;
using FluentAssertions;
using Moq;
using Orion.Players;
using Orion.Utils;
using Xunit;

namespace Orion.Events.Players {
    public class PlayerPacketEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var packet = new Mock<IDirtiable>().Object;
            Func<PlayerPacketEventArgs<IDirtiable>> func = () => new TestArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            Func<PlayerPacketEventArgs<IDirtiable>> func = () => new TestArgs(player, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ShouldBeDirty() {
            var player = new Mock<IPlayer>().Object;
            var isDirty = true;
            var mockPacket = new Mock<IDirtiable>();
            mockPacket.SetupGet(p => p.IsDirty).Returns(() => isDirty);
            mockPacket.Setup(p => p.Clean()).Callback(() => isDirty = false);
            var args = new TestArgs(player, mockPacket.Object);

            args.ShouldBeDirty();
        }

        private class TestArgs : PlayerPacketEventArgs<IDirtiable> {
            public TestArgs(IPlayer player, IDirtiable packet) : base(player, packet) { }
        }
    }
}
