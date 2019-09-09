using System.IO;
using FluentAssertions;
using Orion.Items;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class NpcShopSlotPacketTests {
        public static readonly byte[] NpcShopSlotBytes = {13, 0, 104, 0, 17, 6, 1, 0, 82, 100, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcShopSlotBytes)) {
                var packet = (NpcShopSlotPacket)Packet.ReadFromStream(stream);

                packet.NpcShopSlotIndex.Should().Be(0);
                packet.ItemType.Should().Be(ItemType.SDMG);
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.ItemValue.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcShopSlotBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(NpcShopSlotBytes);
            }
        }
    }
}
