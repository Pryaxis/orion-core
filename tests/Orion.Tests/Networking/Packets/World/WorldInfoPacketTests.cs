using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Orion.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class WorldInfoPacketTests {
        private static readonly byte[] WorldInfoBytes = {
            122, 0, 7, 141, 127, 0, 0, 1, 0, 104, 16, 176, 4, 54, 8, 102, 1, 129, 1, 53, 2, 24, 49, 0, 9, 1, 102, 63,
            129, 163, 174, 200, 216, 57, 65, 188, 220, 22, 170, 161, 45, 221, 99, 1, 0, 0, 0, 194, 0, 0, 0, 0, 51, 0, 1,
            2, 1, 0, 1, 2, 3, 0, 0, 217, 206, 151, 62, 0, 37, 4, 0, 0, 104, 16, 0, 0, 104, 16, 0, 0, 3, 2, 0, 0, 248, 4,
            0, 0, 104, 16, 0, 0, 104, 16, 0, 0, 7, 1, 0, 6, 0, 0, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0,
        };

        [Fact]
        public void ReadFromStream_WorldInfo_IsCorrect() {
            using (var stream = new MemoryStream(WorldInfoBytes)) {
                var packet = (WorldInfoPacket)Packet.ReadFromStream(stream);

                packet.Time.Should().Be(32653);
                packet.IsDaytime.Should().BeTrue();
                packet.IsBloodMoon.Should().BeFalse();
                packet.IsEclipse.Should().BeFalse();
                packet.MoonPhase.Should().Be(0);
                packet.WorldWidth.Should().Be(4200);
                packet.WorldHeight.Should().Be(1200);
                packet.SpawnX.Should().Be(2102);
                packet.SpawnY.Should().Be(358);
                packet.SurfaceY.Should().Be(385);
                packet.RockLayerY.Should().Be(565);
                packet.WorldId.Should().Be(151007512);
                packet.WorldName.Should().Be("f");
                packet.WorldGuid.Should().Be("{aea3813f-d8c8-4139-bcdc-16aaa12ddd63}");
                packet.WorldGeneratorVersion.Should().Be(833223655425);
                packet.MoonType.Should().Be(0);
                packet.TreeBackgroundStyle.Should().Be(51);
                packet.CorruptionBackgroundStyle.Should().Be(0);
                packet.JungleBackgroundStyle.Should().Be(1);
                packet.SnowBackgroundStyle.Should().Be(2);
                packet.HallowBackgroundStyle.Should().Be(1);
                packet.CrimsonBackgroundStyle.Should().Be(0);
                packet.DesertBackgroundStyle.Should().Be(1);
                packet.OceanBackgroundStyle.Should().Be(2);
                packet.IceCaveBackgroundStyle.Should().Be(3);
                packet.UndergroundJungleBackgroundStyle.Should().Be(0);
                packet.HellBackgroundStyle.Should().Be(0);
                packet.WindSpeed.Should().Be(0.2965f);
                packet.NumberOfClouds.Should().Be(0);
                packet.TreeStyleBoundaries.Should().BeEquivalentTo(1061, 4200, 4200);
                packet.TreeStyles.Should().BeEquivalentTo(3, 2, 0, 0);
                packet.CaveBackgroundStyleBoundaries.Should().BeEquivalentTo(1272, 4200, 4200);
                packet.CaveBackgroundStyles.Should().BeEquivalentTo(7, 1, 0, 6);
                packet.Rain.Should().Be(0);
                packet.HasSmashedShadowOrb.Should().BeFalse();
                packet.HasDefeatedEyeOfCthulhu.Should().BeFalse();
                packet.HasDefeatedEvilBoss.Should().BeFalse();
                packet.HasDefeatedSkeletron.Should().BeFalse();
                packet.IsHardMode.Should().BeFalse();
                packet.HasDefeatedClown.Should().BeFalse();
                packet.IsServerSideCharacter.Should().BeFalse();
                packet.HasDefeatedPlantera.Should().BeFalse();
                packet.HasDefeatedDestroyer.Should().BeFalse();
                packet.HasDefeatedTwins.Should().BeFalse();
                packet.HasDefeatedSkeletronPrime.Should().BeFalse();
                packet.HasDefeatedMechanicalBoss.Should().BeFalse();
                packet.IsCloudBackgroundActive.Should().BeFalse();
                packet.IsCrimson.Should().BeTrue();
                packet.IsPumpkinMoon.Should().BeFalse();
                packet.IsFrostMoon.Should().BeFalse();
                packet.IsExpertMode.Should().BeFalse();
                packet.IsFastForwardingTime.Should().BeFalse();
                packet.IsSlimeRain.Should().BeFalse();
                packet.HasDefeatedKingSlime.Should().BeFalse();
                packet.HasDefeatedQueenBee.Should().BeFalse();
                packet.HasDefeatedDukeFishron.Should().BeFalse();
                packet.HasDefeatedMartians.Should().BeFalse();
                packet.HasDefeatedAncientCultist.Should().BeFalse();
                packet.HasDefeatedMoonLord.Should().BeFalse();
                packet.HasDefeatedPumpking.Should().BeFalse();
                packet.HasDefeatedMourningWood.Should().BeFalse();
                packet.HasDefeatedIceQueen.Should().BeFalse();
                packet.HasDefeatedSantank.Should().BeFalse();
                packet.HasDefeatedEverscream.Should().BeFalse();
                packet.HasDefeatedGolem.Should().BeFalse();
                packet.IsBirthdayParty.Should().BeFalse();
                packet.HasDefeatedPirates.Should().BeFalse();
                packet.HasDefeatedFrostLegion.Should().BeFalse();
                packet.HasDefeatedGoblins.Should().BeFalse();
                packet.IsSandstorm.Should().BeFalse();
                packet.IsOldOnesArmy.Should().BeFalse();
                packet.HasDefeatedOldOnesArmyTier1.Should().BeFalse();
                packet.HasDefeatedOldOnesArmyTier2.Should().BeFalse();
                packet.HasDefeatedOldOnesArmyTier3.Should().BeFalse();
                packet.InvasionType.Should().Be(InvasionType.None);
                packet.LobbyId.Should().Be(0);
                packet.SandstormIntensity.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_WorldInfo_IsCorrect() {
            using (var stream = new MemoryStream(WorldInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(WorldInfoBytes);
            }
        }
    }
}
