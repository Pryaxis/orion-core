using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets.World {
    public class TimePacketTests {
        private static readonly byte[] TimeBytes = {12, 0, 18, 1, 0, 128, 0, 0, 200, 0, 200, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(TimeBytes)) {
                var packet = (TimePacket)Packet.ReadFromStream(stream);

                packet.IsDaytime.Should().BeTrue();
                packet.Time.Should().Be(32768);
                packet.SunY.Should().Be(200);
                packet.MoonY.Should().Be(200);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(TimeBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(TimeBytes);
            }
        }
    }
}
