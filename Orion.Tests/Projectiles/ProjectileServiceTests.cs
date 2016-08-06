using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Projectiles;

namespace Orion.Tests.Projectiles
{
	[TestFixture]
	public class ProjectileServiceTests
	{
		private static readonly object[] ProjectileSetDefaultsTestCases = {ProjectileType.AdamantiteChainsaw};

		private static readonly object[] ProjectileSettingDefaultsTestCases = {ProjectileType.AdamantiteChainsaw};

		private static readonly Predicate<IProjectile>[] FindTestCases = {projectile => projectile.Position.X <= 10};

		[TestCaseSource(nameof(ProjectileSetDefaultsTestCases))]
		public void ProjectileSetDefaults_IsCorrect(ProjectileType type)
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();

				var terrariaProjectile = new Terraria.Projectile();
				var projectile = new Projectile(terrariaProjectile);
				var eventOccurred = false;
				projectileService.ProjectileSetDefaults += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaProjectile, args.Projectile.WrappedProjectile);
				};

				projectile.SetDefaults(type);

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCaseSource(nameof(ProjectileSettingDefaultsTestCases))]
		public void ProjectileSettingDefaults_IsCorrect(ProjectileType type)
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();

				var terrariaProjectile = new Terraria.Projectile();
				var projectile = new Projectile(terrariaProjectile);
				var eventOccurred = false;
				projectileService.ProjectileSettingDefaults += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaProjectile, args.Projectile.WrappedProjectile);
					Assert.AreEqual(type, args.Type);
				};

				projectile.SetDefaults(type);

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCaseSource(nameof(ProjectileSettingDefaultsTestCases))]
		public void ProjectileSettingDefaults_ModifiesType(ProjectileType newType)
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				var terrariaProjectile = new Terraria.Projectile();
				var projectile = new Projectile(terrariaProjectile);
				projectileService.ProjectileSettingDefaults += (sender, args) => args.Type = newType;

				projectile.SetDefaults(ProjectileType.None);

				Assert.AreEqual(newType, projectile.Type);
			}
		}

		[TestCaseSource(nameof(ProjectileSettingDefaultsTestCases))]
		public void ProjectileSettingDefaults_Handled_StopsSetDefaults(ProjectileType type)
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				var terrariaProjectile = new Terraria.Projectile();
				var projectile = new Projectile(terrariaProjectile);
				projectileService.ProjectileSettingDefaults += (sender, args) => args.Handled = true;

				projectile.SetDefaults(type);

				Assert.AreEqual(ProjectileType.None, projectile.Type, "SetDefaults should not have occurred.");
			}
		}

		[TestCase(0)]
		[TestCase(1)]
		public void FindProjectiles_Null_ReturnsAll(int populate)
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				for (var i = 0; i < Terraria.Main.projectile.Length; i++)
				{
					Terraria.Main.projectile[i] = new Terraria.Projectile {active = i < populate};
				}

				List<IProjectile> projectiles = projectileService.FindProjectiles().ToList();

				Assert.AreEqual(populate, projectiles.Count);
				for (var i = 0; i < populate; ++i)
				{
					Assert.AreSame(Terraria.Main.projectile[i], projectiles[i].WrappedProjectile);
				}
			}
		}

		[TestCase(1)]
		public void FindProjectiles_MultipleTimes_ReturnsSameInstance(int populate)
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				for (var i = 0; i < Terraria.Main.projectile.Length; ++i)
				{
					Terraria.Main.projectile[i] = new Terraria.Projectile {active = i < populate};
				}

				List<IProjectile> projectiles = projectileService.FindProjectiles().ToList();
				List<IProjectile> projectiles2 = projectileService.FindProjectiles().ToList();

				for (var i = 0; i < populate; ++i)
				{
					Assert.AreSame(projectiles[i], projectiles2[i]);
				}
			}
		}

		[Test, TestCaseSource(nameof(FindTestCases))]
		public void FindProjectiles_IsCorrect(Predicate<IProjectile> predicate)
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				for (var i = 0; i < Terraria.Main.projectile.Length; ++i)
				{
					Terraria.Main.projectile[i] = new Terraria.Projectile {active = true, position = new Vector2(i, 0)};
				}

				IEnumerable<IProjectile> projectiles = projectileService.FindProjectiles(predicate);

				foreach (IProjectile projectile in projectiles)
				{
					Assert.IsTrue(predicate(projectile));
				}
			}
		}
	}
}
