using Microsoft.Owin.Hosting;
using Ninject;
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
		private IKernel _kernel;

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
		/// <param name="kernel">
		/// The master injection container kernel that is shared with the <see cref="Orion"/> instance
		/// </param>
		public OwinRestService
		(
			Orion orion,
			JsonFileConfigurationService<OwinConfiguration> configuration,
			IKernel kernel) : base(orion)
		{
			_configuration = configuration;
			_kernel = kernel;

			Debug.Assert(_configuration != null);
			Debug.Assert(_configuration.Configuration != null);
			Debug.Assert(kernel != null);

			if (Configuration.AutoStart)
			{
				Startup();
			}
		}

		/// <inheritdoc/>
		/// <exception cref="ObjectDisposedException"/>
		/// <exception cref="InvalidOperationException"/>
		public void Startup()
		{
			if (_kernel == null)
				throw new ObjectDisposedException(nameof(OwinRestService));
			if (_webApp != null)
				throw new InvalidOperationException("The service is already started");

			_webApp = WebApp.Start(url: BaseAddress, startup: Startup);
		}

		/// <inheritdoc/>
		public void Shutdown()
		{
			_webApp?.Dispose();
			_webApp = null;
		}

		/// <summary>
		/// Occurs when OWIN requests the service to configure the HTTP service.
		/// </summary>
		/// <param name="appBuilder">The <see cref="IAppBuilder"/> instance for use with configuring the HTTP service</param>
		private void Startup(IAppBuilder appBuilder)
		{
			var config = new HttpConfiguration();

			//ApiControllers need to be able to use dependency injection, so we need to
			//hook up our custom implementation of a Ninject dependency resolver
			config.DependencyResolver = new OwinNinjectDependencyResolver(_kernel);

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
					_webApp?.Dispose();
					_webApp = null;
					_kernel = null;
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
