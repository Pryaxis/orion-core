// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

using System.IO;
using FluentAssertions;
using Xunit;

namespace Orion.Packets.Entities {
    public class EmoteInfoPacketTests {
        public static readonly byte[] NormalBytes = {12, 0, 91, 1, 0, 0, 0, 0, 100, 0, 255, 1};

        [Fact]
        public void ReadFromStream_Normal_IsCorrect() {
            using var stream = new MemoryStream(NormalBytes);
            var packet = (EmoteInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.EmoteIndex.Should().Be(1);
            packet.AnchorType.Should().Be(0);
            packet.AnchorEntityIndex.Should().Be(100);
            packet.EmoteLifetime.Should().Be(255);
            packet.EmoteType.Should().Be(1);
        }

        [Fact]
        public void DeserializeAndSerialize_Normal_SamePacket() {
            NormalBytes.ShouldDeserializeAndSerializeSamePacket();
        }

        public static readonly byte[] RemoveBytes = {8, 0, 91, 1, 0, 0, 0, 255};

        [Fact]
        public void ReadFromStream_Remove_IsCorrect() {
            using var stream = new MemoryStream(RemoveBytes);
            var packet = (EmoteInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.EmoteIndex.Should().Be(1);
            packet.AnchorType.Should().Be(255);
        }

        [Fact]
        public void DeserializeAndSerialize_Remove_SamePacket() {
            RemoveBytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
