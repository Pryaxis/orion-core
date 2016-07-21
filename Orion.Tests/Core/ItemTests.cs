using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Core;

namespace Orion.Tests.Core
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

		[TestCase(100)]
		public void GetDamage_IsCorrect(int damage)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.damage = damage;

			Assert.AreEqual(damage, item.Damage);
		}

		[TestCase(999)]
		public void GetMaxStack_IsCorrect(int maxStack)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.maxStack = maxStack;

			Assert.AreEqual(maxStack, item.MaxStack);
		}

		[TestCase("Name")]
		public void GetName_IsCorrect(string name)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.name = name;

			Assert.AreEqual(name, item.Name);
		}

		private static readonly Vector2[] Positions = {Vector2.One};

		[Test, TestCaseSource(nameof(Positions))]
		public void GetPosition_IsCorrect(Vector2 position)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.position = position;

			Assert.AreEqual(position, item.Position);
		}

		[Test, TestCaseSource(nameof(Positions))]
		public void SetPosition_Updates(Vector2 position)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.Position = position;

			Assert.AreEqual(position, terrariaItem.position);
		}
		
		[TestCase(81)]
		public void GetPrefix_IsCorrect(byte prefix)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.prefix = prefix;

			Assert.AreEqual(prefix, item.Prefix);
		}

		[TestCase(81)]
		public void SetPrefix_Updates(byte prefix)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.Prefix = prefix;

			Assert.AreEqual(prefix, terrariaItem.prefix);
		}

		[TestCase(99)]
		public void GetStack_IsCorrect(int stack)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.stack = stack;

			Assert.AreEqual(stack, item.Stack);
		}

		[TestCase(99)]
		public void SetStack_Updates(int stack)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.Stack = stack;

			Assert.AreEqual(stack, terrariaItem.stack);
		}

		[TestCase(149)]
		public void GetType_IsCorrect(int type)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.netID = type;

			Assert.AreEqual(type, item.Type);
		}

		private static readonly Vector2[] Velocities = {Vector2.One};

		[Test, TestCaseSource(nameof(Velocities))]
		public void GetVelocity_IsCorrect(Vector2 velocity)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			terrariaItem.velocity = velocity;

			Assert.AreEqual(velocity, item.Velocity);
		}

		[Test, TestCaseSource(nameof(Velocities))]
		public void SetVelocity_Updates(Vector2 velocity)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.Velocity = velocity;

			Assert.AreEqual(velocity, terrariaItem.velocity);
		}
	}
}
