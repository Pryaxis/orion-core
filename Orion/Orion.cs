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

		/// <summary>
		/// Contains a reference to the injection container, which contains references
		/// to plugins and services.
		/// </summary>
		internal IKernel injectionContainer;
		internal ServiceMap serviceMap;

        /// <summary>
        /// Returns a reference to Orion's dependency injection container.
        /// </summary>
		public IKernel InjectionContainer { get { return injectionContainer; } }

		public Orion()
		{
			CreateDirectories();
			LoadPluginAssemblies();

			this.serviceMap = new ServiceMap();

			this.injectionContainer = new StandardKernel(
				new Framework.Injection.ServiceInjectionModule(serviceMap)
			);

			this.injectionContainer.Bind<Orion>().ToConstant(this);
		}

		/// <summary>
		/// Enumerates through all directories in Orion's standard directory list
		/// and creates them if they don't exist.
		/// </summary>
		public void CreateDirectories()
		{
			foreach (string dir in standardDirectories)
			{
				Directory.CreateDirectory(dir);
			}
		}

		public void LoadPluginAssemblies()
		{
			foreach (string asmPath in Directory.GetFiles(PluginDirectory, "*.dll"))
			{
				try
				{
					Assembly.LoadFrom(asmPath);
				}
				catch (Exception ex)
				{

				}
			}
		}

		public void StartServer()
		{
			foreach (IService service in injectionContainer.GetAll<IService>())
			{
				Console.WriteLine($"  * Loading {service.Name} by {service.Author}");
			}

			WindowsLaunch.Main(new string[] { });
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					injectionContainer.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
		}
		#endregion
	}
}
