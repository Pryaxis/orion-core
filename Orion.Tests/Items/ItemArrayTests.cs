using System;
using NUnit.Framework;
using Orion.Items;

namespace Orion.Tests.Items
{
	[TestFixture]
	public class ItemArrayTests
	{
		[Test]
		public void Constructor_NullItemArray_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new ItemArray(null));
		}

		[TestCase(0)]
		[TestCase(1)]
		public void Length_IsCorrect(int length)
		{
			var terrariaItemArray = new Terraria.Item[length];
			var itemArray = new ItemArray(terrariaItemArray);

			Assert.AreEqual(length, itemArray.Length);
		}

		[TestCase(0)]
		public void GetItem_IsCorrect(int index)
		{
			var terrariaItemArray = new Terraria.Item[10];
			terrariaItemArray[index] = new Terraria.Item();
			var itemArray = new ItemArray(terrariaItemArray);

			IItem item = itemArray[index];

			Assert.AreEqual(terrariaItemArray[index], item.WrappedItem);
		}

		[TestCase(0)]
		public void GetItem_MultipleTimes_ReturnsSameInstance(int index)
		{
			var terrariaItemArray = new Terraria.Item[10];
			terrariaItemArray[index] = new Terraria.Item();
			var itemArray = new ItemArray(terrariaItemArray);

			IItem item1 = itemArray[index];
			IItem item2 = itemArray[index];

			Assert.AreSame(item1, item2);
		}

		[TestCase(-1)]
		[TestCase(100000)]
		public void GetItem_IndexOutOfRange_ThrowsArgumentOutOfRangeException(int index)
		{
			var terrariaItemArray = new Terraria.Item[10];
			var itemArray = new ItemArray(terrariaItemArray);

			Assert.Throws<ArgumentOutOfRangeException>(() => itemArray[index].SetDefaults(ItemType.None));
		}

		[TestCase(0)]
		public void SetItem_IsCorrect(int index)
		{
			var terrariaItemArray = new Terraria.Item[10];
			var terrariaItem = new Terraria.Item();
			var itemArray = new ItemArray(terrariaItemArray);
			var item = new Item(terrariaItem);

			itemArray[index] = item;

			Assert.AreEqual(terrariaItem, itemArray[index].WrappedItem);
		}

		[TestCase(0)]
		public void SetItem_NullValue_ThrowsArgumentNullException(int index)
		{
			var terrariaItemArray = new Terraria.Item[10];
			var itemArray = new ItemArray(terrariaItemArray);

			Assert.Throws<ArgumentNullException>(() => itemArray[index] = null);
		}

		[TestCase(-1)]
		[TestCase(100000)]
		public void SetItem_IndexOutOfRange_ThrowsArgumentOutOfRangeException(int index)
		{
			var terrariaItemArray = new Terraria.Item[10];
			var terrariaItem = new Terraria.Item();
			var itemArray = new ItemArray(terrariaItemArray);
			var item = new Item(terrariaItem);

			Assert.Throws<ArgumentOutOfRangeException>(() => itemArray[index] = item);
		}
	}
}
