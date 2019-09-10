using System;
using Orion.Networking.Packets;

namespace Orion.Networking.Events {
    /// <summary>
    /// Provides data for the <see cref="INetworkService.SentPacket"/> event.
    /// </summary>
    public sealed class SentPacketEventArgs : EventArgs {
        /// <summary>
        /// Gets the packet's receiver.
        /// </summary>
        public IClient Receiver { get; }

        /// <summary>
        /// Gets the packet that was sent.
        /// </summary>
        public Packet Packet { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SentPacketEventArgs"/> class with the specified receiver and
        /// packet.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="receiver"/> or <paramref name="packet"/> are <c>null</c>.
        /// </exception>
        public SentPacketEventArgs(IClient receiver, Packet packet) {
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
            Packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }
    }
}
