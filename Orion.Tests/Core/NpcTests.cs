using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Core;

namespace Orion.Tests.Core
{
	[TestFixture]
	public class NpcTests
	{
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
		public void GetName_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.name = "Name";

			Assert.AreEqual("Name", npc.Name);
		}

		[Test]
		public void GetPosition_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.position = Vector2.One;

			Assert.AreEqual(Vector2.One, npc.Position);
		}

		[Test]
		public void SetPosition_Updates()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.Position = Vector2.One;

			Assert.AreEqual(Vector2.One, terrariaNpc.position);
		}

		[Test]
		public void GetType_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.type = 5;

			Assert.AreEqual(5, npc.Type);
		}

		[Test]
		public void GetVelocity_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.velocity = Vector2.One;

			Assert.AreEqual(Vector2.One, npc.Velocity);
		}

		[Test]
		public void SetVelocity_Updates()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.Velocity = Vector2.One;

			Assert.AreEqual(Vector2.One, terrariaNpc.velocity);
		}
	}
}
