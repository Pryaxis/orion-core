using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class EntityTeleportationPacketTests {
        public static readonly byte[] EntityTeleportationBytes = {14, 0, 65, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(EntityTeleportationBytes)) {
                var packet = (EntityTeleportationPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TeleportationType.Should().Be(EntityTeleportationType.TeleportPlayerToOtherPlayer);
                packet.TeleportationStyle.Should().Be(0);
                packet.PlayerOrNpcIndex.Should().Be(0);
                packet.Position.Should().Be(Vector2.Zero);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(EntityTeleportationBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(EntityTeleportationBytes);
            }
        }
    }
}
