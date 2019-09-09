using System;
using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerDataPacketTests {
        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var packet = new PlayerDataPacket();
            Action action = () => packet.PlayerName = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] PlayerDataBytes = {
            34, 0, 4, 0, 2, 50, 1, 102, 0, 0, 0, 0, 26, 131, 54, 158, 74, 51, 47, 39, 88, 184, 58, 43, 69, 8, 97, 162,
            167, 255, 212, 159, 76, 0,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerDataBytes)) {
                var packet = (PlayerDataPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerSkinType.Should().Be(2);
                packet.PlayerName.Should().Be("f");
                packet.PlayerHairDye.Should().Be(0);
                packet.PlayerHiddenVisualsFlags.Should().Be(0);
                packet.PlayerHiddenMiscFlags.Should().Be(0);
                packet.PlayerHairColor.Should().Be(new Color(26, 131, 54));
                packet.PlayerSkinColor.Should().Be(new Color(158, 74, 51));
                packet.PlayerEyeColor.Should().Be(new Color(47, 39, 88));
                packet.PlayerShirtColor.Should().Be(new Color(184, 58, 43));
                packet.PlayerUndershirtColor.Should().Be(new Color(69, 8, 97));
                packet.PlayerPantsColor.Should().Be(new Color(162, 167, 255));
                packet.PlayerShoeColor.Should().Be(new Color(212, 159, 76));
                packet.PlayerDifficulty.Should().Be(PlayerDifficulty.Softcore);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerDataBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlayerDataBytes);
            }
        }
    }
}
