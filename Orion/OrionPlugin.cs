using System;

namespace Orion {
    /// <summary>
    /// Provides the base class for an Orion plugin.
    /// </summary>
    public abstract class OrionPlugin : OrionService {
        /// <summary>
        /// Gets the <see cref="OrionKernel"/> instance.
        /// </summary>
        protected OrionKernel Kernel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionPlugin"/> class with the specified
        /// <see cref="OrionKernel"/> instance.
        /// </summary>
        /// <param name="kernel">The kernel instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="kernel"/> is <c>null</c>.</exception>
        protected OrionPlugin(OrionKernel kernel) {
            Kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }
    }
}
