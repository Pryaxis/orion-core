using System;
using FluentAssertions;
using Orion.Items.Events;
using Xunit;

namespace Orion.Tests.Items.Events {
    public class UpdatingItemEventArgsTests {
        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<UpdatingItemEventArgs> func = () => new UpdatingItemEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
