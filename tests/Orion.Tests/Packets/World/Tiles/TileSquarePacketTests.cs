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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using FluentAssertions;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Packets.World.Tiles {
    public class TileSquarePacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new TileSquarePacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
        public void Tiles_Item_GetMutate_MarksAsDirty() {
            var packet = new TileSquarePacket();
            packet.Tiles = new NetworkTiles(1, 1);
            packet.ShouldBeDirty();

            packet.Tiles[0, 0] = new Tile { BlockType = BlockType.Stone };

            packet.ShouldBeDirty();
        }

        [Fact]
        public void Tiles_Set_NullValue_ThrowsArgumentNullException() {
            var packet = new TileSquarePacket();
            Action action = () => packet.Tiles = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = { 17, 0, 20, 1, 0, 153, 16, 171, 1, 1, 0, 3, 0, 72, 0, 0, 0 };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (TileSquarePacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.SquareSize.Should().Be(1);
            packet.TileX.Should().Be(4249);
            packet.TileY.Should().Be(427);
            packet.Tiles.Width.Should().Be(1);
            packet.Tiles.Height.Should().Be(1);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
