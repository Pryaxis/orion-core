using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace Orion.Framework.Injection
{
	/// <summary>
	/// Injects all the base Orion services according what's present in the service map. The service map may be
	/// overridden, in which case the the service will be re-bound.
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
			Kernel.Bind(
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
				);
		}
	}
}
