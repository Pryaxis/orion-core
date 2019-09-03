using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcDamagedEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcDamagedEventArgs> func = () => new NpcDamagedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
