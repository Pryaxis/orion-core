using System;
using FluentAssertions;
using Orion.Items.Events;
using Xunit;

namespace Orion.Tests.Items.Events {
    public class SetItemDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<SetItemDefaultsEventArgs> func = () => new SetItemDefaultsEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
