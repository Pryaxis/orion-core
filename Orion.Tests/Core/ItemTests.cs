using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Entities.Item;

namespace Orion.Tests.Core
{
	[TestFixture]
	public class ItemTests
	{
		private static readonly object[] GetWrappers =
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
			new object[] {nameof(Item.Prefix), nameof(Terraria.Item.prefix), (byte)83},
			new object[] {nameof(Item.ProjectileSpeed), nameof(Terraria.Item.shootSpeed), 1.0f},
			new object[] {nameof(Item.ProjectileType), nameof(Terraria.Item.shoot), 1},
			new object[] {nameof(Item.Scale), nameof(Terraria.Item.scale), 10.0f},
			new object[] {nameof(Item.StackSize), nameof(Terraria.Item.stack), 999},
			new object[] {nameof(Item.Type), nameof(Terraria.Item.netID), 1},
			new object[] {nameof(Item.UseAmmoType), nameof(Terraria.Item.useAmmo), 14},
			new object[] {nameof(Item.UseAnimationTime), nameof(Terraria.Item.useAnimation), 4},
			new object[] {nameof(Item.UseTime), nameof(Terraria.Item.useTime), 4},
			new object[] {nameof(Item.Velocity), nameof(Terraria.Item.velocity), Vector2.One},
			new object[] {nameof(Item.Width), nameof(Terraria.Item.width), 100}
		};

		private static readonly object[] SetWrappers =
		{
			new object[] {nameof(Item.AmmoType), nameof(Terraria.Item.ammo), 14},
			new object[] {nameof(Item.Color), nameof(Terraria.Item.color), Color.White},
			new object[] {nameof(Item.Damage), nameof(Terraria.Item.damage), 100},
			new object[] {nameof(Item.Height), nameof(Terraria.Item.height), 100},
			new object[] {nameof(Item.Knockback), nameof(Terraria.Item.knockBack), 1.0f},
			new object[] {nameof(Item.Position), nameof(Terraria.Item.position), Vector2.One},
			new object[] {nameof(Item.ProjectileSpeed), nameof(Terraria.Item.shootSpeed), 1.0f},
			new object[] {nameof(Item.ProjectileType), nameof(Terraria.Item.shoot), 1},
			new object[] {nameof(Item.Scale), nameof(Terraria.Item.scale), 10.0f},
			new object[] {nameof(Item.UseAmmoType), nameof(Terraria.Item.useAmmo), 14},
			new object[] {nameof(Item.UseAnimationTime), nameof(Terraria.Item.useAnimation), 4},
			new object[] {nameof(Item.UseTime), nameof(Terraria.Item.useTime), 4},
			new object[] {nameof(Item.Velocity), nameof(Terraria.Item.velocity), Vector2.One},
			new object[] {nameof(Item.Width), nameof(Terraria.Item.width), 100}
		};

		[Test]
		public void Constructor_NullItem_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Item(null));
		}

		[TestCaseSource(nameof(GetWrappers))]
		public void GetProperty_IsCorrect(string itemPropertyName, string terrariaItemFieldName, object value)
		{
			var terrariaItem = new Terraria.Item();
			FieldInfo terrariaItemField = typeof(Terraria.Item).GetField(terrariaItemFieldName);
			var item = new Item(terrariaItem);
			PropertyInfo itemProperty = typeof(Item).GetProperty(itemPropertyName);

			terrariaItemField.SetValue(terrariaItem, Convert.ChangeType(value, terrariaItemField.FieldType));

			Assert.AreEqual(value, itemProperty.GetValue(item));
		}

		[TestCaseSource(nameof(SetWrappers))]
		public void SetProperty_IsCorrect(string itemPropertyName, string terrariaItemFieldName, object value)
		{
			var terrariaItem = new Terraria.Item();
			FieldInfo terrariaItemField = typeof(Terraria.Item).GetField(terrariaItemFieldName);
			var item = new Item(terrariaItem);
			PropertyInfo itemProperty = typeof(Item).GetProperty(itemPropertyName);

			itemProperty.SetValue(item, Convert.ChangeType(value, itemProperty.PropertyType));

			Assert.AreEqual(value, terrariaItemField.GetValue(terrariaItem));
		}

		[TestCase(1)]
		public void SetStackSize_IsCorrect(int stackSize)
		{
			var terrariaItem = new Terraria.Item();
			terrariaItem.SetDefaults(1);
			var item = new Item(terrariaItem);

			item.StackSize = stackSize;

			Assert.AreEqual(stackSize, terrariaItem.stack);
		}

		[TestCase(-1)]
		public void SetStackSize_InvalidStackSize_ThrowsArgumentOutOfRangeException(int stackSize)
		{
			var terrariaItem = new Terraria.Item();
			terrariaItem.SetDefaults(1);
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => item.StackSize = stackSize);
		}

		[Test]
		public void GetWrappedItem_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.AreSame(terrariaItem, item.WrappedItem);
		}

		[TestCase(1)]
		public void SetDefaults_IsCorrect(int type)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.SetDefaults(type);

			Assert.AreEqual(type, item.Type);
		}

		[TestCase(-1)]
		[TestCase(100000)]
		public void SetDefaults_InvalidType_ThrowsArgumentOutOfRangeException(int type)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => item.SetDefaults(type));
		}

		[TestCase(1)]
		public void SetPrefix_IsCorrect(int prefix)
		{
			var terrariaItem = new Terraria.Item();
			terrariaItem.SetDefaults(1);
			var item = new Item(terrariaItem);

			item.SetPrefix(prefix);

			Assert.IsTrue(item.Prefix > 0, "Some prefix should have been set.");
		}

		[TestCase(-1)]
		[TestCase(100000)]
		public void SetPrefix_InvalidPrefix_ThrowsArgumentOutOfRangeException(int prefix)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => item.SetPrefix(prefix));
		}
	}
}
