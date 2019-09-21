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

using System;
using System.IO;
using FluentAssertions;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Networking.Packets.World.Tiles {
    public class TileAnimationPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new TileAnimationPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetBlockType_MarksAsDirty() {
            var packet = new TileAnimationPacket();

            packet.BlockType = BlockType.Stone;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetBlockType_NullValue_ThrowsArgumentNullException() {
            var packet = new TileAnimationPacket();
            Action action = () => packet.BlockType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {11, 0, 77, 1, 0, 1, 0, 0, 1, 100, 0};
        public static readonly byte[] InvalidBlockTypeBytes = {11, 0, 77, 1, 0, 255, 255, 0, 1, 100, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (TileAnimationPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.AnimationType.Should().Be(1);
                packet.BlockType.Should().Be(BlockType.Stone);
                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidBlockType_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidBlockTypeBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
