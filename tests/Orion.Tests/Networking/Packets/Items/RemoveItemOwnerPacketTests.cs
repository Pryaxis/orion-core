using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Items;
using Xunit;

namespace Orion.Tests.Networking.Packets.Items {
    public class RemoveItemOwnerPacketTests {
        public static readonly byte[] RemoveItemOwnerBytes = {5, 0, 39, 1, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(RemoveItemOwnerBytes)) {
                var packet = (RemoveItemOwnerPacket)Packet.ReadFromStream(stream);

                packet.ItemIndex.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(RemoveItemOwnerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RemoveItemOwnerBytes);
            }
        }
    }
}
