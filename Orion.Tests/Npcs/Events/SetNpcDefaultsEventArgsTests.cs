using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class SetNpcDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<SetNpcDefaultsEventArgs> func = () => new SetNpcDefaultsEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
