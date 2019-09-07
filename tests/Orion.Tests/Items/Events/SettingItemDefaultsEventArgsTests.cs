using System;
using FluentAssertions;
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
    }
}
