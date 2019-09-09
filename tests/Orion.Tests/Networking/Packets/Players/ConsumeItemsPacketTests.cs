using System.IO;
using FluentAssertions;
using Orion.Items;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class ConsumeItemsPacketTests {
        private static readonly byte[] ConsumeItemsBytes = {8, 0, 110, 179, 13, 1, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ConsumeItemsBytes)) {
                var packet = (ConsumeItemsPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.ItemStackSize.Should().Be(1);
                packet.ItemType.Should().Be(ItemType.CopperShortsword);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ConsumeItemsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ConsumeItemsBytes);
            }
        }
    }
}
