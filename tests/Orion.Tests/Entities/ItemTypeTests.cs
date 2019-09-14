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
    public class ItemTypeTests {
        [Fact]
        public void GetId_IsCorrect() {
            var itemType = new ItemType(100);

            itemType.Id.Should().Be(100);
        }

        [Fact]
        public void Equals_IsCorrect() {
            var itemType = new ItemType(100);
            var itemType2 = new ItemType(100);
            var itemType3 = new ItemType(101);

            itemType.Equals(itemType2).Should().BeTrue();
            itemType.Equals(itemType3).Should().BeFalse();
        }

        [Fact]
        public void EqualsObject_IsCorrect() {
            var itemType = new ItemType(100);
            var itemType2 = new ItemType(100);

            itemType.Equals((object)itemType2).Should().BeTrue();

            // ReSharper disable once SuspiciousTypeConversion.Global
            itemType.Equals("null").Should().BeFalse();
            itemType.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void EqualsOperator_IsCorrect() {
            var itemType = new ItemType(100);
            var itemType2 = new ItemType(100);
            var itemType3 = new ItemType(101);

            (itemType == itemType2).Should().BeTrue();
            (itemType == itemType3).Should().BeFalse();
        }

        [Fact]
        public void NotEqualsOperator_IsCorrect() {
            var itemType = new ItemType(100);
            var itemType2 = new ItemType(100);
            var itemType3 = new ItemType(101);

            (itemType != itemType2).Should().BeFalse();
            (itemType != itemType3).Should().BeTrue();
        }
    }
}
