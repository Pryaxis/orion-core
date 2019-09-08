using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to modify a tile.
    /// </summary>
    public sealed class ModifyTilePacket : Packet {
        /// <summary>
        /// Gets or sets the modification type.
        /// </summary>
        public ModificationType Type { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public short X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public short Y { get; set; }

        /// <summary>
        /// Gets or sets the data attached to the modification.
        /// </summary>
        public short Data { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public byte Style { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            Type = (ModificationType)reader.ReadByte();
            X = reader.ReadInt16();
            Y = reader.ReadInt16();
            Data = reader.ReadInt16();
            Style = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write((byte)Type);
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Data);
            writer.Write(Style);
        }

        /// <summary>
        /// Specifies the modification type.
        /// </summary>
        public enum ModificationType : byte {
#pragma warning disable 1591
            DestroyBlock = 0,
            PlaceBlock,
            DestroyWall,
            PlaceWall,
            DestroyBlockNoItems,
            PlaceRedWire,
            RemoveRedWire,
            HalveBlock,
            PlaceActuator,
            RemoveActuator,
            PlaceBlueWire,
            RemoveBlueWire,
            PlaceGreenWire,
            RemoveGreenWire,
            SlopeBlock,
            FrameTrack,
            PlaceYellowWire,
            RemoveYellowWire,
            PokeLogicGate,
            ActuateBlock,
#pragma warning restore 1591
        }
    }
}
