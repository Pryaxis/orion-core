// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Linq;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Terraria;
using Xunit;

namespace Orion.Projectiles {
    [Collection("TerrariaTestsCollection")]
    public class OrionProjectileServiceTests : IDisposable {
        private readonly IProjectileService _projectileService;

        public OrionProjectileServiceTests() {
            for (var i = 0; i < Main.maxProjectiles + 1; ++i) {
                Main.projectile[i] = new Projectile {whoAmI = i};
            }

            _projectileService = new OrionProjectileService();
        }

        public void Dispose() {
            _projectileService.Dispose();
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var projectile = (OrionProjectile)_projectileService[0];

            projectile.Wrapped.Should().BeSameAs(Main.projectile[0]);
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
            OrionProjectile argsProjectile = null;
            _projectileService.SettingProjectileDefaults += (sender, args) => {
                argsProjectile = (OrionProjectile)args.Projectile;
            };

            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0,
                                                                    0);

            argsProjectile.Should().NotBeNull();
            argsProjectile.Wrapped.Should().BeSameAs(projectile.Wrapped);
        }

        [Theory]
        [InlineData(ProjectileType.CrystalBullet, ProjectileType.VenomBullet)]
        [InlineData(ProjectileType.CrystalBullet, ProjectileType.None)]
        public void ProjectileSettingDefaults_ModifyType_IsCorrect(ProjectileType oldType, ProjectileType newType) {
            _projectileService.SettingProjectileDefaults += (sender, args) => {
                args.Type = newType;
            };

            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(oldType, Vector2.Zero, Vector2.Zero, 0, 0);

            projectile.Type.Should().Be(newType);
        }

        [Fact]
        public void ProjectileSettingDefaults_Handled_IsCorrect() {
            _projectileService.SettingProjectileDefaults += (sender, args) => {
                args.Handled = true;
            };

            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0,
                                                                    0);

            projectile.Type.Should().Be(ProjectileType.None);
        }

        [Fact]
        public void ProjectileSetDefaults_IsCorrect() {
            OrionProjectile argsProjectile = null;
            _projectileService.SetProjectileDefaults += (sender, args) => {
                argsProjectile = (OrionProjectile)args.Projectile;
            };

            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0,
                                                                    0);

            argsProjectile.Should().NotBeNull();
            argsProjectile.Wrapped.Should().BeSameAs(projectile.Wrapped);
        }

        [Fact]
        public void ProjectileUpdating_IsCorrect() {
            OrionProjectile argsProjectile = null;
            _projectileService.UpdatingProjectile += (sender, args) => {
                argsProjectile = (OrionProjectile)args.Projectile;
            };
            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0,
                                                                    0);

            projectile.Wrapped.Update(projectile.Index);

            argsProjectile.Should().NotBeNull();
            argsProjectile.Wrapped.Should().BeSameAs(projectile.Wrapped);
        }

        [Fact]
        public void ProjectileUpdatingAi_IsCorrect() {
            OrionProjectile argsProjectile = null;
            _projectileService.UpdatingProjectileAi += (sender, args) => {
                argsProjectile = (OrionProjectile)args.Projectile;
            };
            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0,
                                                                    0);

            projectile.Wrapped.AI();

            argsProjectile.Should().NotBeNull();
            argsProjectile.Wrapped.Should().BeSameAs(projectile.Wrapped);
        }

        [Fact]
        public void ProjectileUpdatedAi_IsCorrect() {
            OrionProjectile argsProjectile = null;
            _projectileService.UpdatedProjectileAi += (sender, args) => {
                argsProjectile = (OrionProjectile)args.Projectile;
            };
            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0,
                                                                    0);

            projectile.Wrapped.AI();

            argsProjectile.Should().NotBeNull();
            argsProjectile.Wrapped.Should().BeSameAs(projectile.Wrapped);
        }

        [Fact]
        public void ProjectileUpdated_IsCorrect() {
            OrionProjectile argsProjectile = null;
            _projectileService.UpdatedProjectile += (sender, args) => {
                argsProjectile = (OrionProjectile)args.Projectile;
            };
            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0,
                                                                    0);

            projectile.Wrapped.Update(projectile.Index);

            argsProjectile.Should().NotBeNull();
            argsProjectile.Wrapped.Should().BeSameAs(projectile.Wrapped);
        }

        [Fact]
        public void ProjectileRemoving_IsCorrect() {
            OrionProjectile argsProjectile = null;
            _projectileService.RemovingProjectile += (sender, args) => {
                argsProjectile = (OrionProjectile)args.Projectile;
            };
            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0,
                                                                    0);

            projectile.Remove();

            argsProjectile.Should().NotBeNull();
            argsProjectile.Wrapped.Should().BeSameAs(projectile.Wrapped);
        }

        [Fact]
        public void ProjectileRemoving_Handled_IsCorrect() {
            _projectileService.RemovingProjectile += (sender, args) => {
                args.Handled = true;
            };
            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0,
                                                                    0);

            projectile.Remove();

            projectile.Type.Should().Be(ProjectileType.Ale);
        }

        [Fact]
        public void ProjectileRemoved_IsCorrect() {
            OrionProjectile argsProjectile = null;
            _projectileService.RemovedProjectile += (sender, args) => {
                argsProjectile = (OrionProjectile)args.Projectile;
            };
            var projectile =
                (OrionProjectile)_projectileService.SpawnProjectile(ProjectileType.Ale, Vector2.Zero, Vector2.Zero, 0,
                                                                    0);

            projectile.Remove();

            argsProjectile.Should().NotBeNull();
            argsProjectile.Wrapped.Should().BeSameAs(projectile.Wrapped);
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var projectiles = _projectileService.ToList();

            for (var i = 0; i < projectiles.Count; ++i) {
                ((OrionProjectile)projectiles[i]).Wrapped.Should().BeSameAs(Main.projectile[i]);
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
