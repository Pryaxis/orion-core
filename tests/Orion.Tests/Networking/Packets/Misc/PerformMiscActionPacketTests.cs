using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class PerformMiscActionPacketTests {
        public static readonly byte[] PerformMiscActionBytes = {5, 0, 51, 0, 1};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PerformMiscActionBytes)) {
                var packet = (PerformMiscActionPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerOrNpcIndex.Should().Be(0);
                packet.Action.Should().Be(MiscAction.SpawnSkeletron);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PerformMiscActionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(PerformMiscActionBytes);
            }
        }
    }
}
