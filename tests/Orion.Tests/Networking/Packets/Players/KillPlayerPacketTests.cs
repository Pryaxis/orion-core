using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class KillPlayerPacketTests {
        public static readonly byte[] KillPlayerBytes = {14, 0, 118, 0, 128, 4, 116, 101, 115, 116, 100, 0, 1, 1,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(KillPlayerBytes)) {
                var packet = (KillPlayerPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerDeathReason.SourceCustomReason.Should().Be("test");
                packet.Damage.Should().Be(100);
                packet.HitDirection.Should().Be(0);
                packet.IsDeathFromPvp.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(KillPlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(KillPlayerBytes);
            }
        }
    }
}
