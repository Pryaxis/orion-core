using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Data;

namespace Orion.Tests.Data
{
	[TestFixture]
	public class ItemTests
	{
		[Test]
		public void GetBacking_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.AreSame(terrariaItem, item.WrappedItem);
		}

		[Test]
		public void GetDamage_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.damage = 100;

			Assert.AreEqual(100, item.Damage);
		}

		[Test]
		public void GetMaxStack_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.maxStack = 999;

			Assert.AreEqual(999, item.MaxStack);
		}

		[Test]
		public void GetName_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.name = "Name";

			Assert.AreEqual("Name", item.Name);
		}

		[Test]
		public void GetPosition_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.position = Vector2.One;

			Assert.AreEqual(Vector2.One, item.Position);
		}

		[Test]
		public void SetPosition_Updates()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.Position = Vector2.One;

			Assert.AreEqual(Vector2.One, terrariaItem.position);
		}

		[Test]
		public void GetPrefix_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.prefix = 81;

			Assert.AreEqual(81, item.Prefix);
		}

		[Test]
		public void SetPrefix_Updates()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.Prefix = 81;

			Assert.AreEqual(81, terrariaItem.prefix);
		}

		[Test]
		public void GetStack_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.stack = 99;

			Assert.AreEqual(99, item.Stack);
		}

		[Test]
		public void SetStack_Updates()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.Stack = 99;

			Assert.AreEqual(99, terrariaItem.stack);
		}

		[Test]
		public void GetType_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.netID = 149;

			Assert.AreEqual(149, item.Type);
		}

		[Test]
		public void GetVelocity_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.velocity = Vector2.One;

			Assert.AreEqual(Vector2.One, item.Velocity);
		}

		[Test]
		public void SetVelocity_Updates()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.Velocity = Vector2.One;

			Assert.AreEqual(Vector2.One, terrariaItem.velocity);
		}
	}
}
