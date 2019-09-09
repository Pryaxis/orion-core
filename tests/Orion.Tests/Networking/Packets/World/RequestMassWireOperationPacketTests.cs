using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class RequestMassWireOperationPacketTests {
        public static readonly byte[] RequestMassWireOperationBytes = {12, 0, 109, 0, 0, 0, 0, 0, 1, 100, 0, 1,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(RequestMassWireOperationBytes)) {
                var packet = (RequestMassWireOperationPacket)Packet.ReadFromStream(stream);

                packet.StartTileX.Should().Be(0);
                packet.StartTileY.Should().Be(0);
                packet.EndTileX.Should().Be(256);
                packet.EndTileY.Should().Be(100);
                packet.WireOperations.Should().Be(MassWireOperations.RedWire);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(RequestMassWireOperationBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RequestMassWireOperationBytes);
            }
        }
    }
}
