using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="module">A reference to the Orion module instance containing the configuration property</param>
        /// <param name="configurationPropertySelector">
        /// A LINQ style lambda expression pointing to the configuration property inside the Orion module class that will be updated
        /// with the deserialized configuration, and serialized on save
        /// </param>
        void Register<TConfigurationClass>(OrionModuleBase module, Expression<Func<TConfigurationClass>> configurationPropertySelector)
            where TConfigurationClass : class, new();
    }
}
