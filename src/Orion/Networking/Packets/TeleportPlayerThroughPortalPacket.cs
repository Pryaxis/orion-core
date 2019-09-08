using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to teleport a player through a portal.
    /// </summary>
    public sealed class TeleportPlayerThroughPortalPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

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
            PlayerIndex = reader.ReadByte();
            PortalId = reader.ReadInt16();
            NewPosition = reader.ReadVector2();
            NewVelocity = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(PortalId);
            writer.Write(NewPosition);
            writer.Write(NewVelocity);
        }
    }
}
