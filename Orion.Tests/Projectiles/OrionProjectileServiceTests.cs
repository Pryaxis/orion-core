using System;
using System.Linq;
using FluentAssertions;
using Orion.Projectiles;
using Xunit;

namespace Orion.Tests.Projectiles {
    public class OrionProjectileServiceTests : IDisposable {
        private readonly IProjectileService _projectileService;

        public OrionProjectileServiceTests() {
            for (var i = 0; i < Terraria.Main.maxProjectiles; ++i) {
                Terraria.Main.projectile[i] = new Terraria.Projectile {whoAmI = i};
            }
            
            _projectileService = new OrionProjectileService();
        }

        public void Dispose() {
            _projectileService.Dispose();
        }

        [Fact]
        public void GetCount_IsCorrect() {
            _projectileService.Count.Should().Be(Terraria.Main.maxProjectiles);
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var projectile = _projectileService[0];

            projectile.WrappedProjectile.Should().BeSameAs(Terraria.Main.projectile[0]);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            var projectile = _projectileService[0];
            var projectile2 = _projectileService[0];

            projectile.Should().BeSameAs(projectile2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void GetItem_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<IProjectile> func = () => _projectileService[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var projectiles = _projectileService.ToList();

            for (var i = 0; i < projectiles.Count; ++i) {
                projectiles[i].WrappedProjectile.Should().BeSameAs(Terraria.Main.projectile[i]);
            }
        }
    }
}
