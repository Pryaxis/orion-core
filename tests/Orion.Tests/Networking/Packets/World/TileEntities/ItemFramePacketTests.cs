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
using Orion.Entities;
using Xunit;

namespace Orion.Networking.Packets.World.TileEntities {
    public class ItemFramePacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new ItemFramePacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetItemType_MarksAsDirty() {
            var packet = new ItemFramePacket();

            packet.ItemType = ItemType.Sdmg;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetItemType_NullValue_ThrowsArgumentNullException() {
            var packet = new ItemFramePacket();
            Action action = () => packet.ItemType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] Bytes = {12, 0, 89, 0, 1, 100, 0, 17, 6, 82, 1, 0};
        private static readonly byte[] InvalidItemTypeBytes = {12, 0, 89, 0, 1, 100, 0, 255, 127, 82, 1, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (ItemFramePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ItemFrameX.Should().Be(256);
                packet.ItemFrameY.Should().Be(100);
                packet.ItemType.Should().BeSameAs(ItemType.Sdmg);
                packet.ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.ItemStackSize.Should().Be(1);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidItemType_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidItemTypeBytes)) {
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
