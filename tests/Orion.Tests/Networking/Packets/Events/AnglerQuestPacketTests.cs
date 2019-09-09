using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets.Events {
    public class AnglerQuestPacketTests {
        public static readonly byte[] AnglerQuestBytes = {5, 0, 74, 1, 1,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(AnglerQuestBytes)) {
                var packet = (AnglerQuestPacket)Packet.ReadFromStream(stream);

                packet.AnglerQuest.Should().Be(1);
                packet.IsAnglerQuestFinished.Should().BeTrue();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(AnglerQuestBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(AnglerQuestBytes);
            }
        }
    }
}
