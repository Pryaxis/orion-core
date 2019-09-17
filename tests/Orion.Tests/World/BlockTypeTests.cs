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
    [Collection("TerrariaTestsCollection")]
    public class BlockTypeTests {
        [Fact]
        public void GetId_IsCorrect() {
            BlockType.FromId(100).Id.Should().Be(100);
        }

        [Fact]
        public void GetIsUnknown_IsCorrect() {
            BlockType.Stone.IsUnknown.Should().BeFalse();
            BlockType.FromId(ushort.MaxValue).IsUnknown.Should().BeTrue();
        }

        [Fact]
        public void Equals_IsCorrect() {
            var buffType = BlockType.FromId(100);
            var buffType2 = BlockType.FromId(100);

            buffType.Equals(buffType2).Should().BeTrue();

            // ReSharper disable once SuspiciousTypeConversion.Global
            buffType.Equals("null").Should().BeFalse();
            buffType.Equals(null).Should().BeFalse();
        }
    }
}
