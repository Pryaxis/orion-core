using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Npcs;
using Orion.Npcs.Events;

namespace Orion.Tests.Npcs.Events
{
	[TestFixture]
	public class NpcSpawningEventArgsTests
	{
		[Test]
		public void Constructor_NullNpc_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new NpcSpawningEventArgs(null, 0));
		}

		[TestCase(NpcType.AncientCultistSquidhead)]
		public void TestThat_Npc_Get_ReturnsNpc(NpcType npcType)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)npcType, Vector2.Zero);
				NpcSpawningEventArgs args = new NpcSpawningEventArgs(npc, 0);
				Assert.AreSame(npc, args.Npc);
			}
		}

		[TestCase(NpcType.AncientCultistSquidhead, NpcType.AncientDoom)]
		public void TestThat_Type_Set_SetsType(NpcType firstType, NpcType secondType)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)firstType, Vector2.Zero);
				NpcSpawningEventArgs args = new NpcSpawningEventArgs(npc, 0);
				args.Type = secondType;
				Assert.AreEqual(secondType, args.Type);
			}
		}
	}
}
