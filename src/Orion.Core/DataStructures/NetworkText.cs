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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;

namespace Orion.Core.DataStructures
{
    /// <summary>
    /// Represents text transmitted over the network.
    /// </summary>
    public sealed class NetworkText : IEquatable<NetworkText>
    {
        /// <summary>
        /// Represents the empty network text.
        /// </summary>
        public static readonly NetworkText Empty = string.Empty;

        internal readonly Mode _mode;
        internal readonly string _format;
        internal readonly NetworkText[] _args;

        internal NetworkText(Mode mode, string format, params NetworkText[] args)
        {
            Debug.Assert(format != null);
            Debug.Assert(args != null);
            Debug.Assert(mode != Mode.Literal || args.Length == 0);
            Debug.Assert(args.All(a => a != null));

            _mode = mode;
            _format = format;
            _args = args;
        }

        /// <inheritdoc/>
        [Pure]
        public override bool Equals(object obj) => obj is NetworkText other && Equals(other);

        /// <inheritdoc/>
        [Pure]
        public bool Equals(NetworkText? other)
        {
            if (other is null)
            {
                return false;
            }

            if (_mode != other._mode || _format != other._format || _args.Length != other._args.Length)
            {
                return false;
            }

            for (var i = 0; i < _args.Length; ++i)
            {
                if (!_args[i].Equals(other._args[i]))
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
            hashCode.Add(_mode);
            hashCode.Add(_format);
            foreach (var arg in _args)
            {
                hashCode.Add(arg);
            }

            return hashCode.ToHashCode();
        }

        /// <summary>
        /// Returns a string representation of the network text.
        /// </summary>
        /// <returns>A string representation of the network text.</returns>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => _mode switch
        {
            Mode.Literal => _format,
            Mode.Formatted => string.Format(CultureInfo.InvariantCulture, _format, _args),
            Mode.Localized => string.Format(CultureInfo.InvariantCulture, _format, _args),

            _ => throw new InvalidOperationException("Invalid network text mode")
        };

        /// <summary>
        /// Returns a literal <see cref="NetworkText"/> instance with the specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <exception cref="ArgumentNullException"><paramref name="text"/> is <see langword="null"/>.</exception>
        [Pure]
        public static implicit operator NetworkText(string text) =>
            NewNetworkText(Mode.Literal, text, Array.Empty<NetworkText>());

        /// <summary>
        /// Returns a formatted <see cref="NetworkText"/> instance with the specified <paramref name="format"/> and
        /// <paramref name="args"/>. 
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The arguments to substitute.</param>
        /// <returns>The formatted <see cref="NetworkText"/> instance.</returns>
        /// <exception cref="ArgumentException"><paramref name="args"/> contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="format"/> or <paramref name="args"/> are <see langword="null"/>.
        /// </exception>
        [Pure]
        public static NetworkText Formatted(string format, params NetworkText[] args) =>
            NewNetworkText(Mode.Formatted, format, args);

        /// <summary>
        /// Returns a localized <see cref="NetworkText"/> instance with the specified <paramref name="format"/> and
        /// <paramref name="args"/>.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The arguments to substitute.</param>
        /// <returns>The localized <see cref="NetworkText"/> instance.</returns>
        /// <exception cref="ArgumentException"><paramref name="args"/> contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="format"/> or <paramref name="args"/> are <see langword="null"/>.
        /// </exception>
        [Pure]
        public static NetworkText Localized(string format, params NetworkText[] args) =>
            NewNetworkText(Mode.Localized, format, args);

        [Pure]
        private static NetworkText NewNetworkText(Mode mode, string format, NetworkText[] args)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args.Any(a => a == null))
            {
                throw new ArgumentException("Args contains null", nameof(args));
            }

            return new NetworkText(mode, format, args);
        }

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

        internal enum Mode
        {
            Literal = 0,
            Formatted = 1,
            Localized = 2,
        }
    }
}
