using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World.TileEntities;
using Xunit;

namespace Orion.Tests.Networking.Packets.World.TileEntities {
    public class PlaceTileEntityPacketTests {
        private static readonly byte[] PlaceTileEntityBytes = {8, 0, 87, 0, 1, 100, 0, 1,};

        [Fact]
        public void ReadFromStream_Delete_IsCorrect() {
            using (var stream = new MemoryStream(PlaceTileEntityBytes)) {
                var packet = (PlaceTileEntityPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TileEntityX.Should().Be(256);
                packet.TileEntityY.Should().Be(100);
                packet.TileEntityType.Should().Be(TileEntityType.ItemFrame);
            }
        }

        [Fact]
        public void WriteToStream_Delete_IsCorrect() {
            using (var stream = new MemoryStream(PlaceTileEntityBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(PlaceTileEntityBytes);
            }
        }
    }
}
