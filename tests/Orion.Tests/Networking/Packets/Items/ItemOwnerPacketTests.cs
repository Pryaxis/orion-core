using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Items;
using Xunit;

namespace Orion.Tests.Networking.Packets.Items {
    public class ItemOwnerPacketTests {
        private static readonly byte[] ItemOwnerBytes = {6, 0, 22, 144, 1, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ItemOwnerBytes)) {
                var packet = (ItemOwnerPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ItemIndex.Should().Be(400);
                packet.OwnerPlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ItemOwnerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ItemOwnerBytes);
            }
        }
    }
}
