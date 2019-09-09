using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerPvpPacketTests {
        private static readonly byte[] PlayerPvpBytes = {5, 0, 30, 0, 1,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerPvpBytes)) {
                var packet = (PlayerPvpPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerIsInPvp.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerPvpBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlayerPvpBytes);
            }
        }
    }
}
