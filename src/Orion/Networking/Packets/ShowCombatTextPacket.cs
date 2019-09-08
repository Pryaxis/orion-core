using System;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to show combat text.
    /// </summary>
    public sealed class ShowCombatTextPacket : Packet {
        private Terraria.Localization.NetworkText _text = Terraria.Localization.NetworkText.Empty;

        /// <summary>
        /// Gets or sets the text's X position.
        /// </summary>
        public float TextX { get; set; }

        /// <summary>
        /// Gets or sets the text's Y position.
        /// </summary>
        public float TextY { get; set; }

        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public Terraria.Localization.NetworkText Text {
            get => _text;
            set => _text = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            TextX = reader.ReadSingle();
            TextY = reader.ReadSingle();
            TextColor = reader.ReadColor();
            Text = reader.ReadNetworkText();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(TextX);
            writer.Write(TextY);
            writer.Write(TextColor);
            writer.Write(Text);
        }
    }
}
