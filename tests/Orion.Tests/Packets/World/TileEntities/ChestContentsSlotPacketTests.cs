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

namespace Orion.Packets.World.TileEntities {
    public class ChestContentsSlotPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new ChestContentsSlotPacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        public static readonly byte[] Bytes = { 11, 0, 32, 0, 0, 0, 1, 0, 0, 17, 6 };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (ChestContentsSlotPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.ChestIndex.Should().Be(0);
            packet.ChestContentsSlotIndex.Should().Be(0);
            packet.ItemStackSize.Should().Be(1);
            packet.ItemPrefix.Should().Be(ItemPrefix.None);
            packet.ItemType.Should().Be(ItemType.Sdmg);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
