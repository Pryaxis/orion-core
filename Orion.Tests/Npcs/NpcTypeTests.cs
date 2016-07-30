using NUnit.Framework;
using Orion.Npcs;

namespace Orion.Tests.Npcs
{
	[TestFixture]
	public class NpcTypeTests
	{
		private static readonly object[] OpImplicitNpcTypeTestCases =
		{
			new object[] {1, NpcType.BlueSlime}
		};

		private static readonly object[] EqualsObjectTestCases =
		{
			new object[] {1, NpcType.BlueSlime, true},
			new object[] {1, "string", false},
			new object[] {1, null, false}
		};

		[TestCase(1, 1, ExpectedResult = true)]
		[TestCase(1, 2, ExpectedResult = false)]
		public bool OpEquality_IsCorrect(int type1, int type2)
		{
			var npcType1 = new NpcType(type1);
			var npcType2 = new NpcType(type2);

			return npcType1 == npcType2;
		}

		[TestCase(1, ExpectedResult = 1)]
		public int OpImplicitInt_IsCorrect(int type)
		{
			var npcType = new NpcType(type);

			return npcType;
		}

		[TestCaseSource(nameof(OpImplicitNpcTypeTestCases))]
		public void OpImplicitNpcType_IsCorrect(int type, NpcType expectedResult)
		{
			NpcType actualResult = type;

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestCase(1, 1, ExpectedResult = false)]
		[TestCase(1, 2, ExpectedResult = true)]
		public bool OpInequality_IsCorrect(int type1, int type2)
		{
			var npcType1 = new NpcType(type1);
			var npcType2 = new NpcType(type2);

			return npcType1 != npcType2;
		}

		[TestCase(1, 1, ExpectedResult = true)]
		[TestCase(1, 2, ExpectedResult = false)]
		public bool EqualsNpcType_IsCorrect(int type1, int type2)
		{
			var npcType1 = new NpcType(type1);
			var npcType2 = new NpcType(type2);

			return npcType1.Equals(npcType2);
		}

		[TestCaseSource(nameof(EqualsObjectTestCases))]
		public void EqualsObject_IsCorrect(int type, object obj, bool expectedResult)
		{
			var npcType = new NpcType(type);

			bool actualResult = npcType.Equals(obj);

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestCase(1, ExpectedResult = 1)]
		public int GetHashCode_IsCorrect(int type)
		{
			var npcType = new NpcType(type);

			return npcType.GetHashCode();
		}
	}
}
