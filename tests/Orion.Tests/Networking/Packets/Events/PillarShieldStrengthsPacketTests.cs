using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class PillarShieldStrengthsPacketTests {
        private static readonly byte[] PillarShieldStrengthsBytes = {11, 0, 101, 1, 0, 2, 0, 3, 0, 4, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PillarShieldStrengthsBytes)) {
                var packet = (PillarShieldStrengthsPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SolarPillarShieldStrength.Should().Be(1);
                packet.VortexPillarShieldStrength.Should().Be(2);
                packet.NebulaPillarShieldStrength.Should().Be(3);
                packet.StardustPillarShieldStrength.Should().Be(4);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PillarShieldStrengthsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(PillarShieldStrengthsBytes);
            }
        }
    }
}
