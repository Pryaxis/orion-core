// Copyright (c) 2019 Pryaxis & Orion Contributors
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

namespace Orion.Events.Server {
    /// <summary>
    /// Provides data for the <see cref="OrionKernel.ServerCommand"/> event.
    /// </summary>
    [EventArgs("server-command")]
    public sealed class ServerCommandEventArgs : ServerEventArgs, ICancelable {
        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <summary>
        /// Gets the input.
        /// </summary>
        public string Input { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerCommandEventArgs"/> class with the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public ServerCommandEventArgs(string input) {
            Input = input ?? throw new ArgumentNullException(nameof(input));
        }
    }
}
