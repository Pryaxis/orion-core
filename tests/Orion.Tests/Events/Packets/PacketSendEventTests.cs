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
using Orion.Packets;
using Orion.Players;
using Xunit;

namespace Orion.Events.Packets {
    public class PacketSendEventTests {
        [Fact]
        public void Ctor_NullReceiver_ThrowsArgumentNullException() {
            var packet = new TestPacket();

            Assert.Throws<ArgumentNullException>(() => new PacketSendEvent<TestPacket>(ref packet, null!));
        }

        [Fact]
        public void Receiver_Get() {
            var packet = new TestPacket();
            var receiver = new Mock<IPlayer>().Object;
            var evt = new PacketSendEvent<TestPacket>(ref packet, receiver);

            Assert.Same(receiver, evt.Receiver);
        }

        private struct TestPacket : IPacket {
            public PacketId Id => throw new NotImplementedException();
            public int Read(Span<byte> span, PacketContext context) => throw new NotImplementedException();
            public int Write(Span<byte> span, PacketContext context) => throw new NotImplementedException();
        }
    }
}
