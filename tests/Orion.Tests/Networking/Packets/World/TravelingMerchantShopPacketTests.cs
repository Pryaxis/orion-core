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

namespace Orion.Networking.Packets.World {
    public class TravelingMerchantShopPacketTests {
        [Fact]
        public void ShopItemTypes_SetItem_MarksAsDirty() {
            var packet = new TravelingMerchantShopPacket();
            packet.ShopItemTypes[0] = ItemType.Sdmg;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void ShopItemTypes_SetItem_NullValue_ThrowsArgumentNullException() {
            var packet = new TravelingMerchantShopPacket();
            Action action = () => packet.ShopItemTypes[0] = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ShopItemTypes_Count_IsCorrect() {
            var packet = new TravelingMerchantShopPacket();

            packet.ShopItemTypes.Count.Should().Be(Terraria.Chest.maxItems);
        }

        public static readonly byte[] Bytes = {
            83, 0, 72, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        public static readonly byte[] InvalidItemTypeBytes = {
            83, 0, 72, 255, 127, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (TravelingMerchantShopPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                foreach (var itemType in packet.ShopItemTypes) {
                    itemType.Should().BeSameAs(ItemType.None);
                }
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
        public void WriteToStream_IsCorrect() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
