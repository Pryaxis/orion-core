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
    public class ServerConnectPacketTests {
        public static readonly byte[] Bytes = { 15, 0, 1, 11, 84, 101, 114, 114, 97, 114, 105, 97, 49, 57, 52 };

        [Fact]
        public void Version_SetNullValue_ThrowsArgumentNullException() {
            var packet = new ServerConnectPacket();

            Assert.Throws<ArgumentNullException>(() => packet.Version = null!);
        }

        [Fact]
        public void Version_Set_Get() {
            var packet = new ServerConnectPacket();

            packet.Version = "Terraria194";

            Assert.Equal("Terraria194", packet.Version);
        }

        [Fact]
        public void Read() {
            var packet = new ServerConnectPacket();
            var span = Bytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal("Terraria194", packet.Version);
        }

        [Fact]
        public void RoundTrip() {
            TestUtils.RoundTripPacket<ServerConnectPacket>(Bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }
    }
}
