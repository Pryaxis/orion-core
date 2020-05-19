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

namespace Orion.Events {
    /// <summary>
    /// Specifies the priority of an event handler.
    /// </summary>
    /// <remarks>
    /// Priorities allow consumers to register event handlers with ordering. The event handlers are run in order from
    /// <see cref="Lowest"/> to <see cref="Highest"/>, with <see cref="Monitor"/> running last.
    /// </remarks>
    public enum EventPriority {
        /// <summary>
        /// Indicates that the event handler should have the lowest priority.
        /// </summary>
        Lowest,

        /// <summary>
        /// Indicates that the event handler should have low priority.
        /// </summary>
        Low,

        /// <summary>
        /// Indicates that the event handler should have normal priority. This is the default priority.
        /// </summary>
        Normal,

        /// <summary>
        /// Indicates that the event handler should have high priority.
        /// </summary>
        High,

        /// <summary>
        /// Indicates that the event handler should have the highest priority.
        /// </summary>
        Highest,

        /// <summary>
        /// Indicates that the event handler should have the monitor priority. The event handler is guaranteed to see
        /// the latest information, but is required to only <b>observe</b> the event.
        /// </summary>
        Monitor
    }
}
