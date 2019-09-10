using System;
using System.ComponentModel;
using Orion.Networking.Packets;

namespace Orion.Networking.Events {
    /// <summary>
    /// Provides data for the <see cref="INetworkService.ReceivingPacket"/> event.
    /// </summary>
    public sealed class ReceivingPacketEventArgs : HandledEventArgs {
        private Packet _packet;

        /// <summary>
        /// Gets the packet's sender.
        /// </summary>
        public IClient Sender { get; }

        /// <summary>
        /// Gets or sets the packet that is being received.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public Packet Packet {
            get => _packet;
            set {
                _packet = value ?? throw new ArgumentNullException(nameof(value));
                MarkPacketAsDirty();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the packet is dirty.
        /// </summary>
        public bool IsPacketDirty { get; private set; }

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
            _packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }

        /// <summary>
        /// Marks the packet as being dirty.
        /// </summary>
        public void MarkPacketAsDirty() {
            IsPacketDirty = true;
        }
    }
}
