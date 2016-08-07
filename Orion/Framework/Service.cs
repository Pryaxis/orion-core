using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework
{
	/// <summary>
	/// Base class which describes a non-shared (or scoped) service. 
	/// </summary>
	/// <remarks>
	/// Scoped services provide functionality to plugins and other services in the same manner as
	/// shared services, however new copies are instantiated each time they are requested in Orion.
	/// 
	/// Therefore, scoped services *may* contain instance-specific state.
	/// 
	/// Scoped services implement IDisposable, and are disposed when their owning instances get
	/// destroyed.
	/// </remarks>
	public abstract class Service : IService, IDisposable
	{
		public string Author { get; } = "Anonymous";
		public string Name { get; } = "Unnamed";

		public Version Version { get; } = new Version(1,0,0);
		
		/// <summary>
		/// Returns the Orion instance this service belongs to.
		/// </summary>
		protected Orion Orion { get; }

		/// <summary>
		/// Creates a new scoped service.
		/// </summary>
		/// <param name="orion">
		/// A reference to the Orion instance that this service belongs to.
		/// </param>
		protected Service(Orion orion)
		{
			this.Orion = orion;

			var attr = GetType().GetCustomAttribute<ServiceAttribute>();
			if (attr != null)
			{
				Author = attr.Author;
				Name = attr.Name;
			}
		}


		/// <summary>
		/// If your service has unmanaged resources, you must override <see cref="Dispose(bool)"/> and release it.
		/// </summary>
		~Service()
		{
			/*
			 * All unmanaged service resources must be deallocated in Dispose() regardless of if it's a
			 * managed dispose or a finalizer, HGlobal memory *must* be freed regardless or it will leak.
			 * 
			 * See CA1063 https://msdn.microsoft.com/en-us/library/ms244737.aspx
			 */
			Dispose(false);
		}

		/// <summary>
		/// Disposes the service, suppressing the GC from running the finalizer.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposes the service and its unmanaged resources, optionally disposing its managed resources.
		/// </summary>
		/// <param name="disposing">
		/// true if called from a managed disposal, and *both* unmanaged and managed resources must be freed. false
		/// if called from a finalizer, and *only* unmanaged resources may be freed.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
