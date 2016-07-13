using Ninject;
using Ninject.Modules;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Framework.Injection
{
    public class OrionInjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Orion>().ToSelf().InSingletonScope();

            Kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses().InheritedFrom<ServiceBase>()
				.Join.FromAssembliesInPath(".\\plugins").SelectAllClasses().InheritedFrom<ServiceBase>()
                .BindAllInterfaces()
                .Configure(config =>
                {
                    config.InSingletonScope();
                }));
        }
    }
}
