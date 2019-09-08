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

        public static readonly byte[] RequestConnectionBytes = {
            15, 0, 1, 11, 84, 101, 114, 114, 97, 114, 105, 97, 49, 57, 52
        };

        [Fact]
        public void ReadFromStream_RequestConnection_IsCorrect() {
            using (var stream = new MemoryStream(RequestConnectionBytes)) {
                var packet = (RequestConnectionPacket)Packet.ReadFromStream(stream);

                packet.Version.Should().Be("Terraria194");
            }
        }

        [Fact]
        public void WriteToStream_RequestConnection_IsCorrect() {
            using (var stream = new MemoryStream(RequestConnectionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RequestConnectionBytes);
            }
        }

        public static readonly byte[] DisconnectPlayerBytes = {
            21, 0, 2, 2, 15, 67, 76, 73, 46, 75, 105, 99, 107, 77, 101, 115, 115, 97, 103, 101, 0,
        };


        [Fact]
        public void ReadFromStream_DisconnectPlayer_IsCorrect() {
            using (var stream = new MemoryStream(DisconnectPlayerBytes)) {
                var packet = (DisconnectPlayerPacket)Packet.ReadFromStream(stream);

                packet.Reason.ToString().Should().Be("CLI.KickMessage");
            }
        }

        [Fact]
        public void WriteToStream_DisconnectPlayer_IsCorrect() {
            using (var stream = new MemoryStream(DisconnectPlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(DisconnectPlayerBytes);
            }
        }

        public static readonly byte[] ContinueConnectionBytes = {4, 0, 3, 0};

        [Fact]
        public void ReadFromStream_ContinueConnection_IsCorrect() {
            using (var stream = new MemoryStream(ContinueConnectionBytes)) {
                var packet = (ContinueConnectionPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_ContinueConnection_IsCorrect() {
            using (var stream = new MemoryStream(ContinueConnectionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ContinueConnectionBytes);
            }
        }

        public static readonly byte[] UpdatePlayerInfoBytes = {
            34, 0, 4, 0, 2, 50, 1, 102, 0, 0, 0, 0, 26, 131, 54, 158, 74, 51, 47, 39, 88, 184, 58, 43, 69, 8, 97, 162,
            167, 255, 212, 159, 76, 0
        };

        [Fact]
        public void ReadFromStream_UpdatePlayerInfo_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerInfoBytes)) {
                var packet = (UpdatePlayerInfoPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.SkinType.Should().Be(2);
                packet.Name.Should().Be("f");
                packet.HairDye.Should().Be(0);
                packet.HiddenVisualsFlags.Should().Be(0);
                packet.HiddenMiscFlags.Should().Be(0);
                packet.HairColor.Should().Be(new Color(26, 131, 54));
                packet.SkinColor.Should().Be(new Color(158, 74, 51));
                packet.EyeColor.Should().Be(new Color(47, 39, 88));
                packet.ShirtColor.Should().Be(new Color(184, 58, 43));
                packet.UndershirtColor.Should().Be(new Color(69, 8, 97));
                packet.PantsColor.Should().Be(new Color(162, 167, 255));
                packet.ShoeColor.Should().Be(new Color(212, 159, 76));
                packet.Difficulty.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerInfo_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerInfoBytes);
            }
        }

        private static readonly byte[] UpdatePlayerInventorySlotBytes = {10, 0, 5, 0, 0, 1, 0, 59, 179, 13};

        [Fact]
        public void ReadFromStream_UpdatePlayerInventorySlot_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerInventorySlotBytes)) {
                var packet = (UpdatePlayerInventorySlotPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.InventorySlotIndex.Should().Be(0);
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.Godly);
                packet.ItemType.Should().Be(ItemType.CopperShortsword);
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerInventorySlot_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerInventorySlotBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerInventorySlotBytes);
            }
        }

        public static readonly byte[] FinishConnectionBytes = {3, 0, 6};

        [Fact]
        public void ReadFromStream_FinishConnection_IsCorrect() {
            using (var stream = new MemoryStream(FinishConnectionBytes)) {
                Packet.ReadFromStream(stream);
            }
        }

        [Fact]
        public void WriteToStream_FinishConnection_IsCorrect() {
            using (var stream = new MemoryStream(FinishConnectionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(FinishConnectionBytes);
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

        private static readonly byte[] RequestWorldSectionBytes = {11, 0, 8, 255, 255, 255, 255, 255, 255, 255, 255};

        [Fact]
        public void ReadFromStream_RequestWorldSection_IsCorrect() {
            using (var stream = new MemoryStream(RequestWorldSectionBytes)) {
                var packet = (RequestWorldSectionPacket)Packet.ReadFromStream(stream);

                packet.SectionX.Should().Be(-1);
                packet.SectionY.Should().Be(-1);
            }
        }

        [Fact]
        public void WriteToStream_RequestWorldSection_IsCorrect() {
            using (var stream = new MemoryStream(RequestWorldSectionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RequestWorldSectionBytes);
            }
        }

        private static readonly byte[] UpdateClientStatusBytes = {
            28, 0, 9, 15, 0, 0, 0, 2, 18, 76, 101, 103, 97, 99, 121, 73, 110, 116, 101, 114, 102, 97, 99, 101, 46, 52,
            52, 0,
        };

        [Fact]
        public void ReadFromStream_UpdateClientStatus_IsCorrect() {
            using (var stream = new MemoryStream(UpdateClientStatusBytes)) {
                var packet = (UpdateClientStatusPacket)Packet.ReadFromStream(stream);

                packet.StatusIncrease.Should().Be(15);
                packet.StatusText.ToString().Should().Be("LegacyInterface.44");
            }
        }

        [Fact]
        public void WriteToStream_UpdateClientStatus_IsCorrect() {
            using (var stream = new MemoryStream(UpdateClientStatusBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateClientStatusBytes);
            }
        }

        private static readonly byte[] UpdateWorldSectionBytes = {
            254, 7, 10, 1, 109, 87, 189, 143, 28, 73, 21, 175, 170, 254, 152, 158, 153, 253, 152, 237, 195, 189, 235,
            51, 8, 99, 6, 123, 56, 77, 96, 144, 88, 7, 67, 48, 106, 208, 178, 23, 64, 2, 18, 163, 11, 64, 66, 156, 124,
            32, 52, 194, 123, 119, 66, 66, 103, 100, 132, 137, 78, 66, 68, 4, 142, 208, 5, 54, 100, 78, 173, 203, 87,
            228, 151, 238, 95, 177, 34, 198, 188, 143, 223, 235, 170, 26, 159, 45, 117, 245, 171, 122, 245, 222, 239,
            253, 222, 71, 207, 126, 48, 115, 110, 233, 157, 251, 143, 251, 135, 123, 242, 135, 31, 135, 170, 115, 151,
            110, 253, 79, 172, 243, 80, 45, 221, 198, 173, 63, 13, 149, 115, 215, 110, 125, 136, 253, 192, 114, 231,
            214, 199, 44, 95, 67, 190, 114, 235, 17, 244, 27, 236, 255, 153, 247, 123, 185, 231, 232, 159, 234, 145,
            220, 133, 170, 39, 57, 84, 91, 210, 14, 213, 134, 239, 120, 232, 212, 108, 227, 74, 118, 59, 177, 212, 211,
            13, 126, 146, 253, 39, 81, 34, 235, 7, 124, 99, 43, 86, 59, 182, 112, 204, 242, 18, 242, 82, 208, 136, 220,
            68, 239, 130, 254, 49, 246, 37, 154, 14, 168, 28, 238, 155, 94, 15, 36, 151, 130, 68, 173, 72, 108, 1, 183,
            31, 179, 172, 177, 245, 132, 2, 152, 124, 134, 240, 6, 175, 75, 139, 197, 91, 100, 91, 104, 92, 138, 15,
            137, 161, 73, 25, 114, 192, 184, 1, 22, 138, 124, 202, 171, 203, 152, 95, 34, 118, 97, 116, 132, 253, 145,
            97, 222, 10, 166, 13, 235, 120, 196, 243, 73, 140, 199, 1, 105, 103, 209, 77, 17, 85, 130, 113, 99, 24, 59,
            216, 150, 179, 107, 187, 63, 2, 210, 38, 34, 145, 44, 60, 134, 182, 241, 52, 141, 178, 232, 117, 145, 147,
            101, 140, 103, 132, 92, 52, 49, 135, 178, 142, 145, 179, 139, 52, 55, 29, 248, 88, 198, 92, 89, 238, 124,
            172, 158, 165, 176, 171, 49, 110, 145, 161, 75, 120, 218, 198, 155, 99, 228, 241, 34, 238, 27, 110, 203,
            235, 21, 56, 92, 102, 53, 226, 80, 99, 221, 78, 157, 94, 199, 90, 30, 35, 59, 137, 237, 30, 121, 49, 207, 3,
            214, 4, 185, 213, 226, 54, 227, 71, 80, 54, 59, 254, 199, 56, 207, 208, 111, 98, 62, 77, 47, 171, 109, 151,
            225, 54, 30, 165, 186, 199, 89, 28, 143, 210, 14, 89, 102, 86, 173, 227, 58, 84, 169, 228, 118, 20, 89, 73,
            42, 37, 68, 54, 4, 227, 163, 236, 52, 143, 123, 138, 172, 36, 61, 25, 17, 88, 157, 88, 79, 247, 113, 127,
            12, 4, 23, 17, 145, 131, 61, 211, 91, 14, 136, 151, 200, 68, 111, 179, 167, 142, 181, 156, 221, 30, 35, 158,
            139, 148, 245, 109, 214, 45, 121, 125, 93, 163, 91, 114, 166, 109, 38, 198, 105, 101, 243, 100, 152, 115,
            23, 59, 93, 51, 137, 221, 217, 197, 59, 37, 186, 161, 194, 124, 157, 160, 42, 66, 204, 154, 206, 148, 14,
            30, 229, 212, 199, 190, 239, 178, 122, 119, 195, 105, 202, 191, 245, 81, 30, 25, 245, 64, 17, 59, 226, 82,
            176, 72, 60, 147, 52, 222, 109, 54, 191, 151, 89, 63, 119, 168, 47, 201, 239, 163, 108, 250, 100, 245, 149,
            216, 50, 31, 85, 100, 197, 225, 91, 145, 96, 78, 122, 163, 219, 233, 13, 97, 254, 34, 178, 36, 214, 39, 54,
            129, 182, 177, 23, 61, 238, 148, 208, 169, 118, 59, 213, 13, 117, 209, 101, 95, 150, 30, 115, 242, 10, 156,
            109, 108, 234, 14, 61, 96, 147, 148, 242, 245, 251, 47, 217, 243, 200, 195, 52, 237, 120, 203, 117, 18, 251,
            56, 139, 217, 170, 171, 206, 114, 155, 77, 135, 30, 108, 219, 116, 232, 179, 170, 223, 130, 7, 203, 174,
            203, 154, 178, 68, 160, 85, 30, 224, 221, 79, 253, 223, 233, 184, 164, 245, 51, 172, 153, 204, 37, 18, 63,
            6, 219, 157, 49, 99, 3, 112, 248, 228, 55, 233, 240, 178, 33, 212, 103, 77, 155, 124, 252, 11, 236, 26, 33,
            190, 84, 255, 45, 252, 183, 238, 13, 89, 239, 36, 109, 101, 195, 63, 25, 123, 54, 62, 100, 61, 8, 183, 152,
            192, 112, 171, 149, 231, 92, 158, 167, 204, 200, 47, 222, 252, 108, 185, 228, 35, 17, 127, 194, 116, 25,
            123, 218, 98, 63, 44, 1, 37, 201, 111, 184, 243, 71, 207, 54, 104, 105, 117, 73, 36, 23, 113, 198, 6, 31,
            74, 203, 134, 95, 25, 110, 157, 11, 194, 247, 52, 233, 137, 148, 190, 59, 14, 170, 149, 160, 90, 9, 170,
            149, 160, 90, 183, 254, 101, 250, 213, 191, 76, 234, 242, 10, 189, 246, 84, 128, 188, 116, 161, 16, 66, 10,
            33, 100, 216, 127, 234, 6, 130, 37, 212, 151, 216, 117, 119, 191, 240, 115, 169, 139, 47, 60, 147, 87, 150,
            229, 238, 206, 144, 77, 124, 58, 194, 3, 1, 250, 160, 117, 198, 69, 171, 92, 180, 59, 146, 139, 37, 252,
            198, 79, 180, 236, 67, 179, 1, 63, 173, 176, 208, 130, 31, 147, 210, 119, 225, 100, 46, 194, 124, 120, 63,
            165, 231, 250, 231, 22, 183, 80, 250, 74, 162, 47, 78, 229, 249, 59, 217, 249, 83, 194, 202, 210, 61, 19,
            32, 202, 194, 51, 211, 196, 126, 207, 156, 189, 43, 118, 222, 61, 143, 118, 94, 137, 157, 226, 60, 241, 194,
            251, 189, 156, 122, 108, 9, 136, 226, 126, 232, 67, 85, 220, 14, 231, 175, 67, 36, 251, 25, 29, 107, 193,
            180, 178, 20, 51, 210, 242, 154, 128, 22, 116, 183, 67, 2, 146, 29, 114, 244, 153, 3, 190, 151, 130, 219,
            114, 154, 67, 126, 32, 165, 243, 64, 74, 231, 213, 80, 168, 115, 77, 199, 124, 71, 226, 155, 79, 113, 243,
            206, 11, 57, 161, 91, 159, 84, 124, 247, 111, 252, 148, 176, 72, 120, 47, 119, 253, 12, 177, 104, 7, 61, 53,
            201, 62, 153, 101, 154, 32, 206, 99, 154, 46, 39, 207, 86, 92, 145, 112, 42, 7, 167, 146, 224, 83, 57, 56,
            101, 179, 159, 255, 55, 208, 249, 231, 117, 193, 207, 19, 122, 174, 127, 82, 204, 124, 239, 137, 173, 147,
            222, 249, 179, 23, 193, 211, 210, 135, 73, 112, 125, 184, 71, 44, 234, 252, 222, 162, 136, 204, 42, 65, 223,
            72, 52, 191, 173, 218, 193, 197, 169, 60, 231, 195, 187, 228, 160, 164, 76, 77, 200, 107, 235, 30, 78, 200,
            173, 44, 39, 188, 156, 63, 252, 81, 113, 159, 60, 143, 207, 92, 120, 171, 118, 43, 90, 138, 58, 132, 218, 5,
            6, 65, 82, 115, 246, 92, 214, 49, 237, 210, 226, 117, 121, 91, 224, 121, 178, 173, 213, 66, 49, 189, 106,
            221, 78, 129, 173, 11, 169, 146, 113, 239, 202, 62, 236, 147, 238, 249, 235, 119, 4, 205, 184, 247, 62, 132,
            222, 143, 206, 136, 97, 178, 54, 85, 163, 141, 250, 135, 228, 201, 113, 65, 235, 183, 197, 213, 30, 53, 25,
            9, 7, 122, 54, 82, 118, 196, 228, 68, 76, 142, 122, 95, 244, 161, 81, 173, 154, 236, 170, 9, 31, 166, 180,
            30, 169, 97, 218, 90, 113, 100, 181, 231, 195, 146, 37, 217, 85, 63, 135, 186, 189, 82, 15, 51, 243, 234,
            69, 213, 139, 58, 191, 31, 209, 123, 33, 215, 124, 96, 210, 14, 121, 191, 166, 107, 131, 11, 175, 134, 72,
            121, 85, 13, 222, 56, 26, 93, 23, 108, 42, 148, 234, 166, 96, 107, 170, 52, 168, 126, 71, 143, 238, 169, 83,
            98, 92, 44, 31, 65, 172, 193, 217, 62, 153, 47, 234, 197, 138, 236, 5, 90, 72, 169, 246, 164, 183, 42, 97,
            149, 177, 118, 180, 142, 132, 181, 69, 136, 196, 194, 245, 55, 64, 37, 184, 135, 249, 144, 132, 89, 34, 244,
            17, 220, 133, 179, 197, 138, 241, 23, 228, 73, 161, 142, 224, 168, 82, 35, 135, 36, 54, 209, 102, 73, 186,
            124, 122, 44, 80, 93, 0, 225, 115, 68, 80, 36, 174, 42, 100, 139, 25, 157, 24, 53, 124, 188, 96, 175, 114,
            92, 33, 169, 199, 64, 180, 71, 251, 172, 126, 64, 122, 141, 92, 243, 172, 234, 235, 23, 188, 212, 130, 217,
            227, 166, 58, 154, 145, 124, 132, 148, 113, 44, 165, 148, 246, 66, 82, 201, 250, 39, 176, 60, 165, 189, 6,
            150, 9, 204, 10, 244, 112, 91, 178, 233, 125, 48, 92, 75, 54, 212, 116, 194, 226, 161, 132, 164, 30, 138,
            179, 231, 84, 14, 154, 36, 182, 192, 121, 208, 242, 185, 137, 96, 11, 189, 24, 224, 146, 161, 77, 216, 238,
            208, 131, 149, 220, 85, 26, 106, 212, 93, 45, 212, 83, 198, 37, 155, 82, 30, 35, 120, 110, 89, 92, 16, 33,
            11, 193, 88, 38, 174, 42, 37, 104, 52, 224, 83, 142, 11, 129, 239, 217, 87, 137, 178, 45, 146, 66, 245, 32,
            165, 2, 34, 183, 26, 89, 43, 193, 237, 1, 180, 36, 14, 13, 167, 70, 10, 1, 134, 194, 102, 18, 20, 206, 219,
            22, 57, 174, 220, 28, 250, 80, 139, 183, 214, 94, 96, 178, 185, 37, 204, 107, 1, 150, 164, 144, 20, 82, 35,
            253, 204, 249, 89, 141, 134, 14, 228, 20, 236, 129, 167, 16, 231, 86, 45, 225, 112, 206, 245, 238, 9, 74,
            213, 242, 49, 179, 4, 106, 62, 3, 216, 157, 218, 200, 64, 73, 46, 20, 142, 214, 213, 33, 42, 239, 8, 174,
            44, 162, 22, 190, 188, 212, 191, 250, 177, 218, 98, 185, 26, 12, 162, 212, 247, 184, 123, 109, 47, 176, 16,
            172, 149, 13, 80, 3, 91, 85, 188, 197, 181, 87, 69, 178, 143, 208, 139, 95, 225, 28, 43, 200, 113, 172, 204,
            58, 22, 168, 77, 47, 54, 127, 75, 73, 171, 16, 110, 109, 190, 45, 145, 149, 116, 148, 51, 245, 194, 242,
            170, 232, 145, 185, 155, 136, 187, 65, 167, 214, 82, 170, 158, 173, 25, 189, 48, 119, 19, 197, 59, 171, 211,
            106, 169, 20, 68, 13, 16, 7, 58, 91, 66, 116, 205, 226, 125, 88, 26, 163, 15, 70, 66, 114, 230, 4, 83, 232,
            6, 15, 66, 197, 123, 15, 44, 6, 245, 48, 177, 239, 130, 206, 195, 14, 223, 50, 92, 159, 162, 57, 166, 195,
            124, 82, 66, 91, 12, 202, 51, 191, 242, 80, 125, 7, 145, 239, 89, 54, 23, 73, 169, 45, 117, 25, 127, 137,
            57, 190, 123, 194, 225, 169, 41, 182, 254, 125, 155, 126, 106, 3, 49, 180, 202, 193, 93, 30, 11, 108, 127,
            40, 109, 251, 4, 120, 88, 146, 161, 79, 111, 114, 235, 91, 160, 151, 109, 13, 245, 244, 86, 109, 53, 155,
            216, 71, 2, 56, 228, 49, 183, 102, 228, 166, 81, 207, 251, 72, 2, 79, 83, 80, 193, 83, 95, 113, 232, 229,
            239, 161, 164, 91, 155, 246, 26, 223, 158, 138, 7, 195, 84, 95, 112, 161, 132, 27, 176, 63, 1, 120, 105,
            112, 241, 32, 65, 168, 229, 175, 225, 240, 14, 2, 193, 167, 106, 134, 192, 65, 240, 184, 30, 44, 73, 100,
            234, 224, 171, 17, 134, 217, 151, 57, 38, 227, 132, 141, 31, 43, 17, 26, 152, 245, 234, 55, 45, 43, 184,
            182, 143, 26, 146, 227, 133, 252, 122, 104, 130, 123, 237, 47, 102, 255, 246, 147, 31, 124, 240, 254, 135,
            31, 221, 254, 25, 61, 254, 231, 63, 166, 13, 199, 255, 159, 208, 203, 244, 167, 191, 121, 184, 189, 253, 17,
            159, 126, 183, 116, 242, 167, 211, 135, 179, 231, 178, 58, 207, 203, 199, 36, 125, 253, 192, 253, 202, 59,
            250, 177, 231, 254, 50, 251, 151, 255, 53, 253, 217, 65, 175, 127, 165, 215, 247, 221, 255, 1,
        };

        [Fact]
        public void ReadFromStream_UpdateWorldSection_IsCorrect() {
            using (var stream = new MemoryStream(UpdateWorldSectionBytes)) {
                var packet = (UpdateWorldSectionPacket)Packet.ReadFromStream(stream);

                packet.IsCompressed.Should().BeTrue();
                packet.StartX.Should().Be(4200);
                packet.StartY.Should().Be(300);
                packet.Width.Should().Be(200);
                packet.Height.Should().Be(150);
                packet.Tiles.GetLength(0).Should().Be(200);
                packet.Tiles.GetLength(1).Should().Be(150);
                packet.Chests.Should().HaveCount(2);
                packet.Signs.Should().HaveCount(1);
                packet.TileEntities.Should().HaveCount(4);
            }
        }

        [Fact]
        public void WriteToStream_UpdateWorldSection_IsCorrect() {
            using (var stream = new MemoryStream(UpdateWorldSectionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = (UpdateWorldSectionPacket)Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.Position = 0;
                var packet2 = (UpdateWorldSectionPacket)Packet.ReadFromStream(stream2);

                // Compare packet and packet2, since we can't guarantee that DeflateStream will always compress the
                // exact same way.
                for (var x = 0; x < packet.Width; ++x) {
                    for (var y = 0; y < packet.Height; ++y) {
                        packet2.Tiles[x, y].Should().BeEquivalentTo(packet.Tiles[x, y]);
                    }
                }

                for (var i = 0; i < packet.Chests.Count; ++i) {
                    packet2.Chests[i].Should().BeEquivalentTo(packet.Chests[i]);
                }

                for (var i = 0; i < packet.Signs.Count; ++i) {
                    packet2.Signs[i].Should().BeEquivalentTo(packet.Signs[i]);
                }

                for (var i = 0; i < packet.TileEntities.Count; ++i) {
                    packet2.TileEntities[i].Should().BeEquivalentTo(packet.TileEntities[i]);
                }
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

        private static readonly byte[] UpdatePlayerHpBytes = {8, 0, 16, 0, 100, 0, 100, 0};

        [Fact]
        public void ReadFromStream_UpdatePlayerHp_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerHpBytes)) {
                var packet = (UpdatePlayerHpPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.Hp.Should().Be(100);
                packet.MaxHp.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerHp_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerHpBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerHpBytes);
            }
        }

        private static readonly byte[] ModifyTileBytes = {11, 0, 17, 0, 16, 14, 194, 1, 1, 0, 0};

        [Fact]
        public void ReadFromStream_ModifyTile_IsCorrect() {
            using (var stream = new MemoryStream(ModifyTileBytes)) {
                var packet = (ModifyTilePacket)Packet.ReadFromStream(stream);

                packet.Type.Should().Be(ModifyTilePacket.ModificationType.DestroyBlock);
                packet.X.Should().Be(3600);
                packet.Y.Should().Be(450);
                packet.Data.Should().Be(1);
                packet.Style.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_ModifyTile_IsCorrect() {
            using (var stream = new MemoryStream(ModifyTileBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ModifyTileBytes);
            }
        }

        private static readonly byte[] UpdateTimeBytes = {12, 0, 18, 1, 0, 128, 0, 0, 200, 0, 200, 0};

        [Fact]
        public void ReadFromStream_UpdateTime_IsCorrect() {
            using (var stream = new MemoryStream(UpdateTimeBytes)) {
                var packet = (UpdateTimePacket)Packet.ReadFromStream(stream);

                packet.IsDaytime.Should().BeTrue();
                packet.Time.Should().Be(32768);
                packet.SunY.Should().Be(200);
                packet.MoonY.Should().Be(200);
            }
        }

        [Fact]
        public void WriteToStream_UpdateTime_IsCorrect() {
            using (var stream = new MemoryStream(UpdateTimeBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateTimeBytes);
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

        private static readonly byte[] UpdateSquareOfTilesBytes = {
            17, 0, 20, 1, 0, 153, 16, 171, 1, 1, 0, 3, 0, 72, 0, 0, 0,
        };

        [Fact]
        public void ReadFromStream_UpdateSquareOfTiles_IsCorrect() {
            using (var stream = new MemoryStream(UpdateSquareOfTilesBytes)) {
                var packet = (UpdateSquareOfTilesPacket)Packet.ReadFromStream(stream);

                packet.Size.Should().Be(1);
                packet.LiquidChangeType.Should().Be(UpdateSquareOfTilesPacket.Type.None);
                packet.X.Should().Be(4249);
                packet.Y.Should().Be(427);
                packet.Tiles.GetLength(0).Should().Be(1);
                packet.Tiles.GetLength(1).Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_UpdateSquareOfTiles_IsCorrect() {
            using (var stream = new MemoryStream(UpdateSquareOfTilesBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateSquareOfTilesBytes);
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

        private static readonly byte[] UpdatePlayerPvpStatusBytes = {5, 0, 30, 0, 1,};

        [Fact]
        public void ReadFromStream_UpdatePlayerPvpStatus_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerPvpStatusBytes)) {
                var packet = (UpdatePlayerPvpStatusPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerIsInPvp.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerPvpStatus_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerPvpStatusBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerPvpStatusBytes);
            }
        }

        private static readonly byte[] RequestChestContentsBytes = {7, 0, 31, 100, 0, 100, 0,};

        [Fact]
        public void ReadFromStream_RequestChestContents_IsCorrect() {
            using (var stream = new MemoryStream(RequestChestContentsBytes)) {
                var packet = (RequestChestContentsPacket)Packet.ReadFromStream(stream);

                packet.ChestX.Should().Be(100);
                packet.ChestY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_RequestChestContents_IsCorrect() {
            using (var stream = new MemoryStream(RequestChestContentsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RequestChestContentsBytes);
            }
        }

        private static readonly byte[] UpdateChestContentsSlotBytes = {11, 0, 32, 0, 0, 0, 1, 0, 0, 17, 6,};

        [Fact]
        public void ReadFromStream_UpdateChestContentsSlot_IsCorrect() {
            using (var stream = new MemoryStream(UpdateChestContentsSlotBytes)) {
                var packet = (UpdateChestContentsSlotPacket)Packet.ReadFromStream(stream);

                packet.ChestIndex.Should().Be(0);
                packet.ChestContentsSlot.Should().Be(0);
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.None);
                packet.ItemType.Should().Be(ItemType.SDMG);
            }
        }

        [Fact]
        public void WriteToStream_UpdateChestContentsSlot_IsCorrect() {
            using (var stream = new MemoryStream(UpdateChestContentsSlotBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateChestContentsSlotBytes);
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

        private static readonly byte[] ShowHealEffectBytes = {6, 0, 35, 0, 100, 0};

        [Fact]
        public void ReadFromStream_ShowHealEffect_IsCorrect() {
            using (var stream = new MemoryStream(ShowHealEffectBytes)) {
                var packet = (ShowHealEffectPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.HealAmount.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_ShowHealEffectSlot_IsCorrect() {
            using (var stream = new MemoryStream(ShowHealEffectBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ShowHealEffectBytes);
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

        private static readonly byte[] RequestServerPasswordBytes = {3, 0, 37};

        [Fact]
        public void ReadFromStream_RequestServerPassword_IsCorrect() {
            using (var stream = new MemoryStream(RequestServerPasswordBytes)) {
                Packet.ReadFromStream(stream);
            }
        }

        [Fact]
        public void WriteToStream_RequestServerPasswordSlot_IsCorrect() {
            using (var stream = new MemoryStream(RequestServerPasswordBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RequestServerPasswordBytes);
            }
        }

        public static readonly byte[] ContinueConnectionWithServerPasswordBytes = {
            12, 0, 38, 8, 84, 101, 114, 114, 97, 114, 105, 97
        };

        [Fact]
        public void ReadFromStream_ContinueConnectionWithServerPassword_IsCorrect() {
            using (var stream = new MemoryStream(ContinueConnectionWithServerPasswordBytes)) {
                var packet = (ContinueConnectionWithServerPasswordPacket)Packet.ReadFromStream(stream);

                packet.ServerPassword.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_ContinueConnectionWithServerPassword_IsCorrect() {
            using (var stream = new MemoryStream(ContinueConnectionWithServerPasswordBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ContinueConnectionWithServerPasswordBytes);
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

        public static readonly byte[] UpdatePlayerMpBytes = {8, 0, 42, 0, 100, 0, 100, 0,};

        [Fact]
        public void ReadFromStream_UpdatePlayerMp_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerMpBytes)) {
                var packet = (UpdatePlayerMpPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.Mp.Should().Be(100);
                packet.MaxMp.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerMp_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerMpBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerMpBytes);
            }
        }

        public static readonly byte[] ShowManaEffectBytes = {6, 0, 43, 0, 100, 0,};

        [Fact]
        public void ReadFromStream_ShowManaEffect_IsCorrect() {
            using (var stream = new MemoryStream(ShowManaEffectBytes)) {
                var packet = (ShowManaEffectPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.ManaAmount.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_ShowManaEffect_IsCorrect() {
            using (var stream = new MemoryStream(ShowManaEffectBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ShowManaEffectBytes);
            }
        }

        public static readonly byte[] UpdatePlayerTeamBytes = {5, 0, 45, 0, 1,};

        [Fact]
        public void ReadFromStream_UpdatePlayerTeam_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerTeamBytes)) {
                var packet = (UpdatePlayerTeamPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerTeam.Should().Be(PlayerTeam.Red);
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerTeam_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerTeamBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerTeamBytes);
            }
        }

        public static readonly byte[] RequestSignBytes = {7, 0, 46, 0, 1, 100, 0};

        [Fact]
        public void ReadFromStream_RequestSign_IsCorrect() {
            using (var stream = new MemoryStream(RequestSignBytes)) {
                var packet = (RequestSignPacket)Packet.ReadFromStream(stream);

                packet.SignX.Should().Be(256);
                packet.SignY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_RequestSign_IsCorrect() {
            using (var stream = new MemoryStream(RequestSignBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RequestSignBytes);
            }
        }

        public static readonly byte[] UpdateSignBytes = {18, 0, 47, 0, 0, 0, 1, 100, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97};

        [Fact]
        public void ReadFromStream_UpdateSign_IsCorrect() {
            using (var stream = new MemoryStream(UpdateSignBytes)) {
                var packet = (UpdateSignPacket)Packet.ReadFromStream(stream);

                packet.SignIndex.Should().Be(0);
                packet.SignX.Should().Be(256);
                packet.SignY.Should().Be(100);
                packet.SignText.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_UpdateSign_IsCorrect() {
            using (var stream = new MemoryStream(UpdateSignBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateSignBytes);
            }
        }

        public static readonly byte[] UpdateLiquidBytes = {9, 0, 48, 0, 1, 100, 0, 255, 0};

        [Fact]
        public void ReadFromStream_UpdateLiquid_IsCorrect() {
            using (var stream = new MemoryStream(UpdateLiquidBytes)) {
                var packet = (UpdateLiquidPacket)Packet.ReadFromStream(stream);

                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
                packet.LiquidAmount.Should().Be(255);
                packet.LiquidType.Should().Be(LiquidType.Water);
            }
        }

        [Fact]
        public void WriteToStream_UpdateLiquid_IsCorrect() {
            using (var stream = new MemoryStream(UpdateLiquidBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateLiquidBytes);
            }
        }

        public static readonly byte[] FirstSpawnPlayerBytes = {3, 0, 49};

        [Fact]
        public void ReadFromStream_FirstSpawnPlayer_IsCorrect() {
            using (var stream = new MemoryStream(FirstSpawnPlayerBytes)) {
                Packet.ReadFromStream(stream);
            }
        }

        [Fact]
        public void WriteToStream_FirstSpawnPlayer_IsCorrect() {
            using (var stream = new MemoryStream(FirstSpawnPlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(FirstSpawnPlayerBytes);
            }
        }

        public static readonly byte[] UpdatePlayerBuffsBytes = {
            26, 0, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };

        [Fact]
        public void ReadFromStream_UpdatePlayerBuffs_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerBuffsBytes)) {
                var packet = (UpdatePlayerBuffsPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                foreach (var buffType in packet.PlayerBuffs) {
                    buffType.Should().Be(BuffType.None);
                }
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerBuffs_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerBuffsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerBuffsBytes);
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

        public static readonly byte[] AddPlayerBuffBytes = {7, 0, 55, 0, 1, 100, 0};

        [Fact]
        public void ReadFromStream_AddPlayerBuff_IsCorrect() {
            using (var stream = new MemoryStream(AddPlayerBuffBytes)) {
                var packet = (AddPlayerBuffPacket)Packet.ReadFromStream(stream);
                
                packet.PlayerIndex.Should().Be(0);
                packet.BuffType.Should().Be(BuffType.ObsidianSkin);
                packet.BuffTime.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_AddPlayerBuff_IsCorrect() {
            using (var stream = new MemoryStream(AddPlayerBuffBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(AddPlayerBuffBytes);
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
        
        public static readonly byte[] ActivateWiringBytes = {7, 0, 59, 0, 1, 100, 0};

        [Fact]
        public void ReadFromStream_ActivateWiring_IsCorrect() {
            using (var stream = new MemoryStream(ActivateWiringBytes)) {
                var packet = (ActivateWiringPacket)Packet.ReadFromStream(stream);

                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_ActivateWiring_IsCorrect() {
            using (var stream = new MemoryStream(ActivateWiringBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ActivateWiringBytes);
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
        
        public static readonly byte[] PaintBlockBytes = {8, 0, 63, 0, 1, 100, 0, 1};

        [Fact]
        public void ReadFromStream_PaintBlock_IsCorrect() {
            using (var stream = new MemoryStream(PaintBlockBytes)) {
                var packet = (PaintBlockPacket)Packet.ReadFromStream(stream);

                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
                packet.BlockColor.Should().Be(PaintColor.Red);
            }
        }

        [Fact]
        public void WriteToStream_PaintBlock_IsCorrect() {
            using (var stream = new MemoryStream(PaintBlockBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PaintBlockBytes);
            }
        }
        
        public static readonly byte[] PaintWallBytes = {8, 0, 64, 0, 1, 100, 0, 1};

        [Fact]
        public void ReadFromStream_PaintWall_IsCorrect() {
            using (var stream = new MemoryStream(PaintWallBytes)) {
                var packet = (PaintWallPacket)Packet.ReadFromStream(stream);

                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
                packet.WallColor.Should().Be(PaintColor.Red);
            }
        }

        [Fact]
        public void WriteToStream_PaintWall_IsCorrect() {
            using (var stream = new MemoryStream(PaintWallBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PaintWallBytes);
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
        
        public static readonly byte[] UpdateUuidBytes = {12, 0, 68, 8, 84, 101, 114, 114, 97, 114, 105, 97};

        [Fact]
        public void ReadFromStream_UpdateUuid_IsCorrect() {
            using (var stream = new MemoryStream(UpdateUuidBytes)) {
                var packet = (UpdateUuidPacket)Packet.ReadFromStream(stream);

                packet.Uuid.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_UpdateUuid_IsCorrect() {
            using (var stream = new MemoryStream(UpdateUuidBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateUuidBytes);
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

        public static readonly byte[] PerformTeleportationPotionBytes = {3, 0, 73};

        [Fact]
        public void ReadFromStream_PerformTeleportationPotion_IsCorrect() {
            using (var stream = new MemoryStream(PerformTeleportationPotionBytes)) {
                Packet.ReadFromStream(stream).Should().BeOfType<PerformTeleportationPotionPacket>();
            }
        }

        [Fact]
        public void WriteToStream_PerformTeleportationPotion_IsCorrect() {
            using (var stream = new MemoryStream(PerformTeleportationPotionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PerformTeleportationPotionBytes);
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

        public static readonly byte[] UpdateInvasionBytes = {19, 0, 78, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0};

        [Fact]
        public void ReadFromStream_UpdateInvasion_IsCorrect() {
            using (var stream = new MemoryStream(UpdateInvasionBytes)) {
                var packet = (UpdateInvasionPacket)Packet.ReadFromStream(stream);

                packet.NumberOfKills.Should().Be(1);
                packet.NumberOfKillsToProgress.Should().Be(256);
                packet.InvasionIconType.Should().Be(1);
                packet.WaveNumber.Should().Be(2);
            }
        }

        [Fact]
        public void WriteToStream_UpdateInvasion_IsCorrect() {
            using (var stream = new MemoryStream(UpdateInvasionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateInvasionBytes);
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
