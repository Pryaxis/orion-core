// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using TerrariaItem = Terraria.Item;

namespace Orion.Items {
    public class OrionItemStatsTests {
        [Fact]
        public void MaxStackSize_Get() {
            var terrariaItem = new TerrariaItem { maxStack = 100 };
            IItemStats itemStats = new OrionItemStats(terrariaItem);

            itemStats.MaxStackSize.Should().Be(100);
        }

        [Fact]
        public void Rarity_Get() {
            var terrariaItem = new TerrariaItem { rare = (int)ItemRarity.Red };
            IItemStats itemStats = new OrionItemStats(terrariaItem);

            itemStats.Rarity.Should().Be(ItemRarity.Red);
        }
    }
}
