using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;

namespace Orion.Framework
{
	/// <summary>
	/// A Ninject module that scans for services, shared services, and plugins in the executing and plugin assemblies,
	/// binding them into the appropriate scope.
	/// </summary>
	internal sealed class OrionModule : NinjectModule
	{
		private static IEnumerable<Type> GetInterfaces(Type derivedClass)
		{
			return derivedClass.GetInterfaces()
				.Select(type => type.IsGenericType ? type.GetGenericTypeDefinition() : type);
		}

		private static IEnumerable<Type> GetSubclasses<TBase>()
		{
			return new[] {Assembly.GetExecutingAssembly()}
				.Concat(LoadAssembliesFrom(Orion.PluginDirectory))
				.SelectMany(assembly => assembly.GetExportedTypes())
				.Where(type => type.IsSubclassOf(typeof(TBase)))
				.Select(type => type.IsGenericType ? type.GetGenericTypeDefinition() : type);
		}

		private static IEnumerable<Assembly> LoadAssembliesFrom(string path)
		{
			foreach (string assemblyPath in Directory.EnumerateFiles(path, "*.dll"))
			{
				Assembly assembly;
				try
				{
					assembly = Assembly.LoadFrom(assemblyPath);
				}
				catch (Exception ex) when (ex is BadImageFormatException || ex is IOException)
				{
					continue;
				}

				yield return assembly;
			}
		}

		/// <inheritdoc/>
		public override void Load()
		{
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

			foreach (Type service in GetSubclasses<Service>())
			{
				Bind(service).ToSelf().When(request => request.Target != null).InParentScope();
				Bind(service).ToSelf().When(request => request.Target == null).InTransientScope();
				foreach (Type serviceInterface in GetInterfaces(service))
				{
					Bind(serviceInterface).To(service).When(request => request.Target != null).InParentScope();
					Bind(serviceInterface).To(service).When(request => request.Target == null).InTransientScope();
				}
			}

			foreach (Type sharedModule in GetSubclasses<SharedService>().Concat(GetSubclasses<Plugin>()))
			{
				Bind(sharedModule).ToSelf().InSingletonScope();
				Bind(sharedModule.BaseType).To(sharedModule).InSingletonScope();
				foreach (Type sharedModuleInterface in GetInterfaces(sharedModule))
				{
					Bind(sharedModuleInterface).To(sharedModule).InSingletonScope();
				}
			}
		}
	}
}
