using System;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent from the server to the client to show combat text.
    /// </summary>
    public sealed class ShowCombatTextPacket : Packet {
        private Terraria.Localization.NetworkText _text = Terraria.Localization.NetworkText.Empty;
        
        /// <summary>
        /// Gets or sets the text's position.
        /// </summary>
        public Vector2 TextPosition { get; set; }

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

        private protected override PacketType Type => PacketType.ShowCombatText;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            TextPosition = reader.ReadVector2();
            TextColor = reader.ReadColor();
            Text = reader.ReadNetworkText();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(TextPosition);
            writer.Write(TextColor);
            writer.Write(Text);
        }
    }
}
