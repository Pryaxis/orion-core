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
	public class JsonFileConfigurationService<TConfiguration> : Service, IConfigurationService<TConfiguration>
		where TConfiguration : class, new()
	{
		/// <summary>
		/// Gets the configuration directory.
		/// </summary>
		public static string ConfigurationDirectory => "config";

		/// <inheritdoc />
		public TConfiguration Configuration { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonFileConfigurationService{T}"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public JsonFileConfigurationService(Orion orion) : base(orion)
		{
			Load();
		}

		/// <inheritdoc />
		public void Load(Stream stream)
		{
			var serializer = new JsonSerializer {Formatting = Formatting.Indented};

			using (var sr = new StreamReader(stream))
			{
				Configuration = serializer.Deserialize(sr, typeof(TConfiguration)) as TConfiguration;
			}
		}

		/// <inheritdoc/>
		public void Load()
		{
			string configDirectory = Path.Combine(ConfigurationDirectory, typeof(TConfiguration).Name);
			string configFile = Path.Combine(configDirectory, "config.json");
			var serializer = new JsonSerializer();

			Directory.CreateDirectory(configDirectory);

			if (!File.Exists(configFile))
			{
				/*
				 * If the configuration file does not exist, assume default configuration
				 * according to a new blank instance of TConfig, and write it out to file
				 * so a configuration file always exists.
				 */
				Configuration = new TConfiguration();
				Save();
			}
			else
			{
				using (var fs = new FileStream(configFile, FileMode.Open, FileAccess.Read))
				{
					Load(fs);
				}
			}
		}

		/// <inheritdoc/>
		public void Save()
		{
			string configDirectory = Path.Combine(ConfigurationDirectory, typeof(TConfiguration).Name);
			string configFile = Path.Combine(configDirectory, "config.json");

			using (var fs = new FileStream(configFile, FileMode.Create, FileAccess.Write))
			{
				Save(fs);
			}
		}

		/// <inheritdoc />
		public void Save(Stream stream)
		{
			var serializer = new JsonSerializer {Formatting = Formatting.Indented};

			using (var sw = new StreamWriter(stream))
			{
				serializer.Serialize(sw, Configuration);
			}
		}
	}
}
