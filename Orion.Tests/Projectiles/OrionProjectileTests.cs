using System;
using FluentAssertions;
using Orion.Projectiles;
using Xunit;

namespace Orion.Tests.Projectiles {
    public class OrionProjectileTests {
        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);
            Action action = () => projectile.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
