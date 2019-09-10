using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Npcs;
using Orion.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class NpcKillCountPacketTests {
        public static readonly byte[] NpcKillCountBytes = {9, 0, 83, 1, 0, 100, 0, 0, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcKillCountBytes)) {
                var packet = (NpcKillCountPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NpcType.Should().Be(NpcType.BlueSlime);
                packet.NpcTypeKillCount.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcKillCountBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(NpcKillCountBytes);
            }
        }
    }
}
