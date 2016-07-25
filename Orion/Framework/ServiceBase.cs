using System;
using System.Reflection;

namespace Orion.Framework
{
	/// <summary>
	/// Provides the base class for all services.
	/// </summary>
	public abstract class ServiceBase : IService
	{
		/// <summary>
		/// Gets the service author.
		/// </summary>
		public string Author { get; } = "Anonymous";

		/// <summary>
		/// Gets the service name.
		/// </summary>
		public string Name { get; } = "Unnamed";

		/// <summary>
		/// Gets the parent <see cref="Orion"/> instance.
		/// </summary>
		protected Orion Orion { get; }

		/// <summary>
		/// Gets the service version.
		/// </summary>
		public virtual Version Version { get; } = new Version(1, 0, 0);

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceBase"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		protected ServiceBase(Orion orion)
		{
			Orion = orion;

			var attr = GetType().GetCustomAttribute<ServiceAttribute>();
			if (attr != null)
			{
				Author = attr.Author;
				Name = attr.Name;
			}
		}

        /// <summary>
        /// Destructor: ServiceBase
        /// 
        /// If your service has unmanaged resources, you must override <see cref="Dispose(bool)"/>
        /// and release it.
        /// </summary>
        ~ServiceBase()
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
		/// Disposes the service, suppressing the GC from running the finalizer, if any.
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

		public virtual void Start()
		{
		}
	}
}
