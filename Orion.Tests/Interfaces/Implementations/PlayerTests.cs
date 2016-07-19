using System;
using NUnit.Framework;
using Orion.Interfaces.Implementations;

namespace Orion.Tests.Interfaces.Implementations
{
	[TestFixture]
	public class PlayerTests : EntityTests
	{
		protected override void GetEntities(out Terraria.Entity terrariaEntity, out Entity entity)
		{
			var terrariaPlayer = new Terraria.Player();
			terrariaEntity = terrariaPlayer;
			entity = Player.Wrap(terrariaPlayer);
		}

		[Test]
		public void GetBacking_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);

			Assert.AreSame(terrariaPlayer, player.WrappedPlayer);
		}

		[Test]
		public void GetDefense_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);

			terrariaPlayer.statDefense = 100;

			Assert.AreEqual(100, player.Defense);
		}

		[Test]
		public void GetHP_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);

			terrariaPlayer.statLife = 200;

			Assert.AreEqual(200, player.HP);
		}

		[Test]
		public void SetHP_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);

			player.HP = 200;

			Assert.AreEqual(200, terrariaPlayer.statLife);
		}

		[Test]
		public void GetInventory_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);
			
			terrariaPlayer.inventory[0] = new Terraria.Item();

			Assert.AreSame(terrariaPlayer.inventory, player.Inventory.WrappedItemArray);
		}

		[Test]
		public void GetMaxHP_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);

			terrariaPlayer.statLifeMax = 400;

			Assert.AreEqual(400, player.MaxHP);
		}

		[Test]
		public void SetMaxHP_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);

			player.MaxHP = 400;

			Assert.AreEqual(400, terrariaPlayer.statLifeMax);
		}

		[Test]
		public void GetMaxMP_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);

			terrariaPlayer.statManaMax = 200;

			Assert.AreEqual(200, player.MaxMP);
		}

		[Test]
		public void SetMaxMP_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);

			player.MaxMP = 200;

			Assert.AreEqual(200, terrariaPlayer.statManaMax);
		}

		[Test]
		public void GetMP_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);

			terrariaPlayer.statMana = 100;

			Assert.AreEqual(100, player.MP);
		}

		[Test]
		public void SetMP_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			Player player = Player.Wrap(terrariaPlayer);

			player.MP = 100;

			Assert.AreEqual(100, terrariaPlayer.statMana);
		}

		[Test]
		public void SelectedItem_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var terrariaItem = new Terraria.Item();
			Player player = Player.Wrap(terrariaPlayer);

			terrariaPlayer.selectedItem = 1;
			terrariaPlayer.inventory[1] = terrariaItem;

			Assert.AreEqual(terrariaItem, player.SelectedItem.WrappedItem);
		}

		[Test]
		public void Wrap_Null_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => Player.Wrap(null));
		}

		[Test]
		public void Wrap_ReturnsSameInstance()
		{
			var terrariaPlayer = new Terraria.Player();

			Player player1 = Player.Wrap(terrariaPlayer);
			Player player2 = Player.Wrap(terrariaPlayer);

			Assert.AreSame(player1, player2);
		}
	}
}
