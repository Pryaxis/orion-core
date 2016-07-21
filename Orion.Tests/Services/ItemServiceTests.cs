using System;
using System.Collections;
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
		private Orion _orion;

		[OneTimeSetUp]
		public void Setup()
		{
			_orion = new Orion();
			for (int i = 0; i < Terraria.Main.item.Length; ++i)
			{
				Terraria.Main.item[i] = new Terraria.Item();
			}
		}

		[TestCase(1, 1, 0)]
		[TestCase(1, 100, 0)]
		[TestCase(1, 100, 81)]
		public void Create_IsCorrect(int type, int stack, byte prefix)
		{
			var itemService = new ItemService(_orion);

			IItem item = itemService.Create(type, stack, prefix);

			Assert.AreEqual(type, item.Type);
			Assert.AreEqual(stack, item.Stack);
			Assert.AreEqual(prefix, item.Prefix);
		}

		[TestCase(-100, 1, 0)]
		[TestCase(int.MaxValue, 1, 0)]
		[TestCase(1, -1, 0)]
		[TestCase(1, 1, 255)]
		public void Create_ParamOutOfRange_ThrowsException(int type, int stack, byte prefix)
		{
			var itemService = new ItemService(_orion);

			Assert.Throws<ArgumentOutOfRangeException>(() => itemService.Create(type, stack, prefix));
		}

		private static readonly Predicate<IItem>[] Predicates = {item => item.Type == 1};

		[Test, TestCaseSource(nameof(Predicates))]
		public void Find_IsCorrect(Predicate<IItem> predicate)
		{
			var itemService = new ItemService(_orion);
			for (int i = 0; i < Terraria.Main.item.Length; ++i)
			{
				Terraria.Main.item[i].active = true;
				Terraria.Main.item[i].netID = i;
			}

			List<IItem> items = itemService.Find(predicate).ToList();

			foreach (IItem item in items)
			{
				Assert.IsTrue(predicate(item));
			}
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(100)]
		public void GetAll_IsCorrect(int populate)
		{
			var itemService = new ItemService(_orion);
			for (int i = 0; i < Terraria.Main.item.Length; ++i)
			{
				Terraria.Main.item[i].active = i < populate;
				Terraria.Main.item[i].netID = 1;
			}

			List<IItem> items = itemService.GetAll().ToList();

			Assert.AreEqual(populate, items.Count);
			foreach (IItem item in items)
			{
				Assert.AreEqual(1, item.Type);
			}
		}
		
		[TestCase(1, 1, 0)]
		[TestCase(1, 100, 0)]
		[TestCase(1, 100, 81)]
		public void Spawn_IsCorrect(int type, int stack, byte prefix)
		{
			var itemService = new ItemService(_orion);

			IItem item = itemService.Spawn(type, new Vector2(1000, 2000), stack, prefix);

			Assert.AreEqual(type, item.Type);
			Assert.AreEqual(stack, item.Stack);
			Assert.AreEqual(prefix, item.Prefix);
			Assert.That(item.Position.X, Is.InRange(900, 1100));
			Assert.That(item.Position.Y, Is.InRange(1900, 2100));
		}

		[TestCase(-100, 1, 0)]
		[TestCase(int.MaxValue, 1, 0)]
		[TestCase(1, -1, 0)]
		[TestCase(1, 1, 255)]
		public void Spawn_ParamOutOfRange_ThrowsException(int type, int stack, byte prefix)
		{
			var itemService = new ItemService(_orion);

			Assert.Throws<ArgumentOutOfRangeException>(() => itemService.Spawn(type, Vector2.Zero, stack, prefix));
		}
	}
}
