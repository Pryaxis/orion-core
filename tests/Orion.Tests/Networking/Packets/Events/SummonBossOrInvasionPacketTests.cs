using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class SummonBossOrInvasionPacketTests {
        public static readonly byte[] SummonBossOrInvasionBytes = {7, 0, 61, 0, 0, 255, 255};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(SummonBossOrInvasionBytes)) {
                var packet = (SummonBossOrInvasionPacket)Packet.ReadFromStream(stream);

                packet.SummonerPlayerIndex.Should().Be(0);
                packet.BossOrInvasionType.Should().Be(-1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(SummonBossOrInvasionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(SummonBossOrInvasionBytes);
            }
        }
    }
}
