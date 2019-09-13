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
using Microsoft.Xna.Framework;
using Orion.Items;
using Xunit;

namespace Orion.Networking.Packets.Items {
    public class InstancedItemInfoPacketTests {
        private static readonly byte[] InstancedItemInfoBytes = {
            27, 0, 90, 144, 1, 128, 51, 131, 71, 0, 112, 212, 69, 0, 0, 128, 64, 0, 0, 0, 192, 1, 0, 82, 0, 17, 6
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(InstancedItemInfoBytes)) {
                var packet = (InstancedItemInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ItemIndex.Should().Be(400);
                packet.ItemPosition.Should().Be(new Vector2(67175, 6798));
                packet.ItemVelocity.Should().Be(new Vector2(4, -2));
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.ShouldDisownItem.Should().BeFalse();
                packet.ItemType.Should().Be(ItemType.SDMG);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(InstancedItemInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(InstancedItemInfoBytes);
            }
        }
    }
}
