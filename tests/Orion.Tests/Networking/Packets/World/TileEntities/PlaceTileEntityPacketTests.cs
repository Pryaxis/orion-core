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
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Networking.Packets.World.TileEntities {
    public class PlaceTileEntityPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new PlaceTileEntityPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetTileEntityType_MarksAsDirty() {
            var packet = new PlaceTileEntityPacket();

            packet.TileEntityType = TileEntityType.Sign;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetTileEntityType_NullValue_ThrowsArgumentNullException() {
            var packet = new PlaceTileEntityPacket();
            Action action = () => packet.TileEntityType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] Bytes = {8, 0, 87, 0, 1, 100, 0, 1};
        private static readonly byte[] InvalidTileEntityTypeBytes = {8, 0, 87, 0, 1, 100, 0, 255};

        [Fact]
        public void ReadFromStream_Delete_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (PlaceTileEntityPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TileEntityX.Should().Be(256);
                packet.TileEntityY.Should().Be(100);
                packet.TileEntityType.Should().BeSameAs(TileEntityType.ItemFrame);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidTileEntityType_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidTileEntityTypeBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
