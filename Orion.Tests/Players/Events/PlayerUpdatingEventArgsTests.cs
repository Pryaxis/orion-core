using System;
using FluentAssertions;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class PlayerUpdatingEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<PlayerUpdatingEventArgs> func = () => new PlayerUpdatingEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
