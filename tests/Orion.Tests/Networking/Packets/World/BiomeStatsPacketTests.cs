using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class BiomeStatsPacketTests {
        public static readonly byte[] BiomeStatsBytes = {6, 0, 57, 1, 2, 3};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(BiomeStatsBytes)) {
                var packet = (BiomeStatsPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.HallowedAmount.Should().Be(1);
                packet.CorruptionAmount.Should().Be(2);
                packet.CrimsonAmount.Should().Be(3);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(BiomeStatsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(BiomeStatsBytes);
            }
        }
    }
}
