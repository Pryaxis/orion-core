using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Projectiles;
using Xunit;

namespace Orion.Tests.Networking.Packets.Projectiles {
    public class CannonShotPacketTests {
        private static readonly byte[] CannonShotBytes = {18, 0, 108, 100, 0, 0, 0, 0, 0, 0, 1, 100, 0, 0, 0, 0, 0, 1,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(CannonShotBytes)) {
                var packet = (CannonShotPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.Damage.Should().Be(100);
                packet.Knockback.Should().Be(0);
                packet.CannonTileX.Should().Be(256);
                packet.CannonTileY.Should().Be(100);
                packet.Angle.Should().Be(0);
                packet.AmmoType.Should().Be(0);
                packet.ShooterPlayerIndex.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(CannonShotBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(CannonShotBytes);
            }
        }
    }
}
