using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class EndOldOnesArmyPacketTests {
        public static readonly byte[] EndOldOnesArmyBytes = {3, 0, 114,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(EndOldOnesArmyBytes)) {
                Packet.ReadFromStream(stream).Should().BeOfType<EndOldOnesArmyPacket>();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(EndOldOnesArmyBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(EndOldOnesArmyBytes);
            }
        }
    }
}
