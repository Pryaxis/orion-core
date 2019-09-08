using System;
using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the server to update the UUID.
    /// </summary>
    public sealed class UpdateUuidPacket : Packet {
        private string _uuid;

        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        public string Uuid {
            get => _uuid;
            set => _uuid = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) =>
            _uuid = reader.ReadString();

        private protected override void WriteToWriter(BinaryWriter writer) => writer.Write(Uuid);
    }
}
