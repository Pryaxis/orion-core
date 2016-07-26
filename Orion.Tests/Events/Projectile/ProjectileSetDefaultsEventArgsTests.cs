using System;
using NUnit.Framework;
using Orion.Events.Projectile;

namespace Orion.Tests.Events.Projectile
{
	[TestFixture]
	public class ProjectileSetDefaultsEventArgsTests
	{
		[Test]
		public void Constructor_NullProjectile_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new ProjectileSetDefaultsEventArgs(null));
		}
	}
}
