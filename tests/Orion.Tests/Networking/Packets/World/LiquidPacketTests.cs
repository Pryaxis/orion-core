using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class LiquidPacketTests {
        public static readonly byte[] LiquidBytes = {9, 0, 48, 0, 1, 100, 0, 255, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(LiquidBytes)) {
                var packet = (LiquidPacket)Packet.ReadFromStream(stream);

                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
                packet.LiquidAmount.Should().Be(255);
                packet.LiquidType.Should().Be(LiquidType.Water);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(LiquidBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(LiquidBytes);
            }
        }
    }
}
