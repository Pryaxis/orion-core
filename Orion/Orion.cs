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
		internal ServiceMap serviceMap;

		/// <summary>
		/// Gets the dependency injection container, which contains references to plugins and services.
		/// </summary>
		public IKernel InjectionContainer { get; }

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
			foreach (IService service in InjectionContainer.GetAll<IService>())
			{
				Console.WriteLine($"  * Loading {service.Name} by {service.Author}");
			}

			WindowsLaunch.Main(new string[] {});
		}

		#region IDisposable Support

		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					InjectionContainer.Dispose();
				}
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
