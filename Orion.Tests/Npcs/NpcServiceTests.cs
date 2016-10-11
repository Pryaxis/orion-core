using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using Orion.Npcs;
using Orion.Npcs.Events;

namespace Orion.Tests.Npcs
{
	[TestFixture]
	public class NpcServiceTests
	{
		private static readonly object[] SpawnTestCases = { new object[] { NpcType.BlueSlime, new Vector2(1000, 2000) } };

		private static readonly Predicate<INpc>[] FindTestCases = { npc => (int)npc.Type < 100 };

		[Test]
		public void BaseNpcSpawningLimit_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				Assert.AreEqual(Terraria.NPC.defaultMaxSpawns, npcService.BaseNpcSpawningLimit);
			}
		}

		[TestCase(100)]
		public void BaseNpcSpawningLimit_ModifiesLimit(int newSpawnLimit)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				npcService.BaseNpcSpawningLimit = newSpawnLimit;

				Assert.AreEqual(newSpawnLimit, npcService.BaseNpcSpawningLimit);
			}
		}

		[Test]
		public void BaseNpcSpawningRate_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				Assert.AreEqual(Terraria.NPC.defaultSpawnRate, npcService.BaseNpcSpawningRate);
			}
		}

		[TestCase(100)]
		public void BaseNpcSpawningRate_ModifiesRate(int newSpawnRate)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				npcService.BaseNpcSpawningRate = newSpawnRate;

				Assert.AreEqual(newSpawnRate, npcService.BaseNpcSpawningRate);
			}
		}

		[TestCase(NpcType.EyeofCthulhu)]
		public void NpcDroppedLoot_IsCorrect(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC { type = (int)type };
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcDroppedLootEventArgs> handler = (sender, e) => 
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaNpc, e.Npc.WrappedNpc);
				};
				npcService.NpcDroppedLoot += handler;

				terrariaNpc.NPCLoot();
				npcService.NpcDroppedLoot -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.EyeofCthulhu)]
		public void NpcDroppingLoot_IsCorrect(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC { type = (int)type };
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcDroppingLootEventArgs> handler = (sender, e) => 
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaNpc, e.Npc.WrappedNpc);
				};
				npcService.NpcDroppingLoot += handler;

				terrariaNpc.NPCLoot();
				npcService.NpcDroppingLoot -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.EyeofCthulhu)]
		public void NpcDroppingLoot_Handled_StopsNPCLoot(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				EventHandler<NpcDroppingLootEventArgs> handler = (sender, e) => e.Handled = true;
				npcService.NpcDroppingLoot += handler;

				terrariaNpc.NPCLoot();
				npcService.NpcDroppingLoot -= handler;
			}
		}

		[TestCase(NpcType.BlueSlime)]
		public void NpcKilled_IsCorrect(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC { active = true };
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcKilledEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaNpc, e.Npc.WrappedNpc);
				};
				npcService.NpcKilled += handler;

				terrariaNpc.checkDead();
				npcService.NpcKilled -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.BlueSlime)]
		public void NpcSetDefaults_IsCorrect(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcSetDefaultsEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaNpc, e.Npc.WrappedNpc);
				};
				npcService.NpcSetDefaults += handler;

				terrariaNpc.SetDefaults((int)type);
				npcService.NpcSetDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.BlueSlime)]
		public void NpcSetDefaults_OccursFromNetDefaults(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcSetDefaultsEventArgs> handler = (sender, e) => eventOccurred = true;
				npcService.NpcSetDefaults += handler;

				terrariaNpc.SetDefaults((int)type);
				npcService.NpcSetDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase("Blue Slime")]
		public void NpcSetDefaults_OccursFromSetDefaultsString(string name)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcSetDefaultsEventArgs> handler = (sender, e) => eventOccurred = true;
				npcService.NpcSetDefaults += handler;

				terrariaNpc.SetDefaults(name);
				npcService.NpcSetDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.BlueSlime)]
		public void NpcSettingDefaults_IsCorrect(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcSettingDefaultsEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaNpc, e.Npc.WrappedNpc);
				};
				npcService.NpcSettingDefaults += handler;

				terrariaNpc.SetDefaults((int)type);
				npcService.NpcSettingDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.BlueSlime)]
		public void NpcSettingDefaults_ModifiesType(NpcType newType)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				EventHandler<NpcSettingDefaultsEventArgs> handler = (sender, e) => e.Type = newType;
				npcService.NpcSettingDefaults += handler;

				terrariaNpc.SetDefaults((int)NpcType.None);
				npcService.NpcSettingDefaults -= handler;
			}
		}

		[TestCase(NpcType.BlueSlime)]
		public void NpcSettingDefaults_Handled_StopsSetDefaults(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				EventHandler<NpcSettingDefaultsEventArgs> handler = (sender, e) => e.Handled = true;
				npcService.NpcSettingDefaults += handler;

				terrariaNpc.SetDefaults((int)type);
				npcService.NpcSettingDefaults -= handler;

				Assert.AreEqual(0, terrariaNpc.type);
			}
		}

		[TestCase(NpcType.BlueSlime)]
		public void NpcSettingDefaults_OccursFromNetDefaults(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = true;
				EventHandler<NpcSettingDefaultsEventArgs> handler = (sender, e) => eventOccurred = true;
				npcService.NpcSettingDefaults += handler;

				terrariaNpc.SetDefaults((int)type);
				npcService.NpcSettingDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase("Blue Slime")]
		public void NpcSettingDefaults_OccursFromSetDefaultsString(string name)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcSettingDefaultsEventArgs> handler = (sender, e) => eventOccurred = true;
				npcService.NpcSettingDefaults += handler;

				terrariaNpc.SetDefaults(name);
				npcService.NpcSettingDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.BlueSlime)]
		public void NpcSpawned_IsCorrect(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcSpawnedEventArgs> handler = (sender, e) => eventOccurred = true;
				npcService.NpcSpawned += handler;

				Terraria.NPC.NewNPC(0, 0, (int)type);
				npcService.NpcSpawned -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.BlueSlime)]
		public void NpcSpawning_IsCorrect(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcSpawningEventArgs> handler = (sender, e) => eventOccurred = true;
				npcService.NpcSpawning += handler;

				Terraria.NPC.NewNPC(0, 0, (int)type);
				npcService.NpcSpawning -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.BlueSlime)]
		public void NpcStriking_IsCorrect(NpcType type)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcStrikingEventArgs> handler = (sender, e) => 
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaNpc, e.Npc.WrappedNpc);
				};
				npcService.NpcStriking += handler;

				terrariaNpc.StrikeNPC(0, 0, 0);
				npcService.NpcStriking -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.KingSlime)]
		public void NpcTransformed_IsCorrect(NpcType newType)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcTransformedEventArgs> handler = (sender, e) => 
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaNpc, e.Npc.WrappedNpc);
				};
				npcService.NpcTransformed += handler;

				terrariaNpc.Transform((int)newType);
				npcService.NpcTransformed -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.KingSlime)]
		public void NpcTransforming_IsCorrect(NpcType newType)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				var eventOccurred = false;
				EventHandler<NpcTransformingEventArgs> handler = (sender, e) => 
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaNpc, e.Npc.WrappedNpc);
				};
				npcService.NpcTransforming += handler;

				terrariaNpc.Transform((int)newType);
				npcService.NpcTransforming -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(NpcType.KingSlime)]
		public void NpcTransforming_ModifiesType(NpcType newType)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				var terrariaNpc = new Terraria.NPC();
				var npc = new Npc(terrariaNpc);
				EventHandler<NpcTransformingEventArgs> handler = (sender, e) => e.Type = newType;
				npcService.NpcTransforming += handler;

				terrariaNpc.Transform((int)NpcType.None);
				npcService.NpcTransforming -= handler;

				Assert.AreEqual((int)newType, terrariaNpc.type);
			}
		}

		[TestCase(0)]
		[TestCase(1)]
		public void FindNpcs_Null_ReturnsAll(int populate)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				for (var i = 0; i < Terraria.Main.npc.Length; ++i)
				{
					Terraria.Main.npc[i] = new Terraria.NPC { active = i < populate };
				}

				List<INpc> npcs = npcService.FindNpcs().ToList();

				Assert.AreEqual(populate, npcs.Count);
				for (var i = 0; i < populate; ++i)
				{
					Assert.AreSame(Terraria.Main.npc[i], npcs[i].WrappedNpc);
				}
			}
		}

		[TestCase(1)]
		public void FindNpcs_MultipleNpcs_ReturnsSameInstance(int populate)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				for (var i = 0; i < Terraria.Main.npc.Length; ++i)
				{
					Terraria.Main.npc[i] = new Terraria.NPC { active = i < populate };
				}

				List<INpc> npcs = npcService.FindNpcs().ToList();
				List<INpc> npcs2 = npcService.FindNpcs().ToList();

				for (var i = 0; i < populate; ++i)
				{
					Assert.AreSame(npcs[i], npcs2[i]);
				}
			}
		}

		[Test, TestCaseSource(nameof(FindTestCases))]
		public void FindNpcs_IsCorrect(Predicate<INpc> predicate)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				for (var i = 0; i < Terraria.Main.npc.Length; ++i)
				{
					Terraria.Main.npc[i] = new Terraria.NPC { active = true, type = i };
				}

				IEnumerable<INpc> npcs = npcService.FindNpcs(predicate);

				foreach (INpc npc in npcs)
				{
					Assert.IsTrue(predicate(npc));
				}
			}
		}

		[TestCaseSource(nameof(SpawnTestCases))]
		public void SpawnNpc_IsCorrect(NpcType type, Vector2 position)
		{
			using (var orion = new Orion())
			{
				var npcService = orion.GetService<NpcService>();
				INpc npc = npcService.SpawnNpc((int)type, position);

				Assert.AreEqual(type, npc.Type);
				Assert.That(npc.Position.X, Is.InRange(900, 1100));
				Assert.That(npc.Position.Y, Is.InRange(1900, 2100));
			}
		}
	}
}
