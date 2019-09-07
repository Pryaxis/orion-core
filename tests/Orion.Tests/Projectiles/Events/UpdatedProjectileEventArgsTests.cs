using System;
using FluentAssertions;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class UpdatedProjectileEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<UpdatedProjectileEventArgs> func = () => new UpdatedProjectileEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
