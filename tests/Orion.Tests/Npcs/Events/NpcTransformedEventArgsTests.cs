using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcTransformedEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcTransformedEventArgs> func = () => new NpcTransformedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
