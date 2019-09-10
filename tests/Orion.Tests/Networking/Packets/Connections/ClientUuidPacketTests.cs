using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Connections;
using Xunit;

namespace Orion.Tests.Networking.Packets.Connections {
    public class ClientUuidPacketTests {
        [Fact]
        public void SetClientUuid_NullValue_ThrowsArgumentNullException() {
            var packet = new ClientUuidPacket();
            Action action = () => packet.ClientUuid = null;

            action.Should().Throw<ArgumentNullException>();
        }
        
        public static readonly byte[] ClientUuidBytes = {12, 0, 68, 8, 84, 101, 114, 114, 97, 114, 105, 97,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ClientUuidBytes)) {
                var packet = (ClientUuidPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ClientUuid.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ClientUuidBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ClientUuidBytes);
            }
        }
    }
}
