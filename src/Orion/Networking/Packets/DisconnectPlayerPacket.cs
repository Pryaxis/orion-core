using System;
using System.IO;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to disconnect it.
    /// </summary>
    public sealed class DisconnectPlayerPacket : TerrariaPacket {
        private Terraria.Localization.NetworkText _reason = Terraria.Localization.NetworkText.Empty;

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => false;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.DisconnectPlayer;

        /// <summary>
        /// Gets or sets the disconnect reason.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public Terraria.Localization.NetworkText Reason {
            get => _reason;
            set => _reason = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            _reason = reader.ReadNetworkText();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Reason);
        }
    }
}
