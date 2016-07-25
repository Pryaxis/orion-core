using System;
using NUnit.Framework;
using Orion.Events.Npc;

namespace Orion.Tests.Events.Npc
{
	[TestFixture]
	public class NpcTransformedEventArgsTests
	{
		[Test]
		public void Constructor_NullNpc_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new NpcTransformedEventArgs(null, 0));
		}
	}
}
