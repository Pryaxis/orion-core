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

using System;
using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Entities;
using Xunit;

namespace Orion.Networking.Packets.Npcs {
    public class NpcInfoPacketTests {
        [Fact]
        public void SetNpcType_NullValue_ThrowsArgumentNullException() {
            var packet = new NpcInfoPacket();
            Action action = () => packet.NpcType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        private static readonly byte[] Bytes = {
            26, 0, 23, 1, 0, 38, 209, 132, 71, 0, 0, 213, 69, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 130, 22, 0
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (NpcInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

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
            TestUtils.WriteToStream_SameBytes(Bytes);
        }
    }
}
