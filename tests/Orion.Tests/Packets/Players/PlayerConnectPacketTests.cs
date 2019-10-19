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
using System.IO;
using FluentAssertions;
using Xunit;

namespace Orion.Packets.Players {
    public class PlayerConnectPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new PlayerConnectPacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        public void VersionString_Set_Null_ThrowsArgumentNullException() {
            var packet = new PlayerConnectPacket();
            Action action = () => packet.VersionString = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = { 15, 0, 1, 11, 84, 101, 114, 114, 97, 114, 105, 97, 49, 57, 52 };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (PlayerConnectPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.VersionString.Should().Be("Terraria194");
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
