using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class CompleteAnglerQuestPacketTests {
        public static readonly byte[] CompleteAnglerQuestBytes = {3, 0, 75,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(CompleteAnglerQuestBytes)) {
                Packet.ReadFromStream(stream).Should().BeOfType<CompleteAnglerQuestPacket>();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(CompleteAnglerQuestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(CompleteAnglerQuestBytes);
            }
        }
    }
}
