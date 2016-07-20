using System;
using NUnit.Framework;
using Orion.Data;

namespace Orion.Tests.Interfaces.Implementations
{
	[TestFixture]
	public class NpcTests : EntityTests
	{
		protected override void GetEntities(out Terraria.Entity terrariaEntity, out Entity entity)
		{
			var terrariaNpc = new Terraria.NPC();
			terrariaEntity = terrariaNpc;
			entity = Npc.Wrap(terrariaNpc);
		}

		[Test]
		public void GetBacking_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			Npc npc = Npc.Wrap(terrariaNpc);

			Assert.AreSame(terrariaNpc, npc.WrappedNpc);
		}

		[Test]
		public void GetMaxHP_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			Npc npc = Npc.Wrap(terrariaNpc);

			terrariaNpc.lifeMax = 100;

			Assert.AreEqual(100, npc.MaxHP);
		}

		[Test]
		public void SetMaxHP_Updates()
		{
			var terrariaNpc = new Terraria.NPC();
			Npc npc = Npc.Wrap(terrariaNpc);

			npc.MaxHP = 100;

			Assert.AreEqual(100, terrariaNpc.lifeMax);
		}

		[Test]
		public void GetHP_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			Npc npc = Npc.Wrap(terrariaNpc);

			terrariaNpc.life = 50;

			Assert.AreEqual(50, npc.HP);
		}

		[Test]
		public void SetHP_Updates()
		{
			var terrariaNpc = new Terraria.NPC();
			Npc npc = Npc.Wrap(terrariaNpc);

			npc.HP = 50;

			Assert.AreEqual(50, terrariaNpc.life);
		}

		[Test]
		public void GetType_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			Npc npc = Npc.Wrap(terrariaNpc);

			terrariaNpc.type = 5;

			Assert.AreEqual(5, npc.Type);
		}

		[Test]
		public void Wrap_Null_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => Npc.Wrap(null));
		}

		[Test]
		public void Wrap_ReturnsSameInstance()
		{
			var terrariaNpc = new Terraria.NPC();

			Npc npc1 = Npc.Wrap(terrariaNpc);
			Npc npc2 = Npc.Wrap(terrariaNpc);

			Assert.AreSame(npc1, npc2);
		}
	}
}
