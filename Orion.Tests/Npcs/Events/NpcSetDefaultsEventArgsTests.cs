using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcSetDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcSetDefaultsEventArgs> func = () => new NpcSetDefaultsEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
