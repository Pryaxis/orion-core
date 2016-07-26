using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using Orion.Interfaces;
using Orion.Services;

namespace Orion.Tests.Services
{
	[TestFixture]
	public class ProjectileServiceTests
	{
		[TestCase(1)]
		public void ProjectileSetDefaults_IsCorrect(int type)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				bool eventOcccurred = false;
				projectileService.ProjectileSetDefaults += (s, a) =>
				{
					eventOcccurred = true;
					Assert.AreEqual(terrariaProjectile, a.Projectile.WrappedProjectile);
				};

				terrariaProjectile.SetDefaults(type);

				Assert.IsTrue(eventOcccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase(1)]
		public void ProjectileSettingDefaults_IsCorrect(int type)
		{
			using (Orion orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				bool eventOccurred = false;
				projectileService.ProjectileSettingDefaults += (s, a) =>
				{
					eventOccurred = true;
					Assert.AreEqual(type, a.Type);
				};

				terrariaProjectile.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SettingDefaults event should have occurred.");
			}
		}

		[TestCase(1, 2)]
		public void ProjectileSettingDefaults_ModifiesType(int type, int newType)
		{
			using (Orion orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				projectileService.ProjectileSettingDefaults += (s, a) =>
				{
					a.Type = newType;
				};

				terrariaProjectile.SetDefaults(type);

				Assert.AreEqual(newType, terrariaProjectile.type);
			}
		}

		[TestCase(1)]
		public void ProjectileSettingDefaults_Handled_StopsSetDefaults(int type)
		{
			using (Orion orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				projectileService.ProjectileSettingDefaults += (s, a) =>
				{
					a.Handled = true;
				};

				terrariaProjectile.SetDefaults(type);

				Assert.AreEqual(0, terrariaProjectile.type);
			}
		}

		[TestCase(10)]
		[TestCase(100)]
		public void Find_Null_ReturnsAll(int populate)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				for (int i = 0; i < Terraria.Main.projectile.Length; i++)
				{
					Terraria.Main.projectile[i] = new Terraria.Projectile { active = i < populate, name = "A" };
				}
				List<IProjectile> projectiles = projectileService.Find().ToList();

				Assert.AreEqual(populate, projectiles.Count);
				foreach (IProjectile projectile in projectiles)
				{
					Assert.AreEqual("A", projectile.Name);
				}
			}
		}

		private static readonly Predicate<IProjectile>[] Predicates = { projectile => projectile.Position.X <= 10 };
		[Test, TestCaseSource(nameof(Predicates))]
		public void Find_IsCorrect(Predicate<IProjectile> predicate)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				for (int i = 0; i < Terraria.Main.projectile.Length; ++i)
				{
					Terraria.Main.projectile[i] = new Terraria.Projectile { active = true, position = new Vector2(i, 0) };
				}
				List<IProjectile> projectiles = projectileService.Find(predicate).ToList();
				IEnumerable<IProjectile> otherProjectiles = projectileService.Find(p => !projectiles.Contains(p));

				foreach (IProjectile projectile in projectiles)
				{
					Assert.IsTrue(predicate(projectile));
				}
				foreach (IProjectile otherProjectile in otherProjectiles)
				{
					Assert.IsFalse(predicate(otherProjectile));
				}
			}
		}
	}
}
