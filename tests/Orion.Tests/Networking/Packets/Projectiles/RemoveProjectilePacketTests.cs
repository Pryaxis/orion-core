using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Projectiles;
using Xunit;

namespace Orion.Tests.Networking.Packets.Projectiles {
    public class RemoveProjectilePacketTests {
        private static readonly byte[] RemoveProjectileBytes = {6, 0, 29, 1, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(RemoveProjectileBytes)) {
                var packet = (RemoveProjectilePacket)Packet.ReadFromStream(stream);

                packet.ProjectileIdentity.Should().Be(1);
                packet.ProjectileOwnerPlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(RemoveProjectileBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(RemoveProjectileBytes);
            }
        }
    }
}
