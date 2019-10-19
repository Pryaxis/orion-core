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

using System;
using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Players;
using Xunit;

namespace Orion.Packets.Players {
    public class PlayerDataPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new PlayerDataPacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        public void Name_SetNullValue_ThrowsArgumentNullException() {
            var packet = new PlayerDataPacket();
            Action action = () => packet.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {
            34, 0, 4, 0, 2, 50, 1, 102, 0, 0, 0, 0, 26, 131, 54, 158, 74, 51, 47, 39, 88, 184, 58, 43, 69, 8, 97, 162,
            167, 255, 212, 159, 76, 0
        };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (PlayerDataPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.PlayerIndex.Should().Be(0);
            packet.ClothesStyle.Should().Be(2);
            packet.Hairstyle.Should().Be(50);
            packet.Name.Should().Be("f");
            packet.HairDye.Should().Be(0);
            packet.EquipmentHiddenFlags.Should().Be(0);
            packet.MiscEquipmentHiddenFlags.Should().Be(0);
            packet.HairColor.Should().Be(new Color(26, 131, 54));
            packet.SkinColor.Should().Be(new Color(158, 74, 51));
            packet.EyeColor.Should().Be(new Color(47, 39, 88));
            packet.ShirtColor.Should().Be(new Color(184, 58, 43));
            packet.UndershirtColor.Should().Be(new Color(69, 8, 97));
            packet.PantsColor.Should().Be(new Color(162, 167, 255));
            packet.ShoeColor.Should().Be(new Color(212, 159, 76));
            packet.Difficulty.Should().Be(PlayerDifficulty.Softcore);
            packet.HasExtraAccessorySlot.Should().BeFalse();
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
