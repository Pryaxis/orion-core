using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Orion.Interfaces.Implementations;

namespace Orion.Tests.Interfaces.Implementations
{
	[TestFixture]
	public class NpcTests
	{
		[Test]
		public void GetBacking_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			Assert.AreSame(terrariaNpc, npc.Backing);
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
