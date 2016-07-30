using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using Orion.Projectiles;

namespace Orion.Tests.Services
{
	[TestFixture]
	public class ProjectileServiceTests
	{
		private static readonly Predicate<IProjectile>[] Predicates = { projectile => projectile.Position.X <= 10 };

		[TestCase(ProjectileType.AdamantiteChainsaw)]
		public void ProjectileSetDefaults_IsCorrect(ProjectileType type)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				var projectile = new Projectile(terrariaProjectile);
				bool eventOccurred = false;
				projectileService.ProjectileSetDefaults += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaProjectile, args.Projectile.WrappedProjectile);
				};

				projectile.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase(ProjectileType.AdamantiteChainsaw)]
		public void ProjectileSettingDefaults_IsCorrect(ProjectileType type)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				var projectile = new Projectile(terrariaProjectile);
				bool eventOccurred = false;
				projectileService.ProjectileSettingDefaults += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaProjectile, args.Projectile.WrappedProjectile);
					Assert.AreEqual(type, args.Type);
				};

				projectile.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SettingDefaults event should have occurred.");
			}
		}

		[TestCase(ProjectileType.AdamantiteChainsaw, ProjectileType.AdamantiteDrill)]
		public void ProjectileSettingDefaults_ModifiesType(ProjectileType type, ProjectileType newType)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				var projectile = new Projectile(terrariaProjectile);
				projectileService.ProjectileSettingDefaults += (sender, args) => args.Type = newType;

				projectile.SetDefaults(type);

				Assert.AreEqual(newType, terrariaProjectile.type);
			}
		}

		[TestCase(ProjectileType.AdamantiteChainsaw)]
		public void ProjectileSettingDefaults_Handled_StopsSetDefaults(ProjectileType type)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				var terrariaProjectile = new Terraria.Projectile();
				var projectile = new Projectile(terrariaProjectile);
				projectileService.ProjectileSettingDefaults += (sender, args) => args.Handled = true;

				projectile.SetDefaults(type);

				Assert.AreEqual(0, terrariaProjectile.type);
			}
		}

		[TestCase(0)]
		[TestCase(1)]
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

		[TestCase(1)]
		public void Find_MultipleTimes_ReturnsSameInstance(int populate)
		{
			using (var orion = new Orion())
			using (var projectileService = new ProjectileService(orion))
			{
				for (int i = 0; i < Terraria.Main.projectile.Length; ++i)
				{
					Terraria.Main.projectile[i] = new Terraria.Projectile { active = i < populate };
				}

				List<IProjectile> projectiles = projectileService.Find().ToList();
				List<IProjectile> projectiles2 = projectileService.Find().ToList();

				for (int i = 0; i < populate; ++i)
				{
					Assert.AreSame(projectiles[i], projectiles2[i]);
				}
			}
		}

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
