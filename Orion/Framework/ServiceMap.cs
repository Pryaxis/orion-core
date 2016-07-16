using Newtonsoft.Json;
using Ninject;
using Orion.Interfaces;
using Orion.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework
{
	/// <summary>
	/// The service map dictates what Orion service interfaces are bound to what
	/// implementations.
	/// </summary>
	public class ServiceMap
	{

		public ServiceMap()
		{
		}

		/// <summary>
		/// Contains the Orion service map.
		/// 
		/// The service map contains an entry for each service definition in Orion, and provides
		/// the type of the default implementation that the injector will bind to.  The service
		/// map can be overridden via runtime or file-time configuration.
		/// </summary>
		protected Dictionary<Type, Type> serviceMap = new Dictionary<Type, Type>()
		{
			{ typeof(ITileService), typeof(TileService) }
		};

		internal IDictionary<Type, Type> Map => serviceMap;

		/// <summary>
		/// Overrides a service in the service map.  Overrides the type of an Orion service implementation
		/// to something other than the default.
		/// </summary>
		/// <typeparam name="TServiceDefinition">
		/// TService definition is the interface type of the service definition to override
		/// </typeparam>
		/// <typeparam name="TImplementation">
		/// TImplementation is the type of the implementation of the service
		/// </typeparam>
		public void OverrideService<TServiceDefinition, TImplementation>(IKernel kernel)
			where TServiceDefinition : IService
			where TImplementation : ServiceBase
		{
			Type serviceDefinitionName = typeof(TServiceDefinition);

			if (serviceMap.ContainsKey(serviceDefinitionName) == false)
			{
				throw new Exception($"Service definition type {serviceDefinitionName} not found.");
			}

			serviceMap[serviceDefinitionName] = typeof(TImplementation);

			kernel
				.Rebind<TServiceDefinition>()
				.To(typeof(TImplementation))
				.InSingletonScope();
		}



		/// <summary>
		/// Saves the service map out to a file.
		/// </summary>
		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.UTF8, 1024, leaveOpen: true))
			{

			}
		}
	}
}
