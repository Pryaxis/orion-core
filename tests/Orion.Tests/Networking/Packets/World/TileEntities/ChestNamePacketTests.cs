using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World.TileEntities;
using Xunit;

namespace Orion.Tests.Networking.Packets.World.TileEntities {
    public class ChestNamePacketTests {
        [Fact]
        public void SetChestName_NullValue_ThrowsArgumentNullException() {
            var packet = new ChestNamePacket();
            Action action = () => packet.ChestName = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] ChestNameBytes = {
            18, 0, 69, 0, 0, 0, 1, 100, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ChestNameBytes)) {
                var packet = (ChestNamePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ChestIndex.Should().Be(0);
                packet.ChestX.Should().Be(256);
                packet.ChestY.Should().Be(100);
                packet.ChestName.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ChestNameBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ChestNameBytes);
            }
        }
    }
}
