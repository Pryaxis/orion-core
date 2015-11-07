using System;
using System.Linq.Expressions;

using Orion.Modules.Configuration;

namespace Orion.Framework
{
    /// <summary>
    /// Interface that describes an Orion configuration provider.
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Registers the specified Orion module into the automatic configuration system along with
        /// a LINQ-style lambda expression pointing to the configuration property that will be updated
        /// when the configuration is reloaded.
        /// </summary>
        /// <typeparam name="TConfigurationClass">TConfigurationClass is inferred from the type of the property in the LINQ expression</typeparam>
        /// <param name="target">A reference to the object instance instance containing the configuration property</param>
        /// <param name="configurationPropertySelector">
        /// A LINQ style lambda expression pointing to the configuration property inside the class that will be updated
        /// with the deserialized configuration on load, and serialized on save
        /// </param>
        ConfigurationRegistration RegisterProperty<TConfigurationClass>(object target, Expression<Func<TConfigurationClass>> configurationPropertySelector)
            where TConfigurationClass : class, new();
            
        ConfigurationRegistration RegisterProperty<TModule, TConfigurationClass>(TModule target, Expression<Func<TModule, TConfigurationClass>> configurationPropertySelector)
            where TConfigurationClass : class, new()
            where TModule : OrionModuleBase;
            
        object Load(Type moduleType);
        
        TConfigurationObject Load<TConfigurationObject>(Type moduleType)
            where TConfigurationObject : class, new();   
        
        void Save(Type moduleType);
    }
}
