using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Npcs;
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

		[TestCase(1)]
		public void TestThat_Type_ReturnsType(int itemType)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)NpcType.AncientCultistSquidhead, Vector2.Zero);
				NpcDroppedLootEventArgs args = new NpcDroppedLootEventArgs(npc, itemType, 0, 0, 0, 0, 0, 0);
				Assert.AreEqual(itemType, args.Type);
			}
		}

		[TestCase(1)]
		public void TestThat_X_ReturnsX(int x)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)NpcType.AncientCultistSquidhead, Vector2.Zero);
				NpcDroppedLootEventArgs args = new NpcDroppedLootEventArgs(npc, 0, x, 0, 0, 0, 0, 0);
				Assert.AreEqual(x, args.X);
			}
		}

		[TestCase(1)]
		public void TestThat_Y_ReturnsY(int y)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)NpcType.AncientCultistSquidhead, Vector2.Zero);
				NpcDroppedLootEventArgs args = new NpcDroppedLootEventArgs(npc, 0, 0, y, 0, 0, 0, 0);
				Assert.AreEqual(y, args.Y);
			}
		}

		[TestCase(1)]
		public void TestThat_Width_ReturnsWidth(int width)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)NpcType.AncientCultistSquidhead, Vector2.Zero);
				NpcDroppedLootEventArgs args = new NpcDroppedLootEventArgs(npc, 0, 0, 0, width, 0, 0, 0);
				Assert.AreEqual(width, args.Width);
			}
		}

		[TestCase(1)]
		public void TestThat_Height_ReturnsHeight(int height)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)NpcType.AncientCultistSquidhead, Vector2.Zero);
				NpcDroppedLootEventArgs args = new NpcDroppedLootEventArgs(npc, 0, 0, 0, 0, height, 0, 0);
				Assert.AreEqual(height, args.Height);
			}
		}

		[TestCase(1)]
		public void TestThat_Stack_ReturnsStack(int stack)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)NpcType.AncientCultistSquidhead, Vector2.Zero);
				NpcDroppedLootEventArgs args = new NpcDroppedLootEventArgs(npc, 0, 0, 0, 0, 0, stack, 0);
				Assert.AreEqual(stack, args.Stack);
			}
		}

		[TestCase(1)]
		public void TestThat_Prefix_ReturnsPrefix(int prefix)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)NpcType.AncientCultistSquidhead, Vector2.Zero);
				NpcDroppedLootEventArgs args = new NpcDroppedLootEventArgs(npc, 0, 0, 0, 0, 0, 0, prefix);
				Assert.AreEqual(prefix, args.Prefix);
			}
		}

		[TestCase(NpcType.AncientCultistSquidhead)]
		public void TestThat_Npc_ReturnsNpc(NpcType npcType)
		{
			using (Orion orion = new Orion())
			{
				INpcService npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)npcType, Vector2.Zero);
				NpcDroppedLootEventArgs args = new NpcDroppedLootEventArgs(npc, 0, 0, 0, 0, 0, 0, 0);
				Assert.AreSame(npc, args.Npc);
			}
		}
	}
}
