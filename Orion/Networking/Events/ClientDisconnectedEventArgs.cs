using System;

namespace Orion.Networking.Events {
    /// <summary>
    /// Provides data for the <see cref="INetworkService.ClientDisconnected"/> event.
    /// </summary>
    public sealed class ClientDisconnectedEventArgs : EventArgs {
        /// <summary>
        /// Gets the client that disconnected.
        /// </summary>
        public Terraria.RemoteClient Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDisconnectedEventArgs"/> with the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <exception cref="ArgumentNullException"><paramref name="client"/> is <c>null</c>.</exception>
        public ClientDisconnectedEventArgs(Terraria.RemoteClient client) {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }
    }
}
