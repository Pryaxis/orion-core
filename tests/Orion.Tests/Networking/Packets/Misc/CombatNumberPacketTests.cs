using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class CombatNumberPacketTests {
        public static readonly byte[] CombatNumberBytes = {
            18, 0, 81, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 100, 0, 0, 0,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(CombatNumberBytes)) {
                var packet = (CombatNumberPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NumberPosition.Should().Be(Vector2.Zero);
                packet.NumberColor.Should().Be(Color.White);
                packet.Number.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(CombatNumberBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(CombatNumberBytes);
            }
        }
    }
}
