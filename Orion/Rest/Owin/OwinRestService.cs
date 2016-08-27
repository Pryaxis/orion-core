using Microsoft.Owin.Hosting;
using Orion.Configuration;
using Orion.Framework;
using Owin;
using System;
using System.Diagnostics;
using System.Web.Http;

namespace Orion.Rest.Owin
{
	/// <summary>
	/// Implements a <see cref="IRestService"/> using an implementation of the OWIN self hosted framework.
	/// </summary>
	[Service("Rest Server (OWIN)", Author = "Nyx Studios")]
	public class OwinRestService : SharedService, IRestService
	{
		private IConfigurationService<OwinConfiguration> _configuration;
		private IDisposable _webApp;

		/// <summary>
		/// Provides access to the configuration used for the OWIN service.
		/// </summary>
		protected OwinConfiguration Configuration => _configuration.Configuration;

		/// <inheritdoc/>
		public string BaseAddress => _configuration.Configuration.BaseAddress;

		/// <summary>
		/// Initializes a new instance of the <see cref="OwinRestService"/> class
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance</param>
		/// <param name="configuration">
		/// The configuration service instance to provide access to <see cref="OwinConfiguration"/>
		/// </param>
		public OwinRestService(Orion orion, JsonFileConfigurationService<OwinConfiguration> configuration) : base(orion)
		{
			this._configuration = configuration;

			Debug.Assert(_configuration != null);
			Debug.Assert(_configuration.Configuration != null);

			if (this.Configuration.AutoStart)
			{
				this.Startup();
			}
		}

		/// <inheritdoc/>
		public void Startup()
		{
			Debug.Assert(_webApp == null);
			_webApp = WebApp.Start(url: this.BaseAddress, startup: Startup);
		}

		/// <inheritdoc/>
		public void Shutdown()
		{
			Debug.Assert(_webApp != null);
			_webApp.Dispose();
			_webApp = null;
		}

		/// <summary>
		/// Occurs when OWIN requests the service to configure the HTTP service.
		/// </summary>
		/// <param name="appBuilder">The <see cref="IAppBuilder"/> instance for use with configuring the HTTP service</param>
		private void Startup(IAppBuilder appBuilder)
		{
			var config = new HttpConfiguration();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/v1/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			appBuilder.UseWebApi(config);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		/// <summary>
		/// Disposes the <see cref="OwinRestService"/> instance
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					if (_webApp != null)
					{
						_webApp.Dispose();
						_webApp = null;
					}
				}

				disposedValue = true;
			}
		}

		/// <summary>
		/// Disposes of the <see cref="OwinRestService"/> instance
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
		}
		#endregion
	}
}
