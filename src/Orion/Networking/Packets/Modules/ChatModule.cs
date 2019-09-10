using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Module sent for chat purposes.
    /// </summary>
    public sealed class ChatModule : Module {
        /// <summary>
        /// Gets or sets the client's chat command. Applicable when received in the Server context.
        /// </summary>
        public string ClientChatCommand { get; set; }

        /// <summary>
        /// Gets or sets the client's chat text. Applicable when received in the Server context.
        /// </summary>
        public string ClientChatText { get; set; }

        /// <summary>
        /// Gets or sets the server's chatting player index. Applicable when sent in the Server context.
        /// </summary>
        public byte ServerChattingPlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the server's chat text. Applicable when sent in the Server context.
        /// </summary>
        public Terraria.Localization.NetworkText ServerChatText { get; set; }

        /// <summary>
        /// Gets or sets the server's chat color. Applicable when sent in the Server context.
        /// </summary>
        public Color ServerChatColor { get; set; }

        private protected override ModuleType ModuleType => ModuleType.Chat;

        /// <inheritdoc />
        public override string ToString() {
            var beginning = $"{nameof(ModuleType.Chat)}[";
            if (ClientChatCommand != null) {
                return beginning + $"C={ClientChatCommand}, T={ClientChatText}]";
            } else {
                return beginning + $"P={ServerChattingPlayerIndex}, T={ServerChatText}, C={ServerChatColor}]";
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            if (context == PacketContext.Server) {
                ClientChatCommand = reader.ReadString();
                ClientChatText = reader.ReadString();
            } else {
                ServerChattingPlayerIndex = reader.ReadByte();
                ServerChatText = reader.ReadNetworkText();
                ServerChatColor = reader.ReadColor();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            if (context == PacketContext.Server) {
                writer.Write(ServerChattingPlayerIndex);
                writer.Write(ServerChatText);
                writer.Write(ServerChatColor);
            } else {
                writer.Write(ClientChatCommand);
                writer.Write(ClientChatText);
            }
        }
    }
}
