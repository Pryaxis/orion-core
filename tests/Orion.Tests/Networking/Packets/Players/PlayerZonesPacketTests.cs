using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerZonesPacketTests {
        private static readonly byte[] PlayerZonesBytes = {8, 0, 36, 1, 0, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerZonesBytes)) {
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
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerZonesBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(PlayerZonesBytes);
            }
        }
    }
}
