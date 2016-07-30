using NUnit.Framework;
using Orion.Projectiles;

namespace Orion.Tests.Projectiles
{
	[TestFixture]
	public class ProjectileTypeTests
	{
		private static readonly object[] OpExplicitProjectileTypeTestCases =
		{
			new object[] {1, ProjectileType.WoodenArrowFriendly}
		};

		private static readonly object[] EqualsObjectTestCases =
		{
			new object[] {1, ProjectileType.WoodenArrowFriendly, true},
			new object[] {1, "string", false},
			new object[] {1, null, false}
		};

		[TestCase(1, 1, ExpectedResult = true)]
		[TestCase(1, 2, ExpectedResult = false)]
		public bool OpEquality_IsCorrect(int type1, int type2)
		{
			var projectileType1 = new ProjectileType(type1);
			var projectileType2 = new ProjectileType(type2);

			return projectileType1 == projectileType2;
		}

		[TestCase(1, ExpectedResult = 1)]
		public int OpExplicitInt_IsCorrect(int type)
		{
			var projectileType = new ProjectileType(type);

			return (int)projectileType;
		}

		[TestCaseSource(nameof(OpExplicitProjectileTypeTestCases))]
		public void OpExplicitProjectileType_IsCorrect(int type, ProjectileType expectedResult)
		{
			var actualResult = (ProjectileType)type;

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestCase(1, 1, ExpectedResult = false)]
		[TestCase(1, 2, ExpectedResult = true)]
		public bool OpInequality_IsCorrect(int type1, int type2)
		{
			var projectileType1 = new ProjectileType(type1);
			var projectileType2 = new ProjectileType(type2);

			return projectileType1 != projectileType2;
		}

		[TestCase(1, 1, ExpectedResult = true)]
		[TestCase(1, 2, ExpectedResult = false)]
		public bool EqualsProjectileType_IsCorrect(int type1, int type2)
		{
			var projectileType1 = new ProjectileType(type1);
			var projectileType2 = new ProjectileType(type2);

			return projectileType1.Equals(projectileType2);
		}

		[TestCaseSource(nameof(EqualsObjectTestCases))]
		public void EqualsObject_IsCorrect(int type, object obj, bool expectedResult)
		{
			var projectileType = new ProjectileType(type);

			bool actualResult = projectileType.Equals(obj);

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestCase(1, ExpectedResult = 1)]
		public int GetHashCode_IsCorrect(int type)
		{
			var projectileType = new ProjectileType(type);

			return projectileType.GetHashCode();
		}
	}
}
