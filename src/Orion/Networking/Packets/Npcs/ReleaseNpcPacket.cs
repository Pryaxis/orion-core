using System.IO;
using Microsoft.Xna.Framework;
using Orion.Npcs;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent from the client to the server to release an NPC.
    /// </summary>
    public sealed class ReleaseNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC's position.
        /// </summary>
        public Vector2 NpcPosition { get; set; }

        /// <summary>
        /// Gets or sets the NPC type.
        /// </summary>
        public NpcType NpcType { get; set; }

        /// <summary>
        /// Gets or sets the NPC style.
        /// </summary>
        public byte NpcStyle { get; set; }

        private protected override PacketType Type => PacketType.ReleaseNpc;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcPosition = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            NpcType = (NpcType)reader.ReadInt16();
            NpcStyle = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((int)NpcPosition.X);
            writer.Write((int)NpcPosition.Y);
            writer.Write((short)NpcType);
            writer.Write(NpcStyle);
        }
    }
}
