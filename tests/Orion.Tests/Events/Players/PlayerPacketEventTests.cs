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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Moq;
using Orion.Packets;
using Orion.Players;
using Xunit;

namespace Orion.Events.Players {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerPacketEventTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var packet = new TestPacket();

            Assert.Throws<ArgumentNullException>(() => new TestPlayerPacketEvent(null!, ref packet));
        }

        [Fact]
        public void Packet_Get() {
            var player = new Mock<IPlayer>().Object;
            var packet = new TestPacket();
            var evt = new TestPlayerPacketEvent(player, ref packet);

            Assert.True(Unsafe.AreSame(ref packet, ref evt.Packet));
        }

        [Fact]
        public void CancellationReason_Set_Get() {
            var player = new Mock<IPlayer>().Object;
            var packet = new TestPacket();
            var evt = new TestPlayerPacketEvent(player, ref packet);

            evt.CancellationReason = "test";

            Assert.Equal("test", evt.CancellationReason);
        }

        private class TestPlayerPacketEvent : PlayerPacketEvent<TestPacket> {
            public new ref TestPacket Packet => ref base.Packet;

            public TestPlayerPacketEvent(IPlayer player, ref TestPacket packet) : base(player, ref packet) { }
        }

        private struct TestPacket : IPacket {
            public PacketId Id => throw new NotImplementedException();
            public int Read(Span<byte> span, PacketContext context) => throw new NotImplementedException();
            public int Write(Span<byte> span, PacketContext context) => throw new NotImplementedException();
        }
    }
}
