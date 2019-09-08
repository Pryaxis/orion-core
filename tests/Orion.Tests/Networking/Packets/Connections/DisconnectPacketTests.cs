using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Connections;
using Xunit;

namespace Orion.Tests.Networking.Packets.Connections {
    public class DisconnectPacketTests {
        [Fact]
        public void SetReason_NullValue_ThrowsArgumentNullException() {
            var packet = new DisconnectPacket();
            Action action = () => packet.Reason = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] DisconnectPlayerBytes = {
            21, 0, 2, 2, 15, 67, 76, 73, 46, 75, 105, 99, 107, 77, 101, 115, 115, 97, 103, 101, 0,
        };


        [Fact]
        public void ReadFromStream_DisconnectPlayer_IsCorrect() {
            using (var stream = new MemoryStream(DisconnectPlayerBytes)) {
                var packet = (DisconnectPacket)Packet.ReadFromStream(stream);

                packet.Reason.ToString().Should().Be("CLI.KickMessage");
            }
        }

        [Fact]
        public void WriteToStream_DisconnectPlayer_IsCorrect() {
            using (var stream = new MemoryStream(DisconnectPlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(DisconnectPlayerBytes);
            }
        }
    }
}
