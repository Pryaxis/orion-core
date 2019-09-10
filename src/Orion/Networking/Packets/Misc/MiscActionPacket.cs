using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent to perform a miscellaneous action.
    /// </summary>
    public sealed class MiscActionPacket : Packet {
        /// <summary>
        /// Gets or sets the player or NPC index.
        /// </summary>
        public byte PlayerOrNpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public MiscAction Action { get; set; }

        private protected override PacketType Type => PacketType.MiscAction;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Action} by #={PlayerOrNpcIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerOrNpcIndex = reader.ReadByte();
            Action = (MiscAction)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerOrNpcIndex);
            writer.Write((byte)Action);
        }
    }
}
