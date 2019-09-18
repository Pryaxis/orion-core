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

namespace Orion.World {
    public class InvasionTypeTests {
        [Fact]
        public void FromId_IsCorrect() {
            for (byte i = 0; i < 5; ++i) {
                InvasionType.FromId(i).Id.Should().Be(i);
            }

            InvasionType.FromId(5).Should().BeNull();
        }

        [Fact]
        public void FromId_ReturnsSameInstance() {
            var invasionType = InvasionType.FromId(1);
            var invasionType2 = InvasionType.FromId(1);

            invasionType.Should().BeSameAs(invasionType2);
        }
    }
}
