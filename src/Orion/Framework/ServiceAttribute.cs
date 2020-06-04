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

namespace Orion.Framework {
    /// <summary>
    /// Specifies information about a service interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
    public sealed class ServiceAttribute : Attribute {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAttribute"/> class with the specified service
        /// <paramref name="scope"/>.
        /// </summary>
        /// <param name="scope">The service scope.</param>
        public ServiceAttribute(ServiceScope scope) {
            Scope = scope;
        }

        /// <summary>
        /// Gets the service's scope.
        /// </summary>
        /// <value>The service's scope.</value>
        public ServiceScope Scope { get; }
    }
}
