// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Xunit;

namespace Orion.Networking.Packets.Players {
    public class PlayerInfoPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new PlayerInfoPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        private static readonly byte[] Bytes = {15, 0, 13, 0, 72, 16, 0, 0, 31, 131, 71, 0, 48, 212, 69};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
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
                packet.PlayerHeldItemSlotIndex.Should().Be(0);
                packet.PlayerPosition.Should().Be(new Vector2(67134, 6790));
                packet.PlayerVelocity.Should().Be(Vector2.Zero);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
