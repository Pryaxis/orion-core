using System;
using NUnit.Framework;
using Orion.Npcs.Events;

namespace Orion.Tests.Events.Npc
{
	[TestFixture]
	public class NpcSpawnedEventArgsTests
	{
		[Test]
		public void Constructor_NullNpc_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new NpcSpawnedEventArgs(null));
		}
	}
}
