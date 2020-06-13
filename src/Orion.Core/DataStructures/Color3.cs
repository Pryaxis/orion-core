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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Orion.Core.DataStructures {
    /// <summary>
    /// Represents a three byte component color.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct Color3 : IEquatable<Color3> {
        /// <summary>
        /// Gets the color black.
        /// </summary>
        /// <value>The color black.</value>
        [ExcludeFromCodeCoverage] public static Color3 Black { get; } = new Color3(0x00, 0x00, 0x00);

        /// <summary>
        /// Gets the color white.
        /// </summary>
        /// <value>The color white.</value>
        [ExcludeFromCodeCoverage] public static Color3 White { get; } = new Color3(0xff, 0xff, 0xff);

        /// <summary>
        /// Gets the red component.
        /// </summary>
        /// <value>The red component.</value>
        [field: FieldOffset(0)] public byte R { get; }

        /// <summary>
        /// Gets the green component.
        /// </summary>
        /// <value>The green component.</value>
        [field: FieldOffset(1)] public byte G { get; }

        /// <summary>
        /// Gets the blue component.
        /// </summary>
        /// <value>The blue component.</value>
        [field: FieldOffset(2)] public byte B { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color3"/> structure with the specified color components.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        public Color3(byte r, byte g, byte b) {
            R = r;
            G = g;
            B = b;
        }

        /// <inheritdoc/>
        [Pure]
        public override bool Equals(object obj) => obj is Color3 other && Equals(other);

        /// <inheritdoc/>
        [Pure]
        public bool Equals(Color3 other) => R == other.R && G == other.G && B == other.B;

        /// <summary>
        /// Returns the hash code of the color.
        /// </summary>
        /// <returns>The hash code of the color.</returns>
        [Pure]
        public override int GetHashCode() => HashCode.Combine(R, G, B);

        /// <summary>
        /// Returns a string representation of the color.
        /// </summary>
        /// <returns>A string representation of the color.</returns>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"#{R:x2}{G:x2}{B:x2}";

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> is equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left color.</param>
        /// <param name="right">The right color.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [Pure]
        public static bool operator ==(Color3 left, Color3 right) => left.Equals(right);

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> is not equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left color.</param>
        /// <param name="right">The right color.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [Pure]
        public static bool operator !=(Color3 left, Color3 right) => !(left == right);
    }
}
