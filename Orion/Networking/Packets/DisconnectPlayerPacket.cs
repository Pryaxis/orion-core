using System;
using System.IO;
using System.Text;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to disconnect it.
    /// </summary>
    public sealed class DisconnectPlayerPacket : TerrariaPacket {
        private Terraria.Localization.NetworkText _reason = Terraria.Localization.NetworkText.Empty;

        /// <inheritdoc />
        private protected override int HeaderlessLength => Reason.GetBinaryLength(Encoding.UTF8);

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

        /// <summary>
        /// Reads a <see cref="DisconnectPlayerPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static DisconnectPlayerPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            return new DisconnectPlayerPacket {_reason = reader.ReadNetworkText()};
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(Reason);
        }
    }
}
