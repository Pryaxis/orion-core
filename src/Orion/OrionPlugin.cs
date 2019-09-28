// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

namespace Orion {
    /// <summary>
    /// Represents the base class for an Orion plugin, which is a thin wrapper around an <see cref="OrionService"/>.
    /// Plugins are injected using a dependency injection framework.
    /// </summary>
    public abstract class OrionPlugin : OrionService {
        /// <summary>
        /// Gets the Orion kernel.
        /// </summary>
        protected OrionKernel Kernel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionPlugin"/> class with the specified Orion kernel.
        /// </summary>
        /// <param name="kernel">The Orion kernel.</param>
        /// <exception cref="ArgumentNullException"><paramref name="kernel"/> is <c>null</c>.</exception>
        protected OrionPlugin(OrionKernel kernel) {
            Kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }
    }
}
