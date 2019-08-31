namespace Orion.Tests.Networking.Packets {
    using System;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.Xna.Framework;
    using Orion.Networking.Packets;
    using Xunit;

    public class PlayerInfoPacketTests {
        // These canned bytes were taken from a real client.
        private static readonly byte[] Bytes = {
            0, 2, 50, 1, 102, 0, 0, 0, 0, 26, 131, 54, 158, 74, 51, 47, 39, 88, 184, 58, 43, 69, 8, 97, 162, 167, 255,
            212, 159, 76, 0
        };

        [Fact]
        public void FromReader_NullReader_ThrowsArgumentNullException() {
            Func<TerrariaPacket> func = () => PlayerInfoPacket.FromReader(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FromReader_IsCorrect() {
            using (var stream = new MemoryStream(Bytes))
            using (var reader = new BinaryReader(stream)) {
                var packet = PlayerInfoPacket.FromReader(reader);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be(TerrariaPacketType.PlayerInfo);
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
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes))
            using (var reader = new BinaryReader(stream)) 
            using (var stream2 = new MemoryStream()) {
                var packet = PlayerInfoPacket.FromReader(reader);

                packet.WriteToStream(stream2);

                stream2.ToArray().Skip(3).Should().BeEquivalentTo(Bytes);
            }
        }
    }
}
