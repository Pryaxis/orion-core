using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Projectiles;
using Orion.Projectiles.Events;

namespace Orion.Tests.Projectiles
{
	[TestFixture]
	public class ProjectileServiceTests
	{
		public static readonly Predicate<IProjectile>[] FindTestCases = {projectile => projectile.Position.X <= 10};

		[Test]
		public void ProjectileKilled_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				var terrariaProjectile = new Terraria.Projectile();
				var eventOccurred = false;
				EventHandler<ProjectileKilledEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaProjectile, e.Projectile.WrappedProjectile);
				};
				projectileService.ProjectileKilled += handler;

				terrariaProjectile.Kill();
				projectileService.ProjectileKilled -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[Test]
		public void ProjectileKilling_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				var terrariaProjectile = new Terraria.Projectile();
				var eventOccurred = false;
				EventHandler<ProjectileKillingEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaProjectile, e.Projectile.WrappedProjectile);
				};
				projectileService.ProjectileKilling += handler;

				terrariaProjectile.Kill();
				projectileService.ProjectileKilling -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}
		
		[Test]
		public void ProjectileKilling_Handled_StopsKill()
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				var terrariaProjectile = new Terraria.Projectile {active = true};
				EventHandler<ProjectileKillingEventArgs> handler = (sender, e) => e.Handled = true;
				projectileService.ProjectileKilling += handler;
				
				terrariaProjectile.Kill();
				projectileService.ProjectileKilling -= handler;

				Assert.IsTrue(terrariaProjectile.active, "Kill should not have occurred.");
			}
		}

		[Test]
		public void ProjectileSetDefaults_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				var terrariaProjectile = new Terraria.Projectile();
				var eventOccurred = false;
				EventHandler<ProjectileSetDefaultsEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaProjectile, e.Projectile.WrappedProjectile);
				};
				projectileService.ProjectileSetDefaults += handler;

				terrariaProjectile.SetDefaults(0);
				projectileService.ProjectileSetDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(ProjectileType.AdamantiteChainsaw)]
		public void ProjectileSettingDefaults_IsCorrect(ProjectileType type)
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				var terrariaProjectile = new Terraria.Projectile();
				var eventOccurred = false;
				EventHandler<ProjectileSettingDefaultsEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaProjectile, e.Projectile.WrappedProjectile);
					Assert.AreEqual(type, e.Type);
				};
				projectileService.ProjectileSettingDefaults += handler;

				terrariaProjectile.SetDefaults((int)type);
				projectileService.ProjectileSettingDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(ProjectileType.AdamantiteChainsaw)]
		public void ProjectileSettingDefaults_ModifiesType(ProjectileType newType)
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				var terrariaProjectile = new Terraria.Projectile();
				EventHandler<ProjectileSettingDefaultsEventArgs> handler = (sender, e) => e.Type = newType;
				projectileService.ProjectileSettingDefaults += handler;

				terrariaProjectile.SetDefaults(0);
				projectileService.ProjectileSettingDefaults -= handler;

				Assert.AreEqual((int)newType, terrariaProjectile.type);
			}
		}

		[Test]
		public void ProjectileSettingDefaults_Handled_StopsSetDefaults()
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				var terrariaProjectile = new Terraria.Projectile();
				EventHandler<ProjectileSettingDefaultsEventArgs> handler = (sender, e) => e.Handled = true;
				projectileService.ProjectileSettingDefaults += handler;

				terrariaProjectile.SetDefaults(1);
				projectileService.ProjectileSettingDefaults -= handler;

				Assert.AreEqual(0, terrariaProjectile.type, "SetDefaults should not have occurred.");
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

		[Test]
		public void FindProjectiles_MultipleTimes_ReturnsSameInstance()
		{
			using (var orion = new Orion())
			{
				var projectileService = orion.GetService<ProjectileService>();
				for (var i = 0; i < Terraria.Main.projectile.Length; ++i)
				{
					Terraria.Main.projectile[i] = new Terraria.Projectile {active = i < 100};
				}

				List<IProjectile> projectiles = projectileService.FindProjectiles().ToList();
				List<IProjectile> projectiles2 = projectileService.FindProjectiles().ToList();

				for (var i = 0; i < 100; ++i)
				{
					Assert.AreSame(projectiles[i], projectiles2[i]);
				}
			}
		}

		[TestCaseSource(nameof(FindTestCases))]
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
