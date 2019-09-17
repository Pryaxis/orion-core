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
    /// Represents a logic sensor type.
    /// </summary>
    public sealed class LogicSensorType {
#pragma warning disable 1591
        public static readonly LogicSensorType None = new LogicSensorType(0);
        public static readonly LogicSensorType Day = new LogicSensorType(1);
        public static readonly LogicSensorType Night = new LogicSensorType(2);
        public static readonly LogicSensorType PlayerAbove = new LogicSensorType(3);
        public static readonly LogicSensorType Water = new LogicSensorType(4);
        public static readonly LogicSensorType Lava = new LogicSensorType(5);
        public static readonly LogicSensorType Honey = new LogicSensorType(6);
        public static readonly LogicSensorType Liquid = new LogicSensorType(7);
#pragma warning restore 1591

        private static readonly IDictionary<byte, FieldInfo> IdToField = new Dictionary<byte, FieldInfo>();

        private static readonly IDictionary<byte, LogicSensorType> IdToLogicSensorType =
            new Dictionary<byte, LogicSensorType>();

        /// <summary>
        /// Gets the logic sensor type's ID.
        /// </summary>
        public byte Id { get; }

        private LogicSensorType(byte id) {
            Id = id;
        }

        // Initializes lookup tables.
        static LogicSensorType() {
            var fields = typeof(LogicSensorType).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields) {
                if (!(field.GetValue(null) is LogicSensorType logicSensorType)) continue;

                IdToField[logicSensorType.Id] = field;
                IdToLogicSensorType[logicSensorType.Id] = logicSensorType;
            }
        }

        /// <summary>
        /// Returns a logic sensor type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The logic sensor type, or <c>null</c> if none exists.</returns>
        public static LogicSensorType FromId(byte id) =>
            IdToLogicSensorType.TryGetValue(id, out var logicSensorType) ? logicSensorType : null;

        /// <inheritdoc />
        public override string ToString() => IdToField[Id].Name;
    }
}
