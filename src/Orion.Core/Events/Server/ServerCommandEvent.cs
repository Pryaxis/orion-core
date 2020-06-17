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

namespace Orion.Core.Events.Server
{
    /// <summary>
    /// An event that occurs when the server is executing a command via the CLI. This event can be canceled.
    /// </summary>
    [Event("server-cmd")]
    public sealed class ServerCommandEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerCommandEvent"/> class with the specified command
        /// <paramref name="input"/>.
        /// </summary>
        /// <param name="input">The command input.</param>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <see langword="null"/>.</exception>
        public ServerCommandEvent(string input)
        {
            Input = input ?? throw new ArgumentNullException(nameof(input));
        }

        /// <summary>
        /// Gets the command input.
        /// </summary>
        /// <value>The command input.</value>
        public string Input { get; }
    }
}
