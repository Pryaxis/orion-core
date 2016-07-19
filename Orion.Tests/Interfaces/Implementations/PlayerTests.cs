using NUnit.Framework;
using Player = Orion.Interfaces.Implementations.Player;

namespace Orion.Tests.Interfaces.Implementations
{
	[TestFixture]
	public class PlayerTests
	{
		[Test]
		public void GetBacking_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.AreSame(terrariaPlayer, player.Backing);
		}

		[Test]
		public void GetDefense_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.statDefense = 100;

			Assert.AreEqual(100, player.Defense);
		}

		[Test]
		public void GetHP_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.statLife = 200;

			Assert.AreEqual(200, player.HP);
		}

		[Test]
		public void SetHP_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.HP = 200;

			Assert.AreEqual(200, terrariaPlayer.statLife);
		}

		[Test]
		public void GetInventory_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.inventory = new Terraria.Item[59];
			terrariaPlayer.inventory[0] = new Terraria.Item();

			Assert.AreSame(terrariaPlayer.inventory, player.Inventory.Backing);
		}

		[Test]
		public void GetMaxHP_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.statLifeMax = 400;

			Assert.AreEqual(400, player.MaxHP);
		}

		[Test]
		public void SetMaxHP_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.MaxHP = 400;

			Assert.AreEqual(400, terrariaPlayer.statLifeMax);
		}

		[Test]
		public void GetMaxMP_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.statManaMax = 200;

			Assert.AreEqual(200, player.MaxMP);
		}

		[Test]
		public void SetMaxMP_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.MaxMP = 200;

			Assert.AreEqual(200, terrariaPlayer.statManaMax);
		}

		[Test]
		public void GetMP_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.statMana = 100;

			Assert.AreEqual(100, player.MP);
		}

		[Test]
		public void SetMP_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.MP = 100;

			Assert.AreEqual(100, terrariaPlayer.statMana);
		}

		[Test]
		public void SelectedItem_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var terrariaItem = new Terraria.Item();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.selectedItem = 1;
			terrariaPlayer.inventory[1] = terrariaItem;

			Assert.AreEqual(terrariaItem, player.SelectedItem.Backing);
		}
	}
}
