using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerMinionNpcPacketTests {
        public static readonly byte[] PlayerMinionNpcBytes = {6, 0, 115, 1, 100, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerMinionNpcBytes)) {
                var packet = (PlayerMinionNpcPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(1);
                packet.PlayerMinionTargetNpcIndex.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerMinionNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlayerMinionNpcBytes);
            }
        }
    }
}
