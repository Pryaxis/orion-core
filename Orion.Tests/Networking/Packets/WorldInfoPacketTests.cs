namespace Orion.Tests.Networking.Packets {
    using System;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.Xna.Framework;
    using Orion.Networking.Packets;
    using Orion.World;
    using Xunit;

    public class WorldInfoPacketTests {
        // These canned bytes were taken from a real server.
        private static readonly byte[] Bytes = {
            141, 127, 0, 0, 1, 0, 104, 16, 176, 4, 54, 8, 102, 1, 129, 1, 53, 2, 24, 49, 0, 9, 1, 102, 63, 129, 163,
            174, 200, 216, 57, 65, 188, 220, 22, 170, 161, 45, 221, 99, 1, 0, 0, 0, 194, 0, 0, 0, 0, 51, 0, 1, 2, 1, 0,
            1, 2, 3, 0, 0, 217, 206, 151, 62, 0, 37, 4, 0, 0, 104, 16, 0, 0, 104, 16, 0, 0, 3, 2, 0, 0, 248, 4, 0, 0,
            104, 16, 0, 0, 104, 16, 0, 0, 7, 1, 0, 6, 0, 0, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        [Fact]
        public void FromReader_NullReader_ThrowsArgumentNullException() {
            Func<WorldInfoPacket> func = () => WorldInfoPacket.FromReader(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FromReader_IsCorrect() {
            using (var stream = new MemoryStream(Bytes))
            using (var reader = new BinaryReader(stream)) {
                var packet = WorldInfoPacket.FromReader(reader);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeFalse();
                packet.Type.Should().Be(TerrariaPacketType.WorldInfo);
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
                stream.Position.Should().Be(Bytes.Length);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes))
            using (var reader = new BinaryReader(stream)) 
            using (var stream2 = new MemoryStream()) {
                var packet = WorldInfoPacket.FromReader(reader);

                packet.WriteToStream(stream2);

                stream2.ToArray().Skip(3).Should().BeEquivalentTo(Bytes);
            }
        }
    }
}
