using System.Reflection;
using NUnit.Framework;
using Orion.Framework;

namespace Orion.Tests.Framework
{
	[TestFixture]
	public class PluginTests
	{
		[Test]
		public void GetAuthor_IsCorrect()
		{
			using (var orion = new Orion())
			using (var plugin = new TestPlugin(orion))
			{
				Assert.AreEqual("Author", plugin.Author);
			}
		}

		[Test]
		public void GetName_IsCorrect()
		{
			using (var orion = new Orion())
			using (var plugin = new TestPlugin(orion))
			{
				Assert.AreEqual("Test", plugin.Name);
			}
		}

		[Test]
		public void GetVersion_IsCorrect()
		{
			using (var orion = new Orion())
			using (var plugin = new TestPlugin(orion))
			{
				Assert.AreEqual(Assembly.GetExecutingAssembly().GetName().Version, plugin.Version);
			}
		}

		[Plugin("Test", Author = "Author")]
		public class TestPlugin : Plugin
		{
			public TestPlugin(Orion orion) : base(orion)
			{
			}
		}
	}
}
