using System.IO;
using Newtonsoft.Json;
using Orion.Framework;

namespace Orion.Configuration
{
	/// <summary>
	/// Configuration service which loads and saves configuration from disk in JSON
	/// format, using Newtonsoft.Json to load/store data.
	/// </summary>
	[Service("JSON File Configuration Service", Author = "Nyx Studios")]
	public class JsonFileConfigurationService : ServiceBase, IConfigurationService
	{
		/// <summary>
		/// Gets the configuration directory.
		/// </summary>
		public static string ConfigurationDirectory => "config";

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonFileConfigurationService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public JsonFileConfigurationService(Orion orion) : base(orion)
		{
		}

		/// <inheritdoc/>
		public TConfig Load<TService, TConfig>()
			where TService : ServiceBase
			where TConfig : class, new()
		{
			// TODO: generalize with streams instead?
			string configDirectory = Path.Combine(ConfigurationDirectory, typeof(TService).Name);
			string configFile = Path.Combine(configDirectory, "config.json");
			var serializer = new JsonSerializer();
			
			TConfig configObject;

			Directory.CreateDirectory(configDirectory);

			if (File.Exists(configFile) == false)
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
				configObject = serializer.Deserialize(sr, typeof(TConfig)) as TConfig;
			}

			return configObject;
		}

		/// <inheritdoc/>
		public void Save<TService, TConfig>(TConfig config)
			where TService : ServiceBase
			where TConfig : class, new()
		{
			// TODO: generalize with streams instead?
			string configDirectory = Path.Combine(ConfigurationDirectory, typeof(TService).Name);
			string configFile = Path.Combine(configDirectory, "config.json");
			var serializer = new JsonSerializer {Formatting = Formatting.Indented};

			using (var fs = new FileStream(configFile, FileMode.Create, FileAccess.Write))
			using (var sw = new StreamWriter(fs))
			{
				serializer.Serialize(sw, config);
			}
		}
	}
}
