using System;
using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Networking.Packets;
using Orion.Npcs;
using Orion.Players;
using Orion.Projectiles;
using Orion.World;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    [Collection("TerrariaTestsCollection")]
    public class TerrariaPacketTests {
        [Fact]
        public void ReadFromStream_NullStream_ThrowsArgumentNullException() {
            Func<Packet> func = () => Packet.ReadFromStream(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WriteToStream_NullStream_ThrowsArgumentNullException() {
            var packet = new UnknownPacket((PacketType)255);
            Action action = () => packet.WriteToStream(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WriteToStream_PacketTooLong_ThrowsInvalidOperationException() {
            var packet = new UnknownPacket((PacketType)255) {Payload = new byte[ushort.MaxValue]};
            using (var stream = new MemoryStream()) {
                // ReSharper disable once AccessToDisposedClosure
                Action action = () => packet.WriteToStream(stream);

                action.Should().Throw<InvalidOperationException>();
            }
        }

        public static readonly byte[] UnknownBytes = {11, 0, 255, 1, 2, 3, 4, 5, 6, 7, 8,};

        [Fact]
        public void ReadFromStream_Unknown_IsCorrect() {
            using (var stream = new MemoryStream(UnknownBytes)) {
                var packet = (UnknownPacket)Packet.ReadFromStream(stream);

                packet.PacketType.Should().Be((PacketType)255);
                packet.Payload.Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8);
            }
        }

        [Fact]
        public void WriteToStream_Unknown_IsCorrect() {
            using (var stream = new MemoryStream(UnknownBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UnknownBytes);
            }
        }

        private static readonly byte[] UpdateWorldInfoBytes = {
            122, 0, 7, 141, 127, 0, 0, 1, 0, 104, 16, 176, 4, 54, 8, 102, 1, 129, 1, 53, 2, 24, 49, 0, 9, 1, 102, 63,
            129, 163, 174, 200, 216, 57, 65, 188, 220, 22, 170, 161, 45, 221, 99, 1, 0, 0, 0, 194, 0, 0, 0, 0, 51, 0, 1,
            2, 1, 0, 1, 2, 3, 0, 0, 217, 206, 151, 62, 0, 37, 4, 0, 0, 104, 16, 0, 0, 104, 16, 0, 0, 3, 2, 0, 0, 248, 4,
            0, 0, 104, 16, 0, 0, 104, 16, 0, 0, 7, 1, 0, 6, 0, 0, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0
        };

        [Fact]
        public void ReadFromStream_UpdateWorldInfo_IsCorrect() {
            using (var stream = new MemoryStream(UpdateWorldInfoBytes)) {
                var packet = (UpdateWorldInfoPacket)Packet.ReadFromStream(stream);

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
        public void WriteToStream_UpdateWorldInfo_IsCorrect() {
            using (var stream = new MemoryStream(UpdateWorldInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateWorldInfoBytes);
            }
        }

        private static readonly byte[] SyncTileFramesBytes = {11, 0, 11, 18, 0, 1, 0, 22, 0, 3, 0,};

        [Fact]
        public void ReadFromStream_SyncTileFrames_IsCorrect() {
            using (var stream = new MemoryStream(SyncTileFramesBytes)) {
                var packet = (SyncTileFramesPacket)Packet.ReadFromStream(stream);

                packet.StartSectionX.Should().Be(18);
                packet.StartSectionY.Should().Be(1);
                packet.EndSectionX.Should().Be(22);
                packet.EndSectionY.Should().Be(3);
            }
        }

        [Fact]
        public void WriteToStream_SyncTileFrames_IsCorrect() {
            using (var stream = new MemoryStream(SyncTileFramesBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(SyncTileFramesBytes);
            }
        }

        private static readonly byte[] SpawnPlayerBytes = {8, 0, 12, 0, 255, 255, 255, 255,};

        [Fact]
        public void ReadFromStream_SpawnPlayer_IsCorrect() {
            using (var stream = new MemoryStream(SpawnPlayerBytes)) {
                var packet = (SpawnPlayerPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.SpawnX.Should().Be(-1);
                packet.SpawnY.Should().Be(-1);
            }
        }

        [Fact]
        public void WriteToStream_SpawnPlayer_IsCorrect() {
            using (var stream = new MemoryStream(SpawnPlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(SpawnPlayerBytes);
            }
        }

        private static readonly byte[] UpdatePlayerBytes = {15, 0, 13, 0, 72, 16, 0, 0, 31, 131, 71, 0, 48, 212, 69,};

        [Fact]
        public void ReadFromStream_UpdatePlayer_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerBytes)) {
                var packet = (UpdatePlayerPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.IsHoldingUp.Should().BeFalse();
                packet.IsHoldingDown.Should().BeFalse();
                packet.IsHoldingLeft.Should().BeFalse();
                packet.IsHoldingRight.Should().BeTrue();
                packet.IsHoldingJump.Should().BeFalse();
                packet.IsHoldingUseItem.Should().BeFalse();
                packet.Direction.Should().BeTrue();
                packet.IsClimbingRope.Should().BeFalse();
                packet.ClimbingRopeDirection.Should().BeFalse();
                packet.IsMoving.Should().BeFalse();
                packet.IsVortexStealthed.Should().BeFalse();
                packet.IsRightSideUp.Should().BeTrue();
                packet.IsRaisingShield.Should().BeFalse();
                packet.SelectedItemIndex.Should().Be(0);
                packet.Position.Should().Be(new Vector2(67134, 6790));
                packet.Velocity.Should().Be(Vector2.Zero);
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayer_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerBytes);
            }
        }

        private static readonly byte[] UpdatePlayerStatusBytes = {5, 0, 14, 0, 1,};

        [Fact]
        public void ReadFromStream_UpdatePlayerStatus_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerStatusBytes)) {
                var packet = (UpdatePlayerStatusPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.IsActive.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerStatus_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerStatusBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerStatusBytes);
            }
        }

        private static readonly byte[] ToggleDoorBytes = {9, 0, 19, 0, 16, 14, 194, 1, 1};

        [Fact]
        public void ReadFromStream_ToggleDoor_IsCorrect() {
            using (var stream = new MemoryStream(ToggleDoorBytes)) {
                var packet = (ToggleDoorPacket)Packet.ReadFromStream(stream);

                packet.ToggleType.Should().Be(ToggleDoorPacket.Type.OpenDoor);
                packet.X.Should().Be(3600);
                packet.Y.Should().Be(450);
                packet.Direction.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_ToggleDoor_IsCorrect() {
            using (var stream = new MemoryStream(ToggleDoorBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ToggleDoorBytes);
            }
        }

        private static readonly byte[] UpdateItemBytes = {
            27, 0, 21, 144, 1, 128, 51, 131, 71, 0, 112, 212, 69, 0, 0, 128, 64, 0, 0, 0, 192, 1, 0, 82, 0, 17, 6
        };

        [Fact]
        public void ReadFromStream_UpdateItem_IsCorrect() {
            using (var stream = new MemoryStream(UpdateItemBytes)) {
                var packet = (UpdateItemPacket)Packet.ReadFromStream(stream);

                packet.ItemIndex.Should().Be(400);
                packet.Position.Should().Be(new Vector2(67175, 6798));
                packet.Velocity.Should().Be(new Vector2(4, -2));
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.ShouldDisown.Should().BeFalse();
                packet.ItemType.Should().Be(ItemType.SDMG);
            }
        }

        [Fact]
        public void WriteToStream_UpdateItem_IsCorrect() {
            using (var stream = new MemoryStream(UpdateItemBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateItemBytes);
            }
        }

        private static readonly byte[] UpdateItemOwnerBytes = {6, 0, 22, 144, 1, 0};

        [Fact]
        public void ReadFromStream_UpdateItemOwner_IsCorrect() {
            using (var stream = new MemoryStream(UpdateItemOwnerBytes)) {
                var packet = (UpdateItemOwnerPacket)Packet.ReadFromStream(stream);

                packet.ItemIndex.Should().Be(400);
                packet.OwnerPlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_UpdateItemOwner_IsCorrect() {
            using (var stream = new MemoryStream(UpdateItemOwnerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateItemOwnerBytes);
            }
        }

        private static readonly byte[] UpdateNpcBytes = {
            26, 0, 23, 1, 0, 38, 209, 132, 71, 0, 0, 213, 69, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 130, 22, 0,
        };

        [Fact]
        public void ReadFromStream_UpdateNpc_IsCorrect() {
            using (var stream = new MemoryStream(UpdateNpcBytes)) {
                var packet = (UpdateNpcPacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(1);
                packet.Position.Should().Be(new Vector2(68002.3f, 6816));
                packet.Velocity.Should().Be(Vector2.Zero);
                packet.TargetIndex.Should().Be(255);
                packet.HorizontalDirection.Should().BeFalse();
                packet.VerticalDirection.Should().BeTrue();
                packet.AiValues[0].Should().Be(0);
                packet.AiValues[1].Should().Be(0);
                packet.AiValues[2].Should().Be(0);
                packet.AiValues[3].Should().Be(0);
                packet.SpriteDirection.Should().BeFalse();
                packet.IsAtMaxHp.Should().BeTrue();
                packet.NpcType.Should().Be(NpcType.Guide);
                packet.NumberOfHpBytes.Should().Be(0);
                packet.Hp.Should().Be(0);
                packet.ReleaseOwnerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_UpdateNpc_IsCorrect() {
            using (var stream = new MemoryStream(UpdateNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateNpcBytes);
            }
        }

        private static readonly byte[] DamageNpcWithSelectedItemBytes = {6, 0, 24, 1, 0, 0};

        [Fact]
        public void ReadFromStream_DamageNpcWithSelectedItem_IsCorrect() {
            using (var stream = new MemoryStream(DamageNpcWithSelectedItemBytes)) {
                var packet = (DamageNpcWithSelectedItemPacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(1);
                packet.PlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_DamageNpcWithSelectedItem_IsCorrect() {
            using (var stream = new MemoryStream(DamageNpcWithSelectedItemBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(DamageNpcWithSelectedItemBytes);
            }
        }

        private static readonly byte[] UpdateProjectileBytes = {
            31, 0, 27, 0, 0, 128, 57, 131, 71, 0, 200, 212, 69, 254, 14, 40, 65, 147, 84, 121, 193, 205, 204, 128, 64,
            99, 0, 0, 89, 0, 0,
        };

        [Fact]
        public void ReadFromStream_UpdateProjectile_IsCorrect() {
            using (var stream = new MemoryStream(UpdateProjectileBytes)) {
                var packet = (UpdateProjectilePacket)Packet.ReadFromStream(stream);

                packet.ProjectileIdentity.Should().Be(0);
                packet.Position.Should().Be(new Vector2(67187, 6809));
                packet.Knockback.Should().Be(4.025f);
                packet.Damage.Should().Be(99);
                packet.OwnerPlayerIndex.Should().Be(0);
                packet.ProjectileType.Should().Be(ProjectileType.CrystalBullet);
                packet.AiValues[0].Should().Be(0);
                packet.AiValues[1].Should().Be(0);
                packet.ProjectileUuid.Should().Be(-1);
            }
        }

        [Fact]
        public void WriteToStream_UpdateProjectile_IsCorrect() {
            using (var stream = new MemoryStream(UpdateProjectileBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateProjectileBytes);
            }
        }

        private static readonly byte[] DamageNpcBytes = {13, 0, 28, 100, 0, 108, 0, 205, 204, 128, 64, 2, 0,};

        [Fact]
        public void ReadFromStream_DamageNpc_IsCorrect() {
            using (var stream = new MemoryStream(DamageNpcBytes)) {
                var packet = (DamageNpcPacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(100);
                packet.Damage.Should().Be(108);
                packet.Knockback.Should().Be(4.025f);
                packet.HitDirection.Should().Be(1);
                packet.IsCriticalHit.Should().BeFalse();
            }
        }

        [Fact]
        public void WriteToStream_DamageNpc_IsCorrect() {
            using (var stream = new MemoryStream(DamageNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(DamageNpcBytes);
            }
        }

        private static readonly byte[] RemoveProjectileBytes = {6, 0, 29, 1, 0, 0,};

        [Fact]
        public void ReadFromStream_RemoveProjectile_IsCorrect() {
            using (var stream = new MemoryStream(RemoveProjectileBytes)) {
                var packet = (RemoveProjectilePacket)Packet.ReadFromStream(stream);

                packet.ProjectileIdentity.Should().Be(1);
                packet.OwnerPlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_RemoveProjectile_IsCorrect() {
            using (var stream = new MemoryStream(RemoveProjectileBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RemoveProjectileBytes);
            }
        }

        private static readonly byte[] UpdatePlayerChestBytes = {10, 0, 33, 0, 0, 100, 0, 100, 0, 0,};

        [Fact]
        public void ReadFromStream_UpdatePlayerChest_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerChestBytes)) {
                var packet = (UpdatePlayerChestPacket)Packet.ReadFromStream(stream);

                packet.ChestIndex.Should().Be(0);
                packet.ChestX.Should().Be(100);
                packet.ChestY.Should().Be(100);
                packet.ChestName.Should().BeNull();
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerChestSlot_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerChestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerChestBytes);
            }
        }

        private static readonly byte[] ModifyChestBytes = {12, 0, 34, 0, 100, 0, 100, 0, 1, 0, 0, 0};

        [Fact]
        public void ReadFromStream_ModifyChest_IsCorrect() {
            using (var stream = new MemoryStream(ModifyChestBytes)) {
                var packet = (ModifyChestPacket)Packet.ReadFromStream(stream);

                packet.ModificationType.Should().Be(ModifyChestPacket.Type.PlaceContainers);
                packet.ChestX.Should().Be(100);
                packet.ChestY.Should().Be(100);
                packet.ChestStyle.Should().Be(1);
                packet.ChestIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_ModifyChestSlot_IsCorrect() {
            using (var stream = new MemoryStream(ModifyChestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ModifyChestBytes);
            }
        }

        private static readonly byte[] UpdatePlayerZonesBytes = {8, 0, 36, 1, 0, 0, 0, 0};

        [Fact]
        public void ReadFromStream_UpdatePlayerZones_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerZonesBytes)) {
                var packet = (UpdatePlayerZonesPacket)Packet.ReadFromStream(stream);

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
        }

        [Fact]
        public void WriteToStream_UpdatePlayerZonesSlot_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerZonesBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerZonesBytes);
            }
        }

        public static readonly byte[] RemoveItemOwnerBytes = {5, 0, 39, 1, 0};

        [Fact]
        public void ReadFromStream_RemoveItemOwner_IsCorrect() {
            using (var stream = new MemoryStream(RemoveItemOwnerBytes)) {
                var packet = (RemoveItemOwnerPacket)Packet.ReadFromStream(stream);

                packet.ItemIndex.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_RemoveItemOwner_IsCorrect() {
            using (var stream = new MemoryStream(RemoveItemOwnerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RemoveItemOwnerBytes);
            }
        }

        public static readonly byte[] UpdatePlayerTalkingToNpcBytes = {6, 0, 40, 1, 1, 0};

        [Fact]
        public void ReadFromStream_UpdatePlayerTalkingToNpc_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerTalkingToNpcBytes)) {
                var packet = (UpdatePlayerTalkingToNpcPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(1);
                packet.NpcIndex.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerTalkingToNpc_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerTalkingToNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerTalkingToNpcBytes);
            }
        }

        public static readonly byte[] UpdatePlayerItemAnimationBytes = {10, 0, 41, 0, 60, 244, 29, 63, 3, 0,};

        [Fact]
        public void ReadFromStream_UpdatePlayerItemAnimation_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerItemAnimationBytes)) {
                var packet = (UpdatePlayerItemAnimationPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerItemAnimation.Should().Be(3);
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerItemAnimation_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerItemAnimationBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerItemAnimationBytes);
            }
        }

        public static readonly byte[] PerformActionBytes = {5, 0, 51, 0, 1};

        [Fact]
        public void ReadFromStream_PerformAction_IsCorrect() {
            using (var stream = new MemoryStream(PerformActionBytes)) {
                var packet = (PerformActionPacket)Packet.ReadFromStream(stream);

                packet.PlayerOrNpcIndex.Should().Be(0);
                packet.ActionType.Should().Be(PerformActionPacket.Type.SpawnSkeletron);
            }
        }

        [Fact]
        public void WriteToStream_PerformAction_IsCorrect() {
            using (var stream = new MemoryStream(PerformActionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PerformActionBytes);
            }
        }

        public static readonly byte[] UnlockTileBytes = {8, 0, 52, 1, 0, 1, 100, 0};

        [Fact]
        public void ReadFromStream_UnlockTile_IsCorrect() {
            using (var stream = new MemoryStream(UnlockTileBytes)) {
                var packet = (UnlockTilePacket)Packet.ReadFromStream(stream);

                packet.ObjectType.Should().Be(UnlockTilePacket.Type.UnlockChest);
                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_UnlockTile_IsCorrect() {
            using (var stream = new MemoryStream(UnlockTileBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UnlockTileBytes);
            }
        }

        public static readonly byte[] AddNpcBuffBytes = {8, 0, 53, 0, 0, 1, 100, 0};

        [Fact]
        public void ReadFromStream_AddNpcBuff_IsCorrect() {
            using (var stream = new MemoryStream(AddNpcBuffBytes)) {
                var packet = (AddNpcBuffPacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(0);
                packet.BuffType.Should().Be(BuffType.ObsidianSkin);
                packet.BuffTime.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_AddNpcBuff_IsCorrect() {
            using (var stream = new MemoryStream(AddNpcBuffBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(AddNpcBuffBytes);
            }
        }

        public static readonly byte[] UpdateNpcBuffsBytes = {10, 0, 54, 0, 0, 0, 0, 0, 0, 0};

        [Fact]
        public void ReadFromStream_UpdateNpcBuffs_IsCorrect() {
            using (var stream = new MemoryStream(UpdateNpcBuffsBytes)) {
                var packet = (UpdateNpcBuffsPacket)Packet.ReadFromStream(stream);
                
                packet.NpcIndex.Should().Be(0);
                foreach (var buffType in packet.NpcBuffs) {
                    buffType.Should().Be(BuffType.None);
                }
            }
        }

        [Fact]
        public void WriteToStream_UpdateNpcBuffs_IsCorrect() {
            using (var stream = new MemoryStream(UpdateNpcBuffsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateNpcBuffsBytes);
            }
        }

        public static readonly byte[] UpdateNpcNameBytes = {14, 0, 56, 0, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97};

        [Fact]
        public void ReadFromStream_UpdateNpcName_IsCorrect() {
            using (var stream = new MemoryStream(UpdateNpcNameBytes)) {
                var packet = (UpdateNpcNamePacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(0);
                packet.NpcName.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_UpdateNpcName_IsCorrect() {
            using (var stream = new MemoryStream(UpdateNpcNameBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateNpcNameBytes);
            }
        }

        public static readonly byte[] UpdateBiomeStatsBytes = {6, 0, 57, 1, 2, 3};

        [Fact]
        public void ReadFromStream_UpdateBiomeStats_IsCorrect() {
            using (var stream = new MemoryStream(UpdateBiomeStatsBytes)) {
                var packet = (UpdateBiomeStatsPacket)Packet.ReadFromStream(stream);

                packet.GoodAmount.Should().Be(1);
                packet.CorruptionAmount.Should().Be(2);
                packet.CrimsonAmount.Should().Be(3);
            }
        }

        [Fact]
        public void WriteToStream_UpdateBiomeStats_IsCorrect() {
            using (var stream = new MemoryStream(UpdateBiomeStatsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateBiomeStatsBytes);
            }
        }
        
        public static readonly byte[] PlayHarpNoteBytes = {8, 0, 58, 0, 205, 204, 128, 64};

        [Fact]
        public void ReadFromStream_PlayHarpNote_IsCorrect() {
            using (var stream = new MemoryStream(PlayHarpNoteBytes)) {
                var packet = (PlayHarpNotePacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.HarpNote.Should().Be(4.025f);
            }
        }

        [Fact]
        public void WriteToStream_PlayHarpNote_IsCorrect() {
            using (var stream = new MemoryStream(PlayHarpNoteBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlayHarpNoteBytes);
            }
        }
        
        public static readonly byte[] UpdateNpcHomeBytes = {10, 0, 60, 0, 0, 0, 1, 100, 0, 0};

        [Fact]
        public void ReadFromStream_UpdateNpcHome_IsCorrect() {
            using (var stream = new MemoryStream(UpdateNpcHomeBytes)) {
                var packet = (UpdateNpcHomePacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(0);
                packet.NpcHomeX.Should().Be(256);
                packet.NpcHomeY.Should().Be(100);
                packet.IsNpcHomeless.Should().BeFalse();
            }
        }

        [Fact]
        public void WriteToStream_UpdateNpcHome_IsCorrect() {
            using (var stream = new MemoryStream(UpdateNpcHomeBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateNpcHomeBytes);
            }
        }
        
        public static readonly byte[] SummonBossOrInvasionBytes = {7, 0, 61, 0, 0, 255, 255};

        [Fact]
        public void ReadFromStream_SummonBossOrInvasion_IsCorrect() {
            using (var stream = new MemoryStream(SummonBossOrInvasionBytes)) {
                var packet = (SummonBossOrInvasionPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.BossOrInvasionType.Should().Be(-1);
            }
        }

        [Fact]
        public void WriteToStream_SummonBossOrInvasion_IsCorrect() {
            using (var stream = new MemoryStream(SummonBossOrInvasionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(SummonBossOrInvasionBytes);
            }
        }
        
        public static readonly byte[] ShowPlayerDodgeBytes = {5, 0, 62, 0, 1};

        [Fact]
        public void ReadFromStream_ShowPlayerDodge_IsCorrect() {
            using (var stream = new MemoryStream(ShowPlayerDodgeBytes)) {
                var packet = (ShowPlayerDodgePacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerDodgeType.Should().Be(ShowPlayerDodgePacket.Type.NinjaDodge);
            }
        }

        [Fact]
        public void WriteToStream_ShowPlayerDodge_IsCorrect() {
            using (var stream = new MemoryStream(ShowPlayerDodgeBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ShowPlayerDodgeBytes);
            }
        }
        
        public static readonly byte[] TeleportEntityBytes = {14, 0, 65, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        [Fact]
        public void ReadFromStream_TeleportEntity_IsCorrect() {
            using (var stream = new MemoryStream(TeleportEntityBytes)) {
                var packet = (TeleportEntityPacket)Packet.ReadFromStream(stream);

                packet.TeleportationType.Should().Be(TeleportEntityPacket.Type.TeleportPlayerToOtherPlayer);
                packet.TeleportationStyle.Should().Be(0);
                packet.PlayerOrNpcIndex.Should().Be(0);
                packet.Position.Should().Be(Vector2.Zero);
            }
        }

        [Fact]
        public void WriteToStream_TeleportEntity_IsCorrect() {
            using (var stream = new MemoryStream(TeleportEntityBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(TeleportEntityBytes);
            }
        }
        
        public static readonly byte[] HealOtherPlayerBytes = {6, 0, 66, 0, 100, 0};

        [Fact]
        public void ReadFromStream_HealOtherPlayer_IsCorrect() {
            using (var stream = new MemoryStream(HealOtherPlayerBytes)) {
                var packet = (HealOtherPlayerPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.HealAmount.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_HealOtherPlayer_IsCorrect() {
            using (var stream = new MemoryStream(HealOtherPlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(HealOtherPlayerBytes);
            }
        }
        
        public static readonly byte[] RequestOrUpdateChestNameBytes = {18, 0, 69, 0, 0, 0, 1, 100, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97};

        [Fact]
        public void ReadFromStream_RequestOrUpdateChestName_IsCorrect() {
            using (var stream = new MemoryStream(RequestOrUpdateChestNameBytes)) {
                var packet = (RequestOrUpdateChestNamePacket)Packet.ReadFromStream(stream);

                packet.ChestIndex.Should().Be(0);
                packet.ChestX.Should().Be(256);
                packet.ChestY.Should().Be(100);
                packet.ChestName.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_RequestOrUpdateChestName_IsCorrect() {
            using (var stream = new MemoryStream(RequestOrUpdateChestNameBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RequestOrUpdateChestNameBytes);
            }
        }
        
        public static readonly byte[] CatchNpcBytes = {6, 0, 70, 1, 0, 0};

        [Fact]
        public void ReadFromStream_CatchNpc_IsCorrect() {
            using (var stream = new MemoryStream(CatchNpcBytes)) {
                var packet = (CatchNpcPacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(1);
                packet.NpcCatcherPlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_CatchNpc_IsCorrect() {
            using (var stream = new MemoryStream(CatchNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(CatchNpcBytes);
            }
        }
        
        public static readonly byte[] ReleaseNpcBytes = {14, 0, 71, 0, 1, 0, 0, 100, 0, 0, 0, 1, 0, 0};

        [Fact]
        public void ReadFromStream_ReleaseNpc_IsCorrect() {
            using (var stream = new MemoryStream(ReleaseNpcBytes)) {
                var packet = (ReleaseNpcPacket)Packet.ReadFromStream(stream);

                packet.NpcX.Should().Be(256);
                packet.NpcY.Should().Be(100);
                packet.NpcType.Should().Be(NpcType.BlueSlime);
                packet.NpcStyle.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_ReleaseNpc_IsCorrect() {
            using (var stream = new MemoryStream(ReleaseNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ReleaseNpcBytes);
            }
        }

        public static readonly byte[] UpdateTravelingMerchantInventoryBytes = {
            83, 0, 72, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        [Fact]
        public void ReadFromStream_UpdateTravelingMerchantInventory_IsCorrect() {
            using (var stream = new MemoryStream(UpdateTravelingMerchantInventoryBytes)) {
                var packet = (UpdateTravelingMerchantInventoryPacket)Packet.ReadFromStream(stream);

                foreach (var itemType in packet.ShopItems) {
                    itemType.Should().Be(ItemType.None);
                }
            }
        }

        [Fact]
        public void WriteToStream_UpdateTravelingMerchantInventory_IsCorrect() {
            using (var stream = new MemoryStream(UpdateTravelingMerchantInventoryBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateTravelingMerchantInventoryBytes);
            }
        }

        public static readonly byte[] UpdateAnglerQuestBytes = {5, 0, 74, 1, 1};

        [Fact]
        public void ReadFromStream_UpdateAnglerQuest_IsCorrect() {
            using (var stream = new MemoryStream(UpdateAnglerQuestBytes)) {
                var packet = (UpdateAnglerQuestPacket)Packet.ReadFromStream(stream);

                packet.AnglerQuest.Should().Be(1);
                packet.IsAnglerQuestFinished.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_UpdateAnglerQuest_IsCorrect() {
            using (var stream = new MemoryStream(UpdateAnglerQuestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateAnglerQuestBytes);
            }
        }

        public static readonly byte[] CompleteAnglerQuestBytes = {3, 0, 75};

        [Fact]
        public void ReadFromStream_CompleteAnglerQuest_IsCorrect() {
            using (var stream = new MemoryStream(CompleteAnglerQuestBytes)) {
                Packet.ReadFromStream(stream).Should().BeOfType<CompleteAnglerQuestPacket>();
            }
        }

        [Fact]
        public void WriteToStream_CompleteAnglerQuest_IsCorrect() {
            using (var stream = new MemoryStream(CompleteAnglerQuestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(CompleteAnglerQuestBytes);
            }
        }

        public static readonly byte[] UpdateAnglerQuestsCompletedBytes = {7, 0, 76, 1, 1, 0, 0};

        [Fact]
        public void ReadFromStream_UpdateAnglerQuestsCompleted_IsCorrect() {
            using (var stream = new MemoryStream(UpdateAnglerQuestsCompletedBytes)) {
                var packet = (UpdateAnglerQuestsCompletedPacket)Packet.ReadFromStream(stream);

                packet.NumberOfAnglerQuestsCompleted.Should().Be(257);
            }
        }

        [Fact]
        public void WriteToStream_UpdateAnglerQuestsCompleted_IsCorrect() {
            using (var stream = new MemoryStream(UpdateAnglerQuestsCompletedBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateAnglerQuestsCompletedBytes);
            }
        }

        public static readonly byte[] CreateTemporaryAnimationBytes = {11, 0, 77, 1, 0, 1, 0, 0, 1, 100, 0};

        [Fact]
        public void ReadFromStream_CreateTemporaryAnimation_IsCorrect() {
            using (var stream = new MemoryStream(CreateTemporaryAnimationBytes)) {
                var packet = (CreateTemporaryAnimationPacket)Packet.ReadFromStream(stream);

                packet.AnimationType.Should().Be(1);
                packet.BlockType.Should().Be(BlockType.Stone);
                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_CreateTemporaryAnimation_IsCorrect() {
            using (var stream = new MemoryStream(CreateTemporaryAnimationBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(CreateTemporaryAnimationBytes);
            }
        }

        public static readonly byte[] PlaceObjectBytes = {14, 0, 79, 0, 1, 100, 0, 21, 0, 1, 0, 0, 255, 1};

        [Fact]
        public void ReadFromStream_PlaceObject_IsCorrect() {
            using (var stream = new MemoryStream(PlaceObjectBytes)) {
                var packet = (PlaceObjectPacket)Packet.ReadFromStream(stream);
                
                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
                packet.ObjectType.Should().Be(BlockType.Containers);
                packet.ObjectStyle.Should().Be(1);
                packet.Alternate.Should().Be(0);
                packet.RandomState.Should().Be(-1);
                packet.Direction.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_PlaceObject_IsCorrect() {
            using (var stream = new MemoryStream(PlaceObjectBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlaceObjectBytes);
            }
        }

        public static readonly byte[] UpdateOtherPlayerChestBytes = {6, 0, 80, 0, 255, 255};

        [Fact]
        public void ReadFromStream_UpdateOtherPlayerChest_IsCorrect() {
            using (var stream = new MemoryStream(UpdateOtherPlayerChestBytes)) {
                var packet = (UpdateOtherPlayerChestPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerChestIndex.Should().Be(-1);
            }
        }

        [Fact]
        public void WriteToStream_UpdateOtherPlayerChest_IsCorrect() {
            using (var stream = new MemoryStream(UpdateOtherPlayerChestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateOtherPlayerChestBytes);
            }
        }

        public static readonly byte[] CreateCombatTextBytes = {18, 0, 81, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 100, 0, 0, 0};

        [Fact]
        public void ReadFromStream_CreateCombatText_IsCorrect() {
            using (var stream = new MemoryStream(CreateCombatTextBytes)) {
                var packet = (CreateCombatTextPacket)Packet.ReadFromStream(stream);

                packet.TextX.Should().Be(0);
                packet.TextY.Should().Be(0);
                packet.TextColor.Should().Be(Color.White);
                packet.TextNumber.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_CreateCombatText_IsCorrect() {
            using (var stream = new MemoryStream(CreateCombatTextBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(CreateCombatTextBytes);
            }
        }

        public static readonly byte[] UpdateNpcKillsBytes = {9, 0, 83, 1, 0, 100, 0, 0, 0};

        [Fact]
        public void ReadFromStream_UpdateNpcKills_IsCorrect() {
            using (var stream = new MemoryStream(UpdateNpcKillsBytes)) {
                var packet = (UpdateNpcKillsPacket)Packet.ReadFromStream(stream);

                packet.NpcType.Should().Be(NpcType.BlueSlime);
                packet.NpcTypeKillCount.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_UpdateNpcKills_IsCorrect() {
            using (var stream = new MemoryStream(UpdateNpcKillsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateNpcKillsBytes);
            }
        }

        public static readonly byte[] UpdatePlayerStealthBytes = {8, 0, 84, 0, 0, 0, 0, 0};

        [Fact]
        public void ReadFromStream_UpdatePlayerStealth_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerStealthBytes)) {
                var packet = (UpdatePlayerStealthPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerStealthStatus.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerStealth_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerStealthBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerStealthBytes);
            }
        }

        public static readonly byte[] MoveItemIntoChestBytes = {4, 0, 85, 1};

        [Fact]
        public void ReadFromStream_MoveItemIntoChest_IsCorrect() {
            using (var stream = new MemoryStream(MoveItemIntoChestBytes)) {
                var packet = (MoveItemIntoChestPacket)Packet.ReadFromStream(stream);

                packet.PlayerInventorySlot.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_MoveItemIntoChest_IsCorrect() {
            using (var stream = new MemoryStream(MoveItemIntoChestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(MoveItemIntoChestBytes);
            }
        }
    }
}
