using Ninject;
using Orion.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;

namespace Orion
{
	public partial class Orion : IDisposable
	{
		private bool _disposed;
		protected IKernel _injectionContainer;

		/// <summary>
		/// Initializes a new instance of the <see cref="Orion"/> class.
		/// </summary>
		/// <remarks>
		/// This creates Orion's directory structure, loads plugin assemblies, and defines the injection container.
		/// </remarks>
		public Orion()
		{
			CreateDirectories();

			_injectionContainer = new StandardKernel(new SharedServiceInjectionModule(), 
				new ScopedServiceInjectionModule());
			_injectionContainer.Bind<Orion>().ToConstant(this);
		}

		private void CreateDirectories()
		{
			foreach (string dir in standardDirectories)
			{
				Directory.CreateDirectory(dir);
			}
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_injectionContainer.Dispose();
				}
				_disposed = true;
			}
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
		/// Starts the server or client.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		internal void Start(string[] args)
		{
			foreach (ISharedService service in _injectionContainer.GetAll<ISharedService>())
			{
				Console.WriteLine($"  * Loading {service.Name} by {service.Author}");
			}

			WindowsLaunch.Main(args);
		}
	}
}
