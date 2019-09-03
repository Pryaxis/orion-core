using System;
using FluentAssertions;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class PlayerGreetingEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<PlayerGreetingEventArgs> func = () => new PlayerGreetingEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
