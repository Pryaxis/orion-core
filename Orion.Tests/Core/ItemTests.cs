using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Core;

namespace Orion.Tests.Core
{
	[TestFixture]
	public class ItemTests
	{
		private static readonly object[] GetProperties =
		{
			new object[] {nameof(Item.AxePower), nameof(Terraria.Item.axe), 100},
			new object[] {nameof(Item.Damage), nameof(Terraria.Item.damage), 100},
			new object[] {nameof(Item.MaxStackSize), nameof(Terraria.Item.maxStack), 999},
			new object[] {nameof(Item.HammerPower), nameof(Terraria.Item.hammer), 100},
			new object[] {nameof(Item.Name), nameof(Terraria.Item.name), "TEST"},
			new object[] {nameof(Item.PickaxePower), nameof(Terraria.Item.pick), 100},
			new object[] {nameof(Item.Position), nameof(Terraria.Item.position), Vector2.One},
			new object[] {nameof(Item.Prefix), nameof(Terraria.Item.prefix), (byte)83},
			new object[] {nameof(Item.Projectile), nameof(Terraria.Item.shoot), 1},
			new object[] {nameof(Item.StackSize), nameof(Terraria.Item.stack), 999},
			new object[] {nameof(Item.Type), nameof(Terraria.Item.netID), 1},
			new object[] {nameof(Item.Velocity), nameof(Terraria.Item.velocity), Vector2.One},
		};

		private static readonly object[] SetProperties =
		{
			new object[] {nameof(Item.Prefix), nameof(Terraria.Item.prefix), (byte)83},
			new object[] {nameof(Item.StackSize), nameof(Terraria.Item.stack), 999},
			new object[] {nameof(Item.Type), nameof(Terraria.Item.netID), 1},
		};

		[Test]
		public void Constructor_Null_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => new Item(null));
		}

		[TestCaseSource(nameof(GetProperties))]
		public void GetProperty_IsCorrect(string itemPropertyName, string terrariaItemFieldName, object value)
		{
			var terrariaItem = new Terraria.Item();
			FieldInfo terrariaItemField = typeof(Terraria.Item).GetField(terrariaItemFieldName);
			var item = new Item(terrariaItem);
			PropertyInfo itemProperty = typeof(Item).GetProperty(itemPropertyName);

			terrariaItemField.SetValue(terrariaItem, Convert.ChangeType(value, terrariaItemField.FieldType));

			Assert.AreEqual(terrariaItemField.GetValue(terrariaItem), itemProperty.GetValue(item));
		}

		[TestCaseSource(nameof(SetProperties))]
		public void SetProperty_IsCorrect(string itemPropertyName, string terrariaItemFieldName, object value)
		{
			var terrariaItem = new Terraria.Item();
			FieldInfo terrariaItemField = typeof(Terraria.Item).GetField(terrariaItemFieldName);
			var item = new Item(terrariaItem);
			PropertyInfo itemProperty = typeof(Item).GetProperty(itemPropertyName);

			itemProperty.SetValue(item, Convert.ChangeType(value, itemProperty.PropertyType));

			Assert.AreEqual(itemProperty.GetValue(item), terrariaItemField.GetValue(terrariaItem));

		}

		[Test]
		public void GetWrappedItem_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.AreSame(terrariaItem, item.WrappedItem);
		}
	}
}
