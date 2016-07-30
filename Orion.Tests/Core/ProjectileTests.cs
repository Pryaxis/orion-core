using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Projectiles;

namespace Orion.Tests.Core
{
	[TestFixture]
	public class ProjectileTests
	{
		private static readonly object[] GetProperties =
		{
			new object[] {nameof(Projectile.Damage), nameof(Terraria.Projectile.damage), 100},
			new object[] {nameof(Projectile.IsHostile), nameof(Terraria.Projectile.hostile), true},
			new object[] {nameof(Projectile.Name), nameof(Terraria.Projectile.name), "TEST"},
			new object[] {nameof(Projectile.Position), nameof(Terraria.Projectile.position), Vector2.One},
			new object[] {nameof(Projectile.Type), nameof(Terraria.Projectile.type), ProjectileType.AdamantiteChainsaw}
		};

		private static readonly object[] SetProperties =
		{
			new object[] {nameof(Projectile.Position), nameof(Terraria.Projectile.position), Vector2.One},
			new object[] {nameof(Projectile.Velocity), nameof(Terraria.Projectile.velocity), Vector2.One}
		};

		[Test]
		public void Constructor_NullProjectile_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Projectile(null));
		}

		[TestCaseSource(nameof(GetProperties))]
		public void GetProperty_IsCorrect(
			string projectilePropertyName, string terrariaProjectileFieldName, object value)
		{
			var terrariaProjectile = new Terraria.Projectile();
			FieldInfo terrariaProjectileField = typeof(Terraria.Projectile).GetField(terrariaProjectileFieldName);
			var projectile = new Projectile(terrariaProjectile);
			PropertyInfo projectileProperty = typeof(Projectile).GetProperty(projectilePropertyName);

			terrariaProjectileField.SetValue(
				terrariaProjectile, Convert.ChangeType(value, terrariaProjectileField.FieldType));

			Assert.AreEqual(value, projectileProperty.GetValue(projectile));
		}

		[TestCaseSource(nameof(SetProperties))]
		public void SetProperty_IsCorrect(
			string projectilePropertyName, string terrariaProjectileFieldName, object value)
		{
			var terrariaProjectile = new Terraria.Projectile();
			FieldInfo terrariaProjectileField = typeof(Terraria.Projectile).GetField(terrariaProjectileFieldName);
			var projectile = new Projectile(terrariaProjectile);
			PropertyInfo projectileProperty = typeof(Projectile).GetProperty(projectilePropertyName);

			projectileProperty.SetValue(projectile, Convert.ChangeType(value, projectileProperty.PropertyType));

			Assert.AreEqual(value, terrariaProjectileField.GetValue(terrariaProjectile));
		}

		[Test]
		public void GetWrappedProjectile_IsCorrect()
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			Assert.AreSame(terrariaProjectile, projectile.WrappedProjectile);
		}

		[TestCase(ProjectileType.AdamantiteChainsaw)]
		public void SetDefaults_IsCorrect(ProjectileType type)
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			projectile.SetDefaults(type);

			Assert.AreEqual(type, projectile.Type);
		}
	}
}
