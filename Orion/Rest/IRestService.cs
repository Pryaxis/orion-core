using Orion.Framework;
using System;

namespace Orion.Rest
{
	/// <summary>
	/// Provides access to a REST service implementation
	/// </summary>
	public interface IRestService : ISharedService, IDisposable
	{
		/// <summary>
		/// Gets the desired binding address the REST implementation will be using
		/// </summary>
		string BaseAddress { get; }

		/// <summary>
		/// Starts the initialization process of the REST service
		/// </summary>
		void Startup();

		/// <summary>
		/// Shuts down the REST service
		/// </summary>
		void Shutdown();
	}
}
