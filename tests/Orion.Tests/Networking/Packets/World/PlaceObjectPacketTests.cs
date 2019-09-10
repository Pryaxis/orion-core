using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class PlaceObjectPacketTests {
        public static readonly byte[] PlaceObjectBytes = {14, 0, 79, 0, 1, 100, 0, 21, 0, 1, 0, 0, 255, 1,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlaceObjectBytes)) {
                var packet = (PlaceObjectPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ObjectX.Should().Be(256);
                packet.ObjectY.Should().Be(100);
                packet.ObjectType.Should().Be(BlockType.Containers);
                packet.ObjectStyle.Should().Be(1);
                packet.ObjectRandomState.Should().Be(-1);
                packet.ObjectDirection.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlaceObjectBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(PlaceObjectBytes);
            }
        }
    }
}
