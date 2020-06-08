// Copyright (c) 2020 Pryaxis & Orion Contributors
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

using System;
using System.Linq;
using Serilog.Core;
using Xunit;

namespace Orion.World.Chests {
    // These tests depend on Terraria state.
    [Collection("TerrariaTestsCollection")]
    public class OrionChestServiceTests {
        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Chests_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var kernel = new OrionKernel(Logger.None);
            using var chestService = new OrionChestService(kernel, Logger.None);

            Assert.Throws<IndexOutOfRangeException>(() => chestService.Chests[index]);
        }

        [Fact]
        public void Chests_Item_Get() {
            Terraria.Main.chest[1] = new Terraria.Chest();

            using var kernel = new OrionKernel(Logger.None);
            using var chestService = new OrionChestService(kernel, Logger.None);
            var chest = chestService.Chests[1];

            Assert.Equal(1, chest.Index);
            Assert.Same(Terraria.Main.chest[1], ((OrionChest)chest).Wrapped);
        }

        [Fact]
        public void Chests_Item_GetMultipleTimes_ReturnsSameInstance() {
            Terraria.Main.chest[0] = new Terraria.Chest();

            using var kernel = new OrionKernel(Logger.None);
            using var chestService = new OrionChestService(kernel, Logger.None);

            var chest = chestService.Chests[0];
            var chest2 = chestService.Chests[0];

            Assert.Same(chest, chest2);
        }

        [Fact]
        public void Chests_GetEnumerator() {
            for (var i = 0; i < Terraria.Main.maxChests; ++i) {
                Terraria.Main.chest[i] = new Terraria.Chest();
            }

            using var kernel = new OrionKernel(Logger.None);
            using var chestService = new OrionChestService(kernel, Logger.None);

            var chests = chestService.Chests.ToList();

            for (var i = 0; i < chests.Count; ++i) {
                Assert.Same(Terraria.Main.chest[i], ((OrionChest)chests[i]).Wrapped);
            }
        }
    }
}
