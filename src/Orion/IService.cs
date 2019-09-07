using System;

namespace Orion {
    /// <summary>
    /// Represents a service.
    /// </summary>
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
