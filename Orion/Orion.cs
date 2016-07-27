using Ninject;
using Orion.Framework;
using System;
using System.IO;
using System.Reflection;
using Terraria;

namespace Orion
{
	public partial class Orion : IDisposable
	{
		private bool _disposed;
		internal ServiceMap serviceMap;

		/// <summary>
		/// Gets the dependency injection container, which contains references to plugins and services.
		/// </summary>
		public IKernel InjectionContainer { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Orion"/> class.
		/// </summary>
		/// <remarks>
		/// This creates Orion's directory structure, loads plugin assemblies, and defines the injection container.
		/// </remarks>
		public Orion()
		{
			CreateDirectories();
			LoadPluginAssemblies();

			this.serviceMap = new ServiceMap();

			InjectionContainer = new StandardKernel(
				new Framework.Injection.ServiceInjectionModule(serviceMap)
				);

			InjectionContainer.Bind<Orion>().ToConstant(this);
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
					InjectionContainer.Dispose();
				}
				_disposed = true;
			}
		}

		private void LoadPluginAssemblies()
		{
			foreach (string asmPath in Directory.GetFiles(PluginDirectory, "*.dll"))
			{
				try
				{
					Assembly.LoadFrom(asmPath);
				}
				catch (BadImageFormatException)
				{
				}
			}
		}

		/// <summary>
		/// Starts the server.
		/// </summary>
		public void StartServer()
		{
			foreach (IService service in InjectionContainer.GetAll<IService>())
			{
				Console.WriteLine($"  * Loading {service.Name} by {service.Author}");
			}

			WindowsLaunch.Main(new string[] {});
		}
	}
}
