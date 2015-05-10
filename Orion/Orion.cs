using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;
using Orion.Configuration;
using Orion.Hashing;
using Orion.Logging;
using Orion.Users;
using Terraria;
using TerrariaApi.Server;

namespace Orion
{
	[ApiVersion(1, 17)]
	public class Orion : TerrariaPlugin
	{
		/// <summary>
		/// Relative path to the Orion folder
		/// </summary>
		[Temporary("Placeholder value so that it actually runs with TerrariaServer")]
		public string SavePath { get; private set; }

		/// <summary>
		/// Relative path to the log file
		/// </summary>
		[Temporary("Placeholder value so that it actually runs with TerrariaServer")]
		public string LogPath { get; private set; }

		private string PluginPath { get; set; }

		/// <summary>
		/// Relative path to the config file
		/// </summary>
		[Temporary("Placeholder value so that it actually runs with TerrariaServer")]
		public string ConfigPath { get; private set; }
		
		/// <summary>
		/// Config file. Use this to get data from the config
		/// </summary>
		public ConfigFile Config { get; private set; }

		/// <summary>
		/// Database connection to the Orion database
		/// </summary>
		internal IDbConnection Database { get; private set; }
		/// <summary>
		/// User manager object for getting users and setting values
		/// </summary>
		public UserManager Users { get; private set; }
		/// <summary>
		/// Log manager. Use this to write data to the log
		/// </summary>
		public static ILog Log { get; private set; }

		public Hasher HashManager { get; private set; }

		private readonly Dictionary<string, Assembly> _loadedAssemblies = new Dictionary<string, Assembly>(); 
		private readonly List<OrionPlugin> _loadedPlugins = new List<OrionPlugin>(); 

		/// <summary>
		/// Plugin author(s)
		/// </summary>
		public override string Author
		{
			get { return "Nyx Studios"; }
		}

		/// <summary>
		/// Plugin name
		/// </summary>
		public override string Name
		{
			get { return "Orion"; }
		}

		/// <summary>
		/// Plugin version
		/// </summary>
		public override Version Version
		{
			get { return Assembly.GetExecutingAssembly().GetName().Version; }
		}

		public Orion(Main game) : base(game)
		{
			Config = new ConfigFile();
			Order = 0;
			SavePath = "Orion";
			ConfigPath = Path.Combine(SavePath, "Orion.json");
			LogPath = Path.Combine(SavePath, "logs");
			PluginPath = Path.Combine(SavePath, "plugins");
		}

		/// <summary>
		/// Called by the Server API when this plugin is initialized.
		/// This should never be called by a plugin.
		/// </summary>
		public override void Initialize()
		{
			try
			{
				HandleCommandLine(Environment.GetCommandLineArgs());

				if (!Directory.Exists(SavePath))
				{
					Directory.CreateDirectory(SavePath);
				}
				if (!Directory.Exists(LogPath))
				{
					Directory.CreateDirectory(LogPath);
				}
				if (!Directory.Exists(PluginPath))
				{
					Directory.CreateDirectory(PluginPath);
				}

				ConfigFile.ConfigRead += OnConfigRead;
				if (File.Exists(ConfigPath))
				{
					Config = ConfigFile.Read(ConfigPath);
				}
				Config.Write(ConfigPath);

				if (Config.StorageType.ToLower() == "sqlite")
				{
					string sql = Path.Combine(SavePath, "orion.sqlite");
					Database = new SqliteConnection(String.Format("uri=file://{0},Version=3", sql));
				}
				else if (Config.StorageType.ToLower() == "mysql")
				{
					try
					{
						string[] hostport = Config.MySqlHost.Split(':');
						Database = new MySqlConnection
						{
							ConnectionString = String.Format("Server={0}; Port={1}; Database={2}; Uid={3}; Pwd={4};",
								hostport[0],
								hostport.Length > 1 ? hostport[1] : "3306",
								Config.MySqlDbName,
								Config.MySqlUsername,
								Config.MySqlPassword
								)
						};
					}
					catch (MySqlException ex)
					{
						ServerApi.LogWriter.PluginWriteLine(this, ex.ToString(), TraceLevel.Error);
						throw new Exception("MySql not setup correctly");
					}
				}
				else
				{
					throw new Exception("Invalid storage type");
				}

				if (Config.UseSqlLogging)
				{
					Log = new SqlLog(this, Path.Combine(LogPath, "log.log"), false);
				}
				else
				{
					Log = new TextLog(this, Path.Combine(LogPath, "log.log"), false);
				}

				Users = new UserManager(this);
				HashManager = new Hasher(this);

				LoadPlugins();
			}
			catch (Exception ex)
			{
				Log.Error("Fatal Startup Exception");
				Log.Error(ex.ToString());
				Environment.Exit(1);
			}
		}

		/// <summary>Fired when the config file has been read.</summary>
		/// <param name="file">The config file object.</param>
		private void OnConfigRead(ConfigFile file)
		{
			LogPath = file.LogPath;
			PluginPath = file.PluginsPath;
		}

		private void HandleCommandLine(string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				string path;
				switch (args[i].ToLower())
				{
					case "-orionpath":
						path = args[i++];
						if (path.IndexOfAny(Path.GetInvalidPathChars()) == -1)
						{
							SavePath = path;
							ServerApi.LogWriter.PluginWriteLine(this, "Save path has been set to " + path, TraceLevel.Info);
						}
						break;

					case "-configpath":
						path = args[i++];
						if (path.IndexOfAny(Path.GetInvalidPathChars()) == -1)
						{
							ConfigPath = path;
							ServerApi.LogWriter.PluginWriteLine(this, "Config path has been set to " + path, TraceLevel.Info);
						}
						break;

					case "-logpath":
						path = args[i++];
						if (path.IndexOfAny(Path.GetInvalidPathChars()) == -1)
						{
							LogPath = path;
							ServerApi.LogWriter.PluginWriteLine(this, "Log path has been set to " + path, TraceLevel.Info);
						}
						break;
				}
			}
		}

		private void LoadPlugins()
		{
			List<FileInfo> fileInfos = new DirectoryInfo(PluginPath).GetFiles("*.dll").ToList();

			foreach (FileInfo fileInfo in fileInfos)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);

				try
				{
					Assembly assembly;
					if (!_loadedAssemblies.TryGetValue(fileNameWithoutExtension, out assembly))
					{
						assembly = Assembly.Load(File.ReadAllBytes(fileInfo.FullName));
						_loadedAssemblies.Add(fileNameWithoutExtension, assembly);
					}

					foreach (Type type in assembly.GetExportedTypes())
					{
						if (!type.IsSubclassOf(typeof (OrionPlugin)) || !type.IsPublic || type.IsAbstract)
							continue;

						object[] customAttributes = type.GetCustomAttributes(typeof (OrionVersionAttribute), false);
						if (customAttributes.Length == 0)
							continue;

						OrionVersionAttribute orionVersionAttribute = (OrionVersionAttribute) customAttributes[0];
						Version orionVersion = orionVersionAttribute.version;
						if (Version.Major != orionVersion.Major)
						{
							ServerApi.LogWriter.PluginWriteLine(this,
								String.Format("Orion plugin \"{0}\" is designed for a different Orion version ({1}) and was ignored.",
									type.FullName, orionVersion.ToString(2)), TraceLevel.Warning);

							continue;
						}

						OrionPlugin pluginInstance;
						try
						{
							pluginInstance = (OrionPlugin) Activator.CreateInstance(type, this);
						}
						catch (Exception ex)
						{
							// Broken plugins better stop the entire server init.
							throw new InvalidOperationException(
								String.Format("Could not create an instance of orion plugin class \"{0}\".", type.FullName), ex);
						}

						_loadedPlugins.Add(pluginInstance);
					}
				}
				catch (Exception ex)
				{
					// Broken assemblies / plugins better stop the entire server init.
					throw new InvalidOperationException(
						String.Format("Failed to load assembly \"{0}\".", fileInfo.Name), ex);
				}

				IOrderedEnumerable<OrionPlugin> orderedPlugins =
					from plugin in _loadedPlugins
					orderby plugin.Order
					select plugin;

				List<string> loaded = new List<string>(orderedPlugins.Count());

				foreach (OrionPlugin plugin in orderedPlugins)
				{
					try
					{
						plugin.Initialize();
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException(String.Format(
							"Orion plugin \"{0}\" has thrown an exception during initialization.", plugin.Name), ex);
					}
					loaded.Add(String.Format("{0} v{1} ({2})", plugin.Name, plugin.Version, plugin.Author));
				}

				ServerApi.LogWriter.PluginWriteLine(this,
					"Orion has successfully loaded the following plugins: " +
					"\n {" +
					"\n    " + String.Join("\n    ", loaded) +
					"\n }",
					TraceLevel.Info);
			}
		}

		private void UnloadPlugins()
		{
			foreach (OrionPlugin plugin in _loadedPlugins)
			{
				try
				{
					plugin.Dispose();
				}
				catch (Exception ex)
				{
					ServerApi.LogWriter.PluginWriteLine(this, String.Format(
						"Orion plugin \"{0}\" has thrown an exception while being disposed:\n{1}", plugin.Name, ex),
						TraceLevel.Error);
				}
			}
		}

		/// <summary>
		/// Called by the Server API. If <see cref="disposing"/> is true, the plugin should release its resources
		/// This should never be called by a plugin.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				UnloadPlugins();
			}

			base.Dispose(disposing);
		}
	}
}