using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Core;

namespace Orion.Tests.Core
{
	[TestFixture]
	public class PlayerTests
	{
		private static readonly object[] GetWrappers =
		{
			new object[] {nameof(Player.Defense), nameof(Terraria.Player.statDefense), 100},
			new object[] {nameof(Player.Health), nameof(Terraria.Player.statLife), 100},
			new object[] {nameof(Player.MaxHealth), nameof(Terraria.Player.statLifeMax), 100},
			new object[] {nameof(Player.Mana), nameof(Terraria.Player.statMana), 100},
			new object[] {nameof(Player.MaxMana), nameof(Terraria.Player.statManaMax), 100},
			new object[] {nameof(Player.Name), nameof(Terraria.Player.name), "TEST"},
			new object[] {nameof(Player.Position), nameof(Terraria.Player.position), Vector2.One},
		};

		private static readonly object[] SetWrappers =
		{
			new object[] {nameof(Player.Health), nameof(Terraria.Player.statLife), 100},
			new object[] {nameof(Player.MaxHealth), nameof(Terraria.Player.statLifeMax), 100},
			new object[] {nameof(Player.Mana), nameof(Terraria.Player.statMana), 100},
			new object[] {nameof(Player.MaxMana), nameof(Terraria.Player.statManaMax), 100},
			new object[] {nameof(Player.Position), nameof(Terraria.Player.position), Vector2.One},
			new object[] {nameof(Player.Velocity), nameof(Terraria.Player.velocity), Vector2.One}
		};

		[Test]
		public void Constructor_NullPlayer_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Player(null));
		}

		[TestCaseSource(nameof(GetWrappers))]
		public void GetProperty_IsCorrect(string playerPropertyName, string terrariaPlayerFieldName, object value)
		{
			var terrariaPlayer = new Terraria.Player();
			FieldInfo terrariaPlayerField = typeof(Terraria.Player).GetField(terrariaPlayerFieldName);
			var player = new Player(terrariaPlayer);
			PropertyInfo playerProperty = typeof(Player).GetProperty(playerPropertyName);

			terrariaPlayerField.SetValue(terrariaPlayer, Convert.ChangeType(value, terrariaPlayerField.FieldType));

			Assert.AreEqual(value, playerProperty.GetValue(player));
		}

		[TestCaseSource(nameof(SetWrappers))]
		public void SetProperty_IsCorrect(string playerPropertyName, string terrariaPlayerFieldName, object value)
		{
			var terrariaPlayer = new Terraria.Player();
			FieldInfo terrariaPlayerField = typeof(Terraria.Player).GetField(terrariaPlayerFieldName);
			var player = new Player(terrariaPlayer);
			PropertyInfo playerProperty = typeof(Player).GetProperty(playerPropertyName);

			playerProperty.SetValue(player, Convert.ChangeType(value, playerProperty.PropertyType));

			Assert.AreEqual(value, terrariaPlayerField.GetValue(terrariaPlayer));
		}

		[TestCase(1)]
		public void GetInventory_IsCorrect(int index)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.inventory[index] = new Terraria.Item();

			Assert.AreSame(terrariaPlayer.inventory[index], player.GetInventory(index).WrappedItem);
		}

		[TestCase(-1)]
		[TestCase(100000)]
		public void GetInventory_ParamOutOfRange_ThrowsArgumentOutOfRangeException(int index)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.Throws<IndexOutOfRangeException>(() => player.GetInventory(index));
		}

		[TestCase(1)]
		public void GetSelectedItem_IsCorrect(int selectedItem)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.selectedItem = selectedItem;
			terrariaPlayer.inventory[selectedItem] = new Terraria.Item();

			Assert.AreEqual(terrariaPlayer.inventory[selectedItem], player.GetSelectedItem().WrappedItem);
		}

		[TestCase(1)]
		public void SetInventory_Updates(int index)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);
			var item = new Item(new Terraria.Item());

			player.SetInventory(index, item);

			Assert.AreSame(item.WrappedItem, terrariaPlayer.inventory[index]);
		}

		[TestCase(-1)]
		[TestCase(100000)]
		public void SetInventory_OutOfRange_ThrowsArgumentOutOfRangeException(int index)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.Throws<IndexOutOfRangeException>(() => player.SetInventory(index, null));
		}
	}
}
