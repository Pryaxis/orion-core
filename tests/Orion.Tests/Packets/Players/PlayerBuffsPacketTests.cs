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
using Orion.Entities;
using Xunit;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Packets.Players {
    public class PlayerBuffsPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new PlayerBuffsPacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        public void BuffTypes_SetItem_MarksAsDirty() {
            var packet = new PlayerBuffsPacket();

            packet.BuffTypes[0] = BuffType.ObsidianSkin;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void BuffTypes_Count() {
            var packet = new PlayerBuffsPacket();

            packet.BuffTypes.Count.Should().Be(TerrariaPlayer.maxBuffs);
        }

        public static readonly byte[] Bytes = {
            26, 0, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (PlayerBuffsPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.PlayerIndex.Should().Be(0);

            for (var i = 0; i < packet.BuffTypes.Count; ++i) {
                packet.BuffTypes[i].Should().Be(BuffType.None);
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
