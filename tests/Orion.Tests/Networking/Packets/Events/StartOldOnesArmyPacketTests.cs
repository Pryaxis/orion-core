using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class StartOldOnesArmyPacketTests {
        public static readonly byte[] StartOldOnesArmyBytes = {7, 0, 113, 0, 1, 100, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(StartOldOnesArmyBytes)) {
                var packet = (StartOldOnesArmyPacket)Packet.ReadFromStream(stream);

                packet.CrystalX.Should().Be(256);
                packet.CrystalY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(StartOldOnesArmyBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(StartOldOnesArmyBytes);
            }
        }
    }
}
