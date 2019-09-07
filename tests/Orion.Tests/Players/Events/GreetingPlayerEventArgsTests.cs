using System;
using FluentAssertions;
using Moq;
using Orion.Players;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class GreetingPlayerEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<GreetingPlayerEventArgs> func = () => new GreetingPlayerEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void GetPlayer_IsCorrect() {
            var player = new Mock<IPlayer>().Object;
            var args = new GreetingPlayerEventArgs(player);

            args.Player.Should().BeSameAs(player);
        }
    }
}
