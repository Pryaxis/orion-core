using System;
using NUnit.Framework;
using Orion.Events.Npc;

namespace Orion.Tests.Events.Npc
{
	[TestFixture]
	public class NpcDroppingLootEventArgsTests
	{
		[Test]
		public void Constructor_NullNpc_ThrowsException()
		{
			var terrariaItem = new Terraria.Item();
			var item = new global::Orion.Core.Item(terrariaItem);
			Assert.Throws<ArgumentNullException>(() => new NpcDroppingLootEventArgs(null, item));
		}

		[Test]
		public void Constructor_NullItem_ThrowsException()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new global::Orion.Core.Npc(terrariaNpc);
			Assert.Throws<ArgumentNullException>(() => new NpcDroppingLootEventArgs(npc, null));
		}
	}
}
