using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class ShowChatPacketTests {
        public static readonly byte[] ShowChatBytes = {
            18, 0, 107, 255, 255, 255, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97, 100, 0
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowChatBytes)) {
                var packet = (ShowChatPacket)Packet.ReadFromStream(stream);

                packet.ChatColor.Should().Be(Color.White);
                packet.ChatText.ToString().Should().Be("Terraria");
                packet.ChatLineWidth.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowChatBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ShowChatBytes);
            }
        }
    }
}
