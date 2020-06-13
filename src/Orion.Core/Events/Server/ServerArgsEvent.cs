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
using System.Linq;

namespace Orion.Core.Events.Server {
    /// <summary>
    /// An event that occurs when the server arguments are processed.
    /// </summary>
    [Event("server-args")]
    public sealed class ServerArgsEvent : Event {
        private readonly ISet<string> _bools = new HashSet<string>();
        private readonly IDictionary<string, string> _values = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerArgsEvent"/> with the specified <paramref name="args"/>.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgumentException"><paramref name="args"/> contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is <see langword="null"/>.</exception>
        public ServerArgsEvent(params string[] args) {
            if (args is null) {
                throw new ArgumentNullException(nameof(args));
            }

            if (args.Any(a => a == null)) {
                // Not localized because this string is developer-facing.
                throw new ArgumentException("Arguments contains null", nameof(args));
            }

            // Preprocess the arguments.
            foreach (var arg in args) {
                if (arg.StartsWith("--", StringComparison.Ordinal)) {
                    var equals = arg.IndexOf('=', StringComparison.Ordinal);
                    if (equals < 0) {
                        _bools.Add(arg[2..]);
                    } else {
                        _values[arg[2..equals]] = arg[(equals + 1)..];
                    }
                } else if (arg.StartsWith("-", StringComparison.Ordinal)) {
                    // Add the args' characters as flags.
                    foreach (var c in arg[1..]) {
                        _bools.Add(c.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Gets the boolean value of the flag with the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><see langword="true"/> if the flag exists; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="name"/> is <see langword="null"/> or whitespace.
        /// </exception>
        public bool GetBool(string name) {
            if (string.IsNullOrWhiteSpace(name)) {
                // Not localized because this string is developer-facing.
                throw new ArgumentException("Parameter cannot be null or whitespace", nameof(name));
            }

            return _bools.Contains(name);
        }

        /// <summary>
        /// Tries to get the <paramref name="value"/> of the flag with the given <paramref name="name"/>. Returns a
        /// value indicating success.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the flag exists; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="name"/> is <see langword="null"/> or whitespace.
        /// </exception>
        public bool TryGetValue(string name, [NotNullWhen(true)] out string? value) {
            if (string.IsNullOrWhiteSpace(name)) {
                // Not localized because this string is developer-facing.
                throw new ArgumentException("Parameter cannot be null or whitespace", nameof(name));
            }

            return _values.TryGetValue(name, out value);
        }
    }
}
