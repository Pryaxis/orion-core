using System;
using NUnit.Framework;
using Orion.Events.Npc;

namespace Orion.Tests.Events.Npc
{
	[TestFixture]
	public class NpcDroppingLootEventArgsTests
	{
		[Test]
		public void Constructor_NullNpc_ThrowsArgumentNullException()
		{
			var terrariaItem = new Terraria.Item();
			var item = new global::Orion.Entities.Item.Item(terrariaItem);
			Assert.Throws<ArgumentNullException>(() => new NpcDroppingLootEventArgs(null, item));
		}

		[Test]
		public void Constructor_NullItem_ThrowsArgumentNullException()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new global::Orion.Entities.Npc.Npc(terrariaNpc);
			Assert.Throws<ArgumentNullException>(() => new NpcDroppingLootEventArgs(npc, null));
		}
	}
}
