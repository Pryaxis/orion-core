using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Items;
using Orion.Players;

namespace Orion.Tests.Players
{
	[TestFixture]
	public class PlayerTests
	{
		public static readonly object[] GetPropertyTestCases =
		{
			new object[] {nameof(Player.Defense), nameof(Terraria.Player.statDefense), 100},
			new object[] {nameof(Player.HasPvpEnabled), nameof(Terraria.Player.hostile), true},
			new object[] {nameof(Player.Health), nameof(Terraria.Player.statLife), 100},
			new object[] {nameof(Player.Height), nameof(Terraria.Player.height), 100},
			new object[] {nameof(Player.IsDead), nameof(Terraria.Player.dead), true},
			new object[] {nameof(Player.MagicCritBonus), nameof(Terraria.Player.magicCrit), 100},
			new object[] {nameof(Player.MagicDamageMultiplier), nameof(Terraria.Player.magicDamage), 2.0f},
			new object[] {nameof(Player.ManaCostMultiplier), nameof(Terraria.Player.manaCost), 2.0f},
			new object[] {nameof(Player.MaxMinions), nameof(Terraria.Player.maxMinions), 100},
			new object[] {nameof(Player.MaxHealth), nameof(Terraria.Player.statLifeMax), 100},
			new object[] {nameof(Player.Mana), nameof(Terraria.Player.statMana), 100},
			new object[] {nameof(Player.MaxMana), nameof(Terraria.Player.statManaMax), 100},
			new object[] {nameof(Player.MeleeCritBonus), nameof(Terraria.Player.meleeCrit), 100},
			new object[] {nameof(Player.MeleeDamageMultiplier), nameof(Terraria.Player.meleeDamage), 2.0f},
			new object[] {nameof(Player.MinionDamageMultiplier), nameof(Terraria.Player.minionDamage), 2.0f},
			new object[] {nameof(Player.MovementSpeed), nameof(Terraria.Player.moveSpeed), 2.0f},
			new object[] {nameof(Player.Name), nameof(Terraria.Player.name), "TEST"},
			new object[] {nameof(Player.Position), nameof(Terraria.Player.position), Vector2.One},
			new object[] {nameof(Player.RangedCritBonus), nameof(Terraria.Player.rangedCrit), 100},
			new object[] {nameof(Player.RangedDamageMultiplier), nameof(Terraria.Player.rangedDamage), 2.0f},
			new object[] {nameof(Player.Team), nameof(Terraria.Player.team), Team.Red},
			new object[] {nameof(Player.ThrownCritBonus), nameof(Terraria.Player.thrownCrit), 100},
			new object[] {nameof(Player.ThrownDamageMultiplier), nameof(Terraria.Player.thrownDamage), 2.0f},
			new object[] {nameof(Player.Velocity), nameof(Terraria.Player.velocity), Vector2.One},
			new object[] {nameof(Player.Width), nameof(Terraria.Player.width), 100}
		};

		public static readonly object[] SetPropertyTestCases =
		{
			new object[] {nameof(Player.Defense), nameof(Terraria.Player.statDefense), 100},
			new object[] {nameof(Player.HasPvpEnabled), nameof(Terraria.Player.hostile), true},
			new object[] {nameof(Player.Health), nameof(Terraria.Player.statLife), 100},
			new object[] {nameof(Player.Height), nameof(Terraria.Player.height), 100},
			new object[] {nameof(Player.IsDead), nameof(Terraria.Player.dead), true},
			new object[] {nameof(Player.MagicCritBonus), nameof(Terraria.Player.magicCrit), 100},
			new object[] {nameof(Player.MagicDamageMultiplier), nameof(Terraria.Player.magicDamage), 2.0f},
			new object[] {nameof(Player.ManaCostMultiplier), nameof(Terraria.Player.manaCost), 2.0f},
			new object[] {nameof(Player.MaxMinions), nameof(Terraria.Player.maxMinions), 100},
			new object[] {nameof(Player.MaxHealth), nameof(Terraria.Player.statLifeMax), 100},
			new object[] {nameof(Player.Mana), nameof(Terraria.Player.statMana), 100},
			new object[] {nameof(Player.MaxMana), nameof(Terraria.Player.statManaMax), 100},
			new object[] {nameof(Player.MeleeCritBonus), nameof(Terraria.Player.meleeCrit), 100},
			new object[] {nameof(Player.MeleeDamageMultiplier), nameof(Terraria.Player.meleeDamage), 2.0f},
			new object[] {nameof(Player.MinionDamageMultiplier), nameof(Terraria.Player.minionDamage), 2.0f},
			new object[] {nameof(Player.MovementSpeed), nameof(Terraria.Player.moveSpeed), 2.0f},
			new object[] {nameof(Player.Name), nameof(Terraria.Player.name), "TEST"},
			new object[] {nameof(Player.Position), nameof(Terraria.Player.position), Vector2.One},
			new object[] {nameof(Player.RangedCritBonus), nameof(Terraria.Player.rangedCrit), 100},
			new object[] {nameof(Player.RangedDamageMultiplier), nameof(Terraria.Player.rangedDamage), 2.0f},
			new object[] {nameof(Player.Team), nameof(Terraria.Player.team), Team.Red},
			new object[] {nameof(Player.ThrownCritBonus), nameof(Terraria.Player.thrownCrit), 100},
			new object[] {nameof(Player.ThrownDamageMultiplier), nameof(Terraria.Player.thrownDamage), 2.0f},
			new object[] {nameof(Player.Velocity), nameof(Terraria.Player.velocity), Vector2.One},
			new object[] {nameof(Player.Width), nameof(Terraria.Player.width), 100}
		};

		[Test]
		public void Constructor_NullTerrariaPlayer_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Player(null));
		}

		[TestCaseSource(nameof(GetPropertyTestCases))]
		public void GetProperty_IsCorrect(string playerPropertyName, string terrariaPlayerFieldName, object value)
		{
			var terrariaPlayer = new Terraria.Player();
			FieldInfo terrariaPlayerField = typeof(Terraria.Player).GetField(terrariaPlayerFieldName);
			terrariaPlayerField.SetValue(terrariaPlayer, Convert.ChangeType(value, terrariaPlayerField.FieldType));
			var player = new Player(terrariaPlayer);
			PropertyInfo playerProperty = typeof(Player).GetProperty(playerPropertyName);

			object actualValue = playerProperty.GetValue(player);

			Assert.AreEqual(value, actualValue);
		}

		[TestCaseSource(nameof(SetPropertyTestCases))]
		public void SetProperty_IsCorrect(string playerPropertyName, string terrariaPlayerFieldName, object value)
		{
			var terrariaPlayer = new Terraria.Player();
			FieldInfo terrariaPlayerField = typeof(Terraria.Player).GetField(terrariaPlayerFieldName);
			var player = new Player(terrariaPlayer);
			PropertyInfo playerProperty = typeof(Player).GetProperty(playerPropertyName);

			playerProperty.SetValue(player, value);

			Assert.AreEqual(
				Convert.ChangeType(value, terrariaPlayerField.FieldType),
				terrariaPlayerField.GetValue(terrariaPlayer));
		}

		[TestCase(nameof(Player.Dyes), nameof(Terraria.Player.dye))]
		[TestCase(nameof(Player.Equips), nameof(Terraria.Player.armor))]
		[TestCase(nameof(Player.Inventory), nameof(Terraria.Player.inventory))]
		[TestCase(nameof(Player.MiscDyes), nameof(Terraria.Player.miscDyes))]
		[TestCase(nameof(Player.MiscEquips), nameof(Terraria.Player.miscEquips))]
		public void GetItemArray_IsCorrect(string playerPropertyName, string terrariaPlayerFieldName)
		{
			var terrariaPlayer = new Terraria.Player();
			FieldInfo terrariaPlayerField = typeof(Terraria.Player).GetField(terrariaPlayerFieldName);
			var player = new Player(terrariaPlayer);
			PropertyInfo playerProperty = typeof(Player).GetProperty(playerPropertyName);

			var actualArray = (IItemArray)playerProperty.GetValue(player);

			Assert.AreSame(terrariaPlayerField.GetValue(terrariaPlayer), actualArray.WrappedItemArray);
		}
		
		[Test]
		public void GetPiggyBank_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			IItemArray actualPiggyBank = player.PiggyBank;

			Assert.AreSame(terrariaPlayer.bank.item, actualPiggyBank.WrappedItemArray);
		}

		[TestCase(1)]
		public void GetSelectedItem_IsCorrect(int selectedItemIndex)
		{
			var terrariaPlayer = new Terraria.Player { selectedItem = selectedItemIndex };
			terrariaPlayer.inventory[selectedItemIndex] = new Terraria.Item();
			var player = new Player(terrariaPlayer);

			IItem actualItem = player.SelectedItem;

			Assert.AreEqual(terrariaPlayer.inventory[selectedItemIndex], actualItem.WrappedItem);
		}

		[Test]
		public void GetSelectedItem_MultipleTimes_ReturnsSameInstance()
		{
			var terrariaPlayer = new Terraria.Player();
			terrariaPlayer.inventory[0] = new Terraria.Item();
			var player = new Player(terrariaPlayer);

			IItem item1 = player.SelectedItem;
			IItem item2 = player.SelectedItem;
			
			Assert.AreSame(item1, item2);
		}

		[Test]
		public void GetSafe_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			IItemArray actualSafe = player.Safe;

			Assert.AreSame(terrariaPlayer.bank2.item, actualSafe.WrappedItemArray);
		}
		
		[Test]
		public void GetTrashItem_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var terrariaItem = new Terraria.Item();
			terrariaPlayer.trashItem = terrariaItem;
			var player = new Player(terrariaPlayer);

			IItem actualItem = player.TrashItem;

			Assert.AreEqual(terrariaItem, actualItem.WrappedItem);
		}

		[Test]
		public void GetTrashItem_MultipleTimes_ReturnsSameInstance()
		{
			var terrariaPlayer = new Terraria.Player();
			var terrariaItem = new Terraria.Item();
			terrariaPlayer.trashItem = terrariaItem;
			var player = new Player(terrariaPlayer);

			IItem item1 = player.TrashItem;
			IItem item2 = player.TrashItem;

			Assert.AreSame(item1, item2);
		}

		[Test]
		public void SetTrashItem_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var terrariaItem = new Terraria.Item();
			var player = new Player(terrariaPlayer);
			var item = new Item(terrariaItem);

			player.TrashItem = item;

			Assert.AreEqual(terrariaPlayer.trashItem, item.WrappedItem);
		}
	}
}
