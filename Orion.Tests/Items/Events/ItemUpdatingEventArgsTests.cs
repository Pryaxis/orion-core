using System;
using FluentAssertions;
using Orion.Items.Events;
using Xunit;

namespace Orion.Tests.Items.Events {
    public class ItemUpdatingEventArgsTests {
        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<ItemUpdatingEventArgs> func = () => new ItemUpdatingEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
