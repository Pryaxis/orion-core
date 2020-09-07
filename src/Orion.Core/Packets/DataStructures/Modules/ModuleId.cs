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
using System.Collections.Generic;

namespace Orion.Core.Packets.DataStructures.Modules
{
    /// <summary>
    /// Specifies a module ID.
    /// </summary>
    public enum ModuleId : ushort
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Chat = 1,
        Ping = 2,
        Ambience = 3,
        Pylon = 8,
        Particle = 9
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Provides extensions for the <see cref="ModuleId"/> enumeration.
    /// </summary>
    public static class ModuleIdExtensions
    {
        private static readonly Dictionary<ModuleId, Type> _types = new Dictionary<ModuleId, Type>
        {
            [ModuleId.Chat] = typeof(Chat),
            [ModuleId.Ping] = typeof(Ping),
            [ModuleId.Ambience] = typeof(Ambience),
            [ModuleId.Pylon] = typeof(Pylon),
            [ModuleId.Particle] = typeof(Particle)
        };

        /// <summary>
        /// Gets the corresponding type for the module ID.
        /// </summary>
        /// <param name="id">The module ID.</param>
        /// <returns>The corresponding type for the module ID.</returns>
        public static Type Type(this ModuleId id) =>
            _types.TryGetValue(id, out var type) ? type : typeof(UnknownModule);
    }
}
