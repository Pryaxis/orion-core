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
using Orion.Players;
using Serilog;

namespace Orion {
    /// <summary>
    /// Represents the base class for an Orion plugin, which is a thin wrapper around an <see cref="OrionService"/>.
    /// </summary>
    /// <remarks>
    /// Plugins can be injected by the <see cref="OrionKernel"/> instance and have static lifetimes. <para/>
    /// 
    /// The constructors of derived types should use lazily loaded services such as <see cref="Lazy{IPlayerService}"/>.
    /// This allows other plugins to potentially change the binding of <see cref="IPlayerService"/> to some other
    /// service implementation.
    /// </remarks>
    public abstract class OrionPlugin : OrionService {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrionPlugin"/> class with the specified
        /// <see cref="OrionKernel"/> instance and log.
        /// </summary>
        /// <param name="kernel">The <see cref="OrionKernel"/> instance.</param>
        /// <param name="log">The log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="kernel"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        protected OrionPlugin(OrionKernel kernel, ILogger log) : base(kernel, log) { }

        /// <summary>
        /// Initializes the plugin.
        /// </summary>
        /// <remarks>
        /// This method is called by the <see cref="OrionKernel"/> instance after all plugins have been loaded. Lazily
        /// loaded services can be accessed after this point.
        /// </remarks>
        public abstract void Initialize();
    }
}
