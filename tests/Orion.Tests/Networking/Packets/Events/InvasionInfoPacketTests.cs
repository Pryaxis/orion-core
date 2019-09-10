using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class InvasionInfoPacketTests {
        public static readonly byte[] InvasionInfoBytes = {19, 0, 78, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(InvasionInfoBytes)) {
                var packet = (InvasionInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NumberOfKills.Should().Be(1);
                packet.NumberOfKillsToProgress.Should().Be(256);
                packet.InvasionIconType.Should().Be(1);
                packet.WaveNumber.Should().Be(2);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(InvasionInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(InvasionInfoBytes);
            }
        }
    }
}
