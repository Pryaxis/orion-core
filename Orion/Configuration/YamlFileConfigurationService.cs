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
	public class YamlFileConfigurationService<TConfig> : Service, IConfigurationService<TConfig>
		where TConfig : class, new()
	{
		private string _configDirectory => Path.Combine(ConfigurationRootDirectory, typeof(TConfig).Name);
		private string _configFile => Path.Combine(_configDirectory, "config.yaml");

		/// <summary>
		/// Gets the configuration root directory, relative to Orion's working path.
		/// </summary>
		public static string ConfigurationRootDirectory => "config";


		/// <inheritdoc />
		public TConfig Configuration { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="YamlFileConfigurationService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public YamlFileConfigurationService(Orion orion) : base(orion)
		{
			Directory.CreateDirectory(_configDirectory);
		}

		/// <inheritdoc/>
		public void Load()
		{
			var deserializer = new Deserializer();

			if (!File.Exists(_configFile))
			{
				/*
				 * If the configuration file does not exist, assume default configuration
				 * according to a new blank instance of TConfig, and write it out to file
				 * so a configuration file always exists.
				 */
				Configuration = new TConfig();
				Save();
			}
			else
			{
				using (var fs = new FileStream(_configFile, FileMode.Open, FileAccess.Read))
				using (var sr = new StreamReader(fs))
				{
					Configuration = deserializer.Deserialize<TConfig>(sr);
				}
			}
		}

		/// <inheritdoc/>
		public void Save()
		{
			string configFile = Path.Combine(_configDirectory, "config.yaml");
			var serializer = new Serializer(SerializationOptions.EmitDefaults);

			using (var fs = new FileStream(configFile, FileMode.Create, FileAccess.Write))
			using (var sw = new StreamWriter(fs))
			{
				serializer.Serialize(sw, Configuration);
			}
		}
	}
}
