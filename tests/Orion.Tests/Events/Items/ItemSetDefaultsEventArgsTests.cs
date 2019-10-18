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
using FluentAssertions;
using Moq;
using Orion.Items;
using Xunit;

namespace Orion.Events.Items {
    public class ItemSetDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NotDirty() {
            var item = new Mock<IItem>().Object;
            var args = new ItemSetDefaultsEventArgs(item, ItemType.None);

            args.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<ItemSetDefaultsEventArgs> func = () => new ItemSetDefaultsEventArgs(null, ItemType.None);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var item = new Mock<IItem>().Object;
            var args = new ItemSetDefaultsEventArgs(item, ItemType.Sdmg);

            args.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        public void ItemType_Get() {
            var item = new Mock<IItem>().Object;
            var args = new ItemSetDefaultsEventArgs(item, ItemType.Sdmg);

            args.ItemType.Should().Be(ItemType.Sdmg);
        }
    }
}
