using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class ToggleGemLockPacketTests {
        public static readonly byte[] ToggleGemLockBytes = {8, 0, 105, 0, 1, 100, 0, 1,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ToggleGemLockBytes)) {
                var packet = (ToggleGemLockPacket)Packet.ReadFromStream(stream);

                packet.GemLockTileX.Should().Be(256);
                packet.GemLockTileY.Should().Be(100);
                packet.IsGemLockLocked.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ToggleGemLockBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ToggleGemLockBytes);
            }
        }
    }
}
