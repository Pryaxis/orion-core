using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// /Packet sent to teleport an entity.
    /// </summary>
    public sealed class EntityTeleportPacket : Packet {
        /// <summary>
        /// Gets or sets the teleportation type.
        /// </summary>
        public EntityTeleportationType TeleportationType { get; set; }

        /// <summary>
        /// Gets or sets the teleportation style.
        /// </summary>
        public byte TeleportationStyle { get; set; }

        /// <summary>
        /// Gets or sets the player or NPC index.
        /// </summary>
        public short PlayerOrNpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        private protected override PacketType Type => PacketType.EntityTeleport;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            var header = reader.ReadByte();
            TeleportationType = (EntityTeleportationType)(header & 3);
            TeleportationStyle = (byte)((header >> 2) & 3);

            PlayerOrNpcIndex = reader.ReadInt16();
            Position = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            byte header = 0;
            header |= (byte)((byte)TeleportationType & 3);
            header |= (byte)((TeleportationStyle & 3) << 2);
            writer.Write(header);

            writer.Write(PlayerOrNpcIndex);
            writer.Write(Position);
        }
    }
}
