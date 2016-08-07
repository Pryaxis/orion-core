using Orion.Framework;

namespace Orion.Configuration
{
	/// <summary>
	/// Provides a generic interface to interact with persistent configuration for plugins
	/// and services without having to deal with where the data loads and saves from.
	/// </summary>
	public interface IConfigurationService<out TConfiguration> : IService
		where TConfiguration : class, new()
	{
		/// <summary>
		/// Gets the <typeparamref name="TConfiguration"/> object associated with this configuration service.
		/// </summary>
		TConfiguration Configuration { get; }

		/// <summary>
		/// Loads a <typeparamref name="TConfiguration"/> object from the configuration data store, and sets the
		/// <see cref="Configuration"/> object to the deserialized representation of the configuration.
		/// </summary>
		void Load();

		/// <summary>
		/// Saves the <typeparamref name="TConfiguration"/> object to persistent storage.
		/// </summary>
		void Save();
	}
}
