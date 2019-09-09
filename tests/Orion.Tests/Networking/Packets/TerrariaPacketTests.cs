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
