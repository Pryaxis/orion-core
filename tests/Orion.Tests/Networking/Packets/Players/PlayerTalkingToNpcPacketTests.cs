using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerTalkingToNpcPacketTests {
        public static readonly byte[] PlayerTalkingToNpcBytes = {6, 0, 40, 1, 1, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerTalkingToNpcBytes)) {
                var packet = (PlayerTalkingToNpcPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(1);
                packet.NpcIndex.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerTalkingToNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlayerTalkingToNpcBytes);
            }
        }
    }
}
