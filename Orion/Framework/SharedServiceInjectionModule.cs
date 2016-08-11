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
			IEnumerable<Type> services = new[] {Assembly.GetExecutingAssembly()}
				.Concat(AssemblyResolver.LoadAssemblies(Orion.PluginDirectory))
				.SelectMany(a => a.GetExportedTypes())
				.Where(t => t.IsSubclassOf(typeof(SharedService)))
				.Select(t => t.IsGenericType ? t.GetGenericTypeDefinition() : t);

			foreach (Type service in services)
			{
				Bind(service).ToSelf().InSingletonScope();

				IEnumerable<Type> serviceInterfaces = service.GetInterfaces()
					.Select(t => t.IsGenericType ? t.GetGenericTypeDefinition() : t);
				foreach (Type serviceInterface in serviceInterfaces)
				{
					Bind(serviceInterface).To(service).InSingletonScope();
				}
			}
		}
	}
}
