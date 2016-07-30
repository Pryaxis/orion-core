using NUnit.Framework;
using Orion.Items;

namespace Orion.Tests.Items
{
	[TestFixture]
	public class ItemTypeTests
	{
		private static readonly object[] OpExplicitItemTypeTestCases =
		{
			new object[] {1, ItemType.IronPickaxe}
		};

		private static readonly object[] EqualsObjectTestCases =
		{
			new object[] {1, ItemType.IronPickaxe, true},
			new object[] {1, "string", false},
			new object[] {1, null, false}
		};

		[TestCase(1, 1, ExpectedResult = true)]
		[TestCase(1, 2, ExpectedResult = false)]
		public bool OpEquality_IsCorrect(int type1, int type2)
		{
			var itemType1 = new ItemType(type1);
			var itemType2 = new ItemType(type2);

			return itemType1 == itemType2;
		}

		[TestCase(1, ExpectedResult = 1)]
		public int OpExplicitInt_IsCorrect(int type)
		{
			var itemType = new ItemType(type);

			return (int)itemType;
		}

		[TestCaseSource(nameof(OpExplicitItemTypeTestCases))]
		public void OpExplicitItemType_IsCorrect(int type, ItemType expectedResult)
		{
			ItemType actualResult = (ItemType)type;

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestCase(1, 1, ExpectedResult = false)]
		[TestCase(1, 2, ExpectedResult = true)]
		public bool OpInequality_IsCorrect(int type1, int type2)
		{
			var itemType1 = new ItemType(type1);
			var itemType2 = new ItemType(type2);

			return itemType1 != itemType2;
		}

		[TestCase(1, 1, ExpectedResult = true)]
		[TestCase(1, 2, ExpectedResult = false)]
		public bool EqualsItemType_IsCorrect(int type1, int type2)
		{
			var itemType1 = new ItemType(type1);
			var itemType2 = new ItemType(type2);

			return itemType1.Equals(itemType2);
		}

		[TestCaseSource(nameof(EqualsObjectTestCases))]
		public void EqualsObject_IsCorrect(int type, object obj, bool expectedResult)
		{
			var itemType = new ItemType(type);

			bool actualResult = itemType.Equals(obj);

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestCase(1, ExpectedResult = 1)]
		public int GetHashCode_IsCorrect(int type)
		{
			var itemType = new ItemType(type);

			return itemType.GetHashCode();
		}
	}
}
