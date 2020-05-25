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
using System.Runtime.InteropServices;

namespace Orion.Packets.DataStructures {
    /// <summary>
    /// Reprseents a two float component vector.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Vector2f : IEquatable<Vector2f> {
        /// <summary>
        /// The zero vector.
        /// </summary>
        public static readonly Vector2f Zero = new Vector2f(0, 0);

        /// <summary>
        /// Gets the X component.
        /// </summary>
        [field: FieldOffset(0)] public float X { get; }

        /// <summary>
        /// Gets the Y component.
        /// </summary>
        [field: FieldOffset(4)] public float Y { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2f"/> structure with the specified components.
        /// </summary>
        /// <param name="x">The X component.</param>
        /// <param name="y">The Y component.</param>
        public Vector2f(float x, float y) {
            X = x;
            Y = y;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Vector2f vector && Equals(vector);

        /// <inheritdoc/>
        public bool Equals(Vector2f other) => X == other.X && Y == other.Y;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(X, Y);

        /// <summary>
        /// Returns a string representation of the vector.
        /// </summary>
        /// <returns>A string representation of the vector.</returns>
        public override string ToString() => $"({X}, {Y})";

        /// <summary>
        /// Determines whether two vectors are equal.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Vector2f left, Vector2f right) => left.Equals(right);

        /// <summary>
        /// Determines whether two vectors are not equal.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Vector2f left, Vector2f right) => !left.Equals(right);

        /// <summary>
        /// Adds the given vectors.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>The vector sum.</returns>
        public static Vector2f operator +(Vector2f left, Vector2f right) =>
            new Vector2f(left.X + right.X, left.Y + right.Y);

        /// <summary>
        /// Subtracts the given vectors.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>The vector difference.</returns>
        public static Vector2f operator -(Vector2f left, Vector2f right) =>
            new Vector2f(left.X - right.X, left.Y - right.Y);

        /// <summary>
        /// Multiplies the given <paramref name="vector"/> by <paramref name="f"/>.
        /// </summary>
        /// <param name="f">The factor.</param>
        /// <param name="vector">The vector.</param>
        /// <returns>The vector multiplication.</returns>
        public static Vector2f operator *(float f, Vector2f vector) => new Vector2f(f * vector.X, f * vector.Y);

        /// <summary>
        /// Multiplies the given <paramref name="vector"/> by <paramref name="f"/>.
        /// </summary>
        /// <param name="f">The factor.</param>
        /// <param name="vector">The vector.</param>
        /// <returns>The vector multiplication.</returns>
        public static Vector2f operator /(Vector2f vector, float f) => new Vector2f(vector.X / f, vector.Y / f);
    }
}
