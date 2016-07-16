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
				Bind(serviceEntry.Key).To(serviceEntry.Value);
			}
		}
	}
}
