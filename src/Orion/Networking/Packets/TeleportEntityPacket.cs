using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// /Packet sent to teleport an entity.
    /// </summary>
    public sealed class TeleportEntityPacket : Packet {
        /// <summary>
        /// Gets or sets the teleportation type.
        /// </summary>
        public Type TeleportationType { get; set; }

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

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            var header = reader.ReadByte();
            TeleportationType = (Type)(header & 3);
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

        /// <summary>
        /// Gets the teleportation type.
        /// </summary>
        public enum Type {
#pragma warning disable 1591
            TeleportPlayer = 0,
            TeleportNpc = 1,
            TeleportPlayerToOtherPlayer = 2,
#pragma warning restore 1591
        }
    }
}
