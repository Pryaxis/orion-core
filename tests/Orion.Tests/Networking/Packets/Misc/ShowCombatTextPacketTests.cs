using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class ShowCombatTextPacketTests {
        public static readonly byte[] ShowCombatTextBytes = {
            24, 0, 119, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowCombatTextBytes)) {
                var packet = (ShowCombatTextPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.TextPosition.Should().Be(Vector2.Zero);
                packet.TextColor.Should().Be(Color.White);
                packet.Text.ToString().Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowCombatTextBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ShowCombatTextBytes);
            }
        }
    }
}
