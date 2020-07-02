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

namespace Orion.Core
{
    /// <summary>
    /// Specifies information about a service interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
    public sealed class ServiceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAttribute"/> class with the specified service
        /// <paramref name="scope"/>.
        /// </summary>
        /// <param name="scope">The service scope.</param>
        public ServiceAttribute(ServiceScope scope)
        {
            Scope = scope;
        }

        /// <summary>
        /// Gets the service's scope.
        /// </summary>
        /// <value>The service's scope.</value>
        public ServiceScope Scope { get; }
    }

    /// <summary>
    /// Controls the scope of a service. This encompasses the construction policy and lifetime management of the
    /// service.
    /// </summary>
    public enum ServiceScope
    {
        /// <summary>
        /// Indicates that the service should have singleton scope: i.e., only one implementation is ever constructed,
        /// and the lifetime of the implementation is limited to that of the container's.
        /// </summary>
        Singleton,

        /// <summary>
        /// Indicates that the service should have transient scope: i.e., a new implementation is constructed each time,
        /// and the lifetimes of the implementations are not managed.
        /// </summary>
        Transient
    }
}
