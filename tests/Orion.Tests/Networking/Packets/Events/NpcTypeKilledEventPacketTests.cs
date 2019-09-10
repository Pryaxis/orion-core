using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Orion.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class NpcTypeKilledEventPacketTests {
        public static readonly byte[] NpcTypeKilledEventBytes = {5, 0, 97, 1, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcTypeKilledEventBytes)) {
                var packet = (NpcTypeKilledEventPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NpcTypeKilled.Should().Be(NpcType.BlueSlime);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcTypeKilledEventBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(NpcTypeKilledEventBytes);
            }
        }
    }
}
