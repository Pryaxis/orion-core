using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class DamageNpcPacketTests {
        private static readonly byte[] DamageNpcBytes = {13, 0, 28, 100, 0, 108, 0, 205, 204, 128, 64, 2, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(DamageNpcBytes)) {
                var packet = (DamageNpcPacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(100);
                packet.Damage.Should().Be(108);
                packet.Knockback.Should().Be(4.025f);
                packet.HitDirection.Should().Be(1);
                packet.IsCriticalHit.Should().BeFalse();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(DamageNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(DamageNpcBytes);
            }
        }
    }
}
