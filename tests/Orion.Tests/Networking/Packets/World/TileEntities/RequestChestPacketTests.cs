using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World.TileEntities;
using Xunit;

namespace Orion.Tests.Networking.Packets.World.TileEntities {
    public class RequestChestPacketTests {
        private static readonly byte[] RequestChestBytes = {7, 0, 31, 100, 0, 100, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(RequestChestBytes)) {
                var packet = (RequestChestPacket)Packet.ReadFromStream(stream);

                packet.ChestX.Should().Be(100);
                packet.ChestY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(RequestChestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RequestChestBytes);
            }
        }
    }
}
