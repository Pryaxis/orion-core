using System;

namespace Orion.Framework
{
	/// <summary>
	/// Describes an Orion service.
	/// </summary>
	/// <remarks>
	/// Services are modules providing a specific type of functionality to consumers. New instances will always be
	/// injected into objects, and will be disposed of some time after the object is garbage collected. Therefore, they
	/// can contain instance-specific state.
	/// </remarks>
	public interface IService : IDisposable
	{
		/// <summary>
		/// Gets the service's author.
		/// </summary>
		string Author { get; }

		/// <summary>
		/// Gets the service's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the service's version.
		/// </summary>
		Version Version { get; }
	}
}
