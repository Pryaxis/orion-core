using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Npcs;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class BuffNpcPacketTests {
        public static readonly byte[] BuffNpcBytes = {8, 0, 53, 0, 0, 1, 60, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(BuffNpcBytes)) {
                var packet = (BuffNpcPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NpcIndex.Should().Be(0);
                packet.Buff.Should().Be(new Buff(BuffType.ObsidianSkin, TimeSpan.FromSeconds(1)));
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(BuffNpcBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(BuffNpcBytes);
            }
        }
    }
}
