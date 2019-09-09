using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets.Items {
    public class ItemOwnerPacketTests {
        private static readonly byte[] ItemOwnerBytes = {6, 0, 22, 144, 1, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ItemOwnerBytes)) {
                var packet = (ItemOwnerPacket)Packet.ReadFromStream(stream);

                packet.ItemIndex.Should().Be(400);
                packet.OwnerPlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ItemOwnerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ItemOwnerBytes);
            }
        }
    }
}
