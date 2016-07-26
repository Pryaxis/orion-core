using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Core;

namespace Orion.Tests.Core
{
	[TestFixture]
	public class ProjectileTests
	{
		[TestCase(100)]
		public void GetDamage_IsCorrect(int damage)
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			terrariaProjectile.damage = damage;

			Assert.AreEqual(projectile.Damage, damage);
		}

		[TestCase("Name")]
		public void GetName_IsCorrect(string name)
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			terrariaProjectile.name = name;

			Assert.AreEqual(projectile.Name, name);
		}

		[TestCase(1)]
		public void GetType_IsCorrect(int type)
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			terrariaProjectile.type = type;

			Assert.AreEqual(projectile.Type, type);
		}

		private static readonly Vector2[] Positions = { Vector2.One };
		[Test, TestCaseSource(nameof(Positions))]
		public void GetPosition_IsCorrect(Vector2 position)
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			terrariaProjectile.position = position;

			Assert.AreEqual(projectile.Position, position);
		}

		[Test, TestCaseSource(nameof(Positions))]
		public void SetPosition_Updates(Vector2 position)
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			projectile.Position = position;

			Assert.AreEqual(terrariaProjectile.position, position);
		}

		private static readonly Vector2[] Velocities = { Vector2.One };
		[Test, TestCaseSource(nameof(Velocities))]
		public void GetVelocity_IsCorrect(Vector2 velocity)
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			terrariaProjectile.velocity = velocity;

			Assert.AreEqual(projectile.Velocity, velocity);
		}

		[Test, TestCaseSource(nameof(Velocities))]
		public void SetVelocity_Updates(Vector2 velocity)
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			projectile.Velocity = velocity;

			Assert.AreEqual(terrariaProjectile.velocity, velocity);
		}

		[Test]
		public void GetWrappedProjectile_IsCorrect()
		{
			var terrariaProjectile = new Terraria.Projectile();
			var projectile = new Projectile(terrariaProjectile);

			Assert.AreSame(terrariaProjectile, projectile.WrappedProjectile);
		}
	}
}
