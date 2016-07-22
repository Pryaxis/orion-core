using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Core;

namespace Orion.Tests.Core
{
	[TestFixture]
	public class PlayerTests
	{
		[TestCase(100)]
		public void GetDefense_IsCorrect(int defense)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.statDefense = defense;

			Assert.AreEqual(defense, player.Defense);
		}

		[TestCase(200)]
		public void GetHP_IsCorrect(int hp)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.statLife = hp;

			Assert.AreEqual(hp, player.HP);
		}

		[TestCase(200)]
		public void SetHP_Updates(int hp)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.HP = hp;

			Assert.AreEqual(hp, terrariaPlayer.statLife);
		}

		[TestCase(400)]
		public void GetMaxHP_IsCorrect(int maxHP)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.statLifeMax = maxHP;

			Assert.AreEqual(maxHP, player.MaxHP);
		}

		[TestCase(400)]
		public void SetMaxHP_Updates(int maxHP)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.MaxHP = maxHP;

			Assert.AreEqual(maxHP, terrariaPlayer.statLifeMax);
		}

		[TestCase(200)]
		public void GetMaxMP_IsCorrect(int maxMP)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.statManaMax = maxMP;

			Assert.AreEqual(maxMP, player.MaxMP);
		}

		[TestCase(200)]
		public void SetMaxMP_Updates(int maxMP)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.MaxMP = maxMP;

			Assert.AreEqual(maxMP, terrariaPlayer.statManaMax);
		}

		[TestCase(100)]
		public void GetMP_IsCorrect(int mp)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.statMana = mp;

			Assert.AreEqual(mp, player.MP);
		}

		[TestCase(100)]
		public void SetMP_Updates(int mp)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.MP = mp;

			Assert.AreEqual(mp, terrariaPlayer.statMana);
		}

		[TestCase("Name")]
		public void GetName_IsCorrect(string name)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.name = name;

			Assert.AreEqual(name, player.Name);
		}

		private static readonly object[] Positions = {Vector2.One};

		[Test, TestCaseSource(nameof(Positions))]
		public void GetPosition_IsCorrect(Vector2 position)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.position = position;

			Assert.AreEqual(position, player.Position);
		}

		[Test, TestCaseSource(nameof(Positions))]
		public void SetPosition_Updates(Vector2 position)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.Position = position;

			Assert.AreEqual(position, terrariaPlayer.position);
		}

		private static readonly object[] Velocities = {Vector2.One};

		[Test, TestCaseSource(nameof(Velocities))]
		public void GetVelocity_IsCorrect(Vector2 velocity)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			terrariaPlayer.velocity = velocity;

			Assert.AreEqual(velocity, player.Velocity);
		}

		[Test, TestCaseSource(nameof(Velocities))]
		public void SetVelocity_Updates(Vector2 velocity)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			player.Velocity = velocity;

			Assert.AreEqual(velocity, terrariaPlayer.velocity);
		}

		[Test]
		public void GetWrappedPlayer_IsCorrect()
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.AreSame(terrariaPlayer, player.WrappedPlayer);
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
		[TestCase(100)]
		public void GetInventory_ParamOutOfRange_ThrowsException(int index)
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
		[TestCase(100)]
		public void SetInventory_OutOfRange_ThrowsException(int index)
		{
			var terrariaPlayer = new Terraria.Player();
			var player = new Player(terrariaPlayer);

			Assert.Throws<IndexOutOfRangeException>(() => player.SetInventory(index, null));
		}
	}
}
