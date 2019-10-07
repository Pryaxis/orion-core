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
using Orion.World;
using Xunit;

namespace Orion.Packets.World {
    public class WorldInfoPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new WorldInfoPacket();

            packet.SetSimplePropertiesShouldMarkAsDirty();
        }

        [Fact]
        public void WorldName_Set_NullValue_ThrowsArgumentNullException() {
            var packet = new WorldInfoPacket();
            Action action = () => packet.WorldName = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void TreeStyleBoundaries_SetItem_MarksAsDirty() {
            var packet = new WorldInfoPacket();

            packet.TreeStyleBoundaries[0] = 0;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void TreeStyleBoundaries_Count() {
            var packet = new WorldInfoPacket();

            packet.TreeStyleBoundaries.Count.Should().Be(3);
        }

        [Fact]
        public void TreeStyles_SetItem_MarksAsDirty() {
            var packet = new WorldInfoPacket();

            packet.TreeStyles[0] = 0;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void TreeStyles_Count() {
            var packet = new WorldInfoPacket();

            packet.TreeStyles.Count.Should().Be(4);
        }

        [Fact]
        public void CaveBackgroundStyleBoundaries_SetItem_MarksAsDirty() {
            var packet = new WorldInfoPacket();

            packet.CaveBackgroundStyleBoundaries[0] = 0;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void CaveBackgroundStyleBoundaries_Count() {
            var packet = new WorldInfoPacket();

            packet.CaveBackgroundStyleBoundaries.Count.Should().Be(3);
        }

        [Fact]
        public void CaveBackgroundStyles_SetItem_MarksAsDirty() {
            var packet = new WorldInfoPacket();

            packet.CaveBackgroundStyles[0] = 0;

            packet.ShouldBeDirty();
        }

        [Fact]
        public void CaveBackgroundStyles_Count() {
            var packet = new WorldInfoPacket();

            packet.CaveBackgroundStyles.Count.Should().Be(4);
        }

        public static readonly byte[] Bytes = {
            122, 0, 7, 141, 127, 0, 0, 1, 0, 104, 16, 176, 4, 54, 8, 102, 1, 129, 1, 53, 2, 24, 49, 0, 9, 1, 102, 63,
            129, 163, 174, 200, 216, 57, 65, 188, 220, 22, 170, 161, 45, 221, 99, 1, 0, 0, 0, 194, 0, 0, 0, 0, 51, 0, 1,
            2, 1, 0, 1, 2, 3, 0, 0, 217, 206, 151, 62, 0, 37, 4, 0, 0, 104, 16, 0, 0, 104, 16, 0, 0, 3, 2, 0, 0, 248, 4,
            0, 0, 104, 16, 0, 0, 104, 16, 0, 0, 7, 1, 0, 6, 0, 0, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0
        };

        [Fact]
        public void ReadFromStream_WorldInfo() {
            using var stream = new MemoryStream(Bytes);
            var packet = (WorldInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

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
            packet.CurrentInvasionType.Should().Be(InvasionType.None);
            packet.LobbyId.Should().Be(0);
            packet.SandstormIntensity.Should().Be(0);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
