using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to teleport an NPC through a portal.
    /// </summary>
    public sealed class TeleportNpcThroughPortalPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the portal ID.
        /// </summary>
        public short PortalId { get; set; }

        /// <summary>
        /// Gets or sets the new position.
        /// </summary>
        public Vector2 NewPosition { get; set; }

        /// <summary>
        /// Gets or sets the new velocity.
        /// </summary>
        public Vector2 NewVelocity { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            PortalId = reader.ReadInt16();
            NewPosition = reader.ReadVector2();
            NewVelocity = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            writer.Write(PortalId);
            writer.Write(NewPosition);
            writer.Write(NewVelocity);
        }
    }
}
