using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets.Connections {
    public class RequestPasswordPacketTests {
        private static readonly byte[] RequestPasswordBytes = {3, 0, 37,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(RequestPasswordBytes)) {
                Packet.ReadFromStream(stream, PacketContext.Server);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(RequestPasswordBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(RequestPasswordBytes);
            }
        }
    }
}
