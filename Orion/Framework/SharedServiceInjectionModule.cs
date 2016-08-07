using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Ninject.Modules;

namespace Orion.Framework
{
	/// <summary>
	/// A Ninject module that binds shared service implementations to their service definitions.
	/// </summary>
	public class SharedServiceInjectionModule : NinjectModule
	{
		/// <inheritdoc/>
		/// <remarks>
		/// This will scan the Orion assembly and all assemblies in the plugins directory.
		/// </remarks>
		public override void Load()
		{
			IEnumerable<Type> serviceTypes = new[] { Assembly.GetExecutingAssembly() }
				.Concat(AssemblyResolver.LoadAssemblies(Orion.PluginDirectory))
				.SelectMany(a => a.GetExportedTypes())
				.Where(t => t.IsSubclassOf(typeof(SharedService)));

			foreach (Type serviceType in serviceTypes)
			{
				Bind(serviceType).To(serviceType).InSingletonScope();
				foreach (Type interfaceType in serviceType.GetInterfaces())
				{
					Bind(interfaceType).To(serviceType).InSingletonScope();
				}
			}
		}
	}
}
