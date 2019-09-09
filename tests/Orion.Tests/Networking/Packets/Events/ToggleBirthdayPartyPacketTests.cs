using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Events;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class ToggleBirthdayPartyPacketTests {
        public static readonly byte[] ToggleBirthdayPartyBytes = {3, 0, 111,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ToggleBirthdayPartyBytes)) {
                Packet.ReadFromStream(stream).Should().BeOfType<ToggleBirthdayPartyPacket>();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ToggleBirthdayPartyBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(ToggleBirthdayPartyBytes);
            }
        }
    }
}
