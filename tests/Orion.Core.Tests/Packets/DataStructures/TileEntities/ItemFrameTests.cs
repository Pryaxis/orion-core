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

using System.Diagnostics.CodeAnalysis;
using Orion.Core.Items;
using Orion.Core.World.TileEntities;
using Xunit;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class ItemFrameTests
    {
        private readonly byte[] _bytes = { 1, 10, 0, 0, 0, 0, 1, 100, 0, 17, 6, 82, 1, 0 };

        [Fact]
        public void Id_Get()
        {
            var itemFrame = new ItemFrame();

            Assert.Equal(TileEntityId.ItemFrame, itemFrame.Id);
        }

        [Fact]
        public void Item_Set_Get()
        {
            var itemFrame = new ItemFrame();

            itemFrame.Item = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);

            Assert.Equal(new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1), itemFrame.Item);
        }

        [Fact]
        public void Read()
        {
            var itemFrame = TestUtils.ReadTileEntity<ItemFrame>(_bytes, true);

            Assert.Equal(10, itemFrame.Index);
            Assert.Equal(256, itemFrame.X);
            Assert.Equal(100, itemFrame.Y);

            Assert.Equal(new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1), itemFrame.Item);
        }
    }
}
