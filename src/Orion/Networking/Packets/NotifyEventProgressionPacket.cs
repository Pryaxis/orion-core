using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to notify an event progression.
    /// </summary>
    public sealed class NotifyEventProgressionPacket : Packet {
        /// <summary>
        /// Gets or sets the event ID.
        /// </summary>
        public short EventId { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) =>
            EventId = reader.ReadInt16();

        private protected override void WriteToWriter(BinaryWriter writer) => writer.Write(EventId);
    }
}
