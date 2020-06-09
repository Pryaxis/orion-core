// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Orion.DataStructures;
using Orion.Events;
using Orion.Events.Projectiles;
using Serilog.Core;
using Xunit;

namespace Orion.Projectiles {
    [Collection("TerrariaTestsCollection")]
    public class OrionProjectileServiceTests {
        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Projectiles_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);

            Assert.Throws<IndexOutOfRangeException>(() => projectileService.Projectiles[index]);
        }

        [Fact]
        public void Projectiles_Item_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var projectile = projectileService.Projectiles[1];

            Assert.Equal(1, projectile.Index);
            Assert.Same(Terraria.Main.projectile[1], ((OrionProjectile)projectile).Wrapped);
        }

        [Fact]
        public void Projectiles_Item_GetMultipleTimes_ReturnsSameInstance() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);

            var projectile = projectileService.Projectiles[0];
            var projectile2 = projectileService.Projectiles[0];

            Assert.Same(projectile, projectile2);
        }

        [Fact]
        public void Projectiles_GetEnumerator() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);

            var projectiles = projectileService.Projectiles.ToList();

            for (var i = 0; i < projectiles.Count; ++i) {
                Assert.Same(Terraria.Main.projectile[i], ((OrionProjectile)projectiles[i]).Wrapped);
            }
        }

        [Fact]
        public void ProjectileSetDefaults_EventTriggered() {
            Terraria.Main.projectile[0] = new Terraria.Projectile { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<ProjectileDefaultsEvent>(evt => {
                Assert.Same(Terraria.Main.projectile[0], ((OrionProjectile)evt.Projectile).Wrapped);
                Assert.Equal(ProjectileId.CrystalBullet, evt.Id);
                isRun = true;
            }, Logger.None);

            Terraria.Main.projectile[0].SetDefaults((int)ProjectileId.CrystalBullet);

            Assert.True(isRun);
        }

        [Theory]
        [InlineData(ProjectileId.CrystalBullet, ProjectileId.WoodenArrow)]
        [InlineData(ProjectileId.CrystalBullet, ProjectileId.None)]
        public void ProjectileSetDefaults_EventModified(ProjectileId oldId, ProjectileId newId) {
            Terraria.Main.projectile[0] = new Terraria.Projectile { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            kernel.RegisterHandler<ProjectileDefaultsEvent>(evt => evt.Id = newId, Logger.None);

            Terraria.Main.projectile[0].SetDefaults((int)oldId);

            Assert.Equal(newId, (ProjectileId)Terraria.Main.projectile[0].type);
        }

        [Fact]
        public void ProjectileSetDefaults_EventCanceled() {
            Terraria.Main.projectile[0] = new Terraria.Projectile { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            kernel.RegisterHandler<ProjectileDefaultsEvent>(evt => evt.Cancel(), Logger.None);

            Terraria.Main.projectile[0].SetDefaults((int)ProjectileId.CrystalBullet);

            Assert.Equal(ProjectileId.None, (ProjectileId)Terraria.Main.projectile[0].type);
        }

        [Fact]
        public void ProjectileUpdate_EventTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<ProjectileTickEvent>(evt => {
                Assert.Same(Terraria.Main.projectile[0], ((OrionProjectile)evt.Projectile).Wrapped);
                isRun = true;
            }, Logger.None);

            Terraria.Main.projectile[0].Update(0);

            Assert.True(isRun);
        }

        [Fact]
        public void SpawnProjectile() {
            using var kernel = new OrionKernel(Logger.None);
            using var projectileService = new OrionProjectileService(kernel, Logger.None);
            var projectile = projectileService.SpawnProjectile(
                ProjectileId.CrystalBullet, Vector2f.Zero, Vector2f.Zero, 100, 0);

            Assert.NotNull(projectile);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.Equal(ProjectileId.CrystalBullet, projectile.Id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
