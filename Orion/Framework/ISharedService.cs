using System;

namespace Orion.Framework
{
	/// <summary>
	/// Describes a shared Orion service.
	/// </summary>
	/// <remarks>
	/// Shared services are singleton services: the same instance will always be injected. Therefore, they cannot
	/// contain instance-specific state.
	/// </remarks>
	public interface ISharedService
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
