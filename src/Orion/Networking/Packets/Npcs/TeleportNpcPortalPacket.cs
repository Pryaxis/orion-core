using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to teleport an NPC through a portal.
    /// </summary>
    public sealed class TeleportNpcPortalPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the portal ID.
        /// </summary>
        public short PortalId { get; set; }

        /// <summary>
        /// Gets or sets the NPC's new position.
        /// </summary>
        public Vector2 NewNpcPosition { get; set; }

        /// <summary>
        /// Gets or sets the NPC's new velocity.
        /// </summary>
        public Vector2 NewNpcVelocity { get; set; }

        private protected override PacketType Type => PacketType.TeleportNpcPortal;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            PortalId = reader.ReadInt16();
            NewNpcPosition = reader.ReadVector2();
            NewNpcVelocity = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            writer.Write(PortalId);
            writer.Write(NewNpcPosition);
            writer.Write(NewNpcVelocity);
        }
    }
}
