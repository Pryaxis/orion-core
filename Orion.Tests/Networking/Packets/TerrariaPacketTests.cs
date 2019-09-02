using System;
using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Networking.Packets;
using Orion.World;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class TerrariaPacketTests {
        [Fact]
        public void ReadFromStream_NullStream_ThrowsArgumentNullException() {
            Func<TerrariaPacket> func = () => TerrariaPacket.ReadFromStream(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WriteToStream_NullStream_ThrowsArgumentNullException() {
            var packet = new UnknownPacket((TerrariaPacketType)255);
            Action action = () => packet.WriteToStream(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WriteToStream_PacketTooLong_ThrowsInvalidOperationException() {
            var packet = new UnknownPacket((TerrariaPacketType)255) {Payload = new byte[ushort.MaxValue]};
            using (var stream = new MemoryStream()) {
                // ReSharper disable once AccessToDisposedClosure
                Action action = () => packet.WriteToStream(stream);

                action.Should().Throw<InvalidOperationException>();
            }
        }

        public static readonly byte[] RequestConnectionBytes = {
            15, 0, 1, 11, 84, 101, 114, 114, 97, 114, 105, 97, 49, 57, 52
        };

        [Fact]
        public void ReadFromStream_RequestConnection_IsCorrect() {
            using (var stream = new MemoryStream(RequestConnectionBytes)) {
                var packet = (RequestConnectionPacket)TerrariaPacket.ReadFromStream(stream);

                packet.IsSentToClient.Should().BeFalse();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be(TerrariaPacketType.RequestConnection);
                packet.Version.Should().Be("Terraria194");
            }
        }

        [Fact]
        public void WriteToStream_RequestConnection_IsCorrect() {
            using (var stream = new MemoryStream(RequestConnectionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = TerrariaPacket.ReadFromStream(stream);

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
                var packet = (DisconnectPlayerPacket)TerrariaPacket.ReadFromStream(stream);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeFalse();
                packet.Type.Should().Be(TerrariaPacketType.DisconnectPlayer);
                packet.Reason.ToString().Should().Be("CLI.KickMessage");
            }
        }

        [Fact]
        public void WriteToStream_DisconnectPlayer_IsCorrect() {
            using (var stream = new MemoryStream(DisconnectPlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = TerrariaPacket.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(DisconnectPlayerBytes);
            }
        }

        public static readonly byte[] ContinueConnectionBytes = {4, 0, 3, 0};

        [Fact]
        public void ReadFromStream_ContinueConnection_IsCorrect() {
            using (var stream = new MemoryStream(ContinueConnectionBytes)) {
                var packet = (ContinueConnectionPacket)TerrariaPacket.ReadFromStream(stream);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeFalse();
                packet.Type.Should().Be(TerrariaPacketType.ContinueConnection);
                packet.PlayerId.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_ContinueConnection_IsCorrect() {
            using (var stream = new MemoryStream(ContinueConnectionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = TerrariaPacket.ReadFromStream(stream);

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
                var packet = (UpdatePlayerInfoPacket)TerrariaPacket.ReadFromStream(stream);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be(TerrariaPacketType.UpdatePlayerInfo);
                packet.PlayerId.Should().Be(0);
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
                var packet = TerrariaPacket.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerInfoBytes);
            }
        }

        private static readonly byte[] UpdatePlayerInventorySlotBytes = {10, 0, 5, 0, 0, 1, 0, 59, 179, 13};

        [Fact]
        public void ReadFromStream_UpdatePlayerInventorySlot_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerInventorySlotBytes)) {
                var packet = (UpdatePlayerInventorySlotPacket)TerrariaPacket.ReadFromStream(stream);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be(TerrariaPacketType.UpdatePlayerInventorySlot);
                packet.PlayerId.Should().Be(0);
                packet.InventorySlot.Should().Be(0);
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.Godly);
                packet.ItemType.Should().Be(ItemType.CopperShortsword);
            }
        }

        [Fact]
        public void WriteToStream_UpdatePlayerInventorySlot_IsCorrect() {
            using (var stream = new MemoryStream(UpdatePlayerInventorySlotBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = TerrariaPacket.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdatePlayerInventorySlotBytes);
            }
        }

        public static readonly byte[] FinishConnectionBytes = {3, 0, 6};

        [Fact]
        public void ReadFromStream_FinishConnection_IsCorrect() {
            using (var stream = new MemoryStream(FinishConnectionBytes)) {
                var packet = (FinishConnectionPacket)TerrariaPacket.ReadFromStream(stream);

                packet.IsSentToClient.Should().BeFalse();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be(TerrariaPacketType.FinishConnection);
            }
        }

        [Fact]
        public void WriteToStream_FinishConnection_IsCorrect() {
            using (var stream = new MemoryStream(FinishConnectionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = TerrariaPacket.ReadFromStream(stream);

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
                var packet = (UpdateWorldInfoPacket)TerrariaPacket.ReadFromStream(stream);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeFalse();
                packet.Type.Should().Be(TerrariaPacketType.UpdateWorldInfo);
                packet.Time.Should().Be(32653);
                packet.TimeFlags.Should().Be(WorldTimeFlags.IsDaytime);
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
                packet.StateFlags.Should().Be(WorldStateFlags.Crimson);
                packet.StateFlags2.Should().Be(0);
                packet.InvasionType.Should().Be(InvasionType.None);
                packet.LobbyId.Should().Be(0);
                packet.SandstormIntensity.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_UpdateWorldInfo_IsCorrect() {
            using (var stream = new MemoryStream(UpdateWorldInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = TerrariaPacket.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateWorldInfoBytes);
            }
        }

        private static readonly byte[] RequestWorldSectionBytes = {11, 0, 8, 255, 255, 255, 255, 255, 255, 255, 255};

        [Fact]
        public void ReadFromStream_RequestWorldSection_IsCorrect() {
            using (var stream = new MemoryStream(RequestWorldSectionBytes)) {
                var packet = (RequestWorldSectionPacket)TerrariaPacket.ReadFromStream(stream);

                packet.IsSentToClient.Should().BeFalse();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be(TerrariaPacketType.RequestWorldSection);
                packet.SectionX.Should().Be(-1);
                packet.SectionY.Should().Be(-1);
            }
        }

        [Fact]
        public void WriteToStream_RequestWorldSection_IsCorrect() {
            using (var stream = new MemoryStream(RequestWorldSectionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = TerrariaPacket.ReadFromStream(stream);

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
                var packet = (UpdateClientStatusPacket)TerrariaPacket.ReadFromStream(stream);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeFalse();
                packet.Type.Should().Be(TerrariaPacketType.UpdateClientStatus);
                packet.StatusIncrease.Should().Be(15);
                packet.StatusText.ToString().Should().Be("LegacyInterface.44");
            }
        }

        [Fact]
        public void WriteToStream_UpdateClientStatus_IsCorrect() {
            using (var stream = new MemoryStream(UpdateClientStatusBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = TerrariaPacket.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateClientStatusBytes);
            }
        }
    }
}
