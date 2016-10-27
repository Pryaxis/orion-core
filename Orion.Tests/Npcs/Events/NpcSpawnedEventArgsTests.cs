using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Npcs;
using Orion.Npcs.Events;

namespace Orion.Tests.Npcs.Events
{
	[TestFixture]
	public class NpcSpawnedEventArgsTests
	{
		[Test]
		public void Constructor_NullNpc_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new NpcSpawnedEventArgs(null));
		}

		[TestCase(NpcType.AncientCultistSquidhead)]
		public void TestThat_Get_ReturnsNpc(NpcType npcType)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)npcType, Vector2.Zero);
				NpcSpawnedEventArgs args = new NpcSpawnedEventArgs(npc);
				Assert.AreSame(npc, args.Npc);
			}
		}
	}
}
