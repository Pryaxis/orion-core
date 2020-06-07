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

namespace Orion.Events {
    /// <summary>
    /// Marks a method as an event handler and specifies information about it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class EventHandlerAttribute : Attribute {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerAttribute"/> class with the specified
        /// <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        public EventHandlerAttribute(string name) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Gets or sets the event handler's name. This is used for logging.
        /// </summary>
        /// <value>The event handler's name.</value>
        /// <remarks>The naming convention for event handlers is <c>kebab-case</c>.</remarks>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the event handler's priority.
        /// </summary>
        /// <value>The event handler's priority. The default value is <see cref="EventPriority.Normal"/>.</value>
        public EventPriority Priority { get; set; } = EventPriority.Normal;

        /// <summary>
        /// Gets or sets a value indicating whether canceled events should be ignored.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if canceled events should be ignored; otherwise, <see langword="false"/>. The default
        /// value is <see langword="true"/>.
        /// </value>
        public bool IgnoreCanceled { get; set; } = true;
    }
}
