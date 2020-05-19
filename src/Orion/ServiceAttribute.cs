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
using System.Diagnostics.CodeAnalysis;

namespace Orion {
    /// <summary>
    /// Specifies information about a service or plugin.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ServiceAttribute : Attribute {
        private string _author = "Pryaxis";

        /// <summary>
        /// Gets the service's name. This is used for logging.
        /// </summary>
        /// <value>The service's name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the service's author. This is used for logging.
        /// </summary>
        /// <value>The service's author. The default value is <c>Pryaxis</c>.</value>
        /// <exception cref="ArgumentNullException"><param name="value"/> is <see langword="null"/>.</exception>
        [DisallowNull]
        public string Author {
            get => _author;
            set => _author = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAttribute"/> with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        public ServiceAttribute(string name) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
