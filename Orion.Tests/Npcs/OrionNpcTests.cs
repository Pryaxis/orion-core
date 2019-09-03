using System;
using FluentAssertions;
using Orion.Npcs;
using Xunit;

namespace Orion.Tests.Npcs {
    public class OrionNpcTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<INpc> func = () => new OrionNpc(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
