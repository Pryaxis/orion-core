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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;

namespace Orion.Core.Packets.DataStructures
{
    /// <summary>
    /// Represents text transmitted over the network.
    /// </summary>
    /// <remarks>
    /// This class is thread-safe.
    /// </remarks>
    public sealed class NetworkText : IEquatable<NetworkText>
    {
        /// <summary>
        /// Represents the empty network text.
        /// </summary>
        public static readonly NetworkText Empty = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkText"/> class with the specified
        /// <paramref name="mode"/>, <paramref name="format"/>, and <paramref name="substitutions"/>.
        /// </summary>
        /// <param name="mode">The network text mode.</param>
        /// <param name="format">The format.</param>
        /// <param name="substitutions">The substitutions.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="substitutions"/> contains <see langword="null"/> or is non-empty and <paramref name="mode"/>
        /// is <see cref="NetworkTextMode.Literal"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="format"/> or <paramref name="substitutions"/> are <see langword="null"/>.
        /// </exception>
        public NetworkText(NetworkTextMode mode, string format, params NetworkText[] substitutions)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            if (substitutions is null)
            {
                throw new ArgumentNullException(nameof(substitutions));
            }

            if (substitutions.Contains(null!))
            {
                throw new ArgumentException("Substitutions cannot contain null", nameof(substitutions));
            }

            if (mode == NetworkTextMode.Literal && substitutions.Length > 0)
            {
                throw new ArgumentException(
                    "Substitutions cannot be non-empty for literal mode", nameof(substitutions));
            }

            Mode = mode;
            Format = format;
            Substitutions = substitutions;
        }

        /// <summary>
        /// Gets the network text's mode.
        /// </summary>
        /// <value>The network text's mode.</value>
        public NetworkTextMode Mode { get; }

        /// <summary>
        /// Gets the network text's format.
        /// </summary>
        /// <value>The network text's format.</value>
        public string Format { get; }

        /// <summary>
        /// Gets the network text's substitutions.
        /// </summary>
        /// <value>The network text's substitutions.</value>
        public IReadOnlyList<NetworkText> Substitutions { get; }

        /// <inheritdoc/>
        [Pure]
        public override bool Equals(object? obj) => obj is NetworkText other && Equals(other);

        /// <inheritdoc/>
        [Pure]
        public bool Equals(NetworkText? other)
        {
            if (other is null)
            {
                return false;
            }

            if (Mode != other.Mode || Format != other.Format || Substitutions.Count != other.Substitutions.Count)
            {
                return false;
            }

            for (var i = 0; i < Substitutions.Count; ++i)
            {
                if (!Substitutions[i].Equals(other.Substitutions[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the hash code of the network text.
        /// </summary>
        /// <returns>The hash code of the network text.</returns>
        [Pure]
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Mode);
            hashCode.Add(Format);
            foreach (var substitution in Substitutions)
            {
                hashCode.Add(substitution);
            }

            return hashCode.ToHashCode();
        }

        /// <summary>
        /// Returns a string representation of the network text.
        /// </summary>
        /// <returns>A string representation of the network text.</returns>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => Mode switch
        {
            NetworkTextMode.Literal => Format,
            NetworkTextMode.Formatted => string.Format(CultureInfo.InvariantCulture, Format, Substitutions),
            NetworkTextMode.Localized => string.Format(CultureInfo.InvariantCulture, Format, Substitutions),

            _ => throw new InvalidOperationException("Invalid network text mode")
        };

        /// <summary>
        /// Writes the network text to the given <paramref name="span"/>. Returns the number of bytes written.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public int Write(Span<byte> span)
        {
            span[0] = (byte)Mode;
            var length = 1 + span[1..].Write(Format);

            byte numSubstitutions = 0;
            if (Mode != NetworkTextMode.Literal)
            {
                numSubstitutions = (byte)Substitutions.Count;
                span[length++] = numSubstitutions;
            }

            for (var i = 0; i < numSubstitutions; ++i)
            {
                length += Substitutions[i].Write(span[length..]);
            }

            return length;
        }

        /// <summary>
        /// Reads a <see cref="NetworkText"/> instance from the given <paramref name="span"/>. Returns the number of
        /// bytes read.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="value">The resulting <see cref="NetworkText"/> instance.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        public static int Read(Span<byte> span, out NetworkText value)
        {
            var mode = (NetworkTextMode)span[0];
            var length = 1 + span[1..].Read(out var format);
            var substitutions = Array.Empty<NetworkText>();

            byte numSubstitutions = 0;
            if (mode != NetworkTextMode.Literal)
            {
                numSubstitutions = span[length++];
                substitutions = new NetworkText[numSubstitutions];
            }

            for (var i = 0; i < numSubstitutions; ++i)
            {
                length += Read(span[length..], out substitutions[i]);
            }

            value = new NetworkText(mode, format, substitutions);
            return length;
        }

        /// <summary>
        /// Returns a literal <see cref="NetworkText"/> instance with the specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <exception cref="ArgumentNullException"><paramref name="text"/> is <see langword="null"/>.</exception>
        [Pure]
        public static implicit operator NetworkText(string text) =>
            new NetworkText(NetworkTextMode.Literal, text, Array.Empty<NetworkText>());

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> is equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left network text.</param>
        /// <param name="right">The right network text.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [Pure]
        public static bool operator ==(NetworkText? left, NetworkText? right) =>
            left is null ? right is null : left.Equals(right);

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> is not equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left network text.</param>
        /// <param name="right">The right network text.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        [Pure]
        public static bool operator !=(NetworkText? left, NetworkText? right) => !(left == right);
    }

    /// <summary>
    /// Specifies the mode of a <see cref="NetworkText"/>.
    /// </summary>
    public enum NetworkTextMode : byte
    {
        /// <summary>
        /// Indicates a literal.
        /// </summary>
        Literal = 0,

        /// <summary>
        /// Indicates formatted text, with substitutions.
        /// </summary>
        Formatted = 1,

        /// <summary>
        /// Indicates localized text, with substitutions.
        /// </summary>
        Localized = 2
    }
}
