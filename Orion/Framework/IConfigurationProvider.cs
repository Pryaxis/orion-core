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
        /// Registers the specified object's property into the automatic configuration system along with
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
            
		/// <summary>
		/// Registers the specified Orion module into the automatic configuration system along with
		/// a LINQ-style lambda expression pointing to the configuration property that will be updated
		/// when the configuration is reloaded.
		/// </summary>
		/// <returns>A configurationRegistration instance with configurable parameters about the module's registration.</returns>
		/// <param name="target">The OrionModule that contains the property to be updated</param>
		/// <param name="configurationPropertySelector">
		/// A LINQ style lambda expression pointing to the configuration property inside the class that will be updated
		/// with the deserialized configuration on load, and serialized on save
		/// </param>
		/// <typeparam name="TModule">TModule is any Orion module</typeparam>
		/// <typeparam name="TConfigurationClass">TConfigurationClass is inferred from the type of the property in the LINQ expression</typeparam>
        ConfigurationRegistration RegisterProperty<TModule, TConfigurationClass>(TModule target, Expression<Func<TModule, TConfigurationClass>> configurationPropertySelector)
            where TConfigurationClass : class, new()
            where TModule : OrionModuleBase;
        
		/// <summary>
		/// Loads a deserialized configuration object as dictated by the configuration registration
		/// for the specified type.  This method causes the property specified in the configuration
		/// registration to also be updated with the return value from this call to Load().
		/// </summary>
		/// <param name="moduleType">Module type.</param>
        object Load(Type moduleType);
        
		/// <summary>
		/// Loads a deserialized configuration object as dictated by the configuration registration
		/// for the specified type, casted as a <typeparamref>TConfigurationObject</typeparamref>.
		/// This method causes the property specified in the registration to also be updated with the 
		/// return value from this call to Load().
		/// </summary>
		/// <param name="moduleType">Module type.</param>
		/// <typeparam name="TConfigurationObject">The type of the deserialized configuration object</typeparam>
        TConfigurationObject Load<TConfigurationObject>(Type moduleType)
            where TConfigurationObject : class, new();   
        
		/// <summary>
		/// Saves the contents of the registered configuration property to disk as a serialized object.
		/// </summary>
		/// <param name="moduleType">Module type.</param>
        void Save(Type moduleType);
    }
}
