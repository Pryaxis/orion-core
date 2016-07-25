using NUnit.Framework;
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
	}
}
