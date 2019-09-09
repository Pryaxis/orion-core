using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerTeamPacketTests {
        public static readonly byte[] PlayerTeamBytes = {5, 0, 45, 0, 1,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerTeamBytes)) {
                var packet = (PlayerTeamPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerTeam.Should().Be(PlayerTeam.Red);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerTeamBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlayerTeamBytes);
            }
        }
    }
}
