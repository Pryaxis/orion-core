using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Items;
using Orion.Projectiles;

namespace Orion.Tests.Items
{
	[TestFixture]
	public class ItemTests
	{
		private static readonly object[] GetPropertyTestCases =
		{
			new object[] {nameof(Item.AmmoType), nameof(Terraria.Item.ammo), 14},
			new object[] {nameof(Item.AxePower), nameof(Terraria.Item.axe), 100},
			new object[] {nameof(Item.Color), nameof(Terraria.Item.color), Color.White},
			new object[] {nameof(Item.Damage), nameof(Terraria.Item.damage), 100},
			new object[] {nameof(Item.MaxStackSize), nameof(Terraria.Item.maxStack), 999},
			new object[] {nameof(Item.HammerPower), nameof(Terraria.Item.hammer), 100},
			new object[] {nameof(Item.Height), nameof(Terraria.Item.height), 100},
			new object[] {nameof(Item.Knockback), nameof(Terraria.Item.knockBack), 1.0f},
			new object[] {nameof(Item.Name), nameof(Terraria.Item.name), "TEST"},
			new object[] {nameof(Item.PickaxePower), nameof(Terraria.Item.pick), 100},
			new object[] {nameof(Item.Position), nameof(Terraria.Item.position), Vector2.One},
			new object[] {nameof(Item.ProjectileSpeed), nameof(Terraria.Item.shootSpeed), 1.0f},
			new object[] {nameof(Item.Scale), nameof(Terraria.Item.scale), 10.0f},
			new object[] {nameof(Item.StackSize), nameof(Terraria.Item.stack), 999},
			new object[] {nameof(Item.UseAmmoType), nameof(Terraria.Item.useAmmo), 14},
			new object[] {nameof(Item.UseAnimationTime), nameof(Terraria.Item.useAnimation), 4},
			new object[] {nameof(Item.UseTime), nameof(Terraria.Item.useTime), 4},
			new object[] {nameof(Item.Velocity), nameof(Terraria.Item.velocity), Vector2.One},
			new object[] {nameof(Item.Width), nameof(Terraria.Item.width), 100}
		};

		private static readonly object[] SetPropertyTestCases =
		{
			new object[] {nameof(Item.AmmoType), nameof(Terraria.Item.ammo), 14},
			new object[] {nameof(Item.Color), nameof(Terraria.Item.color), Color.White},
			new object[] {nameof(Item.Damage), nameof(Terraria.Item.damage), 100},
			new object[] {nameof(Item.Height), nameof(Terraria.Item.height), 100},
			new object[] {nameof(Item.Knockback), nameof(Terraria.Item.knockBack), 1.0f},
			new object[] {nameof(Item.Position), nameof(Terraria.Item.position), Vector2.One},
			new object[] {nameof(Item.ProjectileSpeed), nameof(Terraria.Item.shootSpeed), 1.0f},
			new object[] {nameof(Item.Scale), nameof(Terraria.Item.scale), 10.0f},
			new object[] {nameof(Item.StackSize), nameof(Terraria.Item.stack), 1},
			new object[] {nameof(Item.UseAmmoType), nameof(Terraria.Item.useAmmo), 14},
			new object[] {nameof(Item.UseAnimationTime), nameof(Terraria.Item.useAnimation), 4},
			new object[] {nameof(Item.UseTime), nameof(Terraria.Item.useTime), 4},
			new object[] {nameof(Item.Velocity), nameof(Terraria.Item.velocity), Vector2.One},
			new object[] {nameof(Item.Width), nameof(Terraria.Item.width), 100}
		};

		private static readonly object[] GetPrefixTestCases = { ItemPrefix.Large };
		
		private static readonly object[] GetProjectileTypeTestCases = { ProjectileType.WoodenArrowFriendly };

		private static readonly object[] SetProjectileTypeTestCases = { ProjectileType.WoodenArrowFriendly };

		private static readonly object[] GetTypeTestCases = { ItemType.IronPickaxe };

		private static readonly object[] SetDefaultsTestCases = { ItemType.IronPickaxe };

		private static readonly object[] SetPrefixTestCases = { ItemPrefix.Large };

		[Test]
		public void Constructor_NullItem_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Item(null));
		}

		[TestCaseSource(nameof(GetPropertyTestCases))]
		public void GetProperty_IsCorrect(string itemPropertyName, string terrariaItemFieldName, object value)
		{
			var terrariaItem = new Terraria.Item();
			FieldInfo terrariaItemField = typeof(Terraria.Item).GetField(terrariaItemFieldName);
			terrariaItemField.SetValue(terrariaItem, value);
			var item = new Item(terrariaItem);
			PropertyInfo itemProperty = typeof(Item).GetProperty(itemPropertyName);
			
			object actualValue = itemProperty.GetValue(item);

			Assert.AreEqual(value, actualValue);
		}

		[TestCaseSource(nameof(SetPropertyTestCases))]
		public void SetProperty_IsCorrect(string itemPropertyName, string terrariaItemFieldName, object value)
		{
			var terrariaItem = new Terraria.Item();
			FieldInfo terrariaItemField = typeof(Terraria.Item).GetField(terrariaItemFieldName);
			var item = new Item(terrariaItem);
			PropertyInfo itemProperty = typeof(Item).GetProperty(itemPropertyName);

			itemProperty.SetValue(item, value);

			Assert.AreEqual(value, terrariaItemField.GetValue(terrariaItem));
		}

		[TestCase(-1)]
		public void SetDamage_NegativeValue_ThrowsArgumentOutOfRangeException(int damage)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => item.Damage = damage);
		}

		[TestCase(-1)]
		public void SetHeight_NegativeValue_ThrowsArgumentOutOfRangeException(int height)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => item.Height = height);
		}

		[TestCaseSource(nameof(GetPrefixTestCases))]
		public void GetPrefix_IsCorrect(ItemPrefix prefix)
		{
			var terrariaItem = new Terraria.Item { prefix = (byte)prefix };
			var item = new Item(terrariaItem);

			ItemPrefix actualPrefix = item.Prefix;

			Assert.AreEqual(prefix, actualPrefix);
		}

		[TestCaseSource(nameof(GetProjectileTypeTestCases))]
		public void GetProjectileType_IsCorrect(ProjectileType type)
		{
			var terrariaItem = new Terraria.Item {shoot = (int)type};
			var item = new Item(terrariaItem);

			ProjectileType actualType = item.ProjectileType;

			Assert.AreEqual(type, actualType);
		}

		[TestCaseSource(nameof(SetProjectileTypeTestCases))]
		public void SetProjectileType_IsCorrect(ProjectileType type)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.ProjectileType = type;

			Assert.AreEqual(type, (ProjectileType)terrariaItem.shoot);
		}

		[TestCase(-1.0f)]
		public void SetScale_NegativeValue_ThrowsArgumentOutOfRangeException(float scale)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => item.Scale = scale);
		}

		[TestCase(-1)]
		public void SetStackSize_NegativeValue_ThrowsArgumentOutOfRangeException(int stackSize)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => item.StackSize = stackSize);
		}

		[TestCaseSource(nameof(GetTypeTestCases))]
		public void GetType_IsCorrect(ItemType type)
		{
			var terrariaItem = new Terraria.Item {netID = (int)type};
			var item = new Item(terrariaItem);

			ItemType actualType = item.Type;

			Assert.AreEqual(type, actualType);
		}

		[TestCase(-1)]
		public void SetUseAnimationTime_NegativeValue_ThrowsArgumentOutOfRangeException(int useAnimationTime)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => item.UseAnimationTime = useAnimationTime);
		}

		[TestCase(-1)]
		public void SetUseTime_NegativeValue_ThrowsArgumentOutOfRangeException(int useTime)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => item.UseTime = useTime);
		}

		[TestCase(-1)]
		public void SetWidth_NegativeValue_ThrowsArgumentOutOfRangeException(int width)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => item.Width = width);
		}

		[Test]
		public void GetWrappedItem_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.AreSame(terrariaItem, item.WrappedItem);
		}

		[TestCaseSource(nameof(SetDefaultsTestCases))]
		public void SetDefaults_IsCorrect(ItemType type)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.SetDefaults(type);

			Assert.AreEqual(type, item.Type);
		}

		[TestCaseSource(nameof(SetPrefixTestCases))]
		public void SetPrefix_IsCorrect(ItemPrefix prefix)
		{
			var terrariaItem = new Terraria.Item();
			terrariaItem.SetDefaults(1);
			var item = new Item(terrariaItem);

			item.SetPrefix(prefix);

			Assert.AreNotEqual(ItemPrefix.None, item.Prefix, "Some prefix should have been set.");
		}
	}
}
