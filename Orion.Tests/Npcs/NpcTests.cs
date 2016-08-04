using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Npcs;

namespace Orion.Tests.Npcs
{
	[TestFixture]
	public class NpcTests
	{
		private static readonly object[] GetPropertyTestCases =
		{
			new object[] {nameof(Npc.Damage), nameof(Terraria.NPC.damage), 100},
			new object[] {nameof(Npc.Defense), nameof(Terraria.NPC.defense), 100},
			new object[] {nameof(Npc.Health), nameof(Terraria.NPC.life), 100},
			new object[] {nameof(Npc.MaxHealth), nameof(Terraria.NPC.lifeMax), 100},
			new object[] {nameof(Npc.Name), nameof(Terraria.NPC.name), "TEST"},
			new object[] {nameof(Npc.Position), nameof(Terraria.NPC.position), Vector2.One}
		};

		private static readonly object[] SetPropertyTestCases =
		{
			new object[] {nameof(Npc.Health), nameof(Terraria.NPC.life), 100},
			new object[] {nameof(Npc.MaxHealth), nameof(Terraria.NPC.lifeMax), 100},
			new object[] {nameof(Npc.Position), nameof(Terraria.NPC.position), Vector2.One},
			new object[] {nameof(Npc.Velocity), nameof(Terraria.NPC.velocity), Vector2.One}
		};

		private static readonly object[] GetTypeTestCases = {NpcType.BlueSlime };

		private static readonly object[] SetDefaultsTestCases = {NpcType.BlueSlime};

		[Test]
		public void Constructor_NullTerrariaNpc_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Npc(null));
		}

		[TestCaseSource(nameof(GetPropertyTestCases))]
		public void GetProperty_IsCorrect(string npcPropertyName, string terrariaNpcFieldName, object value)
		{
			var terrariaNpc = new Terraria.NPC();
			FieldInfo terrariaNpcField = typeof(Terraria.NPC).GetField(terrariaNpcFieldName);
			terrariaNpcField.SetValue(terrariaNpc, value);
			var npc = new Npc(terrariaNpc);
			PropertyInfo npcProperty = typeof(Npc).GetProperty(npcPropertyName);

			object actualValue = npcProperty.GetValue(npc);

			Assert.AreEqual(value, actualValue);
		}

		[TestCaseSource(nameof(SetPropertyTestCases))]
		public void SetProperty_IsCorrect(string npcPropertyName, string terrariaNpcFieldName, object value)
		{
			var terrariaNpc = new Terraria.NPC();
			FieldInfo terrariaNpcField = typeof(Terraria.NPC).GetField(terrariaNpcFieldName);
			var npc = new Npc(terrariaNpc);
			PropertyInfo npcProperty = typeof(Npc).GetProperty(npcPropertyName);

			npcProperty.SetValue(npc, value);

			Assert.AreEqual(value, terrariaNpcField.GetValue(terrariaNpc));
		}

		[TestCase(-1)]
		public void SetHealth_NegativeValue_ThrowsArgumentOutOfRangeException(int health)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			Assert.Throws<ArgumentOutOfRangeException>(() => npc.Health = health);
		}

		[TestCase(-1)]
		public void SetMaxHealth_NegativeValue_ThrowsArgumentOutOfRangeException(int maxHealth)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			Assert.Throws<ArgumentOutOfRangeException>(() => npc.MaxHealth = maxHealth);
		}

		[TestCaseSource(nameof(GetTypeTestCases))]
		public void GetType_IsCorrect(NpcType type)
		{
			var terrariaNpc = new Terraria.NPC {netID = (int)type};
			var npc = new Npc(terrariaNpc);

			NpcType actualType = npc.Type;

			Assert.AreEqual(type, actualType);
		}

		[Test]
		public void GetWrappedNpc_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			Terraria.NPC actualNpc = npc.WrappedNpc;

			Assert.AreSame(terrariaNpc, actualNpc);
		}

		[Test]
		public void Kill_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			terrariaNpc.SetDefaults(1);
			var npc = new Npc(terrariaNpc);

			npc.Kill();
			
			Assert.IsTrue(npc.Health <= 0, "NPC should have been killed.");
		}

		[TestCaseSource(nameof(SetDefaultsTestCases))]
		public void SetDefaults_IsCorrect(NpcType type)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.SetDefaults(type);

			Assert.AreEqual(type, npc.Type);
		}
	}
}
