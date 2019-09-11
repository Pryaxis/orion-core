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

using System;
using FluentAssertions;
using Orion.Items;
using Orion.World.TileEntities;
using Terraria;
using Xunit;

namespace Orion.Tests.World.TileEntities {
    public class OrionChestTests {
        [Theory]
        [InlineData(100)]
        public void GetX_IsCorrect(int x) {
            var terrariaChest = new Chest {x = x};
            var chest = new OrionChest(0, terrariaChest);

            chest.X.Should().Be(x);
        }

        [Theory]
        [InlineData(100)]
        public void SetX_IsCorrect(int x) {
            var terrariaChest = new Chest();
            var chest = new OrionChest(0, terrariaChest);

            chest.X = x;

            terrariaChest.x.Should().Be(x);
        }

        [Theory]
        [InlineData(100)]
        public void GetY_IsCorrect(int y) {
            var terrariaChest = new Chest {y = y};
            var chest = new OrionChest(0, terrariaChest);

            chest.Y.Should().Be(y);
        }

        [Theory]
        [InlineData(100)]
        public void SetY_IsCorrect(int y) {
            var terrariaChest = new Chest();
            var chest = new OrionChest(0, terrariaChest);

            chest.Y = y;

            terrariaChest.y.Should().Be(y);
        }

        [Theory]
        [InlineData("test")]
        public void GetName_IsCorrect(string name) {
            var terrariaChest = new Chest {name = name};
            var chest = new OrionChest(0, terrariaChest);

            chest.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("test")]
        public void SetName_IsCorrect(string name) {
            var terrariaChest = new Chest();
            var chest = new OrionChest(0, terrariaChest);

            chest.Name = name;

            terrariaChest.name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaChest = new Chest();
            var chest = new OrionChest(0, terrariaChest);
            Action action = () => chest.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(ItemType.SDMG, 1, ItemPrefix.Unreal)]
        public void GetItems_IsCorrect(ItemType itemType, int itemStackSize, ItemPrefix itemPrefix) {
            var terrariaChest = new Chest();
            terrariaChest.item[0] = new Item {
                type = (int)itemType,
                stack = itemStackSize,
                prefix = (byte)itemPrefix
            };
            var chest = new OrionChest(0, terrariaChest);

            chest.Items[0].Type.Should().Be(itemType);
            chest.Items[0].StackSize.Should().Be(itemStackSize);
            chest.Items[0].Prefix.Should().Be(itemPrefix);
        }
    }
}
