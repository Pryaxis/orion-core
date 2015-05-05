using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;
using Orion.Configuration;
using Orion.Logging;
using Orion.Users;
using Terraria;
using TerrariaApi.Server;

namespace Orion
{
	[ApiVersion(1, 17)]
	public sealed class Orion : TerrariaPlugin
	{
		internal static Orion instance;
		
		public static string SavePath = "orion";
		public static string ConfigPath { get { return Path.Combine(SavePath, "OrionConfig.json"); } }
		public static ConfigFile Config { get; private set; }

		public static ILog Log { get; set; }

		public static IDbConnection database;
		public static UserManager Users { get; private set; }

		public override string Author
		{
			get { return "Neex Stoodyos"; }
		}

		public override string Name
		{
			get { return "Orion"; }
		}

		public override Version Version
		{
			get { return Assembly.GetExecutingAssembly().GetName().Version; }
		}

		public Orion(Main game) : base(game)
		{
			Config = new ConfigFile();
			Order = 0;
			instance = this;
		}

		public override void Initialize()
		{
			if (!Directory.Exists(SavePath))
				Directory.CreateDirectory(SavePath);

			ConfigFile.ConfigRead += OnConfigRead;
			if (File.Exists(ConfigPath))
			{
				Config = ConfigFile.Read(ConfigPath);
				// Add all the missing config properties in the json file
			}
			Config.Write(ConfigPath);

			if (Config.StorageType.ToLower() == "sqlite")
			{
				string sql = Path.Combine(SavePath, "orion.sqlite");
				database = new SqliteConnection(string.Format("uri=file://{0},Version=3", sql));
			}
			else if (Config.StorageType.ToLower() == "mysql")
			{
				try
				{
					string[] hostport = Config.MySqlHost.Split(':');
					database = new MySqlConnection
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
				Log = new SqlLog(database, Path.Combine(SavePath, "OrionLog.txt"), false);
			}
			else
			{
				Log = new TextLog(Path.Combine(SavePath, "OrionLog.txt"), false);
			}

			Users = new UserManager(database);
		}

		/// <summary>Fired when the config file has been read.</summary>
		/// <param name="file">The config file object.</param>
		private void OnConfigRead(ConfigFile file)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{

			}

			base.Dispose(disposing);
		}
	}
}