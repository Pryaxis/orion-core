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

namespace Orion.Events {
    /// <summary>
    /// An attribute that can be applied to a event handler to indicate its priority.
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class EventHandlerAttribute : Attribute {
        /// <summary>
        /// Gets the priority of the handler.
        /// </summary>
        public EventPriority Priority { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerAttribute"/> class with the specified priority.
        /// </summary>
        /// <param name="priority">The priority.</param>
        public EventHandlerAttribute(EventPriority priority) {
            Priority = priority;
        }
    }
}
