using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Ninject.Modules;

namespace Orion.Framework
{
	/// <summary>
	/// Injects all the base Orion services according what's present in the service map. The service map may be
	/// overridden, in which case the the service will be re-bound.
	/// </summary>
	public class ServiceInjectionModule : NinjectModule
	{
		// TODO: kill off service map?
		protected ServiceMap serviceMap;

		public ServiceInjectionModule(ServiceMap serviceMap)
		{
			this.serviceMap = serviceMap;
		}

		public override void Load()
		{
			IEnumerable<Type> serviceTypes = LoadAssemblies(Orion.PluginDirectory)
				.Concat(new[] {Assembly.GetExecutingAssembly()})
				.SelectMany(a => a.GetExportedTypes())
				.Where(t => t.IsSubclassOf(typeof(ServiceBase)));
			foreach (Type serviceType in serviceTypes)
			{
				foreach (Type interfaceType in serviceType.GetInterfaces())
				{
					Bind(interfaceType).To(serviceType).InSingletonScope();
				}
			}

			/*Kernel.Bind(
				i => i
					.FromThisAssembly()
					.SelectAllTypes().InheritedFrom<ServiceBase>()
					.Join.FromAssembliesInPath(Orion.PluginDirectory).SelectAllClasses().InheritedFrom<ServiceBase>()
					.BindAllInterfaces()
					.Configure(
						config =>
						{
							config.InSingletonScope();
						})
				);*/
		}

		private IEnumerable<Assembly> LoadAssemblies(string path)
		{
			foreach (string assemblyPath in Directory.EnumerateFiles(path, "*.dll"))
			{
				Assembly assembly;
				try
				{
					assembly = Assembly.LoadFile(assemblyPath);
				}
				catch (BadImageFormatException)
				{
					continue;
				}
				yield return assembly;
			}
		}
	}
}
