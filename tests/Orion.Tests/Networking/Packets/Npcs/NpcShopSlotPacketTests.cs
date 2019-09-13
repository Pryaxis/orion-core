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

using System.IO;
using FluentAssertions;
using Orion.Items;
using Xunit;

namespace Orion.Networking.Packets.Npcs {
    public class NpcShopSlotPacketTests {
        public static readonly byte[] NpcShopSlotBytes = {13, 0, 104, 0, 17, 6, 1, 0, 82, 100, 0, 0, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcShopSlotBytes)) {
                var packet = (NpcShopSlotPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NpcShopSlotIndex.Should().Be(0);
                packet.ItemType.Should().Be(ItemType.SDMG);
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.ItemValue.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcShopSlotBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(NpcShopSlotBytes);
            }
        }
    }
}
