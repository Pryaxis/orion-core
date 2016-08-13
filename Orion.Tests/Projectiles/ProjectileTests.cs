using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Projectiles;

namespace Orion.Tests.Projectiles
{
	[TestFixture]
	public class ProjectileTests
	{
		private static readonly object[] GetPropertyTestCases =
		{
			new object[] {nameof(Projectile.Damage), nameof(Terraria.Projectile.damage), 100},
			new object[] {nameof(Projectile.IsHostile), nameof(Terraria.Projectile.hostile), true},
			new object[] {nameof(Projectile.Name), nameof(Terraria.Projectile.name), "TEST"},
			new object[] {nameof(Projectile.Type), nameof(Terraria.Projectile.type), ProjectileType.Amarok},
			new object[] {nameof(Projectile.Position), nameof(Terraria.Projectile.position), Vector2.One}
		};

		private static readonly object[] SetPropertyTestCases =
		{
			new object[] {nameof(Projectile.Damage), nameof(Terraria.Projectile.damage), 100},
			new object[] {nameof(Projectile.IsHostile), nameof(Terraria.Projectile.hostile), true},
			new object[] {nameof(Projectile.Name), nameof(Terraria.Projectile.name), "TEST"},
			new object[] {nameof(Projectile.Position), nameof(Terraria.Projectile.position), Vector2.One}
		};

		private static readonly object[] GetTypeTestCases = {ProjectileType.WoodenArrowFriendly};

		private static readonly object[] SetDefaultsTestCases = {ProjectileType.WoodenArrowFriendly};

		[Test]
		public void Constructor_NullTerrariaProjectile_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Projectile(null));
		}

		[TestCaseSource(nameof(GetPropertyTestCases))]
		public void GetProperty_IsCorrect(
			string projectilePropertyName, string terrariaProjectileFieldName, object value)
		{
			var terrariaProjectile = new Terraria.Projectile();
			FieldInfo terrariaProjectileField = typeof(Terraria.Projectile).GetField(terrariaProjectileFieldName);
			terrariaProjectileField.SetValue(
				terrariaProjectile, Convert.ChangeType(value, terrariaProjectileField.FieldType));
			var projectile = new Projectile(terrariaProjectile);
			PropertyInfo projectileProperty = typeof(Projectile).GetProperty(projectilePropertyName);

			object actualValue = projectileProperty.GetValue(projectile);

			Assert.AreEqual(value, actualValue);
		}

		[TestCaseSource(nameof(SetPropertyTestCases))]
		public void SetProperty_IsCorrect(
			string projectilePropertyName, string terrariaProjectileFieldName, object value)
		{
			var terrariaProjectile = new Terraria.Projectile();
			FieldInfo terrariaProjectileField = typeof(Terraria.Projectile).GetField(terrariaProjectileFieldName);
			var projectile = new Projectile(terrariaProjectile);
			PropertyInfo projectileProperty = typeof(Projectile).GetProperty(projectilePropertyName);

			projectileProperty.SetValue(projectile, value);

			Assert.AreEqual(
				Convert.ChangeType(value, terrariaProjectileField.FieldType),
				terrariaProjectileField.GetValue(terrariaProjectile));
		}

		[Test]
		public void GetWrappedProjectile_IsCorrect()
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			Terraria.Projectile actualProjectile = projectile.WrappedProjectile;

			Assert.AreSame(terrariaProjectile, actualProjectile);
		}

		[TestCaseSource(nameof(SetDefaultsTestCases))]
		public void SetDefaults_IsCorrect(ProjectileType type)
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			projectile.SetDefaults(type);

			Assert.AreEqual(type, projectile.Type);
		}
	}
}
