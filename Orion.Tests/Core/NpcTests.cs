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

		[TestCase(100)]
		public void GetMaxHP_IsCorrect(int maxHP)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.lifeMax = maxHP;

			Assert.AreEqual(maxHP, npc.MaxHP);
		}

		[TestCase(100)]
		public void SetMaxHP_Updates(int maxHP)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.MaxHP = maxHP;

			Assert.AreEqual(maxHP, terrariaNpc.lifeMax);
		}

		[TestCase(50)]
		public void GetHP_IsCorrect(int hp)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.life = hp;

			Assert.AreEqual(hp, npc.HP);
		}

		[TestCase(50)]
		public void SetHP_Updates(int hp)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.HP = hp;

			Assert.AreEqual(hp, terrariaNpc.life);
		}

		[TestCase("Name")]
		public void GetName_IsCorrect(string name)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.name = name;

			Assert.AreEqual(name, npc.Name);
		}

		private static readonly Vector2[] Positions = {Vector2.One};

		[Test, TestCaseSource(nameof(Positions))]
		public void GetPosition_IsCorrect(Vector2 position)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.position = position;

			Assert.AreEqual(position, npc.Position);
		}

		[Test, TestCaseSource(nameof(Positions))]
		public void SetPosition_Updates(Vector2 position)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.Position = position;

			Assert.AreEqual(position, terrariaNpc.position);
		}

		[TestCase(5)]
		public void GetType_IsCorrect(int type)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.type = type;

			Assert.AreEqual(type, npc.Type);
		}

		private static readonly Vector2[] Velocities = {Vector2.One};

		[Test, TestCaseSource(nameof(Velocities))]
		public void GetVelocity_IsCorrect(Vector2 velocity)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			terrariaNpc.velocity = velocity;

			Assert.AreEqual(velocity, npc.Velocity);
		}

		[Test, TestCaseSource(nameof(Velocities))]
		public void SetVelocity_Updates(Vector2 velocity)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.Velocity = velocity;

			Assert.AreEqual(velocity, terrariaNpc.velocity);
		}
	}
}
