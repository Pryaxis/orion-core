using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class ModifyTilePacketTests {
        private static readonly byte[] ModifyTileBytes = {11, 0, 17, 0, 16, 14, 194, 1, 1, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ModifyTileBytes)) {
                var packet = (ModifyTilePacket)Packet.ReadFromStream(stream);

                packet.ModificationType.Should().Be(TileModificationType.DestroyBlock);
                packet.TileX.Should().Be(3600);
                packet.TileY.Should().Be(450);
                packet.ModificationData.Should().Be(1);
                packet.ModificationStyle.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ModifyTileBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ModifyTileBytes);
            }
        }
    }
}
