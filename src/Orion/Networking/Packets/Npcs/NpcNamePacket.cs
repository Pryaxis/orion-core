using System;
using System.IO;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to either request or set an NPC's name.
    /// </summary>
    public sealed class NpcNamePacket : Packet {
        private string _npcName;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string NpcName {
            get => _npcName;
            set => _npcName = value ?? throw new ArgumentNullException(nameof(NpcName));
        }

        private protected override PacketType Type => PacketType.NpcName;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();

            // The packet includes the NPC name if it is read as the client.
            if (context == PacketContext.Client) {
                NpcName = reader.ReadString();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);

            // The packet includes the NPC name if it is written as the server.
            if (context == PacketContext.Server) {
                writer.Write(NpcName);
            }
        }
    }
}
