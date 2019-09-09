using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World.TileEntities;
using Xunit;

namespace Orion.Tests.Networking.Packets.World.TileEntities {
    public class SignTextPacketTests {
        [Fact]
        public void SetSignText_NullValue_ThrowsArgumentNullException() {
            var packet = new SignTextPacket();
            Action action = () => packet.SignText = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] SignTextBytes = {18, 0, 47, 0, 0, 0, 1, 100, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(SignTextBytes)) {
                var packet = (SignTextPacket)Packet.ReadFromStream(stream);

                packet.SignIndex.Should().Be(0);
                packet.SignX.Should().Be(256);
                packet.SignY.Should().Be(100);
                packet.SignText.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(SignTextBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(SignTextBytes);
            }
        }
    }
}
