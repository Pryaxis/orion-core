namespace Orion.Rest.Owin
{
	/// <summary>
	/// OWIN specific configuration for use with the <see cref="OwinRestService"/>
	/// </summary>
	public class OwinConfiguration
	{
		/// <summary>
		/// Defines weather the <see cref="OwinRestService"/> is allowed to auto start
		/// </summary>
		public bool AutoStart { get; set; } = true;

		/// <summary>
		/// Base address for the REST service to listen on
		/// </summary>
		public string BaseAddress { get; set; } = "http://localhost:7878/";
	}
}
