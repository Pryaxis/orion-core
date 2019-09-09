using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World.TileEntities;
using Xunit;

namespace Orion.Tests.Networking.Packets.World.TileEntities {
    public class MoveIntoChestPacketTests {
        public static readonly byte[] MoveIntoChestBytes = {4, 0, 85, 1};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(MoveIntoChestBytes)) {
                var packet = (MoveIntoChestPacket)Packet.ReadFromStream(stream);

                packet.PlayerInventorySlotIndex.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(MoveIntoChestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(MoveIntoChestBytes);
            }
        }
    }
}
