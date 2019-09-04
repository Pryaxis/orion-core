using System;
using FluentAssertions;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class RemovedProjectileEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<RemovedProjectileEventArgs> func = () => new RemovedProjectileEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
