using System;
using FluentAssertions;
using Moq;
using Orion.Projectiles;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class RemovedProjectileEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<RemovedProjectileEventArgs> func = () => new RemovedProjectileEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetProjectile_IsCorrect() {
            var projectile = new Mock<IProjectile>().Object;
            var args = new RemovedProjectileEventArgs(projectile);

            args.Projectile.Should().BeSameAs(projectile);
        }
    }
}
