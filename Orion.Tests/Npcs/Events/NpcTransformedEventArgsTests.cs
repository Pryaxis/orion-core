using System;
using NUnit.Framework;
using Orion.Npcs.Events;

namespace Orion.Tests.Npcs.Events
{
	[TestFixture]
	public class NpcTransformedEventArgsTests
	{
		[Test]
		public void Constructor_NullNpc_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new NpcTransformedEventArgs(null));
		}
	}
}
