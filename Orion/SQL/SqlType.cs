namespace Orion.SQL
{
	public enum SqlType
	{
		/// <summary>
		/// Unknown SQL connection type
		/// </summary>
		Unknown,
		/// <summary>
		/// Connection is to an SQLite database
		/// </summary>
		Sqlite,
		/// <summary>
		/// Connection is to a MySQL database
		/// </summary>
		Mysql
	}
}
