using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class NpcInfoPacketTests {
        private static readonly byte[] NpcInfoBytes = {
            26, 0, 23, 1, 0, 38, 209, 132, 71, 0, 0, 213, 69, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 130, 22, 0,
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcInfoBytes)) {
                var packet = (NpcInfoPacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(1);
                packet.NpcPosition.Should().Be(new Vector2(68002.3f, 6816));
                packet.NpcVelocity.Should().Be(Vector2.Zero);
                packet.NpcTargetIndex.Should().Be(255);
                packet.NpcHorizontalDirection.Should().BeFalse();
                packet.NpcVerticalDirection.Should().BeTrue();
                packet.NpcAiValues[0].Should().Be(0);
                packet.NpcAiValues[1].Should().Be(0);
                packet.NpcAiValues[2].Should().Be(0);
                packet.NpcAiValues[3].Should().Be(0);
                packet.NpcSpriteDirection.Should().BeFalse();
                packet.IsNpcAtMaxHealth.Should().BeTrue();
                packet.NpcType.Should().Be(NpcType.Guide);
                packet.NpcNumberOfHealthBytes.Should().Be(0);
                packet.NpcHealth.Should().Be(0);
                packet.NpcReleaserPlayerIndex.Should().Be(0);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(NpcInfoBytes);
            }
        }
    }
}
