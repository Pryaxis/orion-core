using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to toggle a gem lock.
    /// </summary>
    public sealed class ToggleGemLockPacket : Packet {
        /// <summary>
        /// Gets or sets the gem lock's X coordinate.
        /// </summary>
        public short GemLockX { get; set; }

        /// <summary>
        /// Gets or sets the gem lock's Y coordinate.
        /// </summary>
        public short GemLockY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the gem lock is locked.
        /// </summary>
        public bool IsGemLockLocked { get; set; }

        private protected override PacketType Type => PacketType.ToggleGemLock;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{IsGemLockLocked} @ ({GemLockX}, {GemLockY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            GemLockX = reader.ReadInt16();
            GemLockY = reader.ReadInt16();
            IsGemLockLocked = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(GemLockX);
            writer.Write(GemLockY);
            writer.Write(IsGemLockLocked);
        }
    }
}
