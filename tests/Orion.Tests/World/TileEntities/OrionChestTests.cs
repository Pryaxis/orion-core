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

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Orion.Items;
using Xunit;
using TerrariaChest = Terraria.Chest;
using TerrariaItem = Terraria.Item;

namespace Orion.World.TileEntities {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionChestTests {
        [Fact]
        public void X_Get() {
            var terrariaChest = new TerrariaChest {x = 100};
            for (var i = 0; i < terrariaChest.item.Length; ++i) {
                terrariaChest.item[i] = new TerrariaItem();
            }

            IChest chest = new OrionChest(0, terrariaChest);

            chest.X.Should().Be(100);
        }

        [Fact]
        public void X_GetNotActive() {
            IChest chest = new OrionChest(0, null);

            chest.X.Should().Be(0);
        }

        [Fact]
        public void X_Set() {
            var terrariaChest = new TerrariaChest();
            for (var i = 0; i < terrariaChest.item.Length; ++i) {
                terrariaChest.item[i] = new TerrariaItem();
            }

            IChest chest = new OrionChest(0, terrariaChest);

            chest.X = 100;

            terrariaChest.x.Should().Be(100);
        }

        [Fact]
        public void X_SetNotActive() {
            IChest chest = new OrionChest(0, null);

            chest.X = 0;
        }

        [Fact]
        public void Y_GetIsCorrect() {
            var terrariaChest = new TerrariaChest {y = 100};
            for (var i = 0; i < terrariaChest.item.Length; ++i) {
                terrariaChest.item[i] = new TerrariaItem();
            }

            IChest chest = new OrionChest(0, terrariaChest);

            chest.Y.Should().Be(100);
        }

        [Fact]
        public void Y_GetNotActive() {
            IChest chest = new OrionChest(0, null);

            chest.Y.Should().Be(0);
        }

        [Fact]
        public void Y_Set() {
            var terrariaChest = new TerrariaChest();
            for (var i = 0; i < terrariaChest.item.Length; ++i) {
                terrariaChest.item[i] = new TerrariaItem();
            }

            IChest chest = new OrionChest(0, terrariaChest);

            chest.Y = 100;

            terrariaChest.y.Should().Be(100);
        }

        [Fact]
        public void Y_SetNotActive() {
            IChest chest = new OrionChest(0, null);

            chest.Y = 0;
        }

        [Fact]
        public void Name_Get() {
            var terrariaChest = new TerrariaChest {name = "test"};
            for (var i = 0; i < terrariaChest.item.Length; ++i) {
                terrariaChest.item[i] = new TerrariaItem();
            }

            IChest chest = new OrionChest(0, terrariaChest);

            chest.Name.Should().Be("test");
        }

        [Fact]
        public void Name_GetNotActive() {
            IChest chest = new OrionChest(0, null);

            chest.Name.Should().Be("");
        }

        [Fact]
        public void Name_Set() {
            var terrariaChest = new TerrariaChest();
            for (var i = 0; i < terrariaChest.item.Length; ++i) {
                terrariaChest.item[i] = new TerrariaItem();
            }

            IChest chest = new OrionChest(0, terrariaChest);

            chest.Name = "test";

            terrariaChest.name.Should().Be("test");
        }

        [Fact]
        public void Name_SetNotActive() {
            IChest chest = new OrionChest(0, null);

            chest.Name = "";
        }

        [Fact]
        public void Name_SetNullValue_ThrowsArgumentNullException() {
            var terrariaChest = new TerrariaChest();
            for (var i = 0; i < terrariaChest.item.Length; ++i) {
                terrariaChest.item[i] = new TerrariaItem();
            }

            IChest chest = new OrionChest(0, terrariaChest);
            Action action = () => chest.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Items_Item_Get() {
            var terrariaChest = new TerrariaChest();
            for (var i = 0; i < terrariaChest.item.Length; ++i) {
                terrariaChest.item[i] = new TerrariaItem();
            }

            terrariaChest.item[0] = new TerrariaItem {
                type = (int)ItemType.Sdmg,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };

            IChest chest = new OrionChest(0, terrariaChest);

            chest.Items[0].Type.Should().Be(ItemType.Sdmg);
            chest.Items[0].StackSize.Should().Be(1);
            chest.Items[0].Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void Items_GetNotActive() {
            IChest chest = new OrionChest(0, null);

            var items = chest.Items;

            items.Count.Should().Be(0);
            items.Should().BeEmpty();
            Func<IItem> func = () => items[0];
            func.Should().Throw<IndexOutOfRangeException>();
        }
    }
}
