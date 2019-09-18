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

namespace Orion.World.TileEntities {
    /// <summary>
    /// Represents a (generalized) tile entity type.
    /// </summary>
    public sealed class TileEntityType {
#pragma warning disable 1591
        public static readonly TileEntityType Chest = new TileEntityType(byte.MaxValue);
        public static readonly TileEntityType Sign = new TileEntityType(byte.MaxValue);
        public static readonly TileEntityType TargetDummy = new TileEntityType(0);
        public static readonly TileEntityType ItemFrame = new TileEntityType(1);
        public static readonly TileEntityType LogicSensor = new TileEntityType(2);
#pragma warning restore 1591

        private static readonly TileEntityType[] Types = {TargetDummy, ItemFrame, LogicSensor};

        private static readonly IDictionary<TileEntityType, string> TypeToName =
            new Dictionary<TileEntityType, string>();

        /// <summary>
        /// Gets the tile entity type's ID.
        /// </summary>
        public byte Id { get; }

        static TileEntityType() {
            foreach (var field in typeof(TileEntityType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var tileEntityType = (TileEntityType)field.GetValue(null);
                TypeToName[tileEntityType] = field.Name;
            }
        }

        private TileEntityType(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a tile entity type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The tile entity type, or <c>null</c> if none exists.</returns>
        public static TileEntityType FromId(byte id) => id < Types.Length ? Types[id] : null;

        /// <inheritdoc />
        public override string ToString() => TypeToName[this];
    }
}
