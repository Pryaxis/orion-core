using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to move an item into a chest.
    /// </summary>
    public sealed class MoveItemIntoChestPacket : Packet {
        /// <summary>
        /// Gets or sets the player inventory slot.
        /// </summary>
        public byte PlayerInventorySlot { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) =>
            PlayerInventorySlot = reader.ReadByte();

        private protected override void WriteToWriter(BinaryWriter writer) => writer.Write(PlayerInventorySlot);
    }
}
