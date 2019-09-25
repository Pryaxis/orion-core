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
using Orion.Packets.Npcs;
using Xunit;

namespace Orion.Packets.Items {
    public class ItemOwnerPacketTests {
        [Fact]
        public void SetSimpleProperties_MarkAsDirty() {
            var packet = new NpcAddBuffPacket();

            packet.SetSimplePropertiesShouldMarkAsDirty();
        }

        private static readonly byte[] Bytes = {6, 0, 22, 144, 1, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using var stream = new MemoryStream(Bytes);
            var packet = (ItemOwnerPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.ItemIndex.Should().Be(400);
            packet.ItemOwnerPlayerIndex.Should().Be(0);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
