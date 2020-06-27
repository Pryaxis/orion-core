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
    public class HatRackTests
    {
        private readonly byte[] _bytes =
        {
            5, 10, 0, 0, 0, 0, 1, 100, 0, 15, 197, 10, 0, 1, 0, 197, 10, 0, 1, 0, 239, 3, 0, 1, 0, 239, 3, 0, 1, 0
        };

        private readonly byte[] _emptyBytes = { 5, 10, 0, 0, 0, 0, 1, 100, 0, 0 };

        [Fact]
        public void Id_Get()
        {
            var hatRack = new HatRack();

            Assert.Equal(TileEntityId.HatRack, hatRack.Id);
        }

        [Fact]
        public void Items_Set_Get()
        {
            var hatRack = new HatRack();

            hatRack.Items[0] = new ItemStack(ItemId.VortexHelmet, stackSize: 1);
            hatRack.Items[1] = new ItemStack(ItemId.VortexHelmet, stackSize: 1);

            Assert.Equal(new ItemStack(ItemId.VortexHelmet, stackSize: 1), hatRack.Items[0]);
            Assert.Equal(new ItemStack(ItemId.VortexHelmet, stackSize: 1), hatRack.Items[1]);
        }

        [Fact]
        public void Dyes_Set_Get()
        {
            var hatRack = new HatRack();

            hatRack.Dyes[0] = new ItemStack(ItemId.RedDye, stackSize: 1);
            hatRack.Dyes[1] = new ItemStack(ItemId.RedDye, stackSize: 1);

            Assert.Equal(new ItemStack(ItemId.RedDye, stackSize: 1), hatRack.Dyes[0]);
            Assert.Equal(new ItemStack(ItemId.RedDye, stackSize: 1), hatRack.Dyes[1]);
        }

        [Fact]
        public void Read()
        {
            var hatRack = TestUtils.ReadTileEntity<HatRack>(_bytes, true);

            Assert.Equal(10, hatRack.Index);
            Assert.Equal(256, hatRack.X);
            Assert.Equal(100, hatRack.Y);

            Assert.Equal(new ItemStack(ItemId.VortexHelmet, stackSize: 1), hatRack.Items[0]);
            Assert.Equal(new ItemStack(ItemId.VortexHelmet, stackSize: 1), hatRack.Items[1]);
            Assert.Equal(new ItemStack(ItemId.RedDye, stackSize: 1), hatRack.Dyes[0]);
            Assert.Equal(new ItemStack(ItemId.RedDye, stackSize: 1), hatRack.Dyes[1]);
        }

        [Fact]
        public void Read_Empty()
        {
            var hatRack = TestUtils.ReadTileEntity<HatRack>(_emptyBytes, true);

            Assert.Equal(10, hatRack.Index);
            Assert.Equal(256, hatRack.X);
            Assert.Equal(100, hatRack.Y);

            Assert.Equal(default, hatRack.Items[0]);
            Assert.Equal(default, hatRack.Items[1]);
            Assert.Equal(default, hatRack.Dyes[0]);
            Assert.Equal(default, hatRack.Dyes[1]);
        }
    }
}
