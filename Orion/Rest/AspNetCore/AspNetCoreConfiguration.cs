namespace Orion.Rest.AspNetCore
{
	/// <summary>
	/// Defines the configuration for use with the <see cref="AspNetCoreRestService"/>
	/// </summary>
	public class AspNetCoreConfiguration
	{
		/// <summary>
		/// Defines weather the <see cref="AspNetCoreRestService"/> is allowed to auto start
		/// </summary>
		public bool AutoStart { get; set; } = true;

		/// <summary>
		/// Base address for the REST service to listen on
		/// </summary>
		public string BaseAddress { get; set; } = "http://localhost:7878/";
	}
}
