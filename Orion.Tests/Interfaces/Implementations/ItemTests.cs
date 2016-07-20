using System;
using NUnit.Framework;
using Orion.Data;

namespace Orion.Tests.Interfaces.Implementations
{
	[TestFixture]
	public class ItemTests : EntityTests
	{
		protected override void GetEntities(out Terraria.Entity terrariaEntity, out Entity entity)
		{
			var terrariaItem = new Terraria.Item();
			terrariaEntity = terrariaItem;
			entity = Item.Wrap(terrariaItem);
		}

		[Test]
		public void GetBacking_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			Item item = Item.Wrap(terrariaItem);

			Assert.AreSame(terrariaItem, item.WrappedItem);
		}

		[Test]
		public void GetDamage_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			Item item = Item.Wrap(terrariaItem);

			terrariaItem.damage = 100;

			Assert.AreEqual(100, item.Damage);
		}

		[Test]
		public void GetMaxStack_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			Item item = Item.Wrap(terrariaItem);

			terrariaItem.maxStack = 999;

			Assert.AreEqual(999, item.MaxStack);
		}

		[Test]
		public void GetPrefix_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			Item item = Item.Wrap(terrariaItem);

			terrariaItem.prefix = 81;

			Assert.AreEqual(81, item.Prefix);
		}

		[Test]
		public void SetPrefix_Updates()
		{
			var terrariaItem = new Terraria.Item();
			Item item = Item.Wrap(terrariaItem);

			item.Prefix = 81;

			Assert.AreEqual(81, terrariaItem.prefix);
		}

		[Test]
		public void GetStack_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			Item item = Item.Wrap(terrariaItem);

			terrariaItem.stack = 99;

			Assert.AreEqual(99, item.Stack);
		}

		[Test]
		public void SetStack_Updates()
		{
			var terrariaItem = new Terraria.Item();
			Item item = Item.Wrap(terrariaItem);

			item.Stack = 99;

			Assert.AreEqual(99, terrariaItem.stack);
		}

		[Test]
		public void GetType_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			Item item = Item.Wrap(terrariaItem);

			terrariaItem.netID = 149;

			Assert.AreEqual(149, item.Type);
		}

		[Test]
		public void Wrap_Null_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => Item.Wrap(null));
		}

		[Test]
		public void Wrap_ReturnsSameInstance()
		{
			var terrariaItem = new Terraria.Item();

			Item item1 = Item.Wrap(terrariaItem);
			Item item2 = Item.Wrap(terrariaItem);

			Assert.AreSame(item1, item2);
		}
	}
}
