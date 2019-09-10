using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class FinishAnglerQuestPacketTests {
        public static readonly byte[] FinishAnglerQuestBytes = {3, 0, 75,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(FinishAnglerQuestBytes)) {
                Packet.ReadFromStream(stream, PacketContext.Server).Should().BeOfType<FinishAnglerQuestPacket>();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(FinishAnglerQuestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(FinishAnglerQuestBytes);
            }
        }
    }
}
