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
			new object[] {nameof(Item.AnimationStyle), nameof(Terraria.Item.useStyle), AnimationStyle.Swing},
			new object[] {nameof(Item.AnimationTime), nameof(Terraria.Item.useAnimation), 4},
			new object[] {nameof(Item.AxePower), nameof(Terraria.Item.axe), 100},
			new object[] {nameof(Item.BaitPower), nameof(Terraria.Item.bait), 100},
			new object[] {nameof(Item.CanAutoReuse), nameof(Terraria.Item.autoReuse), true},
			new object[] {nameof(Item.Color), nameof(Terraria.Item.color), Color.White},
			new object[] {nameof(Item.Damage), nameof(Terraria.Item.damage), 100},
			new object[] {nameof(Item.FishingPower), nameof(Terraria.Item.fishingPole), 100},
			new object[] {nameof(Item.GraphicalScale), nameof(Terraria.Item.scale), 10.0f},
			new object[] {nameof(Item.HammerPower), nameof(Terraria.Item.hammer), 100},
			new object[] {nameof(Item.Height), nameof(Terraria.Item.height), 100},
			new object[] {nameof(Item.IsAccessory), nameof(Terraria.Item.accessory), true},
			new object[] {nameof(Item.IsMagicWeapon), nameof(Terraria.Item.magic), true},
			new object[] {nameof(Item.IsMeleeWeapon), nameof(Terraria.Item.melee), true},
			new object[] {nameof(Item.IsRangedWeapon), nameof(Terraria.Item.ranged), true},
			new object[] {nameof(Item.IsThrownWeapon), nameof(Terraria.Item.thrown), true},
			new object[] {nameof(Item.Knockback), nameof(Terraria.Item.knockBack), 1.0f},
			new object[] {nameof(Item.ManaCost), nameof(Terraria.Item.mana), 100},
			new object[] {nameof(Item.MaxStackSize), nameof(Terraria.Item.maxStack), 999},
			new object[] {nameof(Item.Name), nameof(Terraria.Item.Name), "TEST"},
			new object[] {nameof(Item.PickaxePower), nameof(Terraria.Item.pick), 100},
			new object[] {nameof(Item.Position), nameof(Terraria.Item.position), Vector2.One},
			new object[] {nameof(Item.Prefix), nameof(Terraria.Item.prefix), Prefix.Legendary},
			new object[] {nameof(Item.ProjectileSpeed), nameof(Terraria.Item.shootSpeed), 1.0f},
			new object[] {nameof(Item.ProjectileType), nameof(Terraria.Item.shoot), ProjectileType.Amarok},
			new object[] {nameof(Item.Rarity), nameof(Terraria.Item.rare), Rarity.Purple},
			new object[] {nameof(Item.StackSize), nameof(Terraria.Item.stack), 999},
			new object[] {nameof(Item.Type), nameof(Terraria.Item.netID), ItemType.IronPickaxe},
			new object[] {nameof(Item.UseAmmoType), nameof(Terraria.Item.useAmmo), 14},
			new object[] {nameof(Item.UseTime), nameof(Terraria.Item.useTime), 4},
			new object[] {nameof(Item.Velocity), nameof(Terraria.Item.velocity), Vector2.One},
			new object[] {nameof(Item.Width), nameof(Terraria.Item.width), 100}
		};

		private static readonly object[] SetPropertyTestCases =
		{
			new object[] {nameof(Item.AmmoType), nameof(Terraria.Item.ammo), 14},
			new object[] {nameof(Item.AnimationStyle), nameof(Terraria.Item.useStyle), AnimationStyle.Swing},
			new object[] {nameof(Item.AnimationTime), nameof(Terraria.Item.useAnimation), 4},
			new object[] {nameof(Item.AxePower), nameof(Terraria.Item.axe), 100},
			new object[] {nameof(Item.BaitPower), nameof(Terraria.Item.bait), 100},
			new object[] {nameof(Item.CanAutoReuse), nameof(Terraria.Item.autoReuse), true},
			new object[] {nameof(Item.Color), nameof(Terraria.Item.color), Color.White},
			new object[] {nameof(Item.Damage), nameof(Terraria.Item.damage), 100},
			new object[] {nameof(Item.FishingPower), nameof(Terraria.Item.fishingPole), 100},
			new object[] {nameof(Item.GraphicalScale), nameof(Terraria.Item.scale), 10.0f},
			new object[] {nameof(Item.HammerPower), nameof(Terraria.Item.hammer), 100},
			new object[] {nameof(Item.Height), nameof(Terraria.Item.height), 100},
			new object[] {nameof(Item.IsAccessory), nameof(Terraria.Item.accessory), true},
			new object[] {nameof(Item.IsMagicWeapon), nameof(Terraria.Item.magic), true},
			new object[] {nameof(Item.IsMeleeWeapon), nameof(Terraria.Item.melee), true},
			new object[] {nameof(Item.IsRangedWeapon), nameof(Terraria.Item.ranged), true},
			new object[] {nameof(Item.IsThrownWeapon), nameof(Terraria.Item.thrown), true},
			new object[] {nameof(Item.Knockback), nameof(Terraria.Item.knockBack), 1.0f},
			new object[] {nameof(Item.ManaCost), nameof(Terraria.Item.mana), 100},
			new object[] {nameof(Item.MaxStackSize), nameof(Terraria.Item.maxStack), 999},
			new object[] {nameof(Item.Name), nameof(Terraria.Item.Name), "TEST"},
			new object[] {nameof(Item.PickaxePower), nameof(Terraria.Item.pick), 100},
			new object[] {nameof(Item.Position), nameof(Terraria.Item.position), Vector2.One},
			new object[] {nameof(Item.ProjectileSpeed), nameof(Terraria.Item.shootSpeed), 1.0f},
			new object[] {nameof(Item.ProjectileType), nameof(Terraria.Item.shoot), ProjectileType.Amarok},
			new object[] {nameof(Item.Rarity), nameof(Terraria.Item.rare), Rarity.Purple},
			new object[] {nameof(Item.StackSize), nameof(Terraria.Item.stack), 999},
			new object[] {nameof(Item.UseAmmoType), nameof(Terraria.Item.useAmmo), 14},
			new object[] {nameof(Item.UseTime), nameof(Terraria.Item.useTime), 4},
			new object[] {nameof(Item.Velocity), nameof(Terraria.Item.velocity), Vector2.One},
			new object[] {nameof(Item.Width), nameof(Terraria.Item.width), 100}
		};
		
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
			terrariaItemField.SetValue(terrariaItem, Convert.ChangeType(value, terrariaItemField.FieldType));
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

			Assert.AreEqual(
				Convert.ChangeType(value, terrariaItemField.FieldType), terrariaItemField.GetValue(terrariaItem));
		}
		
		[Test]
		public void GetWrappedItem_IsCorrect()
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			Assert.AreSame(terrariaItem, item.WrappedItem);
		}

		[TestCase(ItemType.IronPickaxe)]
		public void SetDefaults_IsCorrect(ItemType type)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			item.SetDefaults(type);

			Assert.AreEqual(type, item.Type);
		}

		[TestCase(Prefix.Legendary)]
		public void SetPrefix_IsCorrect(Prefix prefix)
		{
			var terrariaItem = new Terraria.Item();
			terrariaItem.SetDefaults(1);
			var item = new Item(terrariaItem);

			item.SetPrefix(prefix);

			Assert.AreNotEqual(Prefix.None, item.Prefix, "Some prefix should have been set.");
		}
	}
}
