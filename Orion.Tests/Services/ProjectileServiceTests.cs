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
				projectileService.ProjectileSetDefaults += (sender, args) =>
				{
					eventOcccurred = true;
					Assert.AreEqual(terrariaProjectile, args.Projectile.WrappedProjectile);
				};

				terrariaProjectile.SetDefaults(type);

				Assert.IsTrue(eventOcccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase(1)]
		public void ProjectileSettingDefaults_IsCorrect(int type)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				bool eventOccurred = false;
				projectileService.ProjectileSettingDefaults += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaProjectile, args.Projectile.WrappedProjectile);
					Assert.AreEqual(type, args.Type);
				};

				terrariaProjectile.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SettingDefaults event should have occurred.");
			}
		}

		[TestCase(1, 2)]
		public void ProjectileSettingDefaults_ModifiesType(int type, int newType)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				projectileService.ProjectileSettingDefaults += (sender, args) => args.Type = newType;

				terrariaProjectile.SetDefaults(type);

				Assert.AreEqual(newType, terrariaProjectile.type);
			}
		}

		[TestCase(1)]
		public void ProjectileSettingDefaults_Handled_StopsSetDefaults(int type)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				projectileService.ProjectileSettingDefaults += (sender, args) => args.Handled = true;

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
					Terraria.Main.projectile[i] = new Terraria.Projectile { active = i < populate };
				}
				List<IProjectile> projectiles = projectileService.Find().ToList();

				Assert.AreEqual(populate, projectiles.Count);
				for (int i = 0; i < populate; ++i)
				{
					Assert.AreSame(Terraria.Main.projectile[i], projectiles[i].WrappedProjectile);
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
				IEnumerable<IProjectile> projectiles = projectileService.Find(predicate);

				foreach (IProjectile projectile in projectiles)
				{
					Assert.IsTrue(predicate(projectile));
				}
			}
		}
	}
}
