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
        public void GetId_IsCorrect() {
            PacketType.FromId(100).Id.Should().Be(100);
        }

        [Fact]
        public void GetIsUnknown_IsCorrect() {
            PacketType.PlayerConnect.IsUnknown.Should().BeFalse();
            PacketType.FromId(byte.MaxValue).IsUnknown.Should().BeTrue();
        }

        [Fact]
        public void EqualsObject_IsCorrect() {
            var packetType = PacketType.FromId(100);
            var packetType2 = PacketType.FromId(100);

            packetType.Equals(packetType2).Should().BeTrue();

            // ReSharper disable once SuspiciousTypeConversion.Global
            packetType.Equals("null").Should().BeFalse();
            packetType.Equals(null).Should().BeFalse();
        }
    }
}
