using System;
using NUnit.Framework;
using Orion.Projectiles;
using Orion.Projectiles.Events;

namespace Orion.Tests.Projectiles.Events
{
	[TestFixture]
	public class ProjectileSettingDefaultsEventArgsTests
	{
		[Test]
		public void Constructor_NullProjectile_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(
				() => new ProjectileSettingDefaultsEventArgs(null, ProjectileType.None));
		}
	}
}
