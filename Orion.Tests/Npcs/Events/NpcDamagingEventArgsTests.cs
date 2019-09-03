using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcDamagingEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcDamagingEventArgs> func = () => new NpcDamagingEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
