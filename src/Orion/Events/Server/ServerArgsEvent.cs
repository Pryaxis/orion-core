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

namespace Orion.Events.Server {
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
        public ServerArgsEvent(IEnumerable<string> args) {
            if (args is null) {
                throw new ArgumentNullException(nameof(args));
            }

            foreach (var arg in args) {
                if (arg.StartsWith("--")) {
                    var equals = arg.IndexOf('=');
                    if (equals < 0) {
                        _bools.Add(arg[2..]);
                    } else {
                        _values[arg[2..equals]] = arg[(equals + 1)..];
                    }
                } else if (arg.StartsWith("-")) {
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
        public bool GetBool(string name) => _bools.Contains(name);

        /// <summary>
        /// Tries to get the <paramref name="value"/> of the flag with the given <paramref name="name"/>. Returns a
        /// value indicating success.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the flag exists; otherwise, <see langword="false"/>.</returns>
        public bool TryGetValue(string name, out string value) => _values.TryGetValue(name, out value);
    }
}
