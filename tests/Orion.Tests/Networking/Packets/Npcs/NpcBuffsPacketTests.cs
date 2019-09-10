using System;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Npcs;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class NpcBuffsPacketTests {
        public static readonly byte[] NpcBuffsBytes = {20, 0, 54, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcBuffsBytes)) {
                var packet = (NpcBuffsPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NpcIndex.Should().Be(0);
                foreach (var buff in packet.NpcBuffs) {
                    buff.Should().Be(new Buff(BuffType.None, TimeSpan.Zero));
                }
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcBuffsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(NpcBuffsBytes);
            }
        }
    }
}
