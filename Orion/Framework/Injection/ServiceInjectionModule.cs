using Ninject.Modules;
using Orion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Injection
{
	/// <summary>
	/// Injects all the base Orion services according what's present in the
	/// service map.  The service map may be overridden in which case the
	/// the service will be re-bound.
	/// </summary>
	public class ServiceInjectionModule : NinjectModule
	{
		protected ServiceMap serviceMap;

		public ServiceInjectionModule(ServiceMap serviceMap)
		{
			this.serviceMap = serviceMap;
		}

		public override void Load()
		{
			foreach (var serviceEntry in serviceMap.Map)
			{
				/*
				 * NOTE:
				 * 
				 * Bindings do not implicily flow up the implementation and inheritance
				 * chain, so a binding rule must be created for every type that will be 
				 * requested in orion.
				 * 
				 * For example, a binding to ITileService to Orion.Services.TileService
				 * will not resolve on calls to Get<ServiceBase> or Get<IService> even
				 * though all services implement IService and inherit from ServiceBase,
				 * you need to create the rules for these in the injection engine
				 * explicitly.
				 */
				
				Bind(serviceEntry.Key).To(serviceEntry.Value);
				Bind<IService>().To(serviceEntry.Value);
				Bind<ServiceBase>().To(serviceEntry.Value);
			}
		}
	}
}
