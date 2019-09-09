using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class PaintBlockPacketTests {
        public static readonly byte[] PaintBlockBytes = {8, 0, 63, 0, 1, 100, 0, 1};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PaintBlockBytes)) {
                var packet = (PaintBlockPacket)Packet.ReadFromStream(stream);

                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
                packet.BlockColor.Should().Be(PaintColor.Red);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PaintBlockBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PaintBlockBytes);
            }
        }
    }
}
