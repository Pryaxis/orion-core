using System;
using FluentAssertions;
using Orion.Players.Events;
using Xunit;

namespace Orion.Tests.Players.Events {
    public class UpdatedPlayerEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            Func<UpdatedPlayerEventArgs> func = () => new UpdatedPlayerEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
