using System;

namespace Orion.Framework
{
	/// <summary>
	/// Describes an Orion service.
	/// </summary>
	public interface IService : IDisposable
	{
		/// <summary>
		/// Gets the service author.
		/// </summary>
		string Author { get; }

		/// <summary>
		/// Gets the service name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the service version.
		/// </summary>
		Version Version { get; }
	}
}
