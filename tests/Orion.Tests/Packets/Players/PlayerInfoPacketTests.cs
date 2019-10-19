// Copyright (c) 2019 Pryaxis & Orion Contributors
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

namespace Orion.Packets.Players {
    public class PlayerInfoPacketTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var packet = new PlayerInfoPacket();

            packet.SimpleProperties_Set_MarkAsDirty();
        }

        public static readonly byte[] Bytes = { 15, 0, 13, 0, 72, 16, 0, 0, 31, 131, 71, 0, 48, 212, 69 };

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var packet = (PlayerInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.PlayerIndex.Should().Be(0);
            packet.IsHoldingUp.Should().BeFalse();
            packet.IsHoldingDown.Should().BeFalse();
            packet.IsHoldingLeft.Should().BeFalse();
            packet.IsHoldingRight.Should().BeTrue();
            packet.IsHoldingJump.Should().BeFalse();
            packet.IsHoldingUseItem.Should().BeFalse();
            packet.Direction.Should().BeTrue();
            packet.IsClimbingRope.Should().BeFalse();
            packet.ClimbingRopeDirection.Should().BeFalse();
            packet.IsVortexStealthed.Should().BeFalse();
            packet.IsRightSideUp.Should().BeTrue();
            packet.IsRaisingShield.Should().BeFalse();
            packet.HeldItemSlot.Should().Be(0);
            packet.Position.Should().Be(new Vector2(67134, 6790));
            packet.Velocity.Should().Be(Vector2.Zero);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() => Bytes.ShouldDeserializeAndSerializeSamePacket();
    }
}
