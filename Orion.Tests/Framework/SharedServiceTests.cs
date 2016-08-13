using System.Reflection;
using NUnit.Framework;
using Orion.Framework;

namespace Orion.Tests.Framework
{
	[TestFixture]
	public class SharedServiceTests
	{
		[Test]
		public void GetAuthor_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var service = new TestSharedService(orion);

				Assert.AreEqual("Author", service.Author);
			}
		}

		[Test]
		public void GetName_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var service = new TestSharedService(orion);

				Assert.AreEqual("Test", service.Name);
			}
		}

		[Test]
		public void GetVersion_IsCorrect()
		{
			using (var orion = new Orion())
			{
				var service = new TestSharedService(orion);

				Assert.AreEqual(Assembly.GetExecutingAssembly().GetName().Version, service.Version);
			}
		}

		[Service("Test", Author = "Author")]
		public class TestSharedService : SharedService
		{
			public TestSharedService(Orion orion) : base(orion)
			{
			}
		}
	}
}
