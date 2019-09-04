using System;
using FluentAssertions;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class UpdatingPlayerEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<UpdatingPlayerEventArgs> func = () => new UpdatingPlayerEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
