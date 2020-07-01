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
using Moq;
using Xunit;

namespace Orion.Core.Items
{
    public class IItemTests
    {
        [Fact]
        public void AsItemStack_NullItem_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => IItemExtensions.AsItemStack(null!));
        }

        [Fact]
        public void AsItemStack()
        {
            var item = Mock.Of<IItem>(i => i.Id == ItemId.Sdmg && i.Prefix == ItemPrefix.Unreal && i.StackSize == 1);

            Assert.Equal(new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1), item.AsItemStack());
        }
    }
}
