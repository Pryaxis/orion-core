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

namespace Orion.Events {
    /// <summary>
    /// Specifies information about event arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    [BaseTypeRequired(typeof(Event))]
    public sealed class EventAttribute : Attribute {
        /// <summary>
        /// Gets the event's name. This is used for logs.
        /// </summary>
        /// <value>The event's name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the event is verbose. This is used for logs.
        /// </summary>
        /// <value><see langword="true"/> if the event is verbose; otherwise, <see langword="false"/>.</value>
        public bool IsVerbose { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventAttribute"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name. This is used for logs.</param>
        /// <remarks>
        /// The name should be short while <i>still disambiguating</i> the event among all other events. The convention
        /// is to use <c>kebab-case</c>.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        public EventAttribute(string name) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
