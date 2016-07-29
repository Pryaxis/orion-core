using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Npcs;

namespace Orion.Tests.Core
{
	[TestFixture]
	public class NpcTests
	{
		private static readonly object[] GetProperties =
		{
			new object[] {nameof(Npc.Damage), nameof(Terraria.NPC.damage), 100},
			new object[] {nameof(Npc.Defense), nameof(Terraria.NPC.defense), 100},
			new object[] {nameof(Npc.Health), nameof(Terraria.NPC.life), 100},
			new object[] {nameof(Npc.MaxHealth), nameof(Terraria.NPC.lifeMax), 100},
			new object[] {nameof(Npc.Name), nameof(Terraria.NPC.name), "TEST"},
			new object[] {nameof(Npc.Position), nameof(Terraria.NPC.position), Vector2.One},
			new object[] {nameof(Npc.Type), nameof(Terraria.NPC.netID), 1}
		};

		private static readonly object[] SetProperties =
		{
			new object[] {nameof(Npc.Health), nameof(Terraria.NPC.life), 100},
			new object[] {nameof(Npc.MaxHealth), nameof(Terraria.NPC.lifeMax), 100},
			new object[] {nameof(Npc.Position), nameof(Terraria.NPC.position), Vector2.One},
			new object[] {nameof(Npc.Velocity), nameof(Terraria.NPC.velocity), Vector2.One}
		};

		[Test]
		public void Constructor_NullNpc_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Npc(null));
		}

		[TestCaseSource(nameof(GetProperties))]
		public void GetProperty_IsCorrect(string npcPropertyName, string terrariaNpcFieldName, object value)
		{
			var terrariaNpc = new Terraria.NPC();
			FieldInfo terrariaNpcField = typeof(Terraria.NPC).GetField(terrariaNpcFieldName);
			var npc = new Npc(terrariaNpc);
			PropertyInfo npcProperty = typeof(Npc).GetProperty(npcPropertyName);

			terrariaNpcField.SetValue(terrariaNpc, Convert.ChangeType(value, terrariaNpcField.FieldType));

			Assert.AreEqual(value, npcProperty.GetValue(npc));
		}

		[TestCaseSource(nameof(SetProperties))]
		public void SetProperty_IsCorrect(string npcPropertyName, string terrariaNpcFieldName, object value)
		{
			var terrariaNpc = new Terraria.NPC();
			FieldInfo terrariaNpcField = typeof(Terraria.NPC).GetField(terrariaNpcFieldName);
			var npc = new Npc(terrariaNpc);
			PropertyInfo npcProperty = typeof(Npc).GetProperty(npcPropertyName);

			npcProperty.SetValue(npc, Convert.ChangeType(value, npcProperty.PropertyType));

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

		[Test]
		public void GetWrappedNpc_IsCorrect()
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			Assert.AreSame(terrariaNpc, npc.WrappedNpc);
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

		[TestCase(1)]
		public void SetDefaults_IsCorrect(int type)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.SetDefaults(type);

			Assert.AreEqual(type, npc.Type);
		}

		[TestCase(-1)]
		[TestCase(Terraria.Main.maxNPCTypes)]
		public void SetDefaults_InvalidType_ThrowsArgumentOutOfRangeException(int type)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			Assert.Throws<ArgumentOutOfRangeException>(() => npc.SetDefaults(type));
		}
	}
}
