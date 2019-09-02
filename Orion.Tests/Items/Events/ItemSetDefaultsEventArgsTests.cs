using System;
using FluentAssertions;
using Orion.Items.Events;
using Xunit;

namespace Orion.Tests.Items.Events {
    public class ItemSetDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<ItemSetDefaultsEventArgs> func = () => new ItemSetDefaultsEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
