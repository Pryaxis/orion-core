using System;
using FluentAssertions;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class PlayerJoiningEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<PlayerJoiningEventArgs> func = () => new PlayerJoiningEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
