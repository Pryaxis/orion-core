using Orion.SQL;
using System.ComponentModel;

namespace Orion.Configuration
{
	public class ConfigFile : BaseConfig
	{
		[Description("Folder path to Orion logs")]
		public string LogPath = "orion/logs";
		[Description("Folder path to orion plugins")]
		public string PluginsPath = "orion/plugins";

		[Description("Valid types are \"sqlite\" and \"mysql\"")]
		public SqlType StorageType = SqlType.Sqlite;
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

		public override void OnRead(BaseConfig baseCfg)
		{
			ConfigFile config = (ConfigFile)baseCfg;
			LogPath = config.LogPath;
			PluginsPath = config.PluginsPath;
			StorageType = config.StorageType;
			MySqlHost = config.MySqlHost;
			MySqlDbName = config.MySqlDbName;
			MySqlUsername = config.MySqlUsername;
			MySqlPassword = config.MySqlPassword;
			UseSqlLogging = config.UseSqlLogging;
			MaxSqlLogFailureCount = config.MaxSqlLogFailureCount;
			MaxUserCacheSize = config.MaxUserCacheSize;
			BCryptWorkFactor = config.BCryptWorkFactor;
			HashAlgorithm = config.HashAlgorithm;
		}
	}
}