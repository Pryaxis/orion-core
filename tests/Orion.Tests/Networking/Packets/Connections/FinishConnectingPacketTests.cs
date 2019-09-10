using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets.Connections {
    public class FinishConnectingPacketTests {
        public static readonly byte[] FinishConnectingBytes = {3, 0, 6,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(FinishConnectingBytes)) {
                Packet.ReadFromStream(stream, PacketContext.Server);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(FinishConnectingBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(FinishConnectingBytes);
            }
        }
    }
}
