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

namespace Orion.Core.Framework
{
    /// <summary>
    /// Specifies the priority of a service binding. Service bindings with higher priorities will take precedence over
    /// service bindings with lower priorities.
    /// </summary>
    public enum BindingPriority
    {
        /// <summary>
        /// Indicates that the service binding should have the lowest priority.
        /// </summary>
        Lowest,

        /// <summary>
        /// Indicates that the service binding should have low priority.
        /// </summary>
        Low,

        /// <summary>
        /// Indicates that the service binding should have normal priority. This is the default priority.
        /// </summary>
        Normal,

        /// <summary>
        /// Indicates that the service binding should have high priority.
        /// </summary>
        High,

        /// <summary>
        /// Indicates that the service binding should have the highest priority.
        /// </summary>
        Highest,
    }
}
