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
		public Version Version { get; } = new Version(1, 0, 0);

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
				Version = Version.Parse(attr.Version);
			}
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
		/// Disposes the service and its unmanaged resources, if any, optionally disposing its managed resources, if
		/// any.
		/// </summary>
		/// <param name="disposing">
		/// true to dispose managed and unmanaged resources, false to only dispose unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
		}

		public virtual void Start()
		{
		}
	}
}
