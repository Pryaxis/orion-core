using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World.TileEntities;
using Xunit;

namespace Orion.Tests.Networking.Packets.World.TileEntities {
    public class RequestSignPacketTests {
        public static readonly byte[] RequestSignBytes = {7, 0, 46, 0, 1, 100, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(RequestSignBytes)) {
                var packet = (RequestSignPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SignX.Should().Be(256);
                packet.SignY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(RequestSignBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(RequestSignBytes);
            }
        }
    }
}
