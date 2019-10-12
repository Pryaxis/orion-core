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

namespace Orion {
    /// <summary>
    /// Represents the base class for an Orion service. Services provide concrete functionality to clients, and are
    /// injected using a dependency injection framework.
    /// </summary>
    public abstract class OrionService : IDisposable {
        /// <summary>
        /// Gets the service's author. By default, this will return <c>Pryaxis</c>.
        /// </summary>
        public virtual string Author => "Pryaxis";

        /// <summary>
        /// Gets the service's name. By default, this will return the type name.
        /// </summary>
        public virtual string Name => GetType().Name;

        /// <summary>
        /// Gets the service's version. By default, this will return the containing assembly's version.
        /// </summary>
        public virtual Version Version => GetType().Assembly.GetName().Version;

        /// <inheritdoc/>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the service and any of its unmanaged resources, optionally including its managed resources.
        /// </summary>
        /// <param name="disposeManaged">
        /// <see langword="true"/> to dispose managed resources, otherwise, <see langword="false"/>.
        /// </param>
        protected virtual void Dispose(bool disposeManaged) { }
    }
}
