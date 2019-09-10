using System.IO;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set a player's chest.
    /// </summary>
    public sealed class PlayerChestPacket : Packet {
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
        public string ChestName { get; set; }

        private protected override PacketType Type => PacketType.PlayerChest;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ChestIndex = reader.ReadInt16();
            ChestX = reader.ReadInt16();
            ChestY = reader.ReadInt16();
            var nameLength = reader.ReadByte();

            if (nameLength > 0 && nameLength <= Terraria.Chest.MaxNameLength) {
                ChestName = reader.ReadString();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ChestIndex);
            writer.Write(ChestX);
            writer.Write(ChestY);

            if (ChestName != null) {
                var nameLength = ChestName.Length;
                if (nameLength == 0 || nameLength > Terraria.Chest.MaxNameLength) {
                    nameLength = byte.MaxValue;
                }

                writer.Write((byte)nameLength);
                writer.Write(ChestName);
            } else {
                writer.Write((byte)0);
            }
        }
    }
}
