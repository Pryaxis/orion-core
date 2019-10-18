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

using System.IO;
using FluentAssertions;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Packets.World.Tiles {
    public class TileAnimationPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new TileAnimationPacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        public static readonly byte[] Bytes = { 11, 0, 77, 1, 0, 1, 0, 0, 1, 100, 0 };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (TileAnimationPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.AnimationType.Should().Be(1);
            packet.BlockType.Should().Be(BlockType.Stone);
            packet.TileX.Should().Be(256);
            packet.TileY.Should().Be(100);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
