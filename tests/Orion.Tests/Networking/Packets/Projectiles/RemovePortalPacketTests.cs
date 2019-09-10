using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Projectiles;
using Xunit;

namespace Orion.Tests.Networking.Packets.Projectiles {
    public class RemovePortalPacketTests {
        private static readonly byte[] RemovePortalBytes = {5, 0, 95, 100, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(RemovePortalBytes)) {
                var packet = (RemovePortalPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PortalProjectileIndex.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(RemovePortalBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(RemovePortalBytes);
            }
        }
    }
}
