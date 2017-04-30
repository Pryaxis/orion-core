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
			new object[] {nameof(Npc.Height), nameof(Terraria.NPC.height), 100},
			new object[] {nameof(Npc.IsBoss), nameof(Terraria.NPC.boss), true},
			new object[] {nameof(Npc.MaxHealth), nameof(Terraria.NPC.lifeMax), 100},
			new object[] {nameof(Npc.Name), nameof(Terraria.NPC.FullName), "TEST"},
			new object[] {nameof(Npc.Position), nameof(Terraria.NPC.position), Vector2.One},
			new object[] {nameof(Npc.Type), nameof(Terraria.NPC.netID), NpcType.BlueSlime},
			new object[] {nameof(Npc.Velocity), nameof(Terraria.NPC.velocity), Vector2.One},
			new object[] {nameof(Npc.Width), nameof(Terraria.NPC.width), 100}
		};

		private static readonly object[] SetPropertyTestCases =
		{
			new object[] {nameof(Npc.Damage), nameof(Terraria.NPC.damage), 100},
			new object[] {nameof(Npc.Defense), nameof(Terraria.NPC.defense), 100},
			new object[] {nameof(Npc.Health), nameof(Terraria.NPC.life), 100},
			new object[] {nameof(Npc.Height), nameof(Terraria.NPC.height), 100},
			new object[] {nameof(Npc.IsBoss), nameof(Terraria.NPC.boss), true},
			new object[] {nameof(Npc.MaxHealth), nameof(Terraria.NPC.lifeMax), 100},
			new object[] {nameof(Npc.Name), nameof(Terraria.NPC.FullName), "TEST"},
			new object[] {nameof(Npc.Position), nameof(Terraria.NPC.position), Vector2.One},
			new object[] {nameof(Npc.Velocity), nameof(Terraria.NPC.velocity), Vector2.One},
			new object[] {nameof(Npc.Width), nameof(Terraria.NPC.width), 100}
		};
		
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
			terrariaNpcField.SetValue(terrariaNpc, Convert.ChangeType(value, terrariaNpcField.FieldType));
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

			Assert.AreEqual(
				Convert.ChangeType(value, terrariaNpcField.FieldType), terrariaNpcField.GetValue(terrariaNpc));
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

		[TestCase(NpcType.BlueSlime)]
		public void SetDefaults_IsCorrect(NpcType type)
		{
			var terrariaNpc = new Terraria.NPC();
			var npc = new Npc(terrariaNpc);

			npc.SetDefaults(type);

			Assert.AreEqual(type, npc.Type);
		}
	}
}
