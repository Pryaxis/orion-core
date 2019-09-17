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

using System.Collections.Generic;
using System.Reflection;

namespace Orion.World {
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

        private static readonly IDictionary<byte, FieldInfo> IdToField = new Dictionary<byte, FieldInfo>();
        private static readonly IDictionary<byte, SlopeType> IdToSlopeType = new Dictionary<byte, SlopeType>();

        /// <summary>
        /// Gets the slope type's ID.
        /// </summary>
        public byte Id { get; }

        private SlopeType(byte id) {
            Id = id;
        }

        // Initializes lookup tables.
        static SlopeType() {
            var fields = typeof(SlopeType).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields) {
                if (!(field.GetValue(null) is SlopeType slopeType)) continue;

                IdToField[slopeType.Id] = field;
                IdToSlopeType[slopeType.Id] = slopeType;
            }
        }

        /// <summary>
        /// Returns a slope type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The slope type, or <c>null</c> if none exists.</returns>
        public static SlopeType FromId(byte id) =>
            IdToSlopeType.TryGetValue(id, out var slopeType) ? slopeType : null;

        /// <inheritdoc />
        public override string ToString() => IdToField[Id].Name;
    }
}
