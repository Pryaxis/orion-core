using System;
using NUnit.Framework;
using Orion.Npcs.Events;

namespace Orion.Tests.Npcs.Events
{
	[TestFixture]
	public class NpcDroppedLootEventArgsTests
	{
		[Test]
		public void Constructor_NullNpc_ThrowsArgumentNullException()
		{
			var terrariaItem = new Terraria.Item();
			var item = new global::Orion.Items.Item(terrariaItem);
			Assert.Throws<ArgumentNullException>(() => new NpcDroppedLootEventArgs(null, 0, 0, 0, 0, 0, 0, 0));
		}
	}
}
