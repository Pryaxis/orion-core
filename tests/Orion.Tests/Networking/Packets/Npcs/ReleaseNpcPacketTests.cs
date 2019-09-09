using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Npcs;
using Orion.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class ReleaseNpcPacketTests {
        public static readonly byte[] ReleaseNpcBytes = {14, 0, 71, 0, 1, 0, 0, 100, 0, 0, 0, 1, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ReleaseNpcBytes)) {
                var packet = (ReleaseNpcPacket)Packet.ReadFromStream(stream);

                packet.NpcPosition.Should().Be(new Vector2(256, 100));
                packet.NpcType.Should().Be(NpcType.BlueSlime);
                packet.NpcStyle.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ReleaseNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ReleaseNpcBytes);
            }
        }
    }
}
