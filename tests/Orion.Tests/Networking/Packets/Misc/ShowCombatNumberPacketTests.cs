using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class ShowCombatNumberPacketTests {
        public static readonly byte[] ShowCombatNumberBytes = {
            18, 0, 81, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 100, 0, 0, 0,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowCombatNumberBytes)) {
                var packet = (ShowCombatNumberPacket)Packet.ReadFromStream(stream);

                packet.TextPosition.Should().Be(Vector2.Zero);
                packet.TextColor.Should().Be(Color.White);
                packet.TextNumber.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowCombatNumberBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ShowCombatNumberBytes);
            }
        }
    }
}
