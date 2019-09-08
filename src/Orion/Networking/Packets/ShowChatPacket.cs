using System;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {

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
        /// Gets or sets the chat line width limit.
        /// </summary>
        public short ChatLineWidthLimit { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ChatColor = reader.ReadColor();
            ChatText = reader.ReadNetworkText();
            ChatLineWidthLimit = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ChatColor);
            writer.Write(ChatText);
            writer.Write(ChatLineWidthLimit);
        }
    }
}
