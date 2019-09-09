using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class ProgressionEventPacketTests {
        public static readonly byte[] ProgressionEventBytes = {5, 0, 98, 1, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ProgressionEventBytes)) {
                var packet = (ProgressionEventPacket)Packet.ReadFromStream(stream);

                packet.EventId.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ProgressionEventBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ProgressionEventBytes);
            }
        }
    }
}
