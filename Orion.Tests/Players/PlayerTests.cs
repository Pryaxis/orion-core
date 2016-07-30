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
		private static readonly object[] GetPropertyTestCases =
		{
			new object[] {nameof(Player.Defense), nameof(Terraria.Player.statDefense), 100},
			new object[] {nameof(Player.Health), nameof(Terraria.Player.statLife), 100},
			new object[] {nameof(Player.MaxHealth), nameof(Terraria.Player.statLifeMax), 100},
			new object[] {nameof(Player.Mana), nameof(Terraria.Player.statMana), 100},
			new object[] {nameof(Player.MaxMana), nameof(Terraria.Player.statManaMax), 100},
			new object[] {nameof(Player.Name), nameof(Terraria.Player.name), "TEST"},
			new object[] {nameof(Player.Position), nameof(Terraria.Player.position), Vector2.One},
		};

		private static readonly object[] SetPropertyTestCases =
		{
			new object[] {nameof(Player.Health), nameof(Terraria.Player.statLife), 100},
			new object[] {nameof(Player.MaxHealth), nameof(Terraria.Player.statLifeMax), 100},
			new object[] {nameof(Player.Mana), nameof(Terraria.Player.statMana), 100},
			new object[] {nameof(Player.MaxMana), nameof(Terraria.Player.statManaMax), 100},
			new object[] {nameof(Player.Position), nameof(Terraria.Player.position), Vector2.One},
			new object[] {nameof(Player.Velocity), nameof(Terraria.Player.velocity), Vector2.One}
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

			playerProperty.SetValue(player, Convert.ChangeType(value, playerProperty.PropertyType));

			Assert.AreEqual(value, terrariaPlayerField.GetValue(terrariaPlayer));
		}

		[Test]
		public void GetDyes_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			IItemArray dyes = player.Dyes;

			Assert.AreSame(terrariaPlayer.dye, dyes.WrappedItemArray);
		}

		[Test]
		public void GetEquips_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			IItemArray equips = player.Equips;

			Assert.AreSame(terrariaPlayer.armor, equips.WrappedItemArray);
		}

		[TestCase(-1)]
		public void SetHealth_NegativeValue_ThrowsArgumentOutOfRangeException(int health)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.Throws<ArgumentOutOfRangeException>(() => player.Health = health);
		}

		[Test]
		public void GetInventory_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			IItemArray inventory = player.Inventory;

			Assert.AreSame(terrariaPlayer.inventory, inventory.WrappedItemArray);
		}

		[TestCase(-1)]
		public void SetMana_NegativeValue_ThrowsArgumentOutOfRangeException(int mana)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.Throws<ArgumentOutOfRangeException>(() => player.Mana = mana);
		}

		[TestCase(-1)]
		public void SetMaxHealth_NegativeValue_ThrowsArgumentOutOfRangeException(int maxHealth)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.Throws<ArgumentOutOfRangeException>(() => player.MaxHealth = maxHealth);
		}

		[TestCase(-1)]
		public void SetMaxMana_NegativeValue_ThrowsArgumentOutOfRangeException(int maxMana)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.Throws<ArgumentOutOfRangeException>(() => player.MaxMana = maxMana);
		}

		[Test]
		public void GetMiscDyes_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			IItemArray miscDyes = player.MiscDyes;

			Assert.AreSame(terrariaPlayer.miscDyes, miscDyes.WrappedItemArray);
		}

		[Test]
		public void GetMiscEquips_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			IItemArray miscEquips = player.MiscEquips;

			Assert.AreSame(terrariaPlayer.miscEquips, miscEquips.WrappedItemArray);
		}

		[Test]
		public void GetPiggyBank_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			IItemArray piggyBank = player.PiggyBank;

			Assert.AreSame(terrariaPlayer.bank.item, piggyBank.WrappedItemArray);
		}

		[TestCase(1)]
		public void GetSelectedItem_IsCorrect(int selectedItem)
		{
			var terrariaPlayer = new Terraria.Player();
			terrariaPlayer.inventory[selectedItem] = new Terraria.Item();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.selectedItem = selectedItem;

			Assert.AreEqual(terrariaPlayer.inventory[selectedItem], player.SelectedItem.WrappedItem);
		}

		[TestCase(1)]
		public void GetSelectedItem_MultipleTimes_ReturnsSameInstance(int selectedItem)
		{
			var terrariaPlayer = new Terraria.Player();
			terrariaPlayer.inventory[selectedItem] = new Terraria.Item();
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

			IItemArray safe = player.Safe;

			Assert.AreSame(terrariaPlayer.bank2.item, safe.WrappedItemArray);
		}

		[Test]
		public void GetTrashItem_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var terrariaItem = new Terraria.Item();
			terrariaPlayer.trashItem = terrariaItem;
			var player = new Player(terrariaPlayer);

			IItem trashItem = player.TrashItem;

			Assert.AreEqual(terrariaItem, trashItem.WrappedItem);
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

			Assert.AreEqual(item, player.TrashItem);
			Assert.AreEqual(terrariaPlayer.trashItem, item.WrappedItem);
		}

		[Test]
		public void SetTrashItem_NullValue_ThrowsArgumentNullException()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.Throws<ArgumentNullException>(() => player.TrashItem = null);
		}
	}
}
