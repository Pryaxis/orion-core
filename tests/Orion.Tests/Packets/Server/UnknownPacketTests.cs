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
using Xunit;

namespace Orion.Packets.Server {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class UnknownPacketTests {
        public static readonly byte[] Bytes = { 11, 0, 255, 0, 1, 2, 3, 4, 5, 6, 7 };

        [Fact]
        public void Length_Set_Get() {
            var packet = new UnknownPacket();

            packet.Length = 1234;

            Assert.Equal(1234, packet.Length);
        }

        [Fact]
        public void Id_Set_Get() {
            var packet = new UnknownPacket();

            packet.Id = (PacketId)255;

            Assert.Equal((PacketId)255, packet.Id);
        }

        [Fact]
        public unsafe void Read() {
            var packet = new UnknownPacket();
            packet.Read(Bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);

            Assert.Equal(8, packet.Length);
            for (var i = 0; i < 8; ++i) {
                Assert.Equal(i, packet.Data(i));
            }
        }

        [Fact]
        public void RoundTrip() {
            TestUtils.RoundTrip<UnknownPacket>(Bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void Data() {
            var packet = new UnknownPacket();

            packet.Data(0) = 123;

            Assert.Equal(123, packet.Data(0));
        }
    }
}
