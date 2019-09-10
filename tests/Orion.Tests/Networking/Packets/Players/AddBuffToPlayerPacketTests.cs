using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class AddBuffToPlayerPacketTests {
        public static readonly byte[] AddBuffToPlayerBytes = {9, 0, 55, 0, 1, 60, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(AddBuffToPlayerBytes)) {
                var packet = (AddBuffToPlayerPacket)Packet.ReadFromStream(stream);
                
                packet.PlayerIndex.Should().Be(0);
                packet.Buff.Should().Be(new Buff(BuffType.ObsidianSkin, TimeSpan.FromSeconds(1)));
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(AddBuffToPlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(AddBuffToPlayerBytes);
            }
        }
    }
}
