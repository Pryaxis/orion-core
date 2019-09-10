using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class MiscActionPacketTests {
        public static readonly byte[] MiscActionBytes = {5, 0, 51, 0, 1,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(MiscActionBytes)) {
                var packet = (MiscActionPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerOrNpcIndex.Should().Be(0);
                packet.Action.Should().Be(MiscAction.SpawnSkeletron);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(MiscActionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(MiscActionBytes);
            }
        }
    }
}
