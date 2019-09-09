using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class CatchNpcPacketTests {
        public static readonly byte[] CatchNpcBytes = {6, 0, 70, 1, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(CatchNpcBytes)) {
                var packet = (CatchNpcPacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(1);
                packet.NpcCatcherPlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(CatchNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(CatchNpcBytes);
            }
        }
    }
}
