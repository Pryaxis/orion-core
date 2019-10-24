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
using Xunit;

namespace Orion.Packets.Players {
    public class PlayerZonesPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new PlayerZonesPacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        public static readonly byte[] Bytes = { 8, 0, 36, 1, 0, 0, 0, 0 };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (PlayerZonesPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.PlayerIndex.Should().Be(1);
            packet.IsNearDungeon.Should().BeFalse();
            packet.IsNearCorruption.Should().BeFalse();
            packet.IsNearHallowed.Should().BeFalse();
            packet.IsNearMeteor.Should().BeFalse();
            packet.IsNearJungle.Should().BeFalse();
            packet.IsNearSnow.Should().BeFalse();
            packet.IsNearCrimson.Should().BeFalse();
            packet.IsNearWaterCandle.Should().BeFalse();
            packet.IsNearPeaceCandle.Should().BeFalse();
            packet.IsNearSolarPillar.Should().BeFalse();
            packet.IsNearVortexPillar.Should().BeFalse();
            packet.IsNearNebulaPillar.Should().BeFalse();
            packet.IsNearStardustPillar.Should().BeFalse();
            packet.IsNearDesert.Should().BeFalse();
            packet.IsNearGlowingMushroom.Should().BeFalse();
            packet.IsNearUndergroundDesert.Should().BeFalse();
            packet.IsNearSkyHeight.Should().BeFalse();
            packet.IsNearOverworldHeight.Should().BeFalse();
            packet.IsNearDirtLayerHeight.Should().BeFalse();
            packet.IsNearRockLayerHeight.Should().BeFalse();
            packet.IsNearUnderworldHeight.Should().BeFalse();
            packet.IsNearBeach.Should().BeFalse();
            packet.IsNearRain.Should().BeFalse();
            packet.IsNearSandstorm.Should().BeFalse();
            packet.IsNearOldOnesArmy.Should().BeFalse();
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
