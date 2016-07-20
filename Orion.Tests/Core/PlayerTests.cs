using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Core;

namespace Orion.Tests.Core
{
	[TestFixture]
	public class PlayerTests
	{
		[Test]
		public void GetBacking_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.AreSame(terrariaPlayer, player.WrappedPlayer);
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
		public void GetName_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.name = "Name";

			Assert.AreEqual("Name", player.Name);
		}

		[Test]
		public void GetPosition_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.position = Vector2.One;

			Assert.AreEqual(Vector2.One, player.Position);
		}

		[Test]
		public void SetPosition_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.Position = Vector2.One;

			Assert.AreEqual(Vector2.One, terrariaPlayer.position);
		}

		[Test]
		public void GetVelocity_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.velocity = Vector2.One;

			Assert.AreEqual(Vector2.One, player.Velocity);
		}

		[Test]
		public void SetVelocity_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.Velocity = Vector2.One;

			Assert.AreEqual(Vector2.One, terrariaPlayer.velocity);
		}

		[Test]
		public void GetInventory_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.inventory[1] = new Terraria.Item();

			Assert.AreSame(terrariaPlayer.inventory[1], player.GetInventory(1).WrappedItem);
		}

		[Test]
		public void GetInventory_OutOfRange_ThrowsException()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.Throws<IndexOutOfRangeException>(() => player.GetInventory(-1));
		}

		[Test]
		public void GetSelectedItem_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.selectedItem = 1;
			terrariaPlayer.inventory[1] = new Terraria.Item();

			Assert.AreEqual(terrariaPlayer.inventory[1], player.GetSelectedItem().WrappedItem);
		}

		[Test]
		public void SetInventory_Updates()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);
			var item = new Item(new Terraria.Item());

			player.SetInventory(1, item);

			Assert.AreSame(item.WrappedItem, terrariaPlayer.inventory[1]);
		}

		[Test]
		public void SetInventory_OutOfRange_ThrowsException()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.Throws<IndexOutOfRangeException>(() => player.SetInventory(-1, null));
		}
	}
}
