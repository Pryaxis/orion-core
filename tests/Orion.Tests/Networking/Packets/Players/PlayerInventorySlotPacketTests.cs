using System.IO;
using FluentAssertions;
using Orion.Items;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerInventorySlotPacketTests {
        private static readonly byte[] PlayerInventorySlotBytes = {10, 0, 5, 0, 0, 1, 0, 59, 179, 13,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerInventorySlotBytes)) {
                var packet = (PlayerInventorySlotPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerInventorySlotIndex.Should().Be(0);
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.Godly);
                packet.ItemType.Should().Be(ItemType.CopperShortsword);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerInventorySlotBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlayerInventorySlotBytes);
            }
        }
    }
}
