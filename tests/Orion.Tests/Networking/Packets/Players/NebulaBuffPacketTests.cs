using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class NebulaBuffPacketTests {
        public static readonly byte[] NebulaBuffBytes = {13, 0, 102, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NebulaBuffBytes)) {
                var packet = (NebulaBuffPacket)Packet.ReadFromStream(stream, PacketContext.Server);
                
                packet.PlayerIndex.Should().Be(0);
                packet.BuffType.Should().Be(BuffType.ObsidianSkin);
                packet.BuffPosition.Should().Be(Vector2.Zero);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NebulaBuffBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(NebulaBuffBytes);
            }
        }
    }
}
