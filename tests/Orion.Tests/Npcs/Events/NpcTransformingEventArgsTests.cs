using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcTransformingEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcTransformingEventArgs> func = () => new NpcTransformingEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
