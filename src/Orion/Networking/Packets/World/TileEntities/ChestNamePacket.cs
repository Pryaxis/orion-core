using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Items;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to request or set a chest's name.
    /// </summary>
    public sealed class ChestNamePacket : Packet {
        private string _chestName = "";

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        public short ChestIndex { get; set; }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        public short ChestX { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        public short ChestY { get; set; }

        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        public string ChestName {
            get => _chestName;
            set => _chestName = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.ChestName;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={ChestIndex} @ ({ChestX}, {ChestY}) is {ChestName}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ChestIndex = reader.ReadInt16();
            ChestX = reader.ReadInt16();
            ChestY = reader.ReadInt16();
            ChestName = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ChestIndex);
            writer.Write(ChestX);
            writer.Write(ChestY);
            writer.Write(ChestName);
        }
    }
}
