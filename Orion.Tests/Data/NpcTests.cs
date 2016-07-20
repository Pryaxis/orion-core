using System;
using NUnit.Framework;
using Orion.Data;

namespace Orion.Tests.Data
{
	[TestFixture]
	public class NpcTests : EntityTests
	{
		protected override void GetEntities(out Terraria.Entity terrariaEntity, out Entity entity)
		{
			var terrariaNpc = new Terraria.NPC();
			terrariaEntity = terrariaNpc;
			entity = new Npc(terrariaNpc);
		}

		[Test]
		public void GetBacking_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			Assert.AreSame(terrariaNpc, npc.WrappedNpc);
		}

		[Test]
		public void GetMaxHP_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.lifeMax = 100;

			Assert.AreEqual(100, npc.MaxHP);
		}

		[Test]
		public void SetMaxHP_Updates()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.MaxHP = 100;

			Assert.AreEqual(100, terrariaNpc.lifeMax);
		}

		[Test]
		public void GetHP_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.life = 50;

			Assert.AreEqual(50, npc.HP);
		}

		[Test]
		public void SetHP_Updates()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.HP = 50;

			Assert.AreEqual(50, terrariaNpc.life);
		}

		[Test]
		public void GetType_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.type = 5;

			Assert.AreEqual(5, npc.Type);
		}
	}
}
