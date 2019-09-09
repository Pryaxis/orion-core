using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerMinionPositionPacketTests {
        public static readonly byte[] PlayerMinionPositionBytes = {12, 0, 99, 1, 0, 0, 0, 0, 0, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerMinionPositionBytes)) {
                var packet = (PlayerMinionPositionPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(1);
                packet.PlayerMinionTargetPosition.Should().Be(Vector2.Zero);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerMinionPositionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlayerMinionPositionBytes);
            }
        }
    }
}
