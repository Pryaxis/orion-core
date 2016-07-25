using System;
using NUnit.Framework;
using Orion.Events.Npc;

namespace Orion.Tests.Events.Npc
{
	[TestFixture]
	public class NpcSpawningEventArgsTests
	{
		[Test]
		public void Constructor_NullNpc_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new NpcSpawningEventArgs(null, 0));
		}
	}
}
