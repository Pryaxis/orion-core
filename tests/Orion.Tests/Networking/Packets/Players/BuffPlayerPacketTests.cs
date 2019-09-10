using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class BuffPlayerPacketTests {
        public static readonly byte[] BuffPlayerBytes = {9, 0, 55, 0, 1, 60, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(BuffPlayerBytes)) {
                var packet = (BuffPlayer)Packet.ReadFromStream(stream, PacketContext.Server);
                
                packet.PlayerIndex.Should().Be(0);
                packet.Buff.Should().Be(new Buff(BuffType.ObsidianSkin, TimeSpan.FromSeconds(1)));
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(BuffPlayerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(BuffPlayerBytes);
            }
        }
    }
}
