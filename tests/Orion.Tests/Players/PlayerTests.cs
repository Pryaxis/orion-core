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
using Xunit;

namespace Orion.Players {
    public class PlayerTests {
        [Fact]
        public void SendPacket_NullPlayer_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => PlayerExtensions.SendPacket(null!, new TestPacket()));
        }

        [Fact]
        public void SendPacket() {
            var mockPlayer = new Mock<IPlayer>();

            mockPlayer.Object.SendPacket(new TestPacket());

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<TestPacket>.IsAny));
        }

        private struct TestPacket : IPacket {
            public PacketType Type => throw new NotImplementedException();
            public void Read(ReadOnlySpan<byte> span, PacketContext context) => throw new NotImplementedException();
            public void Write(ref Span<byte> span, PacketContext context) => throw new NotImplementedException();
        }
    }
}
