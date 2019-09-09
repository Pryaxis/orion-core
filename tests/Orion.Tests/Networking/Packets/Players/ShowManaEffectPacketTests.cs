using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class ShowManaEffectPacketTests {
        public static readonly byte[] ShowManaEffectBytes = {6, 0, 43, 0, 100, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowManaEffectBytes)) {
                var packet = (ShowManaEffectPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.ManaAmount.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowManaEffectBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ShowManaEffectBytes);
            }
        }
    }
}
