using System;
using System.Reflection;

namespace Orion {
    /// <summary>
    /// Provides the base class for an <see cref="IService"/>.
    /// </summary>
    public abstract class OrionService : IService {
        /// <inheritdoc />
        public abstract string Author { get; }

        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        /// <remarks>
        /// By default, this property returns the version of the assembly containing the derived type.
        /// </remarks>
        public virtual Version Version => Assembly.GetAssembly(GetType()).GetName().Version;

        /// <summary>
        /// Destroys the service, releasing any of its unmanaged resources.
        /// </summary>
        ~OrionService() {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the service and any of its unmanaged and managed resources.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the service and any of its unmanaged resources, optionally including its managed resources.
        /// </summary>
        /// <param name="disposeManaged">
        /// <c>true</c> if called from a managed disposal and both unmanaged and managed resources must be released,
        /// <c>false</c> if called from a finalizer and only unmanaged resources must be released.
        /// </param>
        /// <remarks>
        /// If your service has unmanaged resources, you must release them here.
        /// </remarks>
        protected virtual void Dispose(bool disposeManaged) { }
    }
}
