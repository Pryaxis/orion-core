using NUnit.Framework;
using Orion.Interfaces.Implementations;

namespace Orion.Tests.Interfaces.Implementations
{
	[TestFixture]
	public class ItemArrayTests
	{
		[Test]
		public void GetItem_IsCorrect()
		{
			var terrariaItemArray = new Terraria.Item[10];
			var itemArray = new ItemArray(terrariaItemArray);

			terrariaItemArray[0] = new Terraria.Item();

			Assert.AreSame(terrariaItemArray[0], itemArray[0].Backing);
		}

		[Test]
		public void GetItem_NoReassignment_ReturnsSameInstance()
		{
			var terrariaItemArray = new Terraria.Item[10];
			var itemArray = new ItemArray(terrariaItemArray);

			terrariaItemArray[0] = new Terraria.Item();

			Assert.AreSame(itemArray[0], itemArray[0]);
		}

		[Test]
		public void SetItem_Updates()
		{
			var terrariaItemArray = new Terraria.Item[10];
			var itemArray = new ItemArray(terrariaItemArray);
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);

			itemArray[0] = item;

			Assert.AreSame(itemArray[0].Backing, terrariaItemArray[0]);
		}
	}
}
