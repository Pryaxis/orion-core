using System.IO;
using FluentAssertions;
using Orion.Items;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World.TileEntities;
using Xunit;

namespace Orion.Tests.Networking.Packets.World.TileEntities {
    public class ItemFramePacketTests {
        private static readonly byte[] ItemFrameBytes = {12, 0, 89, 0, 1, 100, 0, 17, 6, 82, 1, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ItemFrameBytes)) {
                var packet = (ItemFramePacket)Packet.ReadFromStream(stream);

                packet.ItemFrameX.Should().Be(256);
                packet.ItemFrameY.Should().Be(100);
                packet.ItemType.Should().Be(ItemType.SDMG);
                packet.ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.ItemStackSize.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ItemFrameBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ItemFrameBytes);
            }
        }
    }
}
