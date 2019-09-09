using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class PaintWallPacketTests {
        public static readonly byte[] PaintWallBytes = {8, 0, 64, 0, 1, 100, 0, 1};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PaintWallBytes)) {
                var packet = (PaintWallPacket)Packet.ReadFromStream(stream);

                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
                packet.WallColor.Should().Be(PaintColor.Red);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PaintWallBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PaintWallBytes);
            }
        }
    }
}
