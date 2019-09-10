using System.Diagnostics.CodeAnalysis;
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
        /// Gets or sets the portal index.
        /// </summary>
        public short PortalIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC's new position.
        /// </summary>
        public Vector2 NewNpcPosition { get; set; }

        /// <summary>
        /// Gets or sets the NPC's new velocity.
        /// </summary>
        public Vector2 NewNpcVelocity { get; set; }

        private protected override PacketType Type => PacketType.TeleportNpcPortal;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex} @ {NewNpcPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();
            PortalIndex = reader.ReadInt16();
            NewNpcPosition = reader.ReadVector2();
            NewNpcVelocity = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            writer.Write(PortalIndex);
            writer.Write(NewNpcPosition);
            writer.Write(NewNpcVelocity);
        }
    }
}
