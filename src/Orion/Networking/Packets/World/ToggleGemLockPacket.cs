using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to toggle a gem lock.
    /// </summary>
    public sealed class ToggleGemLockPacket : Packet {
        /// <summary>
        /// Gets or sets the gem lock tile's X coordinate.
        /// </summary>
        public short GemLockTileX { get; set; }

        /// <summary>
        /// Gets or sets the gem lock tile's Y coordinate.
        /// </summary>
        public short GemLockTileY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the gem lock is locked.
        /// </summary>
        public bool IsGemLockLocked { get; set; }

        private protected override PacketType Type => PacketType.ToggleGemLock;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            GemLockTileX = reader.ReadInt16();
            GemLockTileY = reader.ReadInt16();
            IsGemLockLocked = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(GemLockTileX);
            writer.Write(GemLockTileY);
            writer.Write(IsGemLockLocked);
        }
    }
}
