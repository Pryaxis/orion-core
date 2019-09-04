using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class DamagedNpcEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<DamagedNpcEventArgs> func = () => new DamagedNpcEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
