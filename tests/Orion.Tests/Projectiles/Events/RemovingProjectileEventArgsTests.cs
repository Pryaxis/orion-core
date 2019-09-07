using System;
using FluentAssertions;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class RemovingProjectileEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<RemovingProjectileEventArgs> func = () => new RemovingProjectileEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
