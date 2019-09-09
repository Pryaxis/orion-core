using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class ShowHealEffectPacketTests {
        private static readonly byte[] ShowHealEffectBytes = {6, 0, 35, 0, 100, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowHealEffectBytes)) {
                var packet = (ShowHealEffectPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.HealAmount.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowHealEffectBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ShowHealEffectBytes);
            }
        }
    }
}
