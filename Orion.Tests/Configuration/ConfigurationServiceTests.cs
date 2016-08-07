using NUnit.Framework;
using Orion.Configuration;

namespace Orion.Tests.Configuration
{
	[TestFixture]
	public class ConfigurationServiceTests
	{
		class TestConfiguration
		{
			private string ReadonlyString { get; } = "test";
		}

		[Test]
		public void ConfigurationService_TestYamlInstance()
		{
			using (Orion orion = new Orion())
			{
				IConfigurationService<TestConfiguration> configService =
					orion.GetService<YamlFileConfigurationService<TestConfiguration>>();

				Assert.IsNotNull(configService);

				Assert.Equals(configService.GetType().GetGenericTypeDefinition(), typeof(TestConfiguration));
				Assert.AreNotEqual(configService.GetType().GetGenericTypeDefinition(), this.GetType());
			}
		}
 	}
}