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
    public class ItemPrefixTests {
        [Fact]
        public void GetId_IsCorrect() {
            ItemPrefix.FromId(82).Id.Should().Be(82);
        }

        [Fact]
        public void GetIsUnknown_IsCorrect() {
            ItemPrefix.Unreal.IsUnknown.Should().BeFalse();
            ItemPrefix.FromId(int.MaxValue).IsUnknown.Should().BeTrue();
        }

        [Fact]
        public void Equals_IsCorrect() {
            var itemPrefix = ItemPrefix.FromId(82);
            var itemPrefix2 = ItemPrefix.FromId(82);

            itemPrefix.Equals(itemPrefix2).Should().BeTrue();

            // ReSharper disable once SuspiciousTypeConversion.Global
            itemPrefix.Equals("null").Should().BeFalse();
            itemPrefix.Equals(null).Should().BeFalse();
        }
    }
}
