using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to increment an NPC's collected coins.
    /// </summary>
    public sealed class IncrementNpcCoinsPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC's extra value.
        /// </summary>
        public float NpcExtraValue { get; set; }

        /// <summary>
        /// Gets or sets the NPC position.
        /// </summary>
        public Vector2 NpcPosition { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            NpcExtraValue = reader.ReadSingle();
            NpcPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            writer.Write(NpcExtraValue);
            writer.Write(NpcPosition);
        }
    }
}
