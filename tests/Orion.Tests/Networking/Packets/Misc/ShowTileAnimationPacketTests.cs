using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class ShowTileAnimationPacketTests {
        public static readonly byte[] ShowTileAnimationBytes = {11, 0, 77, 1, 0, 1, 0, 0, 1, 100, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowTileAnimationBytes)) {
                var packet = (ShowTileAnimationPacket)Packet.ReadFromStream(stream);

                packet.AnimationType.Should().Be(1);
                packet.BlockType.Should().Be(BlockType.Stone);
                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowTileAnimationBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ShowTileAnimationBytes);
            }
        }
    }
}
