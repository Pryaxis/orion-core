using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the client to the server to request a mass wire operation.
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
        public MassWireOperations WireOperations { get; set; }

        private protected override PacketType Type => PacketType.RequestMassWireOperation;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            StartTileX = reader.ReadInt16();
            StartTileY = reader.ReadInt16();
            EndTileX = reader.ReadInt16();
            EndTileY = reader.ReadInt16();
            WireOperations = (MassWireOperations)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(StartTileX);
            writer.Write(StartTileY);
            writer.Write(EndTileX);
            writer.Write(EndTileY);
            writer.Write((byte)WireOperations);
        }
    }
}
