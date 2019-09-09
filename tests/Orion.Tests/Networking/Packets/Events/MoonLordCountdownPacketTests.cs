using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class MoonLordCountdownPacketTests {
        private static readonly byte[] MoonLordCountdownBytes = {7, 0, 103, 100, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(MoonLordCountdownBytes)) {
                var packet = (MoonLordCountdownPacket)Packet.ReadFromStream(stream);

                packet.MoonLordCountdown.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(MoonLordCountdownBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(MoonLordCountdownBytes);
            }
        }
    }
}
