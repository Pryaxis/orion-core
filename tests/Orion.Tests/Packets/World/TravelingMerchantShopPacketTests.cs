﻿// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Items;
using Xunit;
using TerrariaChest = Terraria.Chest;

namespace Orion.Packets.World {
    public class TravelingMerchantShopPacketTests {
        [Fact]
        public void ItemTypes_Count() {
            var packet = new TravelingMerchantShopPacket();

            packet.ItemTypes.Count.Should().Be(TerrariaChest.maxItems);
        }

        [Fact]
        public void ItemTypes_MarksAsDirty() {
            var packet = new TravelingMerchantShopPacket();
            packet.ItemTypes[0] = ItemType.Sdmg;

            packet.ShouldBeDirty();
        }

        public static readonly byte[] Bytes = {
            83, 0, 72, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (TravelingMerchantShopPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            foreach (var itemType in packet.ItemTypes) {
                itemType.Should().Be(ItemType.None);
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
