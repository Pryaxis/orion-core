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
		/// <inheritdoc/>
		/// <remarks>
		/// This will scan the Orion assembly and all assemblies in the plugins directory.
		/// </remarks>
		public override void Load()
		{
			IEnumerable<Type> services = new[] {Assembly.GetExecutingAssembly()}
				.Concat(AssemblyResolver.LoadAssemblies(Orion.PluginDirectory))
				.SelectMany(a => a.GetExportedTypes())
				.Where(t => t.IsSubclassOf(typeof(Service)))
				.Select(t => t.IsGenericType ? t.GetGenericTypeDefinition() : t);

			/*
			 * WORKAROUND:
			 * 
			 * Scoped services are injected into plugins and services in parent scope, where the
			 * service is disposed as soon as the parent object which owns it is disposed, or
			 * the kernel gets released.
			 * 
			 * Scoped services resolved using the Get<> or GetAll<> methods in Orion have no
			 * parent, and the InParentScope resolution fails with a null reference as soon as any
			 * scoped instance is requested in this manner.
			 * 
			 * The workaround is this:
			 * 
			 * If a scoped service is requested with no parent (that is, resolved with Get/All),
			 * it is returned in transient scope, and the lifetime of it is left entirely up to
			 * the caller.
			 * 
			 * This results in two binding rules for each interface, but avoids the resolution 
			 * fails.
			 */

			foreach (Type service in services)
			{
				Bind(service).ToSelf().When(request => request.Target != null).InParentScope();
				Bind(service).ToSelf().When(request => request.Target == null).InTransientScope();

				IEnumerable<Type> serviceInterfaces = service.GetInterfaces()
					.Select(t => t.IsGenericType ? t.GetGenericTypeDefinition() : t);
				foreach (Type serviceInterface in serviceInterfaces)
				{
					Bind(serviceInterface).To(service).When(request => request.Target != null).InParentScope();
					Bind(serviceInterface).To(service).When(request => request.Target == null).InTransientScope();
				}
			}
		}
	}
}
