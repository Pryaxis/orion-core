using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to notify a progression event.
    /// </summary>
    public sealed class ProgressionEventPacket : Packet {
        /// <summary>
        /// Gets or sets the event ID.
        /// </summary>
        public short EventId { get; set; }

        private protected override PacketType Type => PacketType.NotifyEventProgression;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            EventId = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(EventId);
        }
    }
}
