using System;
using FluentAssertions;
using Orion.Projectiles;
using Xunit;

namespace Orion.Tests.Projectiles {
    public class OrionProjectileTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<IProjectile> func = () => new OrionProjectile(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
