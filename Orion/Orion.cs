using Ninject;
using Orion.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;

namespace Orion
{
	/// <summary>
	/// Handles Orion's dependency injection.
	/// </summary>
	public sealed class Orion : IDisposable
	{
		/// <summary>
		/// Gets the plugin directory.
		/// </summary>
		public static string PluginDirectory => "plugins";

		private readonly IKernel _injectionContainer;
		private bool _disposed;

		/// <summary>
		/// Initializes a new instance of the <see cref="Orion"/> class.
		/// </summary>
		internal Orion()
		{
			Directory.CreateDirectory(PluginDirectory);

			_injectionContainer = new StandardKernel(new OrionModule());
			_injectionContainer.Bind<Orion>().ToConstant(this);
		}

		/// <summary>
		/// Disposes the dependency injection kernel. This will dispose all shared services and plugins.
		/// </summary>
		public void Dispose()
		{
			if (_disposed)
			{
				return;
			}

			_injectionContainer.Dispose();
			_disposed = true;
		}
		
		/// <summary>
		/// Gets an Orion service by the type specified.
		/// </summary>
		/// <typeparam name="TService">
		/// TService is the type of any Orion service
		/// </typeparam>
		/// <returns>
		/// The <typeparamref name="TService"/> requested, or null if it cannot be found.
		/// </returns>
		public TService GetService<TService>()
		{
			return _injectionContainer.Get<TService>();
		}

		/// <summary>
		/// Gets a list of all Orion services by the type specified.
		/// </summary>
		/// <typeparam name="TService">
		/// TService is the type of any Orion service
		/// </typeparam>
		/// <returns>
		/// The <typeparamref name="TService"/> requested, or null if it cannot be found.
		/// </returns>
		public IEnumerable<TService> GetServices<TService>()
		{
			return _injectionContainer.GetAll<TService>();
		}

		/// <summary>
		/// Starts the server or client. This will initialize all shared services and plugins.
		/// </summary>
		/// <param name="args">The command-line arguments to use.</param>
		internal void Start(string[] args)
		{
			foreach (ISharedService service in GetServices<ISharedService>())
			{
				Console.WriteLine($"  * Loading {service.Name} by {service.Author}");
			}

			WindowsLaunch.Main(new string[] {});
		}
	}
}
