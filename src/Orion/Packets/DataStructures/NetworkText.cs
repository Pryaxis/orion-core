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
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Orion.Packets.DataStructures {
    /// <summary>
    /// Represents text transmitted over the network.
    /// </summary>
    public class NetworkText : IEquatable<NetworkText> {
        /// <summary>
        /// Gets the network text mode.
        /// </summary>
        /// <value>The network text mode.</value>
        public NetworkTextMode Mode { get; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; }

        /// <summary>
        /// Gets the substitutions. This is only applicable if <see cref="Mode"/> is not
        /// <see cref="NetworkTextMode.Literal"/>.
        /// </summary>
        /// <value>The substitutions.</value>
        public IReadOnlyList<NetworkText> Substitutions { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkText"/> class with the specified
        /// <paramref name="mode"/>, <paramref name="text"/>, and <paramref name="substitutions"/>.
        /// </summary>
        /// <param name="mode">The network text mode.</param>
        /// <param name="text">The text.</param>
        /// <param name="substitutions">The substitutions.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="text"/> or <paramref name="substitutions"/> are <see langword="null"/>.
        /// </exception>
        public NetworkText(NetworkTextMode mode, string text, params NetworkText[] substitutions)
            : this(mode, text, (IReadOnlyList<NetworkText>)substitutions) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkText"/> class with the specified
        /// <paramref name="mode"/>, <paramref name="text"/>, and <paramref name="substitutions"/>.
        /// </summary>
        /// <param name="mode">The network text mode.</param>
        /// <param name="text">The text.</param>
        /// <param name="substitutions">The substitutions.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="text"/> or <paramref name="substitutions"/> are <see langword="null"/>.
        /// </exception>
        public NetworkText(NetworkTextMode mode, string text, IReadOnlyList<NetworkText> substitutions) {
            Mode = mode;
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Substitutions = substitutions ?? throw new ArgumentNullException(nameof(substitutions));
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is NetworkText text && Equals(text);

        /// <inheritdoc/>
        public bool Equals(NetworkText other) {
            if (Mode != other.Mode || Text != other.Text || Substitutions.Count != other.Substitutions.Count) {
                return false;
            }

            for (var i = 0; i < Substitutions.Count; ++i) {
                if (!Substitutions[i].Equals(other.Substitutions[i])) {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode() {
            var hashCode = new HashCode();
            hashCode.Add(Mode);
            hashCode.Add(Text);
            foreach (var substitution in Substitutions) {
                hashCode.Add(substitution);
            }

            return hashCode.ToHashCode();
        }

        internal void Write(ref Span<byte> span, Encoding encoding) {
            Debug.Assert(encoding != null);

            span[0] = (byte)Mode;
            span = span[1..];
            SpanUtils.Write(ref span, Text, encoding);

            byte numSubstitutions = 0;
            if (Mode != NetworkTextMode.Literal) {
                numSubstitutions = (byte)Substitutions.Count;
                span[0] = numSubstitutions;
                span = span[1..];
            }

            for (var i = 0; i < numSubstitutions; ++i) {
                Substitutions[i].Write(ref span, encoding);
            }
        }

        /// <summary>
        /// Returns a value indicating whether two network texts are equal.
        /// </summary>
        /// <param name="left">The left network text.</param>
        /// <param name="right">The right network text.</param>
        /// <returns>
        /// <see langword="true"/> if the network texts are equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(NetworkText left, NetworkText right) => left.Equals(right);

        /// <summary>
        /// Returns a value indicating whether two network texts are not equal.
        /// </summary>
        /// <param name="left">The left network text.</param>
        /// <param name="right">The right network text.</param>
        /// <returns>
        /// <see langword="true"/> if the network texts are not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(NetworkText left, NetworkText right) => !left.Equals(right);

        /// <summary>
        /// Converts the given <paramref name="text"/> into a network text.
        /// </summary>
        /// <param name="text">The text.</param>
        public static implicit operator NetworkText(string text) => new NetworkText(NetworkTextMode.Literal, text);

        internal static NetworkText Read(ref ReadOnlySpan<byte> span, Encoding encoding) {
            Debug.Assert(encoding != null);

            var mode = (NetworkTextMode)span[0];
            span = span[1..];
            var text = SpanUtils.ReadString(ref span, encoding);
            var substitutions = new List<NetworkText>();

            var numSubstitutions = 0;
            if (mode != NetworkTextMode.Literal) {
                numSubstitutions = span[0];
                span = span[1..];
            }

            for (var i = 0; i < numSubstitutions; ++i) {
                substitutions.Add(Read(ref span, encoding));
            }
            return new NetworkText(mode, text, substitutions);
        }
    }
}
