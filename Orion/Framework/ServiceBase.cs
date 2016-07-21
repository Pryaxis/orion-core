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
		/// Gets the parent <see cref="Orion"/> instance.
		/// </summary>
		protected Orion Orion { get; }

		/// <summary>
		/// Gets the service author.
		/// </summary>
		public string Author { get; } = "Anonymous";

		/// <summary>
		/// Gets the service name.
		/// </summary>
		public string Name { get; } = "Unnamed";

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

		public virtual void Start()
		{
		}

		#region IDisposable Support
		private bool _disposed; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				_disposed = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~ServiceBase() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
