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

namespace Orion.Packets.Players {
    public class PlayerInventorySlotPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new PlayerInventorySlotPacket();

            packet.SetSimplePropertiesShouldMarkAsDirty();
        }

        public static readonly byte[] Bytes = {10, 0, 5, 0, 0, 1, 0, 59, 179, 13};

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (PlayerInventorySlotPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.PlayerIndex.Should().Be(0);
            packet.PlayerInventorySlotIndex.Should().Be(0);
            packet.ItemStackSize.Should().Be(1);
            packet.ItemPrefix.Should().Be(ItemPrefix.Godly);
            packet.ItemType.Should().Be(ItemType.CopperShortsword);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
