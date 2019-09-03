using System;
using System.Linq;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Projectiles;
using Xunit;

namespace Orion.Tests.Projectiles {
    public class OrionProjectileServiceTests : IDisposable {
        private readonly IProjectileService _projectileService;

        public OrionProjectileServiceTests() {
            for (var i = 0; i < Terraria.Main.maxProjectiles + 1; ++i) {
                Terraria.Main.projectile[i] = new Terraria.Projectile {whoAmI = i};
            }
            for (var i = 0; i < Terraria.Main.maxDust + 1; ++i) {
                Terraria.Main.dust[i] = new Terraria.Dust {dustIndex = i};
            }
            
            _projectileService = new OrionProjectileService();
        }

        public void Dispose() {
            _projectileService.Dispose();
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
        public void ProjectileSettingDefaults_IsCorrect() {
            IProjectile argsProjectile = null;
            _projectileService.ProjectileSettingDefaults += (sender, args) => {
                argsProjectile = args.Projectile;
            };

            var projectile = _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0);

            argsProjectile.Should().NotBeNull();
            argsProjectile.WrappedProjectile.Should().BeSameAs(projectile.WrappedProjectile);
        }

        [Theory]
        [InlineData(ProjectileType.CrystalBullet, ProjectileType.VenomBullet)]
        [InlineData(ProjectileType.CrystalBullet, ProjectileType.None)]
        public void ProjectileSettingDefaults_ModifyType_IsCorrect(ProjectileType oldType, ProjectileType newType) {
            _projectileService.ProjectileSettingDefaults += (sender, args) => {
                args.Type = newType;
            };

            var projectile = _projectileService.SpawnProjectile(oldType, Vector2.Zero, Vector2.Zero, 0, 0);

            projectile.Type.Should().Be(newType);
        }

        [Fact]
        public void ProjectileSettingDefaults_Handled_IsCorrect() {
            _projectileService.ProjectileSettingDefaults += (sender, args) => {
                args.Handled = true;
            };

            var projectile = _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0);

            projectile.Type.Should().Be(ProjectileType.None);
        }

        [Fact]
        public void ProjectileSetDefaults_IsCorrect() {
            IProjectile argsProjectile = null;
            _projectileService.ProjectileSetDefaults += (sender, args) => {
                argsProjectile = args.Projectile;
            };

            var projectile = _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0);

            argsProjectile.Should().NotBeNull();
            argsProjectile.WrappedProjectile.Should().BeSameAs(projectile.WrappedProjectile);
        }

        [Fact]
        public void ProjectileUpdating_IsCorrect() {
            IProjectile argsProjectile = null;
            _projectileService.ProjectileUpdating += (sender, args) => {
                argsProjectile = args.Projectile;
            };
            var projectile = _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0);

            projectile.WrappedProjectile.Update(projectile.Index);

            argsProjectile.Should().NotBeNull();
            argsProjectile.WrappedProjectile.Should().BeSameAs(projectile.WrappedProjectile);
        }

        [Fact]
        public void ProjectileUpdatingAi_IsCorrect() {
            IProjectile argsProjectile = null;
            _projectileService.ProjectileUpdatingAi += (sender, args) => {
                argsProjectile = args.Projectile;
            };
            var projectile = _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0);

            projectile.WrappedProjectile.AI();

            argsProjectile.Should().NotBeNull();
            argsProjectile.WrappedProjectile.Should().BeSameAs(projectile.WrappedProjectile);
        }

        [Fact]
        public void ProjectileUpdatedAi_IsCorrect() {
            IProjectile argsProjectile = null;
            _projectileService.ProjectileUpdatedAi += (sender, args) => {
                argsProjectile = args.Projectile;
            };
            var projectile = _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0);

            projectile.WrappedProjectile.AI();

            argsProjectile.Should().NotBeNull();
            argsProjectile.WrappedProjectile.Should().BeSameAs(projectile.WrappedProjectile);
        }

        [Fact]
        public void ProjectileUpdated_IsCorrect() {
            IProjectile argsProjectile = null;
            _projectileService.ProjectileUpdated += (sender, args) => {
                argsProjectile = args.Projectile;
            };
            var projectile = _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0);
            
            projectile.WrappedProjectile.Update(projectile.Index);

            argsProjectile.Should().NotBeNull();
            argsProjectile.WrappedProjectile.Should().BeSameAs(projectile.WrappedProjectile);
        }

        [Fact]
        public void ProjectileRemoving_IsCorrect() {
            IProjectile argsProjectile = null;
            _projectileService.ProjectileRemoving += (sender, args) => {
                argsProjectile = args.Projectile;
            };
            var projectile = _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0);

            projectile.Remove();

            argsProjectile.Should().NotBeNull();
            argsProjectile.WrappedProjectile.Should().BeSameAs(projectile.WrappedProjectile);
        }

        [Fact]
        public void ProjectileRemoving_Handled_IsCorrect() {
            _projectileService.ProjectileRemoving += (sender, args) => {
                args.Handled = true;
            };
            var projectile = _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0);

            projectile.Remove();

            projectile.Type.Should().Be(ProjectileType.Ale);
        }

        [Fact]
        public void ProjectileRemoved_IsCorrect() {
            IProjectile argsProjectile = null;
            _projectileService.ProjectileRemoved += (sender, args) => {
                argsProjectile = args.Projectile;
            };
            var projectile = _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0);

            projectile.Remove();

            argsProjectile.Should().NotBeNull();
            argsProjectile.WrappedProjectile.Should().BeSameAs(projectile.WrappedProjectile);
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var projectiles = _projectileService.ToList();

            for (var i = 0; i < projectiles.Count; ++i) {
                projectiles[i].WrappedProjectile.Should().BeSameAs(Terraria.Main.projectile[i]);
            }
        }
        
        [Theory]
        [InlineData(ProjectileType.CrystalBullet, 100, 0.5)]
        [InlineData(ProjectileType.RainbowRodBullet, 0, 0.5)]
        public void SpawnProjectile_IsCorrect(ProjectileType type, int damage, float knockback) {
            var projectile = _projectileService.SpawnProjectile(type, Vector2.Zero, Vector2.Zero, damage, knockback);

            projectile.Type.Should().Be(type);
            projectile.Damage.Should().Be(damage);
            projectile.Knockback.Should().Be(knockback);
        }

        [Fact]
        public void SpawnProjectile_InvalidAiValues_ThrowsArgumentException() {
            Func<IProjectile> action = () =>
                _projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0, 0, new float[4]);

            action.Should().Throw<ArgumentException>();
        }
    }
}
