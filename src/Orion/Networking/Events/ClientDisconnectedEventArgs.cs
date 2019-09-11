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

using System;

namespace Orion.Networking.Events {
    /// <summary>
    /// Provides data for the <see cref="INetworkService.ClientDisconnected"/> handlers.
    /// </summary>
    public sealed class ClientDisconnectedEventArgs : EventArgs {
        /// <summary>
        /// Gets the client that disconnected.
        /// </summary>
        public IClient Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDisconnectedEventArgs"/> with the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <exception cref="ArgumentNullException"><paramref name="client"/> is <c>null</c>.</exception>
        public ClientDisconnectedEventArgs(IClient client) {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }
    }
}
