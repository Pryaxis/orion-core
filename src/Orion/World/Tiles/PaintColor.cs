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
    /// Represents a paint color.
    /// </summary>
    public sealed class PaintColor {
#pragma warning disable 1591
        public static readonly PaintColor None = new PaintColor(0);
        public static readonly PaintColor Red = new PaintColor(1);
        public static readonly PaintColor Orange = new PaintColor(2);
        public static readonly PaintColor Yellow = new PaintColor(3);
        public static readonly PaintColor Lime = new PaintColor(4);
        public static readonly PaintColor Green = new PaintColor(5);
        public static readonly PaintColor Teal = new PaintColor(6);
        public static readonly PaintColor Cyan = new PaintColor(7);
        public static readonly PaintColor SkyBlue = new PaintColor(8);
        public static readonly PaintColor Blue = new PaintColor(9);
        public static readonly PaintColor Purple = new PaintColor(10);
        public static readonly PaintColor Violet = new PaintColor(11);
        public static readonly PaintColor Pink = new PaintColor(12);
        public static readonly PaintColor DeepRed = new PaintColor(13);
        public static readonly PaintColor DeepOrange = new PaintColor(14);
        public static readonly PaintColor DeepYellow = new PaintColor(15);
        public static readonly PaintColor DeepLime = new PaintColor(16);
        public static readonly PaintColor DeepGreen = new PaintColor(17);
        public static readonly PaintColor DeepTeal = new PaintColor(18);
        public static readonly PaintColor DeepCyan = new PaintColor(19);
        public static readonly PaintColor DeepSkyBlue = new PaintColor(20);
        public static readonly PaintColor DeepBlue = new PaintColor(21);
        public static readonly PaintColor DeepPurple = new PaintColor(22);
        public static readonly PaintColor DeepViolet = new PaintColor(23);
        public static readonly PaintColor DeepPink = new PaintColor(24);
        public static readonly PaintColor Black = new PaintColor(25);
        public static readonly PaintColor White = new PaintColor(26);
        public static readonly PaintColor Gray = new PaintColor(27);
        public static readonly PaintColor Brown = new PaintColor(28);
        public static readonly PaintColor Shadow = new PaintColor(29);
        public static readonly PaintColor Negative = new PaintColor(30);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 31;
        private static readonly PaintColor[] Colors = new PaintColor[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the paint color's ID.
        /// </summary>
        public byte Id { get; }

        private PaintColor(byte id) {
            Id = id;
        }

        static PaintColor() {
            foreach (var field in typeof(PaintColor).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var paintColor = (PaintColor)field.GetValue(null);
                Colors[ArrayOffset + paintColor.Id] = paintColor;
                Names[ArrayOffset + paintColor.Id] = field.Name;
            }
        }

        /// <summary>
        /// Returns a paint color converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The paint color, or <c>null</c> if none exists.</returns>
        public static PaintColor FromId(byte id) => ArrayOffset + id < ArraySize ? Colors[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
