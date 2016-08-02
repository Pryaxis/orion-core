using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Ninject.Modules;

namespace Orion.Framework
{
	/// <summary>
	/// A Ninject module that binds service interfaces to implementations.
	/// </summary>
	public class ServiceInjectionModule : NinjectModule
	{
		// TODO: kill off service map?
		protected ServiceMap serviceMap;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceInjectionModule"/> class.
		/// </summary>
		public ServiceInjectionModule(ServiceMap serviceMap)
		{
			this.serviceMap = serviceMap;
		}

		/// <inheritdoc/>
		/// <remarks>
		/// This will scan the Orion assembly and all assemblies in the plugins directory.
		/// </remarks>
		public override void Load()
		{
			IEnumerable<Type> serviceTypes = new[] {Assembly.GetExecutingAssembly()}
				.Concat(LoadAssemblies(Orion.PluginDirectory))
				.SelectMany(a => a.GetExportedTypes())
				.Where(t => t.IsSubclassOf(typeof(ServiceBase)));
			foreach (Type serviceType in serviceTypes)
			{
				Bind(serviceType).To(serviceType).InSingletonScope();
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

		private static IEnumerable<Assembly> LoadAssemblies(string path)
		{
			foreach (string assemblyPath in Directory.EnumerateFiles(path, "*.dll"))
			{
				Assembly assembly;
				try
				{
					assembly = Assembly.LoadFrom(assemblyPath);
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
