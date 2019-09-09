using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class NpcStealCoinsPacketTests {
        public static readonly byte[] NpcStealCoinsBytes = {17, 0, 92, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcStealCoinsBytes)) {
                var packet = (NpcStealCoinsPacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(0);
                packet.NpcStolenValue.Should().Be(0);
                packet.CoinPosition.Should().Be(new Vector2(0));
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcStealCoinsBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(NpcStealCoinsBytes);
            }
        }
    }
}
