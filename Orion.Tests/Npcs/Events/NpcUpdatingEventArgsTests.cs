using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcUpdatingEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcUpdatingEventArgs> func = () => new NpcUpdatingEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
