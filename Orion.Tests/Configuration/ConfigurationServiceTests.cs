using NUnit.Framework;
using Orion.Configuration;

namespace Orion.Tests.Configuration
{
	[TestFixture]
	public class ConfigurationServiceTests
	{
		private class TestConfiguration
		{
			private string ReadonlyString { get; } = "test";
		}

		[Test]
		public void ConfigurationService_TestYamlInstance()
		{
			using (Orion orion = new Orion())
			{
				using (IConfigurationService<TestConfiguration> configService =
					   orion.GetService<YamlFileConfigurationService<TestConfiguration>>())
				{
					Assert.IsNotNull(configService);
					Assert.AreEqual(typeof(TestConfiguration), configService.GetType().GetGenericArguments()[0]);
				}
			}
		}

		[Test]
		public void ConfigurationService_ConfigurationElementShouldNotBeNullAfterInitialization()
		{
			using (Orion orion = new Orion())
			{
				using (IConfigurationService<TestConfiguration> configService =
					   orion.GetService<YamlFileConfigurationService<TestConfiguration>>())
				{
					Assert.IsNotNull(configService.Configuration);
				}
			}
		}
	}
}
