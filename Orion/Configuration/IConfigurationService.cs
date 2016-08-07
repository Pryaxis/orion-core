using System.IO;
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
		/// Loads a <typeparamref name="TConfiguration"/> from a custom stream.
		/// </summary>
		void Load(Stream stream);

		/// <summary>
		/// Loads a <typeparamref name="TConfiguration"/> object from the implementation's default data store, and sets
		/// the <see cref="Configuration"/> object to the deserialized representation of the configuration.
		/// </summary>
		void Load();

		/// <summary>
		/// Saves the <typeparamref name="TConfiguration"/> object to the implementaton's default persistent storage.
		/// </summary>
		void Save();

		/// <summary>
		/// Saves the <typeparamref name="TConfiguration"/> object to a custom stream.
		/// </summary>
		void Save(Stream stream);
	}
}
