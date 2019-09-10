using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Connections;
using Xunit;

namespace Orion.Tests.Networking.Packets.Connections {
    public class ClientStatusPacketTests {
        [Fact]
        public void SetStatusText_NullValue_ThrowsArgumentNullException() {
            var packet = new ClientStatusPacket();
            Action action = () => packet.StatusText = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] ClientStatusBytes = {
            28, 0, 9, 15, 0, 0, 0, 2, 18, 76, 101, 103, 97, 99, 121, 73, 110, 116, 101, 114, 102, 97, 99, 101, 46, 52,
            52, 0,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ClientStatusBytes)) {
                var packet = (ClientStatusPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.StatusIncrease.Should().Be(15);
                packet.StatusText.ToString().Should().Be("LegacyInterface.44");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ClientStatusBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ClientStatusBytes);
            }
        }
    }
}
