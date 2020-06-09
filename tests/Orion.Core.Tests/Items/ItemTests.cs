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

namespace Orion.Core.Items {
    public class ItemTests {
        [Fact]
        public void AsItemStack_NullItem_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => ItemExtensions.AsItemStack(null!));
        }

        [Fact]
        public void AsItemStack() {
            var mockItem = new Mock<IItem>();
            mockItem.Setup(i => i.Id).Returns(ItemId.Sdmg);
            mockItem.Setup(i => i.StackSize).Returns(1);
            mockItem.Setup(i => i.Prefix).Returns(ItemPrefix.Unreal);

            Assert.Equal(new ItemStack(ItemId.Sdmg, 1, ItemPrefix.Unreal), mockItem.Object.AsItemStack());
        }
    }
}
