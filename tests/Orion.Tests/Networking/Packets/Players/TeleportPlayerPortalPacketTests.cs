using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class TeleportPlayerPortalPacketTests {
        public static readonly byte[] TeleportPlayerPortalBytes = {
            22, 0, 96, 100, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(TeleportPlayerPortalBytes)) {
                var packet = (TeleportPlayerPortalPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerIndex.Should().Be(100);
                packet.PortalIndex.Should().Be(2);
                packet.PlayerNewPosition.Should().Be(Vector2.Zero);
                packet.PlayerNewVelocity.Should().Be(Vector2.Zero);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(TeleportPlayerPortalBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(TeleportPlayerPortalBytes);
            }
        }
    }
}
