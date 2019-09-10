using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class UnlockObjectTypePacketTests {
        public static readonly byte[] UnlockObjectBytes = {8, 0, 52, 1, 0, 1, 100, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(UnlockObjectBytes)) {
                var packet = (UnlockObjectPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ObjectType.Should().Be(UnlockObjectType.Chest);
                packet.TileX.Should().Be(256);
                packet.TileY.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(UnlockObjectBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(UnlockObjectBytes);
            }
        }
    }
}
