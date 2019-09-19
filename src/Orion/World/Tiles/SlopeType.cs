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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Orion.World.Tiles {
    /// <summary>
    /// Represents a slope type.
    /// </summary>
    public sealed class SlopeType {
#pragma warning disable 1591
        public static readonly SlopeType None = new SlopeType(0);
        public static readonly SlopeType TopRight = new SlopeType(1);
        public static readonly SlopeType TopLeft = new SlopeType(2);
        public static readonly SlopeType BottomRight = new SlopeType(3);
        public static readonly SlopeType BottomLeft = new SlopeType(4);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 5;
        private static readonly SlopeType[] Slopes = new SlopeType[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the slope type's ID.
        /// </summary>
        public byte Id { get; }

        private SlopeType(byte id) {
            Id = id;
        }

        static SlopeType() {
            foreach (var field in typeof(SlopeType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var slopeType = (SlopeType)field.GetValue(null);
                Slopes[ArrayOffset + slopeType.Id] = slopeType;
                Names[ArrayOffset + slopeType.Id] = field.Name;
            }
        }

        /// <summary>
        /// Returns a slope type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The slope type, or <c>null</c> if none exists.</returns>
        public static SlopeType FromId(byte id) => ArrayOffset + id < ArraySize ? Slopes[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
