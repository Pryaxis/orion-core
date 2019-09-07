using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class UpdatedNpcEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<UpdatedNpcEventArgs> func = () => new UpdatedNpcEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
