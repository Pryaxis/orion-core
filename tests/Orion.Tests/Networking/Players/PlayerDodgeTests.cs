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
using Xunit;

namespace Orion.Networking.Players {
    public class PlayerDodgeTests {
        [Fact]
        public void FromId_IsCorrect() {
            for (byte i = 1; i < 3; ++i) {
                PlayerDodge.FromId(i).Id.Should().Be(i);
            }

            PlayerDodge.FromId(0).Should().BeNull();
            PlayerDodge.FromId(3).Should().BeNull();
        }

        [Fact]
        public void FromId_ReturnsSameInstance() {
            var playerDodge = PlayerDodge.FromId(1);
            var playerDodge2 = PlayerDodge.FromId(1);

            playerDodge.Should().BeSameAs(playerDodge2);
        }
    }
}
