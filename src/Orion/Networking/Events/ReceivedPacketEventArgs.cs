using System;
using Orion.Networking.Packets;

namespace Orion.Networking.Events {
    /// <summary>
    /// Provides data for the <see cref="INetworkService.ReceivedPacket"/> event.
    /// </summary>
    public sealed class ReceivedPacketEventArgs : EventArgs {
        /// <summary>
        /// Gets the packet's sender.
        /// </summary>
        public Terraria.RemoteClient Sender { get; }

        /// <summary>
        /// Gets the packet that was received.
        /// </summary>
        public Packet Packet { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedPacketEventArgs"/> class with the specified sender and
        /// packet.
        /// </summary>
        /// <param name="sender">The packet's sender.</param>
        /// <param name="packet">The packet that was received.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="sender"/> is <c>null</c> or <paramref name="packet"/> is <c>null</c>.
        /// </exception>
        public ReceivedPacketEventArgs(Terraria.RemoteClient sender, Packet packet) {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }
    }
}
