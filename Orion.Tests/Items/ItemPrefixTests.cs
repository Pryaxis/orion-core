using NUnit.Framework;
using Orion.Items;
using Orion.Npcs;

namespace Orion.Tests.Items
{
	[TestFixture]
	public class ItemPrefixTests
	{
		private static readonly object[] OpExplicitItemPrefixTestCases =
		{
			new object[] {1, ItemPrefix.Large}
		};

		private static readonly object[] EqualsObjectTestCases =
		{
			new object[] {1, ItemPrefix.Large, true},
			new object[] {1, "string", false},
			new object[] {1, null, false}
		};

		[TestCase(1, 1, ExpectedResult = true)]
		[TestCase(1, 2, ExpectedResult = false)]
		public bool OpEquality_IsCorrect(int prefix1, int prefix2)
		{
			var itemPrefix1 = new ItemPrefix(prefix1);
			var itemPrefix2 = new ItemPrefix(prefix2);

			return itemPrefix1 == itemPrefix2;
		}

		[TestCase(1, ExpectedResult = 1)]
		public int OpExplicitInt_IsCorrect(int prefix)
		{
			var itemPrefix = new ItemPrefix(prefix);

			return (int)itemPrefix;
		}

		[TestCaseSource(nameof(OpExplicitItemPrefixTestCases))]
		public void OpExplicitItemPrefix_IsCorrect(int prefix, ItemPrefix expectedResult)
		{
			ItemPrefix actualResult = (ItemPrefix)prefix;

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestCase(1, 1, ExpectedResult = false)]
		[TestCase(1, 2, ExpectedResult = true)]
		public bool OpInequality_IsCorrect(int prefix1, int prefix2)
		{
			var itemPrefix1 = new ItemPrefix(prefix1);
			var itemPrefix2 = new ItemPrefix(prefix2);

			return itemPrefix1 != itemPrefix2;
		}

		[TestCase(1, 1, ExpectedResult = true)]
		[TestCase(1, 2, ExpectedResult = false)]
		public bool EqualsItemPrefix_IsCorrect(int prefix1, int prefix2)
		{
			var itemPrefix1 = new ItemPrefix(prefix1);
			var itemPrefix2 = new ItemPrefix(prefix2);

			return itemPrefix1.Equals(itemPrefix2);
		}

		[TestCaseSource(nameof(EqualsObjectTestCases))]
		public void EqualsObject_IsCorrect(int prefix, object obj, bool expectedResult)
		{
			var itemPrefix = new ItemPrefix(prefix);

			bool actualResult = itemPrefix.Equals(obj);

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestCase(1, ExpectedResult = 1)]
		public int GetHashCode_IsCorrect(int prefix)
		{
			var itemPrefix = new ItemPrefix(prefix);

			return itemPrefix.GetHashCode();
		}
	}
}
