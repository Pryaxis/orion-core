using System;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace Orion.Configuration
{
	public class ConfigFile
	{
		[Description("Valid types are \"sqlite\" and \"mysql\"")] public string StorageType = "sqlite";

		[Description("The MySQL hostname and port to direct connections to")] 
		public string MySqlHost = "localhost:3306";
		[Description("Database name to connect to")]
		public string MySqlDbName = "";
		[Description("Database username to connect with")] 
		public string MySqlUsername = "";
		[Description("Database password to connect with")] 
		public string MySqlPassword = "";

		[Description("Whether or not to use SQL-based logging. False uses text logs")]
		public bool UseSqlLogging = false;
		[Description("Number of times the SQL log must fail to insert data before it reverts to text logging")] 
		public int MaxSqlLogFailureCount = 10;

		[Description("The maximum number of User objects that will be cached at any time")]
		public int MaxUserCacheSize = 255;

		[Description("Determines the BCrypt work factor to use. If increased, all passwords will be upgraded to new work-factor on verify. The number of computational rounds is 2^n. Increase with caution. Range: 5-31.")] 
		public int BCryptWorkFactor = 7;

		[Description("Valid types are \"sha512\", \"sha256\", \"md5\", append with \"-xp\" for the xp supported algorithms.")]
		public string HashAlgorithm = "sha512";

		/// <summary>
		/// Reads a configuration file from a given path
		/// </summary>
		/// <param name="path">string path</param>
		/// <returns>ConfigFile object</returns>
		public static ConfigFile Read(string path)
		{
			if (!File.Exists(path))
				return new ConfigFile();
			using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return Read(fs);
			}
		}

		/// <summary>
		/// Reads the configuration file from a stream
		/// </summary>
		/// <param name="stream">stream</param>
		/// <returns>ConfigFile object</returns>
		public static ConfigFile Read(Stream stream)
		{
			using (StreamReader sr = new StreamReader(stream))
			{
				ConfigFile cf = JsonConvert.DeserializeObject<ConfigFile>(sr.ReadToEnd());
				if (ConfigRead != null)
					ConfigRead(cf);
				return cf;
			}
		}

		/// <summary>
		/// Writes the configuration to a given path
		/// </summary>
		/// <param name="path">string path - Location to put the config file</param>
		public void Write(string path)
		{
			using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				Write(fs);
			}
		}

		/// <summary>
		/// Writes the configuration to a stream
		/// </summary>
		/// <param name="stream">stream</param>
		public void Write(Stream stream)
		{
			string str = JsonConvert.SerializeObject(this, Formatting.Indented);
			using (StreamWriter sw = new StreamWriter(stream))
			{
				sw.Write(str);
			}
		}

		/// <summary>
		/// On config read hook
		/// </summary>
		public static Action<ConfigFile> ConfigRead;
	}
}