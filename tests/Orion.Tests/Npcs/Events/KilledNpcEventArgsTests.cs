using System;
using FluentAssertions;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class KilledNpcEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<KilledNpcEventArgs> func = () => new KilledNpcEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
