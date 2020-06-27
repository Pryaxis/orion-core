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

using Orion.Core.Items;
using Orion.Core.World.TileEntities;
using Xunit;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    public class MannequinTests
    {
        private readonly byte[] _bytes =
        {
            3, 10, 0, 0, 0, 0, 1, 100, 0, 7, 7, 197, 10, 0, 1, 0, 198, 10, 0, 1, 0, 199, 10, 0, 1, 0, 239, 3, 0, 1, 0,
            239, 3, 0, 1, 0, 239, 3, 0, 1, 0
        };

        private readonly byte[] _emptyBytes = { 3, 10, 0, 0, 0, 0, 1, 100, 0, 0, 0 };

        [Fact]
        public void Id_Get()
        {
            var mannequin = new Mannequin();

            Assert.Equal(TileEntityId.Mannequin, mannequin.Id);
        }

        [Fact]
        public void Items_Set_Get()
        {
            var mannequin = new Mannequin();

            mannequin.Items[0] = new ItemStack(ItemId.VortexHelmet, stackSize: 1);
            mannequin.Items[1] = new ItemStack(ItemId.VortexBreastplate, stackSize: 1);
            mannequin.Items[2] = new ItemStack(ItemId.VortexLeggings, stackSize: 1);

            Assert.Equal(new ItemStack(ItemId.VortexHelmet, stackSize: 1), mannequin.Items[0]);
            Assert.Equal(new ItemStack(ItemId.VortexBreastplate, stackSize: 1), mannequin.Items[1]);
            Assert.Equal(new ItemStack(ItemId.VortexLeggings, stackSize: 1), mannequin.Items[2]);
            for (var i = 3; i < 8; ++i)
            {
                Assert.Equal(default, mannequin.Items[i]);
            }
        }

        [Fact]
        public void Dyes_Set_Get()
        {
            var mannequin = new Mannequin();

            mannequin.Dyes[0] = new ItemStack(ItemId.RedDye, stackSize: 1);
            mannequin.Dyes[1] = new ItemStack(ItemId.RedDye, stackSize: 1);
            mannequin.Dyes[2] = new ItemStack(ItemId.RedDye, stackSize: 1);

            Assert.Equal(new ItemStack(ItemId.RedDye, stackSize: 1), mannequin.Dyes[0]);
            Assert.Equal(new ItemStack(ItemId.RedDye, stackSize: 1), mannequin.Dyes[1]);
            Assert.Equal(new ItemStack(ItemId.RedDye, stackSize: 1), mannequin.Dyes[2]);
            for (var i = 3; i < 8; ++i)
            {
                Assert.Equal(default, mannequin.Dyes[i]);
            }
        }

        [Fact]
        public void Read()
        {
            var mannequin = TestUtils.ReadTileEntity<Mannequin>(_bytes, true);

            Assert.Equal(10, mannequin.Index);
            Assert.Equal(256, mannequin.X);
            Assert.Equal(100, mannequin.Y);

            Assert.Equal(new ItemStack(ItemId.VortexHelmet, stackSize: 1), mannequin.Items[0]);
            Assert.Equal(new ItemStack(ItemId.VortexBreastplate, stackSize: 1), mannequin.Items[1]);
            Assert.Equal(new ItemStack(ItemId.VortexLeggings, stackSize: 1), mannequin.Items[2]);
            for (var i = 3; i < 8; ++i)
            {
                Assert.Equal(default, mannequin.Items[i]);
            }

            Assert.Equal(new ItemStack(ItemId.RedDye, stackSize: 1), mannequin.Dyes[0]);
            Assert.Equal(new ItemStack(ItemId.RedDye, stackSize: 1), mannequin.Dyes[1]);
            Assert.Equal(new ItemStack(ItemId.RedDye, stackSize: 1), mannequin.Dyes[2]);
            for (var i = 3; i < 8; ++i)
            {
                Assert.Equal(default, mannequin.Dyes[i]);
            }
        }

        [Fact]
        public void Read_Empty()
        {
            var mannequin = TestUtils.ReadTileEntity<Mannequin>(_emptyBytes, true);

            Assert.Equal(10, mannequin.Index);
            Assert.Equal(256, mannequin.X);
            Assert.Equal(100, mannequin.Y);

            for (var i = 0; i < 8; ++i)
            {
                Assert.Equal(default, mannequin.Items[i]);
            }

            for (var i = 0; i < 8; ++i)
            {
                Assert.Equal(default, mannequin.Dyes[i]);
            }
        }
    }
}
