using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class UpdateOldOnesArmyPacketTests {
        public static readonly byte[] UpdateOldOnesArmyBytes = {7, 0, 116, 1, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(UpdateOldOnesArmyBytes)) {
                var packet = (UpdateOldOnesArmyPacket)Packet.ReadFromStream(stream);

                packet.TimeLeftBetweenWaves.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(UpdateOldOnesArmyBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(UpdateOldOnesArmyBytes);
            }
        }
    }
}
