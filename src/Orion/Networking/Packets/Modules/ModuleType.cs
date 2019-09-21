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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Represents a module type.
    /// </summary>
    public sealed class ModuleType {
#pragma warning disable 1591
        public static ModuleType LiquidChanges = new ModuleType(0);
        public static ModuleType Chat = new ModuleType(1);
#pragma warning disable 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 2;
        private static readonly ModuleType[] Modules = new ModuleType[ArraySize];
        private static readonly string[] Names = new string[ArraySize];
        private static readonly Func<Module>[] Constructors = new Func<Module>[ArraySize];

        /// <summary>
        /// Gets the module type's ID.
        /// </summary>
        public ushort Id { get; }

        /// <summary>
        /// Gets the module type's constructor.
        /// </summary>
        public Func<Module> Constructor => Constructors[Id];

        static ModuleType() {
            foreach (var field in typeof(ModuleType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var moduleType = (ModuleType)field.GetValue(null);
                Modules[ArrayOffset + moduleType.Id] = moduleType;
                Names[ArrayOffset + moduleType.Id] = field.Name;
            }

            foreach (var type in typeof(Module).Assembly.ExportedTypes
                                               .Where(t => t.IsSubclassOf(typeof(Module)) && t != typeof(Module))) {
                var moduleType = ((Module)Activator.CreateInstance(type)).Type;
                Constructors[ArrayOffset + moduleType.Id] = () => (Module)Activator.CreateInstance(type);
            }
        }

        private ModuleType(ushort id) {
            Id = id;
        }

        /// <summary>
        /// Returns a module type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The module type.</returns>
        public static ModuleType FromId(ushort id) => ArrayOffset + id < ArraySize ? Modules[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
