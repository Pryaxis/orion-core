using System;
using FluentAssertions;
using Orion.Items.Events;
using Xunit;

namespace Orion.Tests.Items.Events {
    public class UpdatedItemEventArgsTests {
        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<UpdatedItemEventArgs> func = () => new UpdatedItemEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
