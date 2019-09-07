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
