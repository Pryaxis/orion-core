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
using Orion.Packets;
using Xunit;

namespace Orion.Events.Packets {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PacketEventTests {
        [Fact]
        public void Packet_Get() {
            var packet = new TestPacket();
            packet.Value = 100;
            var evt = new TestPacketEvent<TestPacket>(ref packet);

            Assert.Equal(100, evt.Packet.Value);
        }

        [Fact]
        public void IsDirty_PacketIsDirty_ReturnsTrue() {
            var packet = new TestPacket();
            var evt = new TestPacketEvent<TestPacket>(ref packet);
            packet.IsDirty = true;

            Assert.True(evt.IsDirty);
        }

        [Fact]
        public void IsDirty_PacketModified_ReturnsTrue() {
            var packet = new TestPacket();
            var evt = new TestPacketEvent<TestPacket>(ref packet);
            packet.Value = 100;

            Assert.True(evt.IsDirty);
        }

        [Fact]
        public void IsDirty_NothingChanged_ReturnsFalse() {
            var packet = new TestPacket();
            var evt = new TestPacketEvent<TestPacket>(ref packet);

            Assert.False(evt.IsDirty);
        }

        [Fact]
        public void Clean() {
            var packet = new TestPacket();
            var evt = new TestPacketEvent<TestPacket>(ref packet);
            packet.IsDirty = true;
            packet.Value = 100;

            evt.Clean();

            Assert.False(evt.IsDirty);
        }

        public struct TestPacket : IPacket {
            public int Value;

            public bool IsDirty { get; set; }

            public void Clean() {
                IsDirty = false;
            }

            public PacketType Type => throw new NotImplementedException();
            public void Read(ReadOnlySpan<byte> span, PacketContext context) => throw new NotImplementedException();
            public void Write(ref Span<byte> span, PacketContext context) => throw new NotImplementedException();
        }

        private class TestPacketEvent<TPacket> : PacketEvent<TPacket> where TPacket : struct, IPacket {
            public TestPacketEvent(ref TPacket packet) : base(ref packet) { }
        }
    }
}
