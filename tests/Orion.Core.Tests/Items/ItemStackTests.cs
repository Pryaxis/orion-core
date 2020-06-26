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
using Xunit;

namespace Orion.Core.Items
{
    public class ItemStackTests
    {
        [Fact]
        public void Id_Get()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);

            Assert.Equal(ItemId.Sdmg, itemStack.Id);
        }

        [Fact]
        public void StackSize_Get()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);

            Assert.Equal(1, itemStack.StackSize);
        }

        [Fact]
        public void Prefix_Get()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);

            Assert.Equal(ItemPrefix.Unreal, itemStack.Prefix);
        }

        [Fact]
        public void IsEmpty_ReturnsTrue()
        {
            var itemStack = new ItemStack(ItemId.None, ItemPrefix.None, 10);
            var itemStack2 = new ItemStack(ItemId.Sdmg, ItemPrefix.None, 0);

            Assert.True(itemStack.IsEmpty);
            Assert.True(itemStack2.IsEmpty);
        }

        [Fact]
        public void IsEmpty_ReturnsFalse()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);

            Assert.False(itemStack.IsEmpty);
        }

        [Fact]
        public void Equals_ReturnsTrue()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);
            var itemStack2 = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);

            Assert.True(itemStack.Equals((object)itemStack2));
            Assert.True(itemStack.Equals(itemStack2));
        }

        [Fact]
        public void Equals_ReturnsFalse()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);
            var itemStack2 = new ItemStack(ItemId.Sdmg, ItemPrefix.None, 1);
            var itemStack3 = new ItemStack(ItemId.Sdmg, ItemPrefix.None, 2);
            var itemStack4 = new ItemStack(ItemId.DirtBlock, ItemPrefix.None, 999);

            Assert.False(itemStack.Equals(1));
            Assert.False(itemStack.Equals((object)itemStack2));
            Assert.False(itemStack.Equals((object)itemStack3));
            Assert.False(itemStack.Equals((object)itemStack4));
            Assert.False(itemStack.Equals(itemStack2));
            Assert.False(itemStack.Equals(itemStack3));
            Assert.False(itemStack.Equals(itemStack4));
        }

        [Fact]
        public void GetHashCode_Equals_AreEqual()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);
            var itemStack2 = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);

            Assert.Equal(itemStack.GetHashCode(), itemStack2.GetHashCode());
        }

        [Fact]
        public void Deconstruct()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);

            var (id, prefix, stackSize) = itemStack;

            Assert.Equal(ItemId.Sdmg, id);
            Assert.Equal(ItemPrefix.Unreal, prefix);
            Assert.Equal(1, stackSize);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Equality_ReturnsTrue()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);
            var itemStack2 = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);

            Assert.True(itemStack == itemStack2);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Equality_ReturnsFalse()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);
            var itemStack2 = new ItemStack(ItemId.Sdmg, ItemPrefix.None, 1);
            var itemStack3 = new ItemStack(ItemId.Sdmg, ItemPrefix.None, 2);
            var itemStack4 = new ItemStack(ItemId.DirtBlock, ItemPrefix.None, 999);

            Assert.False(itemStack == itemStack2);
            Assert.False(itemStack == itemStack3);
            Assert.False(itemStack == itemStack4);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Inequality_ReturnsTrue()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);
            var itemStack2 = new ItemStack(ItemId.Sdmg, ItemPrefix.None, 1);
            var itemStack3 = new ItemStack(ItemId.Sdmg, ItemPrefix.None, 2);
            var itemStack4 = new ItemStack(ItemId.DirtBlock, ItemPrefix.None, 999);

            Assert.True(itemStack != itemStack2);
            Assert.True(itemStack != itemStack3);
            Assert.True(itemStack != itemStack4);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Inequality_ReturnsFalse()
        {
            var itemStack = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);
            var itemStack2 = new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1);

            Assert.False(itemStack != itemStack2);
        }
    }
}
