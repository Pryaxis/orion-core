using System;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Projectiles;
using Xunit;

namespace Orion.Tests.Projectiles {
    public class OrionProjectileTests {
        [Theory]
        [InlineData(100)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaProjectile = new Terraria.Projectile {whoAmI = index};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Index.Should().Be(index);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsActive_IsCorrect(bool isActive) {
            var terrariaProjectile = new Terraria.Projectile {active = isActive};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.IsActive.Should().Be(isActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsActive_IsCorrect(bool isActive) {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.IsActive = isActive;

            terrariaProjectile.active.Should().Be(isActive);
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);
            Action action = () => projectile.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPosition_IsCorrect() {
            var terrariaProjectile = new Terraria.Projectile {position = new Vector2(100, 100)};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetPosition_IsCorrect() {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Position = new Vector2(100, 100);

            terrariaProjectile.position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetVelocity_IsCorrect() {
            var terrariaProjectile = new Terraria.Projectile {velocity = new Vector2(100, 100)};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetVelocity_IsCorrect() {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Velocity = new Vector2(100, 100);

            terrariaProjectile.velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetSize_IsCorrect() {
            var terrariaProjectile = new Terraria.Projectile {Size = new Vector2(100, 100)};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Size.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetSize_IsCorrect() {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Size = new Vector2(100, 100);

            terrariaProjectile.Size.Should().Be(new Vector2(100, 100));
        }

        [Theory]
        [InlineData(ProjectileType.StarWrath)]
        public void GetType_IsCorrect(ProjectileType projectileType) {
            var terrariaProjectile = new Terraria.Projectile {type = (int)projectileType};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Type.Should().Be(projectileType);
        }

        [Theory]
        [InlineData(100)]
        public void GetDamage_IsCorrect(int damage) {
            var terrariaProjectile = new Terraria.Projectile {damage = damage};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Damage.Should().Be(damage);
        }

        [Theory]
        [InlineData(100)]
        public void SetDamage_IsCorrect(int damage) {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Damage = damage;

            terrariaProjectile.damage.Should().Be(damage);
        }

        [Theory]
        [InlineData(100)]
        public void GetKnockback_IsCorrect(float knockback) {
            var terrariaProjectile = new Terraria.Projectile {knockBack = knockback};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Knockback.Should().Be(knockback);
        }

        [Theory]
        [InlineData(100)]
        public void SetKnockback_IsCorrect(float knockback) {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Knockback = knockback;

            terrariaProjectile.knockBack.Should().Be(knockback);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsHostile_IsCorrect(bool isHostile) {
            var terrariaProjectile = new Terraria.Projectile {hostile = isHostile};
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.IsHostile.Should().Be(isHostile);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsHostile_IsCorrect(bool isHostile) {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.IsHostile = isHostile;

            terrariaProjectile.hostile.Should().Be(isHostile);
        }

        [Theory]
        [InlineData(ProjectileType.StarWrath)]
        public void ApplyType_IsCorrect(ProjectileType projectileType) {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.ApplyType(projectileType);

            terrariaProjectile.type.Should().Be((int)projectileType);
        }
    }
}
