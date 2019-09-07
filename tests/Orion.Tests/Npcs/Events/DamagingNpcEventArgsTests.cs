using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class DamagingNpcEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<DamagingNpcEventArgs> func = () => new DamagingNpcEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
