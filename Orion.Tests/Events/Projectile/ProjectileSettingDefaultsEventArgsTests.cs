using System;
using NUnit.Framework;
using Orion.Events.Projectile;

namespace Orion.Tests.Events.Projectile
{
	[TestFixture]
	public class ProjectileSettingDefaultsEventArgsTests
	{
		[Test]
		public void Constructor_NullProjectile_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new ProjectileSettingDefaultsEventArgs(null, 0));
		}
	}
}
