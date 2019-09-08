using System;
using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to request a mass wire operation.
    /// </summary>
    public sealed class RequestMassWireOperationPacket : Packet {
        /// <summary>
        /// Gets or sets the starting tile's X position.
        /// </summary>
        public short StartTileX { get; set; }

        /// <summary>
        /// Gets or sets the starting tile's Y position.
        /// </summary>
        public short StartTileY { get; set; }

        /// <summary>
        /// Gets or sets the ending tile's X position.
        /// </summary>
        public short EndTileX { get; set; }

        /// <summary>
        /// Gets or sets the ending tile's X position.
        /// </summary>
        public short EndTileY { get; set; }

        /// <summary>
        /// Gets or sets the wire operation type.
        /// </summary>
        public Type WireOperationType { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            StartTileX = reader.ReadInt16();
            StartTileY = reader.ReadInt16();
            EndTileX = reader.ReadInt16();
            EndTileY = reader.ReadInt16();
            WireOperationType = (Type)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(StartTileX);
            writer.Write(StartTileY);
            writer.Write(EndTileX);
            writer.Write(EndTileY);
            writer.Write((byte)WireOperationType);
        }

        /// <summary>
        /// Specifies the operation type.
        /// </summary>
        [Flags]
        public enum Type : byte {
            #pragma warning disable 1591
            None = 0,
            RedWire = 1,
            GreenWire = 2,
            BlueWire = 4,
            YellowWire = 8,
            Actuator = 16,
            Cut = 32,
            #pragma warning restore 1591
        }
    }
}
