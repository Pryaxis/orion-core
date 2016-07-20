using System;
using NUnit.Framework;
using Orion.Data;

namespace Orion.Tests.Data
{
	[TestFixture]
	public class ItemArrayTests
	{
		[Test]
		public void GetItem_IsCorrect()
		{
			var terrariaItemArray = new Terraria.Item[10];
			ItemArray itemArray = ItemArray.Wrap(terrariaItemArray);

			terrariaItemArray[0] = new Terraria.Item();

			Assert.AreSame(terrariaItemArray[0], itemArray[0].WrappedItem);
		}

		[Test]
		public void GetItem_NoReassignment_ReturnsSameInstance()
		{
			var terrariaItemArray = new Terraria.Item[10];
			ItemArray itemArray = ItemArray.Wrap(terrariaItemArray);

			terrariaItemArray[0] = new Terraria.Item();

			Assert.AreSame(itemArray[0], itemArray[0]);
		}
		
		[Test]
		public void SetItem_Updates()
		{
			var terrariaItemArray = new Terraria.Item[10];
			ItemArray itemArray = ItemArray.Wrap(terrariaItemArray);
			var terrariaItem = new Terraria.Item();
			Item item = Item.Wrap(terrariaItem);

			itemArray[0] = item;

			Assert.AreSame(itemArray[0].WrappedItem, terrariaItemArray[0]);
		}

		[Test]
		public void Wrap_Null_ThrowsException()
		{
			Assert.Throws<ArgumentNullException>(() => ItemArray.Wrap(null));
		}

		[Test]
		public void Wrap_ReturnsSameInstance()
		{
			var terrariaItemArray = new Terraria.Item[10];

			ItemArray itemArray1 = ItemArray.Wrap(terrariaItemArray);
			ItemArray itemArray2 = ItemArray.Wrap(terrariaItemArray);

			Assert.AreSame(itemArray1, itemArray2);
		}
	}
}
