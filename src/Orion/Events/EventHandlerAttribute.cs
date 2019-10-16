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

namespace Orion.Events {
    /// <summary>
    /// Specifies that a method is an event handler. This controls many aspects of an event handler.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class EventHandlerAttribute : Attribute {
        private string? name;

        /// <summary>
        /// Gets the event handler's priority.
        /// </summary>
        /// <value>The event handler's priority. The default value is <see cref="EventPriority.Normal"/>.</value>
        public EventPriority Priority { get; }

        /// <summary>
        /// Gets or sets the event handler's name. This is used for logs. If <see langword="null"/>, then the name
        /// will be the method name.
        /// </summary>
        /// <value>The event handler's name.</value>
        /// <exception cref="ArgumentNullException"><param name="value"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This should be short while <i>still disambiguating</i> the event handler among all other event handlers for
        /// that event. The convention is to use <c>kebab-case</c>.
        /// </remarks>
        [DisallowNull]
        public string? Name {
            get => name;
            set => name = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerAttribute"/> class with the specified priority.
        /// </summary>
        /// <param name="priority">The priority.</param>
        public EventHandlerAttribute(EventPriority priority) {
            Priority = priority;
        }
    }
}
