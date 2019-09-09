using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerBuffsPacketTests {
        public static readonly byte[] PlayerBuffsBytes = {
            26, 0, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerBuffsBytes)) {
                var packet = (PlayerBuffsPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                foreach (var buffType in packet.PlayerBuffTypes) {
                    buffType.Should().Be(BuffType.None);
                }
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerBuffsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlayerBuffsBytes);
            }
        }
    }
}
