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
using System.Diagnostics.CodeAnalysis;
using Orion.Core.Items;
using Xunit;

namespace Orion.Core.World.Chests {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionChestTests {
        [Fact]
        public void Name_GetNullValue() {
            var terrariaChest = new Terraria.Chest { x = 256, y = 100, name = null };
            var chest = new OrionChest(terrariaChest);

            Assert.Equal(string.Empty, chest.Name);
        }

        [Fact]
        public void Name_Get() {
            var terrariaChest = new Terraria.Chest { x = 256, y = 100, name = "test" };
            var chest = new OrionChest(terrariaChest);

            Assert.Equal("test", chest.Name);
        }

        [Fact]
        public void Name_SetNullValue_ThrowsArgumentNullException() {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            Assert.Throws<ArgumentNullException>(() => chest.Name = null!);
        }

        [Fact]
        public void Name_Set() {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            chest.Name = "test";

            Assert.Equal("test", terrariaChest.name);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100)]
        public void Items_Get_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            Assert.Throws<IndexOutOfRangeException>(() => chest.Items[index]);
        }

        [Fact]
        public void Items_Get_Item_Get() {
            var terrariaChest = new Terraria.Chest();
            terrariaChest.item[0] = new Terraria.Item {
                type = (int)ItemId.Sdmg,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };

            var chest = new OrionChest(terrariaChest);

            Assert.Equal(new ItemStack(ItemId.Sdmg, 1, ItemPrefix.Unreal), chest.Items[0]);
        }

        [Fact]
        public void Items_Get_Item_Get_Null() {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            Assert.Equal(default, chest.Items[0]);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100)]
        public void Items_Get_Item_SetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            Assert.Throws<IndexOutOfRangeException>(() => chest.Items[index] = default);
        }

        [Fact]
        public void Items_Get_Item_Set() {
            var terrariaChest = new Terraria.Chest();
            terrariaChest.item[0] = new Terraria.Item();
            var chest = new OrionChest(terrariaChest);

            chest.Items[0] = new ItemStack(ItemId.Sdmg, 1, ItemPrefix.Unreal);

            Assert.Equal(ItemId.Sdmg, (ItemId)terrariaChest.item[0].type);
            Assert.Equal(1, terrariaChest.item[0].stack);
            Assert.Equal(ItemPrefix.Unreal, (ItemPrefix)terrariaChest.item[0].prefix);
        }

        [Fact]
        public void Items_Get_Item_Set_Null() {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            chest.Items[0] = new ItemStack(ItemId.Sdmg, 1, ItemPrefix.Unreal);

            Assert.Equal(ItemId.Sdmg, (ItemId)terrariaChest.item[0].type);
            Assert.Equal(1, terrariaChest.item[0].stack);
            Assert.Equal(ItemPrefix.Unreal, (ItemPrefix)terrariaChest.item[0].prefix);
        }

        [Fact]
        public void Index_Get() {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(1, terrariaChest);

            Assert.Equal(1, chest.Index);
        }

        [Fact]
        public void IsActive_Get_ReturnsFalse() {
            var chest = new OrionChest(null);

            Assert.False(chest.IsActive);
        }

        [Fact]
        public void IsActive_Get_ReturnsTrue() {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            Assert.True(chest.IsActive);
        }

        [Fact]
        public void X_Get() {
            var terrariaChest = new Terraria.Chest { x = 256, y = 100, name = "test" };
            var chest = new OrionChest(terrariaChest);

            Assert.Equal(256, chest.X);
        }

        [Fact]
        public void X_Set() {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            chest.X = 256;

            Assert.Equal(256, terrariaChest.x);
        }

        [Fact]
        public void Y_Get() {
            var terrariaChest = new Terraria.Chest { x = 256, y = 100, name = "test" };
            var chest = new OrionChest(terrariaChest);

            Assert.Equal(100, chest.Y);
        }

        [Fact]
        public void Y_Set() {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);

            chest.Y = 100;

            Assert.Equal(100, terrariaChest.y);
        }
    }
}
