using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcKilledEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcKilledEventArgs> func = () => new NpcKilledEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
