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
using Serilog.Events;

namespace Orion.Events {
    /// <summary>
    /// Specifies information about an event.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class EventAttribute : Attribute {
        /// <summary>
        /// Gets the event's name. This is used for logging.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the event's logging level.
        /// </summary>
        /// <value>The event's logging level. The default value is <see cref="LogEventLevel.Debug"/>.</value>
        public LogEventLevel LoggingLevel { get; set; } = LogEventLevel.Debug;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventAttribute"/> class with the specified
        /// <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        public EventAttribute(string name) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
