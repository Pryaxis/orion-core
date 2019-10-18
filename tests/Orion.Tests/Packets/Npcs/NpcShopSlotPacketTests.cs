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
using Orion.Items;
using Xunit;

namespace Orion.Packets.Npcs {
    public class NpcShopSlotPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new NpcShopSlotPacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        public static readonly byte[] Bytes = { 14, 0, 104, 0, 17, 6, 1, 0, 82, 100, 0, 0, 0, 1 };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (NpcShopSlotPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.NpcShopSlotIndex.Should().Be(0);
            packet.ItemType.Should().Be(ItemType.Sdmg);
            packet.ItemStackSize.Should().Be(1);
            packet.ItemPrefix.Should().Be(ItemPrefix.Unreal);
            packet.ItemValue.Should().Be(100);
            packet.CanBuyItemOnlyOnce.Should().BeTrue();
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
