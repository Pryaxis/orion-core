// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Serilog;

namespace Orion.Core.Framework.Extensions
{
    /// <summary>
    /// Provides the base class for an Orion extension (service or plugin).
    /// </summary>
    public abstract class OrionExtension : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrionExtension"/> class with the specified
        /// <paramref name="kernel"/> and <paramref name="log"/>.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="log">The extension-specific log to log to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="kernel"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        protected OrionExtension(OrionKernel kernel, ILogger log)
        {
            Kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>The kernel.</value>
        public OrionKernel Kernel { get; }

        /// <summary>
        /// Gets the extension-specific log.
        /// </summary>
        /// <value>The extension-specific log.</value>
        public ILogger Log { get; }

        /// <summary>
        /// Disposes the extension, releasing any resources associated with it.
        /// </summary>
        public virtual void Dispose() { }
    }
}
