using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class DamagePlayerPacketTests {
        public static readonly byte[] DamagePlayerBytes = {15, 0, 117, 0, 128, 4, 116, 101, 115, 116, 100, 0, 1, 1, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(DamagePlayerBytes)) {
                var packet = (DamagePlayerPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerDeathReason.SourceCustomReason.Should().Be("test");
                packet.Damage.Should().Be(100);
                packet.HitDirection.Should().Be(0);
                packet.IsHitCritical.Should().BeTrue();
                packet.IsHitFromPvp.Should().BeFalse();
                packet.HitCooldown.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(DamagePlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(DamagePlayerBytes);
            }
        }
    }
}
