using System;

namespace Orion.Framework
{
	public class ServiceBase : IService
	{
		/// <summary>
		/// Contains a reference to the Orion instance in which it belongs
		/// </summary>
		protected Orion Orion { get; set; }

		public string Author { get; } = "Nyx Studios";

		public string Name { get; }

		public Version Version { get; } = new Version(1, 0, 0);

		/// <summary>
		/// Creates a new instance of an Orion service.
		/// </summary>
		protected ServiceBase(Orion orion)
		{
			ServiceAttribute serviceAttr;

			this.Orion = orion;

			serviceAttr = Attribute.GetCustomAttribute(this.GetType(), typeof(ServiceAttribute)) as ServiceAttribute;
			if (serviceAttr != null)
			{
				Author = serviceAttr.Author;
				Name = serviceAttr.Name;
				Version = serviceAttr.Version;
			}
		}

		public virtual void Start()
		{
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
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
