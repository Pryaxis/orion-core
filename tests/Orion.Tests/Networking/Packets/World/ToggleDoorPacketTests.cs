using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class ToggleDoorPacketTests {
        private static readonly byte[] ToggleDoorBytes = {9, 0, 19, 0, 16, 14, 194, 1, 1};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ToggleDoorBytes)) {
                var packet = (ToggleDoorPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ToggleType.Should().Be(DoorToggleType.OpenDoor);
                packet.DoorX.Should().Be(3600);
                packet.DoorY.Should().Be(450);
                packet.ToggleDirection.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ToggleDoorBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ToggleDoorBytes);
            }
        }
    }
}
