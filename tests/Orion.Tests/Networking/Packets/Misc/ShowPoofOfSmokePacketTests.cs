using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class ShowPoofOfSmokePacketTests {
        public static readonly byte[] ShowPoofOfSmokeBytes = {7, 0, 106, 0, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowPoofOfSmokeBytes)) {
                var packet = (ShowPoofOfSmokePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SmokePosition.Should().Be(new HalfVector2());
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ShowPoofOfSmokeBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ShowPoofOfSmokeBytes);
            }
        }
    }
}
