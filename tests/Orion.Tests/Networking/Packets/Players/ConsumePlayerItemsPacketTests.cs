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

namespace Orion.Networking.Packets.Players {
    public class ConsumePlayerItemsPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new ConsumePlayerItemsPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetItemType_MarksAsDirty() {
            var packet = new ConsumePlayerItemsPacket();
            packet.ItemType = ItemType.Sdmg;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetItemType_NullValue_ThrowsArgumentNullException() {
            var packet = new ConsumePlayerItemsPacket();
            Action action = () => packet.ItemType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] Bytes = {8, 0, 110, 179, 13, 1, 0, 0};
        private static readonly byte[] ItemTypeInvalidBytes = {8, 0, 110, 255, 127, 1, 0, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (ConsumePlayerItemsPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerIndex.Should().Be(0);
                packet.ItemStackSize.Should().Be(1);
                packet.ItemType.Should().BeSameAs(ItemType.CopperShortsword);
            }
        }

        [Fact]
        public void ReadFromStream_InvalidItemType_ThrowsPacketException() {
            using (var stream = new MemoryStream(ItemTypeInvalidBytes)) {
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
