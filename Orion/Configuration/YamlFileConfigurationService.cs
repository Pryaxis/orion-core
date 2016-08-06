using Orion.Framework;
using YamlDotNet.Serialization;
using System.IO;

namespace Orion.Configuration
{
	/// <summary>
	/// Configuration service which loads and saves configuration from disk in YAML
	/// format, using YamlDotNet to load/store data.
	/// </summary>
	[Service("YAML File Configuration Service", Author = "Nyx Studios")]
	public class YamlFileConfigurationService : SharedService, IConfigurationService
	{
		/// <summary>
		/// Gets the configuration directory.
		/// </summary>
		public static string ConfigurationDirectory => "config";

		/// <summary>
		/// Initializes a new instance of the <see cref="YamlFileConfigurationService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public YamlFileConfigurationService(Orion orion) : base(orion)
		{
		}

		/// <inheritdoc/>
		public TConfig Load<TService, TConfig>()
			where TService : SharedService
			where TConfig : class, new()
		{
			string configDirectory = Path.Combine(ConfigurationDirectory, typeof(TService).Name);
			string configFile = Path.Combine(configDirectory, "config.yaml");
			var deserializer = new Deserializer();

			TConfig configObject;

			Directory.CreateDirectory(configDirectory);

			if (!File.Exists(configFile))
			{
				/*
				 * If the configuration file does not exist, assume default configuration
				 * according to a new blank instance of TConfig, and write it out to file
				 * so a configuration file always exists.
				 */
				configObject = new TConfig();
				Save<TService, TConfig>(configObject);

				return configObject;
			}

			using (var fs = new FileStream(configFile, FileMode.Open, FileAccess.Read))
			using (var sr = new StreamReader(fs))
			{
				configObject = deserializer.Deserialize<TConfig>(sr);
			}

			return configObject;
		}

		/// <inheritdoc/>
		public void Save<TService, TConfig>(TConfig config)
			where TService : SharedService
			where TConfig : class, new()
		{
			string configDirectory = Path.Combine(ConfigurationDirectory, typeof(TService).Name);
			string configFile = Path.Combine(configDirectory, "config.yaml");
			var serializer = new Serializer(SerializationOptions.EmitDefaults);

			using (var fs = new FileStream(configFile, FileMode.Create, FileAccess.Write))
			using (var sw = new StreamWriter(fs))
			{
				serializer.Serialize(sw, config);
			}
		}
	}
}
