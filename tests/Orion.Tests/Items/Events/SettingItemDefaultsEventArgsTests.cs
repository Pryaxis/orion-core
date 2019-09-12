// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using Orion.Items.Events;
using Xunit;

namespace Orion.Tests.Items.Events {
    public class SettingItemDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<SettingItemDefaultsEventArgs> func = () => new SettingItemDefaultsEventArgs(null, ItemType.None);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var item = new Mock<IItem>().Object;
            var args = new SettingItemDefaultsEventArgs(item, ItemType.None);

            args.Item.Should().BeSameAs(item);
        }
    }
}
