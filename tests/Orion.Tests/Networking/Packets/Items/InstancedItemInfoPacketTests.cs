using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets.Items {
    public class InstancedItemInfoPacketTests {
        private static readonly byte[] InstancedItemInfoBytes = {
            27, 0, 90, 144, 1, 128, 51, 131, 71, 0, 112, 212, 69, 0, 0, 128, 64, 0, 0, 0, 192, 1, 0, 82, 0, 17, 6,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(InstancedItemInfoBytes)) {
                var packet = (InstancedItemInfoPacket)Packet.ReadFromStream(stream);

                packet.ItemIndex.Should().Be(400);
                packet.Position.Should().Be(new Vector2(67175, 6798));
                packet.Velocity.Should().Be(new Vector2(4, -2));
                packet.ItemStackSize.Should().Be(1);
                packet.ItemPrefix.Should().Be(ItemPrefix.Unreal);
                packet.ShouldDisown.Should().BeFalse();
                packet.ItemType.Should().Be(ItemType.SDMG);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(InstancedItemInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(InstancedItemInfoBytes);
            }
        }
    }
}
