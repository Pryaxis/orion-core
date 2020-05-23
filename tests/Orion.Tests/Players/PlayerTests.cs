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
using Orion.Packets.Server;
using Xunit;

namespace Orion.Players {
    public class PlayerTests {
        private delegate void DisconnectCallback(ref ServerDisconnectPacket packet);

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

        [Fact]
        public void Disconect_NullPlayer_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => PlayerExtensions.Disconnect(null!, "test"));
        }

        [Fact]
        public void Disconect_NullReason_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;

            Assert.Throws<ArgumentNullException>(() => player.Disconnect(null!));
        }

        [Fact]
        public void Disconnect() {
            var mockPlayer = new Mock<IPlayer>();
            mockPlayer
                .Setup(p => p.SendPacket(ref It.Ref<ServerDisconnectPacket>.IsAny))
                .Callback((DisconnectCallback)((ref ServerDisconnectPacket packet) => {
                    Assert.Equal("test", packet.Reason);
                }));

            mockPlayer.Object.Disconnect("test");

            mockPlayer.Verify(p => p.SendPacket(ref It.Ref<ServerDisconnectPacket>.IsAny));
        }

        private struct TestPacket : IPacket {
            public PacketId Id => throw new NotImplementedException();
            public void Read(ReadOnlySpan<byte> span, PacketContext context) => throw new NotImplementedException();
            public void Write(ref Span<byte> span, PacketContext context) => throw new NotImplementedException();
        }
    }
}
