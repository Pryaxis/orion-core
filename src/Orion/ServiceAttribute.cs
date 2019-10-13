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
using JetBrains.Annotations;

namespace Orion {
    /// <summary>
    /// Specifies information about a service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    [BaseTypeRequired(typeof(OrionService))]
    public sealed class ServiceAttribute : Attribute {
        private string _author = "Pryaxis";

        /// <summary>
        /// Gets the service's name, which is used for logs.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the author, which is used for logs. By default, this will be <c>Pryaxis</c>.
        /// </summary>
        /// <exception cref="ArgumentNullException"><param name="value"/> is <see langword="null"/>.</exception>
        public string Author {
            get => _author;
            set => _author = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAttribute"/> with the specified name.
        /// </summary>
        /// <param name="name">
        /// The name, which is used for logs.
        /// 
        /// <para>
        /// This should be short while still disambiguating the service among all services. The convention is to use
        /// <c>kebab-case</c>.
        /// </para>
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        public ServiceAttribute(string name) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
