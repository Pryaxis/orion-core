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

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Orion.Items;
using Xunit;
using TerrariaItem = Terraria.Item;
using TerrariaItemFrame = Terraria.GameContent.Tile_Entities.TEItemFrame;

namespace Orion.World.TileEntities {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionItemFrameTests {
        [Fact]
        public void ItemType_Get() {
            var terrariaItemFrame = new TerrariaItemFrame {
                item = new TerrariaItem { type = (int)ItemType.Sdmg }
            };
            IItemFrame itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.ItemType.Should().Be(ItemType.Sdmg);
        }

        [Fact]
        public void ItemType_Set() {
            var terrariaItemFrame = new TerrariaItemFrame { item = new TerrariaItem() };
            IItemFrame itemFrame = new OrionItemFrame(terrariaItemFrame);
            itemFrame.ItemType = ItemType.Sdmg;

            terrariaItemFrame.item.type.Should().Be((int)ItemType.Sdmg);
        }

        [Fact]
        public void ItemStackSize_Get() {
            var terrariaItemFrame = new TerrariaItemFrame {
                item = new TerrariaItem { stack = 1 }
            };
            IItemFrame itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.ItemStackSize.Should().Be(1);
        }

        [Fact]
        public void ItemStackSize_Set() {
            var terrariaItemFrame = new TerrariaItemFrame { item = new TerrariaItem() };
            IItemFrame itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.ItemStackSize = 1;

            terrariaItemFrame.item.stack.Should().Be(1);
        }

        [Fact]
        public void ItemPrefix_Get() {
            var terrariaItemFrame = new TerrariaItemFrame {
                item = new TerrariaItem { prefix = (byte)ItemPrefix.Unreal }
            };
            IItemFrame itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.ItemPrefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void ItemPrefix_Set() {
            var terrariaItemFrame = new TerrariaItemFrame { item = new TerrariaItem() };
            IItemFrame itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.ItemPrefix = ItemPrefix.Unreal;

            terrariaItemFrame.item.prefix.Should().Be((byte)ItemPrefix.Unreal);
        }
    }
}
