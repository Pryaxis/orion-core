using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class TeleportationPotionPacketTests {
        public static readonly byte[] TeleportationPotionBytes = {3, 0, 73,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(TeleportationPotionBytes)) {
                Packet.ReadFromStream(stream).Should().BeOfType<TeleportationPotionPacket>();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(TeleportationPotionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(TeleportationPotionBytes);
            }
        }
    }
}
