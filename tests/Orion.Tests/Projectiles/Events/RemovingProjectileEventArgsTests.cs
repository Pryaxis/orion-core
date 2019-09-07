using System;
using FluentAssertions;
using Moq;
using Orion.Projectiles;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class RemovingProjectileEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<RemovingProjectileEventArgs> func = () => new RemovingProjectileEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetProjectile_IsCorrect() {
            var projectile = new Mock<IProjectile>().Object;
            var args = new RemovingProjectileEventArgs(projectile);

            args.Projectile.Should().BeSameAs(projectile);
        }
    }
}
