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

            packet.SetSimplePropertiesShouldMarkAsDirty();
        }

        private static readonly byte[] Bytes = { 8, 0, 36, 1, 0, 0, 0, 0 };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (PlayerZonesPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.PlayerIndex.Should().Be(1);
            packet.IsPlayerNearDungeonZone.Should().BeFalse();
            packet.IsPlayerNearCorruptionZone.Should().BeFalse();
            packet.IsPlayerNearHallowedZone.Should().BeFalse();
            packet.IsPlayerNearMeteorZone.Should().BeFalse();
            packet.IsPlayerNearJungleZone.Should().BeFalse();
            packet.IsPlayerNearSnowZone.Should().BeFalse();
            packet.IsPlayerNearCrimsonZone.Should().BeFalse();
            packet.IsPlayerNearWaterCandleZone.Should().BeFalse();
            packet.IsPlayerNearPeaceCandleZone.Should().BeFalse();
            packet.IsPlayerNearSolarTowerZone.Should().BeFalse();
            packet.IsPlayerNearVortexTowerZone.Should().BeFalse();
            packet.IsPlayerNearNebulaTowerZone.Should().BeFalse();
            packet.IsPlayerNearStardustTowerZone.Should().BeFalse();
            packet.IsPlayerNearDesertZone.Should().BeFalse();
            packet.IsPlayerNearGlowingMushroomZone.Should().BeFalse();
            packet.IsPlayerNearUndergroundDesertZone.Should().BeFalse();
            packet.IsPlayerNearSkyHeightZone.Should().BeFalse();
            packet.IsPlayerNearOverworldHeightZone.Should().BeFalse();
            packet.IsPlayerNearDirtLayerHeightZone.Should().BeFalse();
            packet.IsPlayerNearRockLayerHeightZone.Should().BeFalse();
            packet.IsPlayerNearUnderworldHeightZone.Should().BeFalse();
            packet.IsPlayerNearBeachZone.Should().BeFalse();
            packet.IsPlayerNearRainZone.Should().BeFalse();
            packet.IsPlayerNearSandstormZone.Should().BeFalse();
            packet.IsPlayerNearOldOnesArmyZone.Should().BeFalse();
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
