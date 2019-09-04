using System;
using FluentAssertions;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class UpdatingProjectileEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<UpdatingProjectileEventArgs> func = () => new UpdatingProjectileEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
