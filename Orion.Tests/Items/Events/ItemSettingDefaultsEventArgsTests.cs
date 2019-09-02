using System;
using FluentAssertions;
using Orion.Items;
using Orion.Items.Events;
using Xunit;

namespace Orion.Tests.Items.Events {
    public class ItemSettingDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<ItemSettingDefaultsEventArgs> func = () => new ItemSettingDefaultsEventArgs(null, ItemType.None);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
