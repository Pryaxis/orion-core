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
using Xunit;
using TerrariaItem = Terraria.Item;

namespace Orion.Items {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionItemTests {
        [Fact]
        public void Name_Get() {
            var terrariaItem = new TerrariaItem { _nameOverride = "test" };
            var item = new OrionItem(terrariaItem);

            item.Name.Should().Be("test");
        }

        [Fact]
        public void Name_Set() {
            var terrariaItem = new TerrariaItem();
            var item = new OrionItem(terrariaItem);

            item.Name = "test";

            terrariaItem.Name.Should().Be("test");
        }

        [Fact]
        public void Name_Set_NullValue_ThrowsArgumentNullException() {
            var terrariaItem = new TerrariaItem();
            var item = new OrionItem(terrariaItem);
            Action action = () => item.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Type_Get() {
            var terrariaItem = new TerrariaItem { type = (int)ItemType.Sdmg };
            var item = new OrionItem(terrariaItem);

            item.Type.Should().Be(ItemType.Sdmg);
        }

        [Fact]
        public void StackSize_Get() {
            var terrariaItem = new TerrariaItem { stack = 100 };
            var item = new OrionItem(terrariaItem);

            item.StackSize.Should().Be(100);
        }

        [Fact]
        public void StackSize_Set() {
            var terrariaItem = new TerrariaItem();
            var item = new OrionItem(terrariaItem);

            item.StackSize = 100;

            terrariaItem.stack.Should().Be(100);
        }

        [Fact]
        public void Prefix_Get() {
            var terrariaItem = new TerrariaItem { prefix = (byte)ItemPrefix.Unreal };
            var item = new OrionItem(terrariaItem);

            item.Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void Stats_Get() {
            var terrariaItem = new TerrariaItem { prefix = (byte)ItemPrefix.Unreal };
            var item = new OrionItem(terrariaItem);

            item.Stats.Should().NotBeNull();
        }

        [Fact]
        public void Type_Set() {
            var terrariaItem = new TerrariaItem();
            var item = new OrionItem(terrariaItem);

            item.SetType(ItemType.Sdmg);

            terrariaItem.type.Should().Be((int)ItemType.Sdmg);
        }

        [Fact]
        public void Prefix_Set() {
            var terrariaItem = new TerrariaItem();
            terrariaItem.SetDefaults((int)ItemType.Sdmg);
            var item = new OrionItem(terrariaItem);

            item.SetPrefix(ItemPrefix.Unreal);

            terrariaItem.prefix.Should().Be((byte)ItemPrefix.Unreal);
        }
    }
}
