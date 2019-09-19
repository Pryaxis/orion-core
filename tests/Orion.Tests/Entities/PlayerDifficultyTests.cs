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

namespace Orion.Entities {
    public class PlayerDifficultyTests {
        [Fact]
        public void FromId_IsCorrect() {
            for (byte i = 0; i < 3; ++i) {
                PlayerDifficulty.FromId(i).Id.Should().Be(i);
            }

            PlayerDifficulty.FromId(3).Should().BeNull();
        }

        [Fact]
        public void FromId_ReturnsSameInstance() {
            var playerDifficulty = PlayerDifficulty.FromId(1);
            var playerDifficulty2 = PlayerDifficulty.FromId(1);

            playerDifficulty.Should().BeSameAs(playerDifficulty2);
        }
    }
}
