using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Connections;
using Xunit;

namespace Orion.Tests.Networking.Packets.Connections {
    public class StartConnectingPacketTests {
        [Fact]
        public void SetVersion_Null_ThrowsArgumentNullException() {
            var packet = new StartConnectingPacket();
            Action action = () => packet.Version = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] StartConnectingBytes = {
            15, 0, 1, 11, 84, 101, 114, 114, 97, 114, 105, 97, 49, 57, 52,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(StartConnectingBytes)) {
                var packet = (StartConnectingPacket)Packet.ReadFromStream(stream);

                packet.Version.Should().Be("Terraria194");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(StartConnectingBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(StartConnectingBytes);
            }
        }
    }
}
