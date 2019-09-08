using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Connections;
using Xunit;

namespace Orion.Tests.Networking.Packets.Connections {
    public class PasswordResponsePacketTests {
        public static readonly byte[] PasswordResponseBytes = {
            12, 0, 38, 8, 84, 101, 114, 114, 97, 114, 105, 97
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PasswordResponseBytes)) {
                var packet = (PasswordResponsePacket)Packet.ReadFromStream(stream);

                packet.Password.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PasswordResponseBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PasswordResponseBytes);
            }
        }
    }
}
