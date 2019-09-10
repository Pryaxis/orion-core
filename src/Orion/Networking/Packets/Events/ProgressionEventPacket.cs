using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to notify a progression event.
    /// </summary>
    public sealed class ProgressionEventPacket : Packet {
        /// <summary>
        /// Gets or sets the event ID.
        /// </summary>
        // TODO: implement enum for this.
        public short EventId { get; set; }

        private protected override PacketType Type => PacketType.ProgressionEvent;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{EventId}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            EventId = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(EventId);
        }
    }
}
