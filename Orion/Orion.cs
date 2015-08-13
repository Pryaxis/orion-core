using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;

using Orion.SQL;
using Orion.Net;
using Orion.Bans;
using Orion.Hashing;
using Orion.Logging;
using Orion.Grouping;
using Orion.UserAccounts;
using Orion.Configuration;
using Utils = Orion.Utilities.Utils;

using Terraria;
using TerrariaApi.Server;

namespace Orion
{
	[ApiVersion(1, 21)]
	public class Orion : TerrariaPlugin
	{
		/// <summary>
		/// Nyx Studios
		/// </summary>
		public override string Author
		{
			get { return "Nyx Studios"; }
		}

		/// <summary>
		/// Orion
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



		/// <summary>
		/// Relative path to the Orion folder
		/// </summary>
		public string SavePath { get; private set; }

		/// <summary>
		/// Relative path to the log file
		/// </summary>
		public string LogPath { get; private set; }

		private string PluginPath { get; set; }

		/// <summary>
		/// Relative path to the config file
		/// </summary>
		public string ConfigPath { get; private set; }

		/// <summary>
		/// Config file. Use this to get data from the config
		/// </summary>
		public ConfigFile Config { get; private set; }

		/// <summary>
		/// Database connection to the Orion database
		/// </summary>
		internal IDbConnection Database { get; private set; }
		public ConfigCreator ConfigCreator { get; private set; }
		/// <summary>
		/// User handling object for getting users and setting values
		/// </summary>
		public UserAccountHandler Users { get; private set; }
		public GroupHandler Groups { get; private set; }
		/// <summary>
		/// Ban handling object for retrieving and creating bans
		/// </summary>
		public BanHandler Bans { get; private set; }
		/// <summary>
		/// Hash handling object for creating and upgrading hashes
		/// </summary>
		public Hasher HashHandler { get; private set; }
		/// <summary>
		/// Collection of handy utility methods
		/// </summary>
		public Utils Utils { get; private set; }
		/// <summary>
		/// Collection of handy utility methods for Terraria networking
		/// </summary>
		public NetUtils NetUtils { get; private set; }
		/// <summary>
		/// Collection of packet hooks
		/// </summary>
		public PacketRepackager Packets { get; private set; }
		/// <summary>
		/// Delegate method used with the <see cref="OnInitialized"></see> event
		/// </summary>
		public delegate void LoadedEvent();
		/// <summary>
		/// Event fired when Orion has been initialized
		/// </summary>
		public event LoadedEvent OnInitialized;
		/// <summary>
		/// Log manager. Use this to write data to the log
		/// </summary>
		public ILog Log { get; private set; }
		/// <summary>
		/// Assemblies loaded during plugin initialization
		/// </summary>
		private readonly Dictionary<string, Assembly> _loadedAssemblies = new Dictionary<string, Assembly>(); 
		/// <summary>
		/// Loaded plugins
		/// </summary>
		/// This is readonly so that plugins may view other plugins that are loaded, but not modify the list
		public readonly List<OrionPlugin> loadedPlugins = new List<OrionPlugin>();

		/// <summary>
		/// Constructor. Inherited from <see cref="TerrariaPlugin"></see>
		/// </summary>
		/// <param name="game"></param>
		public Orion(Main game) : base(game)
		{
			Order = 0;
			SavePath = "Orion";
			ConfigPath = Path.Combine(SavePath, "configs");
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

				ConfigCreator = new ConfigCreator(this);
				//Initialize config file. This will write it if it doesn't exist, or read it if it does.
				Config = ConfigCreator.Create<ConfigFile>("Orion");

				if (Config.StorageType == SqlType.Sqlite)
				{
					//Connect to SQLite database
					string sql = Path.Combine(SavePath, "orion.sqlite");
					Database = new SqliteConnection(String.Format("uri=file://{0},Version=3", sql));
				}
				else if (Config.StorageType == SqlType.Mysql)
				{
					//Connect to MySQL database
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
						throw new Exception(Strings.MySqlException);
					}
				}
				else
				{
					throw new Exception(Strings.InvalidDbTypeException);
				}
				
				//Commented until I figure out why it breaks
				/*if (Config.UseSqlLogging)
				{
					Log = new SqlLog(this, Path.Combine(LogPath, "log.log"), false);
				}
				else
				{
                    Log = new Log(Path.Combine(SavePath, "log4net.config"));
					Console.WriteLine("Log is not null");
				}*/

				Users = new UserAccountHandler(this);
				Groups = new GroupHandler(this);
				Bans = new BanHandler(this);
				Utils = new Utils(this);
				NetUtils = new NetUtils(this);
				HashHandler = new Hasher(this);
				Packets = new PacketRepackager(this);

				ServerApi.Hooks.NetGetData.Register(this, Packets.GetAndRepackage);
				ServerApi.Hooks.NetSendData.Register(this, Packets.SendAndRepackage);

				LoadPlugins();

				OrionInitialized();
			}
			catch (Exception ex)
			{
				Log.Error(Strings.StartupException);
				Log.Error(ex.ToString());
				Environment.Exit(1);
			}
		}

		/// <summary>
		/// Calls the OnInitialized event
		/// </summary>
		private void OrionInitialized()
		{
			if (OnInitialized != null)
			{
				OnInitialized();
			}
		}

		/// <summary>
		/// Handles command-line parameters
		/// </summary>
		/// <param name="args"></param>
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
							ServerApi.LogWriter.PluginWriteLine(this, String.Format(Strings.SavePathCmdLineOutput, path), TraceLevel.Info);
						}
						break;

					case "-configpath":
						path = args[i++];
						if (path.IndexOfAny(Path.GetInvalidPathChars()) == -1)
						{
							ConfigPath = path;
							ServerApi.LogWriter.PluginWriteLine(this, String.Format(Strings.ConfigPathCmdLineOutput, path), TraceLevel.Info);
						}
						break;
				}
			}
		}

		/// <summary>
		/// Loads dll plugins from the <see cref="PluginPath"/>
		/// </summary>
		private void LoadPlugins()
		{
			//Get a list of FileInfo for every file in the plugin directory
			List<FileInfo> fileInfos = new DirectoryInfo(PluginPath).GetFiles("*.dll").ToList();
		    var loaded = new List<string>();

			foreach (FileInfo fileInfo in fileInfos)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);

				try
				{
					//attempt to load the file
					Assembly assembly;
					if (!_loadedAssemblies.TryGetValue(fileNameWithoutExtension, out assembly))
					{
						assembly = Assembly.Load(File.ReadAllBytes(fileInfo.FullName));
						_loadedAssemblies.Add(fileNameWithoutExtension, assembly);
					}

					//iterate over the Types in the assembly
					foreach (Type type in assembly.GetExportedTypes())
					{
						//if it's not inheriting OrionPlugin, ignore
						if (!type.IsSubclassOf(typeof (OrionPlugin)) || !type.IsPublic || type.IsAbstract)
							continue;

						//Get the attributes the Type is using
						object[] customAttributes = type.GetCustomAttributes(typeof (OrionVersionAttribute), false);
												
						//if there's no attributes, ignore
						if (customAttributes.Length == 0)
							continue;

						//Assume that the first attribute will always be the OrionVersionAttribute
						OrionVersionAttribute orionVersionAttribute = (OrionVersionAttribute) customAttributes[0];
						Version orionVersion = orionVersionAttribute.version;
						//Make sure the plugin's major version matches our major version.
						if (Version.Major != orionVersion.Major)
						{
							ServerApi.LogWriter.PluginWriteLine(this,
								String.Format(Strings.PluginIgnoredOutput, type.FullName, orionVersion.ToString(2)), TraceLevel.Warning);

							continue;
						}

						//try and create an instance of the plugin
						OrionPlugin pluginInstance;
						try
						{
							pluginInstance = (OrionPlugin) Activator.CreateInstance(type, this);
						}
						catch (Exception ex)
						{
							// Broken plugins better stop the entire server init.
							throw new InvalidOperationException(
								String.Format(Strings.PluginInstanceFailedException, type.FullName), ex);
						}

						loadedPlugins.Add(pluginInstance);
					}
				}
				catch (Exception ex)
				{
					// Broken assemblies / plugins better stop the entire server init.
					throw new InvalidOperationException(
						String.Format(Strings.AssemblyLoadFailedException, fileInfo.Name), ex);
				}

				//order plugins by their order
				IOrderedEnumerable<OrionPlugin> orderedPlugins =
					from plugin in loadedPlugins
					orderby plugin.Order
					select plugin;

				//iterate over the ordered plugins and try and initialize them
				foreach (OrionPlugin plugin in orderedPlugins)
				{
					try
					{
						plugin.Initialize();
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException(String.Format(
							Strings.PluginInitializeException, plugin.Name), ex);
					}
					loaded.Add(String.Format("{0} v{1} ({2})", plugin.Name, plugin.Version, plugin.Author));
				}
			}

            ServerApi.LogWriter.PluginWriteLine(this,
                    String.Format(Strings.PluginsLoadedOutput, String.Join("\n    ", loaded)),
                    TraceLevel.Info);
		}

		/// <summary>
		/// Unloads loaded plugins
		/// </summary>
		private void UnloadPlugins()
		{
			//iterate over loaded plugins and try to dispose them
			foreach (OrionPlugin plugin in loadedPlugins)
			{
				try
				{
					plugin.Dispose();
				}
				catch (Exception ex)
				{
					ServerApi.LogWriter.PluginWriteLine(this, String.Format(
						Strings.PluginDisposedOutput, plugin.Name, ex),
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
				Users.UserCache.Flush();
			}

			base.Dispose(disposing);
		}
	}
}