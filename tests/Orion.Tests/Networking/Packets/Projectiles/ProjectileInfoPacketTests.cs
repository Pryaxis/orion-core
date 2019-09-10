using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Projectiles;
using Orion.Projectiles;
using Xunit;

namespace Orion.Tests.Networking.Packets.Projectiles {
    public class ProjectileInfoPacketTests {
        private static readonly byte[] ProjectileInfoBytes = {
            31, 0, 27, 0, 0, 128, 57, 131, 71, 0, 200, 212, 69, 254, 14, 40, 65, 147, 84, 121, 193, 205, 204, 128, 64,
            99, 0, 0, 89, 0, 0,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ProjectileInfoBytes)) {
                var packet = (ProjectileInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ProjectileIdentity.Should().Be(0);
                packet.ProjectilePosition.Should().Be(new Vector2(67187, 6809));
                packet.ProjectileKnockback.Should().Be(4.025f);
                packet.ProjectileDamage.Should().Be(99);
                packet.OwnerPlayerIndex.Should().Be(0);
                packet.ProjectileType.Should().Be(ProjectileType.CrystalBullet);
                packet.ProjectileAiValues[0].Should().Be(0);
                packet.ProjectileAiValues[1].Should().Be(0);
                packet.ProjectileUuid.Should().Be(-1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ProjectileInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ProjectileInfoBytes);
            }
        }
    }
}
