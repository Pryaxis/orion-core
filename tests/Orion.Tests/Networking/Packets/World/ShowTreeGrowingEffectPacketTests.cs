using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class ShowTreeGrowingEffectPacketTests {
        private static readonly byte[] ShowTreeGrowingEffectBytes = {11, 0, 112, 1, 0, 1, 100, 0, 10, 1, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowTreeGrowingEffectBytes)) {
                var packet = (ShowTreeGrowingEffectPacket)Packet.ReadFromStream(stream);

                packet.TreeX.Should().Be(256);
                packet.TreeY.Should().Be(100);
                packet.TreeHeight.Should().Be(10);
                packet.TreeType.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowTreeGrowingEffectBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ShowTreeGrowingEffectBytes);
            }
        }
    }
}
