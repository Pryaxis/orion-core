using System;
using FluentAssertions;
using Orion.Items.Events;
using Xunit;

namespace Orion.Tests.Items.Events {
    public class ItemUpdatedEventArgsTests {
        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<ItemUpdatedEventArgs> func = () => new ItemUpdatedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
