using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Npcs;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class NpcBuffsPacketTests {
        public static readonly byte[] NpcBuffsBytes = {10, 0, 54, 0, 0, 0, 0, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcBuffsBytes)) {
                var packet = (NpcBuffsPacket)Packet.ReadFromStream(stream);
                
                packet.NpcIndex.Should().Be(0);
                foreach (var buffType in packet.NpcBuffs) {
                    buffType.Should().Be(BuffType.None);
                }
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcBuffsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(NpcBuffsBytes);
            }
        }
    }
}
