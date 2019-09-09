using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Items;
using Xunit;

namespace Orion.Tests.Networking.Packets.Items {
    public class ItemInfoPacketTests {
        private static readonly byte[] ItemInfoBytes = {
            27, 0, 21, 144, 1, 128, 51, 131, 71, 0, 112, 212, 69, 0, 0, 128, 64, 0, 0, 0, 192, 1, 0, 82, 0, 17, 6,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ItemInfoBytes)) {
                var packet = (ItemInfoPacket)Packet.ReadFromStream(stream);

                packet.ItemIndex.Should().Be(400);
                packet.ItemPosition.Should().Be(new Vector2(67175, 6798));
                packet.ItemVelocity.Should().Be(new Vector2(4, -2));
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.ShouldDisownItem.Should().BeFalse();
                packet.ItemType.Should().Be(ItemType.SDMG);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ItemInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ItemInfoBytes);
            }
        }
    }
}
