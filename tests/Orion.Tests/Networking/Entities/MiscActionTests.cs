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
using Orion.Networking.Misc;
using Xunit;

namespace Orion.Networking.Entities {
    public class MiscActionTests {
        [Fact]
        public void FromId_IsCorrect() {
            for (byte i = 1; i < 5; ++i) {
                MiscAction.FromId(i).Id.Should().Be(i);
            }

            MiscAction.FromId(0).Should().BeNull();
            MiscAction.FromId(5).Should().BeNull();
        }

        [Fact]
        public void FromId_ReturnsSameInstance() {
            var miscAction = MiscAction.FromId(1);
            var miscAction2 = MiscAction.FromId(1);

            miscAction.Should().BeSameAs(miscAction2);
        }
    }
}
