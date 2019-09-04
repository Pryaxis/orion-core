using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class UpdatingNpcEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<UpdatingNpcEventArgs> func = () => new UpdatingNpcEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
