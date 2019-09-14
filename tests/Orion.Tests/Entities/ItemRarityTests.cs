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
using Microsoft.Xna.Framework;
using Xunit;

namespace Orion.Entities {
    public class ItemRarityTests {
        [Fact]
        public void GetId_IsCorrect() {
            var itemRarity = new ItemRarity(100);

            itemRarity.Level.Should().Be(100);
        }

        [Fact]
        public void GetColor_RandomLevel_ReturnsWhite() {
            var itemRarity = new ItemRarity(100);

            itemRarity.Color.Should().Be(Color.White);
        }

        [Fact]
        public void Equals_IsCorrect() {
            var itemRarity = new ItemRarity(100);
            var itemRarity2 = new ItemRarity(100);
            var itemRarity3 = new ItemRarity(101);

            itemRarity.Equals(itemRarity2).Should().BeTrue();
            itemRarity.Equals(itemRarity3).Should().BeFalse();
        }

        [Fact]
        public void EqualsObject_IsCorrect() {
            var itemRarity = new ItemRarity(100);
            var itemRarity2 = new ItemRarity(100);

            itemRarity.Equals((object)itemRarity2).Should().BeTrue();

            // ReSharper disable once SuspiciousTypeConversion.Global
            itemRarity.Equals("null").Should().BeFalse();
            itemRarity.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void EqualsOperator_IsCorrect() {
            var itemRarity = new ItemRarity(100);
            var itemRarity2 = new ItemRarity(100);
            var itemRarity3 = new ItemRarity(101);

            (itemRarity == itemRarity2).Should().BeTrue();
            (itemRarity == itemRarity3).Should().BeFalse();
        }

        [Fact]
        public void NotEqualsOperator_IsCorrect() {
            var itemRarity = new ItemRarity(100);
            var itemRarity2 = new ItemRarity(100);
            var itemRarity3 = new ItemRarity(101);

            (itemRarity != itemRarity2).Should().BeFalse();
            (itemRarity != itemRarity3).Should().BeTrue();
        }
    }
}
