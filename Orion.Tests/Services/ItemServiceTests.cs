using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Orion.Interfaces;
using Orion.Services;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Tests.Services
{
	[TestFixture]
	public class ItemServiceTests
	{
		[TestCase(1)]
		public void SetDefaults_IsCorrect(int type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				bool eventOccurred = false;
				itemService.SetDefaults += (s, a) =>
				{
					eventOccurred = true;
					Assert.AreEqual(type, a.Type);
				};
				var terrariaItem = new Terraria.Item();

				terrariaItem.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase(-48)]
		[TestCase(1)]
		public void SetDefaults_OccursFromNetDefaults(int type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				bool eventOccurred = false;
				itemService.SetDefaults += (s, a) =>
				{
					eventOccurred = true;
				};
				var terrariaItem = new Terraria.Item();
				
				terrariaItem.netDefaults(type);

				Assert.IsTrue(eventOccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase("Gold Broadsword")]
		public void SetDefaults_OccursFromSetDefaultsString(string type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				bool eventOccurred = false;
				itemService.SetDefaults += (s, a) =>
				{
					eventOccurred = true;
				};
				var terrariaItem = new Terraria.Item();

				terrariaItem.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase(1)]
		public void SettingDefaults_IsCorrect(int type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				bool eventOccurred = false;
				itemService.SettingDefaults += (s, a) =>
				{
					eventOccurred = true;
					Assert.AreEqual(type, a.Type);
				};
				var terrariaItem = new Terraria.Item();

				terrariaItem.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SetDefaults event should have occurred.");
			}
		}

		[TestCase(1, 2)]
		public void SettingDefaults_ModifiesType(int type, int newType)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				bool eventOccurred = false;
				itemService.SettingDefaults += (s, a) =>
				{
					eventOccurred = true;
					a.Type = newType;
				};
				var terrariaItem = new Terraria.Item();

				terrariaItem.netDefaults(type);

				Assert.IsTrue(eventOccurred, "SettingDefaults event should have occurred.");
				Assert.AreEqual(newType, terrariaItem.type);
			}
		}

		[TestCase(-48)]
		[TestCase(1)]
		public void SettingDefaults_OccursFromNetDefaults(int type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				bool eventOccurred = false;
				itemService.SetDefaults += (s, a) =>
				{
					eventOccurred = true;
				};
				var terrariaItem = new Terraria.Item();

				terrariaItem.netDefaults(type);

				Assert.IsTrue(eventOccurred, "SettingDefaults event should have occurred.");
			}
		}

		[TestCase("Gold Broadsword")]
		public void SettingDefaults_OccursFromSetDefaultsString(string type)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				bool eventOccurred = false;
				itemService.SettingDefaults += (s, a) =>
				{
					eventOccurred = true;
				};
				var terrariaItem = new Terraria.Item();

				terrariaItem.SetDefaults(type);

				Assert.IsTrue(eventOccurred, "SettingDefaults event should have occurred.");
			}
		}

		[TestCase(1, 1, 0)]
		[TestCase(1, 100, 0)]
		[TestCase(1, 100, 81)]
		public void Create_IsCorrect(int type, int stack, byte prefix)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				IItem item = itemService.Create(type, stack, prefix);

				Assert.AreEqual(type, item.Type);
				Assert.AreEqual(stack, item.Stack);
				Assert.AreEqual(prefix, item.Prefix);
			}
		}

		[TestCase(-100, 1, 0)]
		[TestCase(int.MaxValue, 1, 0)]
		[TestCase(1, -1, 0)]
		[TestCase(1, 1, 255)]
		public void Create_ParamOutOfRange_ThrowsException(int type, int stack, byte prefix)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				Assert.Throws<ArgumentOutOfRangeException>(() => itemService.Create(type, stack, prefix));
			}
		}

		private static readonly Predicate<IItem>[] Predicates = {item => item.Type < 100};

		[Test, TestCaseSource(nameof(Predicates))]
		public void Find_IsCorrect(Predicate<IItem> predicate)
		{
			for (int i = 0; i < Terraria.Main.item.Length; ++i)
			{
				Terraria.Main.item[i] = new Terraria.Item { active = true, netID = i };
			}
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				List<IItem> items = itemService.Find(predicate).ToList();
				List<IItem> otherItems = itemService.GetAll().Where(i => !items.Contains(i)).ToList();

				foreach (IItem item in items)
				{
					Assert.IsTrue(predicate(item));
				}
				foreach (IItem item in otherItems)
				{
					Assert.IsFalse(predicate(item));
				}
			}
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(100)]
		public void GetAll_IsCorrect(int populate)
		{
			for (int i = 0; i < Terraria.Main.item.Length; ++i)
			{
				Terraria.Main.item[i] = new Terraria.Item { active = i < populate, netID = 1 };
			}
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				List<IItem> items = itemService.GetAll().ToList();

				Assert.AreEqual(populate, items.Count);
				foreach (IItem item in items)
				{
					Assert.AreEqual(1, item.Type);
				}
			}
		}

		[TestCase(1, 1, 0)]
		[TestCase(1, 100, 0)]
		[TestCase(1, 100, 81)]
		public void Spawn_IsCorrect(int type, int stack, byte prefix)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				IItem item = itemService.Spawn(type, new Vector2(1000, 2000), stack, prefix);

				Assert.AreEqual(type, item.Type);
				Assert.AreEqual(stack, item.Stack);
				Assert.AreEqual(prefix, item.Prefix);
				Assert.That(item.Position.X, Is.InRange(900, 1100));
				Assert.That(item.Position.Y, Is.InRange(1900, 2100));
			}
		}

		[TestCase(-100, 1, 0)]
		[TestCase(int.MaxValue, 1, 0)]
		[TestCase(1, -1, 0)]
		[TestCase(1, 1, 255)]
		public void Spawn_ParamOutOfRange_ThrowsException(int type, int stack, byte prefix)
		{
			using (var orion = new Orion())
			using (var itemService = new ItemService(orion))
			{
				Assert.Throws<ArgumentOutOfRangeException>(() => itemService.Spawn(type, Vector2.Zero, stack, prefix));
			}
		}
	}
}
