using System;

namespace Orion.Framework
{
	/// <summary>
	/// Interface which describes all Orion services.
	/// </summary>
	public interface IService : IDisposable
	{
		/// <summary>
		/// Author of the Service
		/// </summary>
		string Author { get; }

		/// <summary>
		/// Name of the service
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Service version
		/// </summary>
		Version Version { get; }
	}
}
