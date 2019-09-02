using System;
using FluentAssertions;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Players {
    public class OrionPlayerTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<IPlayer> func = () => new OrionPlayer(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
