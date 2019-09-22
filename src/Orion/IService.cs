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
using JetBrains.Annotations;

namespace Orion {
    /// <summary>
    /// Represents a service. Services provide concrete units of functionality to clients and are injected using a
    /// dependency injection framework. They may be made instanced using an <see cref="InstancedServiceAttribute"/>.
    /// </summary>
    [PublicAPI]
    public interface IService : IDisposable {
        /// <summary>
        /// Gets the service's author.
        /// </summary>
        [NotNull]
        string Author { get; }

        /// <summary>
        /// Gets the service's name.
        /// </summary>
        [NotNull]
        string Name { get; }

        /// <summary>
        /// Gets the service's version.
        /// </summary>
        [NotNull]
        Version Version { get; }
    }
}
