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
using Xunit;

namespace Orion.Items {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionItemTests {
        [Fact]
        public void Name_Get() {
            var terrariaItem = new Terraria.Item { _nameOverride = "test" };
            var item = new OrionItem(terrariaItem);

            Assert.Equal("test", item.Name);
        }

        [Fact]
        public void Name_SetNullValue_ThrowsArgumentNullException() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            Assert.Throws<ArgumentNullException>(() => item.Name = null!);
        }

        [Fact]
        public void Name_Set() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.Name = "test";

            Assert.Equal("test", terrariaItem.Name);
        }

        [Fact]
        public void Type_Get() {
            var terrariaItem = new Terraria.Item { type = (int)ItemId.Sdmg };
            var item = new OrionItem(terrariaItem);

            Assert.Equal(ItemId.Sdmg, item.Id);
        }

        [Fact]
        public void SetId() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);

            item.SetId(ItemId.Sdmg);

            Assert.Equal(ItemId.Sdmg, (ItemId)terrariaItem.type);
        }
    }
}
