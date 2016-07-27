using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using Orion.Configuration;
using Orion.Entities.Item;
using Orion.Entities.Player;
using Orion.Entities.Projectile;
using Orion.World;

namespace Orion.Framework
{
	/// <summary>
	/// The service map dictates what Orion service interfaces are bound to what
	/// implementations.
	/// </summary>
	public class ServiceMap
	{
		/// <summary>
		/// Contains the Orion service map.
		/// 
		/// The service map contains an entry for each service definition in Orion, and provides
		/// the type of the default implementation that the injector will bind to.  The service
		/// map can be overridden via runtime or file-time configuration.
		/// </summary>
		protected Dictionary<Type, Type> serviceMap = new Dictionary<Type, Type>
		{
			[typeof(IConfigurationService)] = typeof(JsonFileConfigurationService),
			[typeof(IItemService)] = typeof(ItemService),
			[typeof(IPlayerService)] = typeof(PlayerService),
			[typeof(IProjectileService)] = typeof(ProjectileService),
			[typeof(ITileService)] = typeof(TileService),
			[typeof(IWorldService)] = typeof(WorldService)
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
			where TImplementation : TServiceDefinition
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
		/// Saves the service map to a stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.UTF8, 1024, leaveOpen: true))
			{
				throw new NotImplementedException();
			}
		}
	}
}
