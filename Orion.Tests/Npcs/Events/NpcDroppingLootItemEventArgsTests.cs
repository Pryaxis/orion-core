using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcDroppingLootItemEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcDroppingLootItemEventArgs> func = () => new NpcDroppingLootItemEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
