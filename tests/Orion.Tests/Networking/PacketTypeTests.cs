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

using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Networking {
    public class PacketTypeTests {
        [Fact]
        public void FromId_IsCorrect() {
            for (byte i = 0; i < Terraria.ID.MessageID.Count; ++i) {
                PacketType.FromId(i)?.Id.Should().Be(i);
            }

            PacketType.FromId(Terraria.ID.MessageID.Count).Should().BeNull();
        }

        [Fact]
        public void FromId_ReturnsSameInstance() {
            var packetType = PacketType.FromId(1);
            var packetType2 = PacketType.FromId(1);

            packetType.Should().BeSameAs(packetType2);
        }
    }
}
