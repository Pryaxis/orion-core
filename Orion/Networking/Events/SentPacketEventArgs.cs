using System;
using Orion.Networking.Packets;

namespace Orion.Networking.Events {
    /// <summary>
    /// Provides data for the <see cref="INetworkService.SentPacket"/> event.
    /// </summary>
    public sealed class SentPacketEventArgs : EventArgs {
        /// <summary>
        /// Gets the packet that was sent.
        /// </summary>
        public TerrariaPacket Packet { get; }

        /// <summary>
        /// Gets the packet's receiver.
        /// </summary>
        public Terraria.RemoteClient Receiver { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SentPacketEventArgs"/> class with the specified receiver and
        /// packet.
        /// </summary>
        /// <param name="receiver">The packet's receiver.</param>
        /// <param name="packet">The packet that was received.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="receiver"/> is <c>null</c> or <paramref name="packet"/> is <c>null</c>.
        /// </exception>
        public SentPacketEventArgs(Terraria.RemoteClient receiver, TerrariaPacket packet) {
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
            Packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }
    }
}
