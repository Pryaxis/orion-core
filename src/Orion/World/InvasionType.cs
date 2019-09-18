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

using System.Reflection;

namespace Orion.World {
    /// <summary>
    /// Represents an invasion type.
    /// </summary>
    public sealed class InvasionType {
#pragma warning disable 1591
        public static readonly InvasionType None = new InvasionType(0);
        public static readonly InvasionType Goblins = new InvasionType(1);
        public static readonly InvasionType FrostLegion = new InvasionType(2);
        public static readonly InvasionType Pirates = new InvasionType(3);
        public static readonly InvasionType Martians = new InvasionType(4);
#pragma warning restore 1591

        private static readonly InvasionType[] Types = new InvasionType[5];
        private static readonly string[] Names = new string[5];

        /// <summary>
        /// Gets the door action's ID.
        /// </summary>
        public byte Id { get; }

        static InvasionType() {
            foreach (var field in typeof(InvasionType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var invasionType = (InvasionType)field.GetValue(null);
                Types[invasionType.Id] = invasionType;
                Names[invasionType.Id] = field.Name;
            }
        }

        private InvasionType(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns an invasion type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The invasion type, or <c>null</c> if none exists.</returns>
        public static InvasionType FromId(byte id) => id < Types.Length ? Types[id] : null;

        /// <inheritdoc />
        public override string ToString() => Names[Id];
    }
}
