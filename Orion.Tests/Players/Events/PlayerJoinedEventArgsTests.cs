using System;
using FluentAssertions;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class PlayerJoinedEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<PlayerJoinedEventArgs> func = () => new PlayerJoinedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
