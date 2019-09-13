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
using FluentAssertions;
using Microsoft.Xna.Framework;
using Terraria;
using Xunit;

namespace Orion.Projectiles {
    public class OrionProjectileTests {
        [Theory]
        [InlineData(100)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaProjectile = new Projectile {whoAmI = index};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Index.Should().Be(index);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsActive_IsCorrect(bool isActive) {
            var terrariaProjectile = new Projectile {active = isActive};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.IsActive.Should().Be(isActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsActive_IsCorrect(bool isActive) {
            var terrariaProjectile = new Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.IsActive = isActive;

            terrariaProjectile.active.Should().Be(isActive);
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaProjectile = new Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);
            Action action = () => projectile.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPosition_IsCorrect() {
            var terrariaProjectile = new Projectile {position = new Vector2(100, 100)};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetPosition_IsCorrect() {
            var terrariaProjectile = new Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Position = new Vector2(100, 100);

            terrariaProjectile.position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetVelocity_IsCorrect() {
            var terrariaProjectile = new Projectile {velocity = new Vector2(100, 100)};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetVelocity_IsCorrect() {
            var terrariaProjectile = new Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Velocity = new Vector2(100, 100);

            terrariaProjectile.velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetSize_IsCorrect() {
            var terrariaProjectile = new Projectile {Size = new Vector2(100, 100)};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Size.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetSize_IsCorrect() {
            var terrariaProjectile = new Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Size = new Vector2(100, 100);

            terrariaProjectile.Size.Should().Be(new Vector2(100, 100));
        }

        [Theory]
        [InlineData(ProjectileType.StarWrath)]
        public void GetType_IsCorrect(ProjectileType projectileType) {
            var terrariaProjectile = new Projectile {type = (int)projectileType};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Type.Should().Be(projectileType);
        }

        [Theory]
        [InlineData(100)]
        public void GetDamage_IsCorrect(int damage) {
            var terrariaProjectile = new Projectile {damage = damage};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Damage.Should().Be(damage);
        }

        [Theory]
        [InlineData(100)]
        public void SetDamage_IsCorrect(int damage) {
            var terrariaProjectile = new Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Damage = damage;

            terrariaProjectile.damage.Should().Be(damage);
        }

        [Theory]
        [InlineData(100)]
        public void GetKnockback_IsCorrect(float knockback) {
            var terrariaProjectile = new Projectile {knockBack = knockback};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Knockback.Should().Be(knockback);
        }

        [Theory]
        [InlineData(100)]
        public void SetKnockback_IsCorrect(float knockback) {
            var terrariaProjectile = new Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Knockback = knockback;

            terrariaProjectile.knockBack.Should().Be(knockback);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsHostile_IsCorrect(bool isHostile) {
            var terrariaProjectile = new Projectile {hostile = isHostile};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.IsHostile.Should().Be(isHostile);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsHostile_IsCorrect(bool isHostile) {
            var terrariaProjectile = new Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.IsHostile = isHostile;

            terrariaProjectile.hostile.Should().Be(isHostile);
        }

        [Theory]
        [InlineData(ProjectileType.StarWrath)]
        public void ApplyType_IsCorrect(ProjectileType projectileType) {
            var terrariaProjectile = new Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.ApplyType(projectileType);

            terrariaProjectile.type.Should().Be((int)projectileType);
        }
    }
}
