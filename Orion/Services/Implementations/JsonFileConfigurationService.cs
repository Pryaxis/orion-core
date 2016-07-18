using Newtonsoft.Json;
using Orion.Framework;
using Orion.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Services.Implementations
{
	/// <summary>
	/// Configuration service which loads and saves configuration from disk in JSON
	/// format, using Newtonsoft.Json to load/store data.
	/// </summary>
	public class JsonFileConfigurationService : ServiceBase, IConfigurationService
	{
		public static string ConfigurationDirectory => "config";

		public JsonFileConfigurationService(Orion orion) : base(orion)
		{
		}

		public TConfig Load<TService, TConfig>()
			where TService : ServiceBase
			where TConfig : class, new()
		{
			string configDirectory = Path.Combine(ConfigurationDirectory, typeof(TService).Name);
			string configFile = Path.Combine(configDirectory, "config.json");
			JsonSerializer serializer = new JsonSerializer();
			
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

			using (FileStream fs = new FileStream(configFile, FileMode.Open, FileAccess.Read))
			using (StreamReader sr = new StreamReader(fs))
			{
				configObject = serializer.Deserialize(sr, typeof(TConfig)) as TConfig;
			}

			return configObject;
		}

		public void Save<TService, TConfig>(TConfig config)
			where TService : ServiceBase
			where TConfig : class, new()
		{
			throw new NotImplementedException();
		}
	}
}
