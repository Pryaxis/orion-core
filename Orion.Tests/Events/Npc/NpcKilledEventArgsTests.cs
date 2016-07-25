using System;
using NUnit.Framework;
using Orion.Events.Npc;

namespace Orion.Tests.Events.Npc
{
	[TestFixture]
	public class NpcKilledEventArgsTests
	{
		[Test]
		public void Constructor_Null_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new NpcKilledEventArgs(null));
		}
	}
}
