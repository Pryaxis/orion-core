namespace Orion.Framework {
    using System;

    /// <summary>
    /// Represents a service.
    /// </summary>
    /// <remarks>
    /// Services are modules providing a specific type of functionality to consumers. By default, these services are
    /// statically loaded once Orion is loaded, and disposed once Orion is disposed. However, services can be annotated
    /// with <see cref="InstancedServiceAttribute"/>, in which case they will be injected per-instance and disposed
    /// once the parent object is garbage collected.
    /// </remarks>
    public interface IService : IDisposable {
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
