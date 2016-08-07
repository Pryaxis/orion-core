using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;

namespace Orion.Framework
{
	/// <summary>
	/// Ninject module responsible for injecting scoped services.
	/// </summary>
	/// <remarks>
	/// Scoped services are in in "parent scope", that is, a new instance of each
	/// service is injected to each plugin or service which requests it, and lives
	/// as long as the object that injects it lives.
	/// </remarks>
	public class ScopedServiceInjectionModule : NinjectModule
	{
		public override void Load()
		{
			IEnumerable<Type> serviceTypes = new[] {Assembly.GetExecutingAssembly()}
				.Concat(AssemblyResolver.LoadAssemblies(Orion.PluginDirectory))
				.SelectMany(a => a.GetExportedTypes())
				.Where(t => t.IsSubclassOf(typeof(Service)));

			foreach (Type serviceType in serviceTypes)
			{
				Bind(serviceType).To(serviceType).InParentScope();
				foreach (Type interfaceType in serviceType.GetInterfaces())
				{
					Bind(interfaceType).To(serviceType).InParentScope();
				}
			}
		}
	}
}