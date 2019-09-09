using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class DamageNpcWithItemPacketTests {
        private static readonly byte[] DamageNpcWithItemBytes = {6, 0, 24, 1, 0, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(DamageNpcWithItemBytes)) {
                var packet = (DamageNpcWithItemPacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(1);
                packet.DamagerPlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(DamageNpcWithItemBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(DamageNpcWithItemBytes);
            }
        }
    }
}
