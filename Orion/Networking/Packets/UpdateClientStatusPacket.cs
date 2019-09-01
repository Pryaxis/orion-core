using System;
using System.IO;
using System.Text;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to update the client's status.
    /// </summary>
    public sealed class UpdateClientStatusPacket : TerrariaPacket {
        private Terraria.Localization.NetworkText _statusText = Terraria.Localization.NetworkText.Empty;

        private protected override int HeaderlessLength => 4 + StatusText.GetBinaryLength(Encoding.UTF8);

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => false;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.UpdateClientStatus;

        /// <summary>
        /// Gets or sets the status increase.
        /// </summary>
        public int StatusIncrease { get; set; }

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public Terraria.Localization.NetworkText StatusText {
            get => _statusText;
            set => _statusText = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Reads an <see cref="UpdateClientStatusPacket"/> from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        public static UpdateClientStatusPacket FromReader(BinaryReader reader) {
            if (reader == null) {
                throw new ArgumentNullException(nameof(reader));
            }

            return new UpdateClientStatusPacket {
                StatusIncrease = reader.ReadInt32(),
                _statusText = reader.ReadNetworkText(),
            };
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(StatusIncrease);
            writer.Write(StatusText);
        }
    }
}
