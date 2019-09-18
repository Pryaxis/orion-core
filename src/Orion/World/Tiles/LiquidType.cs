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

namespace Orion.World.Tiles {
    /// <summary>
    /// Represents a liquid type.
    /// </summary>
    public sealed class LiquidType {
#pragma warning disable 1591
        public static readonly LiquidType Water = new LiquidType(0);
        public static readonly LiquidType Lava = new LiquidType(1);
        public static readonly LiquidType Honey = new LiquidType(2);
#pragma warning restore 1591

        private static readonly LiquidType[] Types = {Water, Lava, Honey};
        private static readonly string[] Names = {nameof(Water), nameof(Lava), nameof(Honey)};

        /// <summary>
        /// Gets the liquid type's ID.
        /// </summary>
        public byte Id { get; }

        private LiquidType(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a liquid type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The liquid type, or <c>null</c> if none exists.</returns>
        public static LiquidType FromId(byte id) => id < Types.Length ? Types[id] : null;

        /// <inheritdoc />
        public override string ToString() => Names[Id];
    }
}
