using System;
using System.ComponentModel;
using Orion.Networking.Packets;

namespace Orion.Networking.Events {
    /// <summary>
    /// Provides data for the <see cref="INetworkService.ReceivingPacket"/> event.
    /// </summary>
    public class ReceivingPacketEventArgs : HandledEventArgs {
        private TerrariaPacket _packet;

        /// <summary>
        /// Gets a value indicating whether the packet is dirty.
        /// </summary>
        public bool IsPacketDirty { get; private set; }

        /// <summary>
        /// Gets or sets the packet that is being received.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public TerrariaPacket Packet {
            get => _packet;
            set {
                _packet = value ?? throw new ArgumentNullException(nameof(value));
                MarkPacketAsDirty();
            }
        }

        /// <summary>
        /// Gets the packet's sender.
        /// </summary>
        public Terraria.RemoteClient Sender { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivingPacketEventArgs"/> class with the specified sender
        /// and packet.
        /// </summary>
        /// <param name="sender">The packet's sender.</param>
        /// <param name="packet">The packet that is being received.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="sender"/> is <c>null</c> or <paramref name="packet"/> is <c>null</c>.
        /// </exception>
        public ReceivingPacketEventArgs(Terraria.RemoteClient sender, TerrariaPacket packet) {
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
