using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Orion.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class NpcKilledEventPacketTests {
        public static readonly byte[] NpcKilledEventBytes = {5, 0, 97, 1, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcKilledEventBytes)) {
                var packet = (NpcKilledEventPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.KilledNpcType.Should().Be(NpcType.BlueSlime);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcKilledEventBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(NpcKilledEventBytes);
            }
        }
    }
}
