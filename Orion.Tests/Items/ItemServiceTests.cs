using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Items;
using Orion.Items.Events;

namespace Orion.Tests.Items
{
	[TestFixture]
	public class ItemServiceTests
	{
		private static readonly Predicate<IItem>[] FindTestCases = { item => (int)item.Type < 100 };
		
		[TestCase(ItemType.IronPickaxe)]
		public void ItemSetDefaults_IsCorrect(ItemType type)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				var terrariaItem = new Terraria.Item();
				var eventOccurred = false;
				EventHandler<ItemSetDefaultsEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaItem, e.Item.WrappedItem);
				};
				itemService.ItemSetDefaults += handler;

				terrariaItem.SetDefaults((int)type);
				itemService.ItemSetDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(ItemType.IronPickaxe)]
		public void ItemSetDefaults_OccursFromNetDefaults(ItemType type)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				var terrariaItem = new Terraria.Item();
				var eventOccurred = false;
				EventHandler<ItemSetDefaultsEventArgs> handler = (sender, e) => eventOccurred = true;
				itemService.ItemSetDefaults += handler;
				
				terrariaItem.netDefaults((int)type);
				itemService.ItemSetDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase("Gold Broadsword")]
		public void ItemSetDefaults_OccursFromSetDefaultsString(string type)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				var terrariaItem = new Terraria.Item();
				var eventOccurred = false;
				EventHandler<ItemSetDefaultsEventArgs> handler = (sender, e) => eventOccurred = true;
				itemService.ItemSetDefaults += handler;

				terrariaItem.SetDefaults(type);
				itemService.ItemSetDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(ItemType.IronPickaxe)]
		public void ItemSettingDefaults_IsCorrect(ItemType type)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				var terrariaItem = new Terraria.Item();
				var eventOccurred = false;
				EventHandler<ItemSettingDefaultsEventArgs> handler = (sender, e) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaItem, e.Item.WrappedItem);
					Assert.AreEqual(type, e.Type);
				};
				itemService.ItemSettingDefaults += handler;

				terrariaItem.SetDefaults((int)type);
				itemService.ItemSettingDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(ItemType.IronPickaxe)]
		public void ItemSettingDefaults_ModifiesType(ItemType newType)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				var terrariaItem = new Terraria.Item();
				EventHandler<ItemSettingDefaultsEventArgs> handler = (sender, e) => e.Type = newType;
				itemService.ItemSettingDefaults += handler;

				terrariaItem.SetDefaults();
				itemService.ItemSettingDefaults -= handler;
				
				Assert.AreEqual((int)newType, terrariaItem.netID);
			}
		}

		[TestCase]
		public void ItemSettingDefaults_Handled_StopsSetDefaults()
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				var terrariaItem = new Terraria.Item();
				EventHandler<ItemSettingDefaultsEventArgs> handler = (sender, e) => e.Handled = true;
				itemService.ItemSettingDefaults += handler;

				terrariaItem.SetDefaults(1);
				itemService.ItemSettingDefaults -= handler;

				Assert.AreEqual(0, terrariaItem.netID, "SetDefaults should not have occurred.");
			}
		}

		[TestCase(ItemType.IronPickaxe)]
		public void ItemSettingDefaults_OccursFromNetDefaults(ItemType type)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				var terrariaItem = new Terraria.Item();
				var eventOccurred = false;
				EventHandler<ItemSettingDefaultsEventArgs> handler = (sender, e) => eventOccurred = true;
				itemService.ItemSettingDefaults += handler;

				terrariaItem.netDefaults((int)type);
				itemService.ItemSettingDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase("Gold Broadsword")]
		public void ItemSettingDefaults_OccursFromSetDefaultsString(string type)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				var terrariaItem = new Terraria.Item();
				var eventOccurred = false;
				EventHandler<ItemSettingDefaultsEventArgs> handler = (sender, e) => eventOccurred = true;
				itemService.ItemSettingDefaults += handler;

				terrariaItem.SetDefaults(type);
				itemService.ItemSettingDefaults -= handler;

				Assert.IsTrue(eventOccurred);
			}
		}

		[TestCase(ItemType.IronPickaxe, 1, Prefix.None)]
		[TestCase(ItemType.IronPickaxe, 100, Prefix.None)]
		[TestCase(ItemType.IronPickaxe, 100, Prefix.Legendary)]
		public void CreateItem_IsCorrect(ItemType type, int stack, Prefix prefix)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				IItem item = itemService.CreateItem(type, stack, prefix);

				Assert.AreEqual(type, item.Type);
				Assert.AreEqual(stack, item.StackSize);
				Assert.AreEqual(prefix != Prefix.None, item.Prefix != Prefix.None);
			}
		}

		[TestCase(0)]
		[TestCase(1)]
		public void FindItems_Null_ReturnsAll(int populate)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				for (var i = 0; i < Terraria.Main.item.Length; ++i)
				{
					Terraria.Main.item[i] = new Terraria.Item {active = i < populate};
				}

				List<IItem> items = itemService.FindItems().ToList();

				Assert.AreEqual(populate, items.Count);
				for (var i = 0; i < populate; ++i)
				{
					Assert.AreSame(Terraria.Main.item[i], items[i].WrappedItem);
				}
			}
		}

		[Test]
		public void FindItems_MultipleTimes_ReturnsSameInstance()
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				for (var i = 0; i < Terraria.Main.item.Length; ++i)
				{
					Terraria.Main.item[i] = new Terraria.Item {active = i < 100};
				}

				List<IItem> items = itemService.FindItems().ToList();
				List<IItem> items2 = itemService.FindItems().ToList();
				
				for (var i = 0; i < 100; ++i)
				{
					Assert.AreSame(items[i], items2[i]);
				}
			}
		}

		[TestCaseSource(nameof(FindTestCases))]
		public void FindItems_IsCorrect(Predicate<IItem> predicate)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				for (var i = 0; i < Terraria.Main.item.Length; ++i)
				{
					Terraria.Main.item[i] = new Terraria.Item {active = true, type = i};
				}

				IEnumerable<IItem> items = itemService.FindItems(predicate);

				foreach (IItem item in items)
				{
					Assert.IsTrue(predicate(item));
				}
			}
		}

		[TestCase(ItemType.IronPickaxe, 1, Prefix.None)]
		[TestCase(ItemType.IronPickaxe, 100, Prefix.None)]
		[TestCase(ItemType.IronPickaxe, 100, Prefix.Legendary)]
		public void SpawnItem_IsCorrect(ItemType type, int stack, Prefix prefix)
		{
			using (var orion = new Orion())
			{
				var itemService = orion.GetService<ItemService>();
				IItem item = itemService.SpawnItem(type, new Vector2(1000, 2000), stack, prefix);

				Assert.AreEqual(type, item.Type);
				Assert.AreEqual(stack, item.StackSize);
				Assert.AreEqual(prefix != Prefix.None, item.Prefix != Prefix.None);
				Assert.That(item.Position.X, Is.InRange(900, 1100));
				Assert.That(item.Position.Y, Is.InRange(1900, 2100));
			}
		}
	}
}
