using System;
using FluentAssertions;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class PlayerUpdatedEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<PlayerUpdatedEventArgs> func = () => new PlayerUpdatedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
