using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class ActivateWirePacketTests {
        public static readonly byte[] ActivateWireBytes = {7, 0, 59, 0, 1, 100, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ActivateWireBytes)) {
                var packet = (ActivateWirePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WireX.Should().Be(256);
                packet.WireY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ActivateWireBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ActivateWireBytes);
            }
        }
    }
}
