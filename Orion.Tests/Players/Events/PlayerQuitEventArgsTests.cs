using System;
using FluentAssertions;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class PlayerQuitEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<PlayerQuitEventArgs> func = () => new PlayerQuitEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
