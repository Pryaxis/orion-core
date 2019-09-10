using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerAnglerQuestsCompletedPacketTests {
        public static readonly byte[] UpdateAnglerQuestsCompletedBytes = {7, 0, 76, 1, 1, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(UpdateAnglerQuestsCompletedBytes)) {
                var packet = (PlayerAnglerQuestsCompletedPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerNumberOfAnglerQuestsCompleted.Should().Be(257);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(UpdateAnglerQuestsCompletedBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(UpdateAnglerQuestsCompletedBytes);
            }
        }
    }
}
