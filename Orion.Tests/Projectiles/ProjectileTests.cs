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
			new object[] {nameof(Projectile.Height), nameof(Terraria.Projectile.height), 100},
			new object[] {nameof(Projectile.IsHostile), nameof(Terraria.Projectile.hostile), true},
			new object[] {nameof(Projectile.IsMagic), nameof(Terraria.Projectile.magic), true},
			new object[] {nameof(Projectile.IsMelee), nameof(Terraria.Projectile.melee), true},
			new object[] {nameof(Projectile.IsMinion), nameof(Terraria.Projectile.minion), true},
			new object[] {nameof(Projectile.IsRanged), nameof(Terraria.Projectile.ranged), true},
			new object[] {nameof(Projectile.IsThrown), nameof(Terraria.Projectile.thrown), true},
			new object[] {nameof(Projectile.Name), nameof(Terraria.Projectile.Name), "TEST"},
			new object[] {nameof(Projectile.Position), nameof(Terraria.Projectile.position), Vector2.One},
			new object[] {nameof(Projectile.Type), nameof(Terraria.Projectile.type), ProjectileType.Amarok},
			new object[] {nameof(Projectile.Velocity), nameof(Terraria.Projectile.velocity), Vector2.One},
			new object[] {nameof(Projectile.Width), nameof(Terraria.Projectile.width), 100}
		};

		private static readonly object[] SetPropertyTestCases =
		{
			new object[] {nameof(Projectile.Damage), nameof(Terraria.Projectile.damage), 100},
			new object[] {nameof(Projectile.Height), nameof(Terraria.Projectile.height), 100},
			new object[] {nameof(Projectile.IsHostile), nameof(Terraria.Projectile.hostile), true},
			new object[] {nameof(Projectile.IsMagic), nameof(Terraria.Projectile.magic), true},
			new object[] {nameof(Projectile.IsMelee), nameof(Terraria.Projectile.melee), true},
			new object[] {nameof(Projectile.IsMinion), nameof(Terraria.Projectile.minion), true},
			new object[] {nameof(Projectile.IsRanged), nameof(Terraria.Projectile.ranged), true},
			new object[] {nameof(Projectile.IsThrown), nameof(Terraria.Projectile.thrown), true},
			new object[] {nameof(Projectile.Name), nameof(Terraria.Projectile.Name), "TEST"},
			new object[] {nameof(Projectile.Position), nameof(Terraria.Projectile.position), Vector2.One},
			new object[] {nameof(Projectile.Velocity), nameof(Terraria.Projectile.velocity), Vector2.One},
			new object[] {nameof(Projectile.Width), nameof(Terraria.Projectile.width), 100}
		};
		
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

		[Test]
		public void Kill_IsCorrect()
		{
			var terrariaProjectile = new Terraria.Projectile {active = true};
			var projectile = new Projectile(terrariaProjectile);

			projectile.Kill();

			Assert.IsFalse(terrariaProjectile.active);
		}

		[TestCase(ProjectileType.Amarok)]
		public void SetDefaults_IsCorrect(ProjectileType type)
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			projectile.SetDefaults(type);

			Assert.AreEqual(type, projectile.Type);
		}
	}
}
