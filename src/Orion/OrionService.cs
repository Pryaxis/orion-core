// Copyright (c) 2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace Orion {
    /// <summary>
    /// Represents the base class for an Orion service.
    /// </summary>
    /// <remarks>
    /// Services provide concrete functionality to clients and are injected using an <see cref="OrionKernel"/> instance.
    /// Existing service bindings may be overridden if necessary.
    /// </remarks>
    [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly",
        Justification = "IDisposable pattern makes no sense")]
    public abstract class OrionService : IDisposable {
        /// <summary>
        /// Gets the service's <see cref="OrionKernel"/> instance.
        /// </summary>
        /// <value>The service's <see cref="OrionKernel"/> instance.</value>
        public OrionKernel Kernel { get; }

        /// <summary>
        /// Gets the service's log.
        /// </summary>
        /// <value>The service's log.</value>
        public ILogger Log { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionService"/> class with the specified
        /// <see cref="OrionKernel"/> instance and log.
        /// </summary>
        /// <param name="kernel">The <see cref="OrionKernel"/> instance.</param>
        /// <param name="log">The log.</param>
        /// <exception cref="ArgumentNullException"><paramref name="log"/> is <see langword="null"/>.</exception>
        protected OrionService(OrionKernel kernel, ILogger log) {
            Kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Disposes the service, releasing any resources associated with it.
        /// </summary>
        [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize",
            Justification = "IDisposable pattern makes no sense")]
        public virtual void Dispose() { }
    }
}
