using System;
using FluentAssertions;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class GreetingPlayerEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<GreetingPlayerEventArgs> func = () => new GreetingPlayerEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
