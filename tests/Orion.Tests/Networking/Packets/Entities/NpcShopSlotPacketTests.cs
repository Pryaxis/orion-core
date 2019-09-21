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

namespace Orion.Networking.Packets.Entities {
    public class NpcShopSlotPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new NpcShopSlotPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetItemType_MarksAsDirty() {
            var packet = new NpcShopSlotPacket();
            packet.ItemType = ItemType.Sdmg;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetItemType_NullValue_ThrowsArgumentNullException() {
            var packet = new NpcShopSlotPacket();
            Action action = () => packet.ItemType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetItemPrefix_MarksAsDirty() {
            var packet = new NpcShopSlotPacket();
            packet.ItemPrefix = ItemPrefix.Unreal;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetItemPrefix_NullValue_ThrowsArgumentNullException() {
            var packet = new NpcShopSlotPacket();
            Action action = () => packet.ItemPrefix = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {13, 0, 104, 0, 17, 6, 1, 0, 82, 100, 0, 0, 0};
        public static readonly byte[] InvalidItemTypeBytes = {13, 0, 104, 0, 255, 127, 1, 0, 82, 100, 0, 0, 0};
        public static readonly byte[] InvalidItemPrefixBytes = {13, 0, 104, 0, 17, 6, 1, 0, 255, 100, 0, 0, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (NpcShopSlotPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NpcShopSlotIndex.Should().Be(0);
                packet.ItemType.Should().Be(ItemType.Sdmg);
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.ItemValue.Should().Be(100);
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
        public void ReadFromStream_InvalidItemPrefix_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidItemPrefixBytes)) {
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
