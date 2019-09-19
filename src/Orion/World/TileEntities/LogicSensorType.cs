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

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 8;
        private static readonly LogicSensorType[] LogicSensors = new LogicSensorType[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the logic sensor type's ID.
        /// </summary>
        public byte Id { get; }

        private LogicSensorType(byte id) {
            Id = id;
        }

        static LogicSensorType() {
            foreach (var field in typeof(LogicSensorType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var logicSensorType = (LogicSensorType)field.GetValue(null);
                LogicSensors[ArrayOffset + logicSensorType.Id] = logicSensorType;
                Names[ArrayOffset + logicSensorType.Id] = field.Name;
            }
        }

        /// <summary>
        /// Returns a logic sensor type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The logic sensor type, or <c>null</c> if none exists.</returns>
        public static LogicSensorType FromId(byte id) =>
            ArrayOffset + id < ArraySize ? LogicSensors[ArrayOffset + id] : null;

        /// <inheritdoc />
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
