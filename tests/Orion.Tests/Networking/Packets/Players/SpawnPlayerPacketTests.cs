using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class SpawnPlayerPacketTests {
        private static readonly byte[] SpawnPlayerBytes = {8, 0, 12, 0, 255, 255, 255, 255,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(SpawnPlayerBytes)) {
                var packet = (SpawnPlayerPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerSpawnX.Should().Be(-1);
                packet.PlayerSpawnY.Should().Be(-1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(SpawnPlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(SpawnPlayerBytes);
            }
        }
    }
}
