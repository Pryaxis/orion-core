using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcUpdatedEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcUpdatedEventArgs> func = () => new NpcUpdatedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
