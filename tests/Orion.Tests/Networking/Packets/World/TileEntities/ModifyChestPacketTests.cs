using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World.TileEntities;
using Xunit;

namespace Orion.Tests.Networking.Packets.World.TileEntities {
    public class ModifyChestPacketTests {
        private static readonly byte[] ModifyChestBytes = {12, 0, 34, 0, 100, 0, 100, 0, 1, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ModifyChestBytes)) {
                var packet = (ModifyChestPacket)Packet.ReadFromStream(stream);

                packet.ModificationType.Should().Be(ChestModificationType.PlaceContainers);
                packet.ChestX.Should().Be(100);
                packet.ChestY.Should().Be(100);
                packet.ChestStyle.Should().Be(1);
                packet.ChestIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ModifyChestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ModifyChestBytes);
            }
        }
    }
}
