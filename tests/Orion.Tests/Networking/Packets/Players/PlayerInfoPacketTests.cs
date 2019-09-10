using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Tests.Networking.Packets.Players {
    public class PlayerInfoPacketTests {
        private static readonly byte[] PlayerInfoBytes = {15, 0, 13, 0, 72, 16, 0, 0, 31, 131, 71, 0, 48, 212, 69,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerInfoBytes)) {
                var packet = (PlayerInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.PlayerIndex.Should().Be(0);
                packet.IsPlayerHoldingUp.Should().BeFalse();
                packet.IsPlayerHoldingDown.Should().BeFalse();
                packet.IsPlayerHoldingLeft.Should().BeFalse();
                packet.IsPlayerHoldingRight.Should().BeTrue();
                packet.IsPlayerHoldingJump.Should().BeFalse();
                packet.IsPlayerHoldingUseItem.Should().BeFalse();
                packet.PlayerDirection.Should().BeTrue();
                packet.IsPlayerClimbingRope.Should().BeFalse();
                packet.PlayerClimbingRopeDirection.Should().BeFalse();
                packet.IsPlayerVortexStealthed.Should().BeFalse();
                packet.IsPlayerRightSideUp.Should().BeTrue();
                packet.IsPlayerRaisingShield.Should().BeFalse();
                packet.PlayerSelectedItemIndex.Should().Be(0);
                packet.PlayerPosition.Should().Be(new Vector2(67134, 6790));
                packet.PlayerVelocity.Should().Be(Vector2.Zero);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(PlayerInfoBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(PlayerInfoBytes);
            }
        }
    }
}
