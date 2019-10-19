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
using Orion.Packets;
using Xunit;

namespace Orion.Events.Players {
    public class PacketEventArgsTests {
        [Fact]
        public void Ctor_NotDirty() {
            var packet = new Mock<Packet>().Object;
            var args = new TestArgs(packet);

            args.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            Func<PacketEventArgs> func = () => new TestArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Packet_Get() {
            var packet = new Mock<Packet>().Object;
            var args = new TestArgs(packet);

            args.Packet.Should().BeSameAs(packet);
        }

        [Fact]
        public void IsDirty_Get() {
            var isDirty = true;
            var mockPacket = new Mock<Packet>();
            mockPacket.SetupGet(p => p.IsDirty).Returns(() => isDirty);
            mockPacket.Setup(p => p.Clean()).Callback(() => isDirty = false);
            var args = new TestArgs(mockPacket.Object);

            args.ShouldBeDirty();
        }

        private class TestArgs : PacketEventArgs {
            public TestArgs(Packet packet) : base(packet) { }
        }
    }
}
