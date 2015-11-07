using System;
using Orion.Framework;
using System.Linq.Expressions;

namespace Orion.Extensions
{
	public static class OrionModuleBaseConfigurationExtensions
	{
		public static void RegisterProperty<TModule, TConfigurationClass>(this TModule module, Expression<Func<TModule, TConfigurationClass>> configurationPropertySelector)
            where TConfigurationClass : class, new()
            where TModule : OrionModuleBase
        {
            module.Core.Configuration.RegisterProperty(module, configurationPropertySelector);
        }
	}	
}