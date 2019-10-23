// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Events;
using Orion.Events.Projectiles;
using Serilog.Core;
using Xunit;
using Main = Terraria.Main;
using TerrariaProjectile = Terraria.Projectile;

namespace Orion.Projectiles {
    [Collection("TerrariaTestsCollection")]
    public class OrionProjectileServiceTests {
        public OrionProjectileServiceTests() {
            for (var i = 0; i < Main.projectile.Length; ++i) {
                Main.projectile[i] = new TerrariaProjectile { whoAmI = i };
            }
        }

        [Fact]
        public void Projectiles_Item_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var projectile = projectileService.Projectiles[1];

            projectile.Index.Should().Be(1);
            ((OrionProjectile)projectile).Wrapped.Should().BeSameAs(Main.projectile[1]);
        }

        [Fact]
        public void Projectiles_Item_GetMultipleTimes_ReturnsSameInstance() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var projectile = projectileService.Projectiles[0];
            var projectile2 = projectileService.Projectiles[0];

            projectile.Should().BeSameAs(projectile2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Projectiles_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            Func<IProjectile> func = () => projectileService.Projectiles[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Projectiles_GetEnumerator() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var projectiles = projectileService.Projectiles.ToList();

            for (var i = 0; i < projectiles.Count; ++i) {
                ((OrionProjectile)projectiles[i]).Wrapped.Should().BeSameAs(Main.projectile[i]);
            }
        }

        [Fact]
        public void ProjectileSetDefaults() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<ProjectileDefaultsEvent>(e => {
                isRun = true;
                ((OrionProjectile)e.Projectile).Wrapped.Should().BeSameAs(Main.projectile[0]);
                e.ProjectileType.Should().Be(ProjectileType.CrystalBullet);
            });

            Main.projectile[0].SetDefaults((int)ProjectileType.CrystalBullet);

            isRun.Should().BeTrue();
        }

        [Theory]
        [InlineData(ProjectileType.CrystalBullet, ProjectileType.WoodenArrow)]
        [InlineData(ProjectileType.CrystalBullet, ProjectileType.None)]
        public void ProjectileSetDefaults_ModifyProjectileType(
                ProjectileType oldType, ProjectileType newType) {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            kernel.RegisterHandler<ProjectileDefaultsEvent>(e => e.ProjectileType = newType);

            Main.projectile[0].SetDefaults((int)oldType);

            Main.projectile[0].type.Should().Be((int)newType);
        }

        [Fact]
        public void ProjectileSetDefaults_Canceled() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            kernel.RegisterHandler<ProjectileDefaultsEvent>(e => e.Cancel());

            Main.projectile[0].SetDefaults((int)ProjectileType.CrystalBullet);

            Main.projectile[0].type.Should().Be(0);
        }

        [Fact]
        public void ProjectileUpdate() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<ProjectileUpdateEvent>(e => {
                isRun = true;
                ((OrionProjectile)e.Projectile).Wrapped.Should().BeSameAs(Main.projectile[0]);
            });

            Main.projectile[0].Update(0);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void ProjectileUpdate_Canceled() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            kernel.RegisterHandler<ProjectileUpdateEvent>(e => e.Cancel());

            Main.projectile[0].Update(0);
        }

        [Fact]
        public void ProjectileRemove() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<ProjectileRemoveEvent>(e => {
                isRun = true;
                ((OrionProjectile)e.Projectile).Wrapped.Should().BeSameAs(Main.projectile[0]);
            });

            Main.projectile[0].Kill();

            isRun.Should().BeTrue();
        }

        [Fact]
        public void ProjectileRemove_Canceled() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            kernel.RegisterHandler<ProjectileRemoveEvent>(e => e.Cancel());
            Main.projectile[0].SetDefaults((int)ProjectileType.CrystalBullet);

            Main.projectile[0].Kill();

            Main.projectile[0].active.Should().BeTrue();
        }

        [Fact]
        public void SpawnProjectile() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var projectile = projectileService.SpawnProjectile(ProjectileType.CrystalBullet, Vector2.Zero,
                Vector2.Zero, 100, 0);

            projectile.Should().NotBeNull();
            projectile.Type.Should().Be(ProjectileType.CrystalBullet);
        }

        [Fact]
        public void SpawnProjectile_AiValues() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var projectile = projectileService.SpawnProjectile(ProjectileType.CrystalBullet, Vector2.Zero,
                Vector2.Zero, 100, 0, new float[] { 1, 2 });

            projectile.Should().NotBeNull();
            projectile.Type.Should().Be(ProjectileType.CrystalBullet);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void SpawnProjectile_AiValuesWrongLength_ThrowsArgumentException(int aiValuesLength) {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            Func<IProjectile> func = () => projectileService.SpawnProjectile(ProjectileType.CrystalBullet, Vector2.Zero,
                Vector2.Zero, 100, 0, new float[aiValuesLength]);

            func.Should().Throw<ArgumentException>();
        }
    }
}
