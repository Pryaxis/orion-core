using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerManaPacketTests {
        public static readonly byte[] PlayerManaBytes = {8, 0, 42, 0, 100, 0, 100, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerManaBytes)) {
                var packet = (PlayerManaPacket)Packet.ReadFromStream(stream);

                packet.PlayerIndex.Should().Be(0);
                packet.PlayerMana.Should().Be(100);
                packet.PlayerMaxMana.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerManaBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(PlayerManaBytes);
            }
        }
    }
}
