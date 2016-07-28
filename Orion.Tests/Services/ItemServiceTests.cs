using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Orion.Entities.Item;

namespace Orion.Tests.Services
{
	[TestFixture]
	public class ItemServiceTests
	{
		private static readonly Predicate<IItem>[] Predicates = { item => item.Type < 100 };

		[TestCase(1)]
		public void ItemSetDefaults_IsCorrect(int type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				var terrariaItem = new Terraria.Item();
				bool eventOccurred = false;
				itemService.ItemSetDefaults += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaItem, args.Item.WrappedItem);
				};

				terrariaItem.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase(-48)]
		[TestCase(1)]
		public void ItemSetDefaults_OccursFromNetDefaults(int type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				var terrariaItem = new Terraria.Item();
				bool eventOccurred = false;
				itemService.ItemSetDefaults += (sender, args) => eventOccurred = true;
				
				terrariaItem.netDefaults(type);

				Assert.IsTrue(eventOccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase("Gold Broadsword")]
		public void ItemSetDefaults_OccursFromSetDefaultsString(string type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				var terrariaItem = new Terraria.Item();
				bool eventOccurred = false;
				itemService.ItemSetDefaults += (sender, args) => eventOccurred = true;

				terrariaItem.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase(1)]
		public void ItemSettingDefaults_IsCorrect(int type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				var terrariaItem = new Terraria.Item();
				bool eventOccurred = false;
				itemService.ItemSettingDefaults += (sender, args) =>
				{
					eventOccurred = true;
					Assert.AreEqual(terrariaItem, args.Item.WrappedItem);
					Assert.AreEqual(type, args.Type);
				};

				terrariaItem.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase(1, 2)]
		public void ItemSettingDefaults_ModifiesType(int type, int newType)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				var terrariaItem = new Terraria.Item();
				itemService.ItemSettingDefaults += (sender, args) => args.Type = newType;

				terrariaItem.SetDefaults(type);
				
				Assert.AreEqual(newType, terrariaItem.type);
			}
		}

		[TestCase(1)]
		public void ItemSettingDefaults_Handled_StopsSetDefaults(int type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				var terrariaItem = new Terraria.Item();
				itemService.ItemSettingDefaults += (sender, args) => args.Handled = true;

				terrariaItem.SetDefaults(type);

				Assert.AreEqual(0, terrariaItem.type);
			}
		}

		[TestCase(-48)]
		[TestCase(1)]
		public void ItemSettingDefaults_OccursFromNetDefaults(int type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				var terrariaItem = new Terraria.Item();
				bool eventOccurred = false;
				itemService.ItemSettingDefaults += (sender, args) => eventOccurred = true;

				terrariaItem.netDefaults(type);

				Assert.IsTrue(eventOccurred, "SettingDefaults event should have occurred.");
			}
		}

		[TestCase("Gold Broadsword")]
		public void ItemSettingDefaults_OccursFromSetDefaultsString(string type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				var terrariaItem = new Terraria.Item();
				bool eventOccurred = false;
				itemService.ItemSettingDefaults += (sender, args) => eventOccurred = true;

				terrariaItem.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SettingDefaults event should have occurred.");
			}
		}

		[TestCase(1, 1, 0)]
		[TestCase(2, 100, 0)]
		[TestCase(1, 1, 81)]
		public void Create_IsCorrect(int type, int stack, int prefix)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				IItem item = itemService.Create(type, stack, prefix);

				Assert.AreEqual(type, item.Type);
				Assert.AreEqual(stack, item.StackSize);
				Assert.AreEqual(prefix != 0, item.Prefix != 0);
			}
		}

		[TestCase(-1, 1, 0)]
		[TestCase(Terraria.Main.maxItemTypes, 1, 0)]
		[TestCase(1, -1, 0)]
		[TestCase(1, 1, -1)]
		[TestCase(1, 1, Terraria.Item.maxPrefixes)]
		public void Create_ParamOutOfRange_ThrowsArgumentOutOfRangeException(int type, int stack, int prefix)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				Assert.Throws<ArgumentOutOfRangeException>(() => itemService.Create(type, stack, prefix));
			}
		}

		[TestCase(0)]
		[TestCase(1)]
		public void Find_Null_ReturnsAll(int populate)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				for (int i = 0; i < Terraria.Main.item.Length; ++i)
				{
					Terraria.Main.item[i] = new Terraria.Item {active = i < populate};
				}

				List<IItem> items = itemService.Find().ToList();

				Assert.AreEqual(populate, items.Count);
				for (int i = 0; i < populate; ++i)
				{
					Assert.AreSame(Terraria.Main.item[i], items[i].WrappedItem);
				}
			}
		}

		[TestCase(1)]
		public void Find_MultipleTimes_ReturnsSameInstance(int populate)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				for (int i = 0; i < Terraria.Main.item.Length; ++i)
				{
					Terraria.Main.item[i] = new Terraria.Item { active = i < populate };
				}

				List<IItem> items = itemService.Find().ToList();
				List<IItem> items2 = itemService.Find().ToList();
				
				for (int i = 0; i < populate; ++i)
				{
					Assert.AreSame(items[i], items2[i]);
				}
			}
		}

		[Test, TestCaseSource(nameof(Predicates))]
		public void Find_IsCorrect(Predicate<IItem> predicate)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				for (int i = 0; i < Terraria.Main.item.Length; ++i)
				{
					Terraria.Main.item[i] = new Terraria.Item {active = true, type = i};
				}

				IEnumerable<IItem> items = itemService.Find(predicate);

				foreach (IItem item in items)
				{
					Assert.IsTrue(predicate(item));
				}
			}
		}

		[TestCase(1, 1, 0)]
		[TestCase(2, 100, 0)]
		[TestCase(1, 1, 81)]
		public void Spawn_IsCorrect(int type, int stack, int prefix)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				IItem item = itemService.Spawn(type, new Vector2(1000, 2000), stack, prefix);

				Assert.AreEqual(type, item.Type);
				Assert.AreEqual(stack, item.StackSize);
				Assert.AreEqual(prefix != 0, item.Prefix != 0);
				Assert.That(item.Position.X, Is.InRange(900, 1100));
				Assert.That(item.Position.Y, Is.InRange(1900, 2100));
			}
		}

		[TestCase(-1, 1, 0)]
		[TestCase(Terraria.Main.maxItemTypes, 1, 0)]
		[TestCase(1, -1, 0)]
		[TestCase(1, 1, -1)]
		[TestCase(1, 1, Terraria.Item.maxPrefixes)]
		public void Spawn_ParamOutOfRange_ThrowsArgumentOutOfRangeException(int type, int stack, int prefix)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				Assert.Throws<ArgumentOutOfRangeException>(() => itemService.Spawn(type, Vector2.Zero, stack, prefix));
			}
		}
	}
}
