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
    public class ChestContentsSlotPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new ChestContentsSlotPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetItemPrefix_MarksAsDirty() {
            var packet = new ChestContentsSlotPacket();

            packet.ItemPrefix = ItemPrefix.Unreal;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetItemPrefix_NullValue_ThrowsArgumentNullException() {
            var packet = new ChestContentsSlotPacket();
            Action action = () => packet.ItemPrefix = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetItemType_MarksAsDirty() {
            var packet = new ChestContentsSlotPacket();

            packet.ItemType = ItemType.Sdmg;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetItemType_NullValue_ThrowsArgumentNullException() {
            var packet = new ChestContentsSlotPacket();
            Action action = () => packet.ItemType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] Bytes = {11, 0, 32, 0, 0, 0, 1, 0, 0, 17, 6};
        private static readonly byte[] InvalidItemPrefixBytes = {11, 0, 32, 0, 0, 0, 1, 0, 255, 17, 6};
        private static readonly byte[] InvalidItemTypeBytes = {11, 0, 32, 0, 0, 0, 1, 0, 0, 255, 127};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (ChestContentsSlotPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ChestIndex.Should().Be(0);
                packet.ChestContentsSlotIndex.Should().Be(0);
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().BeSameAs(ItemPrefix.None);
                packet.ItemType.Should().BeSameAs(ItemType.Sdmg);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidItemPrefix_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidItemPrefixBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
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
