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
    public class PlayerBuffsPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new PlayerBuffsPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void PlayerBuffTypes_SetItem_MarksAsDirty() {
            var packet = new PlayerBuffsPacket();

            packet.PlayerBuffTypes[0] = BuffType.ObsidianSkin;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void PlayerBuffTypes_SetNullValue_ThrowsArgumentNullException() {
            var packet = new PlayerBuffsPacket();
            Action action = () => packet.PlayerBuffTypes[0] = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void PlayerBuffTypes_Count_IsCorrect() {
            var packet = new PlayerBuffsPacket();

            packet.PlayerBuffTypes.Count.Should().Be(Terraria.Player.maxBuffs);
        }

        public static readonly byte[] Bytes = {
            26, 0, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        public static readonly byte[] InvalidBuffTypeBytes = {
            26, 0, 50, 0, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (PlayerBuffsPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerIndex.Should().Be(0);

                for (var i = 0; i < packet.PlayerBuffTypes.Count; ++i) {
                    packet.PlayerBuffTypes[i].Should().BeSameAs(BuffType.None);
                }
            }
        }

        [Fact]
        public void ReadFromStream_InvalidBuffType_ThrowsPacketException() {
            using (var stream = new MemoryStream(InvalidBuffTypeBytes)) {
                Func<Packet> func = () => Packet.ReadFromStream(stream, PacketContext.Server);

                func.Should().Throw<PacketException>();
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
