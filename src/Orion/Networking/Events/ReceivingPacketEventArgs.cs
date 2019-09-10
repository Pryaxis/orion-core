using System;
using System.ComponentModel;
using Orion.Networking.Packets;

namespace Orion.Networking.Events {
    /// <summary>
    /// Provides data for the <see cref="INetworkService.ReceivingPacket"/> event.
    /// </summary>
    public sealed class ReceivingPacketEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the packet's sender.
        /// </summary>
        public IClient Sender { get; }

        /// <summary>
        /// Gets the packet that is being received.
        /// </summary>
        public Packet Packet { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivingPacketEventArgs"/> class with the specified sender
        /// and packet.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sender"/> or <paramref name="packet"/> are <c>null</c>.
        /// </exception>
        public ReceivingPacketEventArgs(IClient sender, Packet packet) {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }
    }
}
