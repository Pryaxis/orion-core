using Orion.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Services
{
	/// <summary>
	/// Service Definition: IConfigurationService
	/// 
	/// Provides a generic interface to interact with persistent configuration for plugins
	/// and services without having to deal with where the data loads and saves from.
	/// </summary>
	public interface IConfigurationService : IService
	{
		/// <summary>
		/// Deserializes a <typeparamref name="TConfig"/> object from the configuration
		/// data store.
		/// </summary>
		/// <typeparam name="TConfig">
		/// TConfig is the class which stores the configuration members
		/// </typeparam>
		/// <returns>
		/// The deserialized configuration object as was loaded from the configuration datastore
		/// </returns>
		TConfig Load<TService, TConfig>()
			where TService : ServiceBase
			where TConfig : class, new();

		/// <summary>
		/// Saves the <typeparamref name="TConfig"/> object to persistent storage.
		/// </summary>
		/// <typeparam name="TConfig">
		/// TConfig is the class which stores the configuration members
		/// </typeparam>
		void Save<TService, TConfig>(TConfig config) 
			where TService : ServiceBase
			where TConfig : class, new();
	}
}
