using System;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent to the client to show chat.
    /// </summary>
    public sealed class ShowChatPacket : Packet {
        private Terraria.Localization.NetworkText _chatText;

        /// <summary>
        /// Gets or sets the chat color.
        /// </summary>
        public Color ChatColor { get; set; }

        /// <summary>
        /// Gets or sets the chat text.
        /// </summary>
        public Terraria.Localization.NetworkText ChatText {
            get => _chatText;
            set => _chatText = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the chat line width.
        /// </summary>
        public short ChatLineWidth { get; set; }

        private protected override PacketType Type => PacketType.ShowChat;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ChatColor = reader.ReadColor();
            ChatText = reader.ReadNetworkText();
            ChatLineWidth = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ChatColor);
            writer.Write(ChatText);
            writer.Write(ChatLineWidth);
        }
    }
}
