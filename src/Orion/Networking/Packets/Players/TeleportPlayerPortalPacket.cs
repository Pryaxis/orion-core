using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to teleport a player through a portal.
    /// </summary>
    public sealed class TeleportPlayerPortalPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the portal index.
        /// </summary>
        public short PortalIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's new position.
        /// </summary>
        public Vector2 PlayerNewPosition { get; set; }

        /// <summary>
        /// Gets or sets the player's new velocity.
        /// </summary>
        public Vector2 PlayerNewVelocity { get; set; }

        private protected override PacketType Type => PacketType.TeleportPlayerPortal;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} @ {PlayerNewPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PortalIndex = reader.ReadInt16();
            PlayerNewPosition = reader.ReadVector2();
            PlayerNewVelocity = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PortalIndex);
            writer.Write(PlayerNewPosition);
            writer.Write(PlayerNewVelocity);
        }
    }
}
