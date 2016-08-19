using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Orion.Configuration;
using Orion.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace Orion.Rest.AspNetCore
{
	/// <summary>
	/// Implements a <see cref="IRestService"/> using the ASP.NET Core framework.
	/// </summary>
	[Service("Rest Service (ASP)", Author = "Nyx Studios")]
	public class AspNetCoreRestService : SharedService, IRestService
	{
		private IConfigurationService<AspNetCoreConfiguration> _configuration;
		private IDisposable _host;

		/// <summary>
		/// Provides access to the configuration used for the service.
		/// </summary>
		protected AspNetCoreConfiguration Configuration => _configuration.Configuration;

		/// <inheritdoc/>
		public string BaseAddress => _configuration.Configuration.BaseAddress;

		/// <summary>
		/// Initializes a new instance of the <see cref="AspNetCoreRestService"/> class
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance</param>
		/// <param name="configuration">
		/// The configuration service instance to provide access to <see cref="AspNetCoreConfiguration"/>
		/// </param>
		public AspNetCoreRestService(Orion orion, JsonFileConfigurationService<AspNetCoreConfiguration> configuration) : base(orion)
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
			Debug.Assert(_host == null);
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseStartup<AspNetCoreStartup>()
				.UseUrls(_configuration.Configuration.BaseAddress)
				.Build();

			host.Start();

			_host = host;
		}

		/// <inheritdoc/>
		public void Shutdown()
		{
			Debug.Assert(_host != null);
			_host.Dispose();
			_host = null;
		}

		/// <summary>
		/// ASP .Net Core startup class.
		/// Used to configure the http service.
		/// </summary>
		internal class AspNetCoreStartup
		{
			/// <summary>
			/// Adds framework services
			/// </summary>
			/// <param name="services"></param>
			public void ConfigureServices(IServiceCollection services)
			{
				var builder = services.AddMvc();

				//TODO: I haven't yet found docs about MVC loading external
				//assemblies. But currently it doesn't work, so I need
				//to each assembly that references Microsoft.AspNetCore.*
				//in manually
				var assemblies = AppDomain.CurrentDomain.GetAssemblies()
					.Where(x => !x.FullName.StartsWith("Microsoft.AspNetCore")
						&& x.GetReferencedAssemblies().Any(
							r => r.Name.StartsWith("Microsoft.AspNetCore")
						)
					);
				foreach (var assembly in assemblies)
				{
					builder.AddApplicationPart(assembly);
				}
			}

			/// <summary>
			/// Configures the application request pipeline
			/// </summary>
			/// <param name="app"></param>
			public void Configure(IApplicationBuilder app)
			{
				app.UseMvc();
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		/// <summary>
		/// Disposes the <see cref="AspNetCoreRestService"/> instance
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					if (_host != null)
					{
						_host.Dispose();
						_host = null;
					}
				}

				disposedValue = true;
			}
		}

		/// <summary>
		/// Disposes of the <see cref="AspNetCoreRestService"/> instance
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
		}
		#endregion
	}
}
