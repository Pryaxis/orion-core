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
            packet.IsNearDungeonZone.Should().BeFalse();
            packet.IsNearCorruptionZone.Should().BeFalse();
            packet.IsNearHallowedZone.Should().BeFalse();
            packet.IsNearMeteorZone.Should().BeFalse();
            packet.IsNearJungleZone.Should().BeFalse();
            packet.IsNearSnowZone.Should().BeFalse();
            packet.IsNearCrimsonZone.Should().BeFalse();
            packet.IsNearWaterCandleZone.Should().BeFalse();
            packet.IsNearPeaceCandleZone.Should().BeFalse();
            packet.IsNearSolarTowerZone.Should().BeFalse();
            packet.IsNearVortexTowerZone.Should().BeFalse();
            packet.IsNearNebulaTowerZone.Should().BeFalse();
            packet.IsNearStardustTowerZone.Should().BeFalse();
            packet.IsNearDesertZone.Should().BeFalse();
            packet.IsNearGlowingMushroomZone.Should().BeFalse();
            packet.IsNearUndergroundDesertZone.Should().BeFalse();
            packet.IsNearSkyHeightZone.Should().BeFalse();
            packet.IsNearOverworldHeightZone.Should().BeFalse();
            packet.IsNearDirtLayerHeightZone.Should().BeFalse();
            packet.IsNearRockLayerHeightZone.Should().BeFalse();
            packet.IsNearUnderworldHeightZone.Should().BeFalse();
            packet.IsNearBeachZone.Should().BeFalse();
            packet.IsNearRainZone.Should().BeFalse();
            packet.IsNearSandstormZone.Should().BeFalse();
            packet.IsNearOldOnesArmyZone.Should().BeFalse();
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
