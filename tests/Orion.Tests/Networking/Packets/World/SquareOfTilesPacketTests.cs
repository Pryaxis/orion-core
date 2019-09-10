using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    [Collection("TerrariaTestsCollection")]
    public class SquareOfTilesPacketTests {
        [Fact]
        public void SetTiles_NullValue_ThrowsArgumentNullException() {
            var packet = new SquareTilesPacket();
            Action action = () => packet.Tiles = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] SquareOfTilesBytes = {
            17, 0, 20, 1, 0, 153, 16, 171, 1, 1, 0, 3, 0, 72, 0, 0, 0,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(SquareOfTilesBytes)) {
                var packet = (SquareTilesPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SquareSize.Should().Be(1);
                packet.LiquidChangeType.Should().Be(LiquidChangeType.None);
                packet.TileX.Should().Be(4249);
                packet.TileY.Should().Be(427);
                packet.Tiles.GetLength(0).Should().Be(1);
                packet.Tiles.GetLength(1).Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(SquareOfTilesBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(SquareOfTilesBytes);
            }
        }
    }
}
